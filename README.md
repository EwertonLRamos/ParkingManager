# ParkingManager (.NET)

Aqui você identificará todos os requisitos, regras de negócios, decisões arquiteturais e o histórico passo a passo da implementação do sistema **ParkingManager** que foi desenvolvido como parte de um teste técnico.

---

## 1. Visão Geral e Requisitos (Teste Técnico V1.4)

O objetivo do projeto é construir um sistema backend simples para gerenciar a operação de um estacionamento. O sistema controla vagas disponíveis, entradas e saídas de veículos através de um webhook e realiza o cálculo de receita (faturamento).

### Requisitos Funcionais:
- **Carga Inicial:** Ao iniciar a solução, buscar e armazenar os dados da garagem/vagas a partir de um endpoint externo (`GET /garage`). Esses dados são considerados a "fonte da verdade".
- **Integração de Eventos:** Aceitar requisições `POST` em um `/webhook` para receber eventos de `ENTRY` (Entrada), `PARKED` (Estacionado) e `EXIT` (Saída).
- **Cálculo de Receita:** Fornecer uma API REST (`GET /revenue`) que retorne a receita total para um setor em uma data específica.

### Payloads Recebidos (Webhook):
- **ENTRY:** `{"license_plate": "ZUL0001", "entry_time": "2025-01-01T12:00:00.000Z", "event_type": "ENTRY"}`
- **PARKED:** `{"license_plate": "ZUL0001", "lat": -23.561684, "lng": -46.655981, "event_type": "PARKED"}`
- **EXIT:** `{"license_plate": "ZUL0001", "exit_time": "2025-01-01T12:00:00.000Z", "event_type": "EXIT"}`

---

## 2. Modelagem do Domínio e Regras de Negócio

O domínio foi modelado utilizando os princípios do **Domain-Driven Design (DDD)**.

### Agregados e Entidades:
- **`ParkingSession` (Agregado Principal):** Representa o ciclo completo de um veículo no estacionamento (`ENTRY` -> `PARKED` -> `EXIT`).
- **`Spot` (Vaga):** Pertence a um setor, possui localização (`lat`/`lng`) e pode estar livre ou ocupada. Só comporta um veículo por vez.
- **`Sector` (Setor):** Divisão lógica da garagem que dita o preço base (`basePrice`) e a capacidade.

### Regras de Negócio (Precificação e Lotação):
- **Isenção inicial:** Os primeiros 30 minutos são totalmente grátis.
- **Tarifação:** Após 30 minutos, é cobrada a tarifa fixa por hora (baseada no `basePrice` do setor), sempre arredondando para cima (ex: 1h15 vira 2h).
- **Bloqueio de Ocupação Máxima:** Não é permitido processar novas entradas (`ENTRY`) se o estacionamento (ou setor) estiver 100% ocupado.
- **Preço Dinâmico (Surge Pricing):** Calculado na hora da entrada com base na ocupação:
  - Lotação < 25%: 10% de desconto.
  - Lotação entre 25% e 50%: Preço normal.
  - Lotação entre 51% e 75%: Acréscimo de 10%.
  - Lotação > 75%: Acréscimo de 25%.

### Pontos de Atenção (Resiliência):
- Webhooks podem chegar fora de ordem ou duplicados (exigindo controle de idempotência/consistência).
- Consideração transacional na entrada para evitar "overbooking" de vagas.

---

## 3. Arquitetura e Decisões Técnicas

O projeto foi construído seguindo **Clean Architecture**, **SOLID** e **CQRS**.

- **ParkingManager.Domain:** Coração do software. Contém as Entidades, Value Objects, Enums e as interfaces dos repositórios (`IParkingSessionRepository`). 
- **ParkingManager.Application:** Onde residem os Casos de Uso. Adotou-se o padrão **CQRS** separando:
  - *Commands* (`EntryCommand`, `ExitCommand`) que alteram estado.
  - *Queries* (`RevenueQuery`) focadas apenas em leitura de faturamento.
- **ParkingManager.Infrastructure:** Implementação do Entity Framework Core, DbContext, Migrations e dos repositórios concretos de domínio.
- **ParkingManager.API:** Camada de apresentação (Controllers, Injeção de Dependência, Extensões de Startup).

---

## 4. Guias de Implementação e Soluções Adotadas (Histórico)

Abaixo estão os resumos das etapas técnicas solucionadas durante a mentoria:

### 4.1. Configuração do Banco de Dados
A conexão com o SQL Server foi configurada isolando credenciais do código:
1. Em `appsettings.json`, a chave `DefaultConnection` foi definida. *(Nota: no MacOS, recomendado uso da imagem docker `mcr.microsoft.com/azure-sql-edge` e a flag `TrustServerCertificate=True`)*.
2. No `Program.cs`, o EF Core foi ativado usando: `builder.Services.AddDbContext<ParkingManagerDbContext>(options => options.UseSqlServer(...))`

### 4.2. Correção de Avisos de Migração do EF Core
Durante a criação de migrations, erros de exclusão em cascata (Cascade Delete) ocorreram:
- **Solução:** No método `OnModelCreating` do `ParkingManagerDbContext`, as relações entre `ParkingSession` e `Spot`/`Sector` foram mapeadas com `DeleteBehavior.Restrict` para evitar o erro do SQL Server sobre múltiplos caminhos em cascata. As propriedades decimais de latitude e longitude receberam especificações de precisão explícitas.

### 4.3. Carga Inicial de Dados (Seed via API)
Para cumprir o requisito de ler `GET /garage` na inicialização:
- **Solução:** Foi criada uma `HostExtensions.cs` na pasta de extensões da API contendo um método `public static async Task FillDatabaseAsync(this IHost host)`. No `Program.cs`, basta chamar `await app.FillDatabaseAsync()` antes do `app.Run()`. Isso realiza a ingestão e mapeamento de setores e vagas (`SectorDto` e `SpotDto`) antes da API abrir portas.

### 4.4. Implementação de Repositórios de Domínio
O padrão Repository foi aplicado para isolar o acesso a dados e respeitar a Inversão de Dependência:
- Interfaces (`IParkingSessionRepository`, etc.) foram criadas no **Domain**.
- Implementações (`ParkingSessionRepository`) com `DbContext` injetado foram alocadas no **Infrastructure**.

### 4.5. Ajuste no Payload do Webhook (JSON Múltiplos Campos)
Problema: O `System.Text.Json` não permitia usar `[JsonPropertyName]` múltiplos em uma mesma propriedade `Timestamp` (para `entry_time` ou `exit_time`).
- **Solução:** A classe `WebhookPayload` foi separada em `EntryTime` (mapeando "entry_time") e `ExitTime` (mapeando "exit_time"). Uma propriedade getter `Timestamp` unificada foi criada aplicando a regra de fallback (`EntryTime ?? ExitTime`) e decorada com `[JsonIgnore]` para não interferir na serialização original.

### 4.6. Consulta de Receita (Faturamento)
Para criar a regra da query de `GET /revenue`:
- O repositório foi ensinado a buscar apenas sessões finalizadas (`Status == EXIT`).
- O `RevenueController` repassa o filtro (data e setor) para a `RevenueQuery`.
- O Handler faz o somatório final das sessões encerradas no período e devolve o valor de forma limpa.

---

## 5. Como Executar a Aplicação e Próximos Passos

### Como rodar:
1. Configure o banco via **Docker**: `docker run --cap-add SYS_PTRACE -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=SuaSenha123!' -p 1433:1433 --name sqledge -d mcr.microsoft.com/azure-sql-edge`
2. Certifique-se de que a `DefaultConnection` no `appsettings.json` aponta para `localhost,1433`.
3. Rode as migrations no terminal (na raiz):
   ```bash
   dotnet ef database update --project ParkingManager.Infrastructure/ParkingManager.Infrastructure.csproj --startup-project ParkingManager.API/ParkingManager.API.csproj
4. Inicie a aplicação com `dotnet run` (via API). O Swagger ficará disponível para os testes em `POST /webhook` e `GET /revenue`.

---

### 6. Próximos Passos (Evolução Contínua)
1. Implementar UnitOfWork (UoW): Unificar e gerenciar a transação de persistência garantindo atomicidade nas execuções dos Handlers de Comandos.
2. Adicionar FluentValidation: Criar validações robustas em pipelines de requisição, garantindo placas válidas, formatos de data coerentes antes de processar as entidades de Domínio.