# Developer Evaluation - Sales API

API construída para resolver o desafio técnico proposto em [`README_DESAFIO.md`](../README_DESAFIO.md), utilizando boas práticas como DDD, Clean Architecture, CQRS, e testes unitários.

---

## 🚀 Executando com Docker Compose

Pré-requisitos:
- Docker + Docker Compose instalados

```
docker compose up --build
```

Esse comando irá:
- Subir a API em https://localhost:5000
- Subir o PostgreSQL com migrations aplicadas
- Expor o Swagger em https://localhost:5000/swagger

🔐 Connection string utilizada:
```
Host=db;Port=5432;Database=developer-evaluation;Username=postgres;Password=postgres
```

---

## 📊 Relatório de cobertura disponível em:
`backend/src/Abi.DeveloperEvaluation.Unit/TestResults/CoverageReport/index.html`

---

## 🧠 Sobre o Desafio

O desafio consiste em criar uma API para registro de vendas com:
- CRUD completo
- Aplicação de regras de negócio por item
- Suporte a cancelamento de venda
- Estrutura com separação em camadas e foco em escalabilidade

> Regras implementadas detalhadas em [`README_DESAFIO.md`](../README_DESAFIO.md)

---

## 🧱 Estrutura e Patterns

### Domain
- Entity (`Sale`, `SaleItem`)
- Value Object
- Domain Events (estrutura pronta)

### Application
- CQRS com MediatR
- DTOs, Commands, Queries, Handlers
- Validations com FluentValidation

### Infra
- Repository Pattern
- EF Core + PostgreSQL

### WebApi
- Controllers REST
- Middlewares de erro (`DomainException`, `ValidationException`)
- HealthCheck customizado

### IoC
- Injeção de dependência isolada

---

## 📐 Nomeclaturas e Organização

| Tipo         | Sufixo         | Exemplo                         |
|--------------|----------------|----------------------------------|
| Commands     | `Command`      | `CreateSaleCommand`              |
| Queries      | `Query`        | `GetSaleByIdQuery`               |
| Handlers     | `Handler`      | `GetAllSalesHandler`             |
| DTOs         | `Request/Response` | `SaleRequest`, `SaleResponse` |
| Middlewares  | `Middleware`   | `ValidationExceptionMiddleware`  |

---

## ✅ Testes

✔️ Foram priorizados testes **unitários** devido ao curto prazo:
- Alta cobertura em `Application` (Handlers, Validações)
- Testes em `Common` (HealthCheck, Responses)

🔜 Testes de integração não implementados, mas estrutura preparada.

---

> Desenvolvido por [Vinicius Oliveira](https://github.com/vinialexandre)