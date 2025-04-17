Segue um README mais **objetivo**, com explicação clara sobre o desafio, estrutura, padrões utilizados e execução via Docker:

---

# Developer Evaluation - Sales API

API RESTful desenvolvida para resolver o desafio técnico proposto, com foco em arquitetura limpa, boas práticas de design e testes unitários.

📁 Repositório: [vinialexandre/abi-gth-omnia-developer-evaluation](https://github.com/vinialexandre/abi-gth-omnia-developer-evaluation)

---

## 🔧 Como executar com Docker Compose

Pré-requisitos:
- Docker e Docker Compose instalados

### Comando:
```bash
docker compose up --build
```

Isso irá:
- Subir o container da API (`Abi.DeveloperEvaluation.WebApi`)
- Subir o container PostgreSQL
- Aplicar migrations automaticamente

### Acessos:
- Swagger: https://localhost:5000/swagger
- API: https://localhost:5000
- PostgreSQL: `Host=db;Port=5432;Database=developer-evaluation;Username=postgres;Password=postgres`

---

## 🧠 Desafio

Criar uma API para registrar vendas com:
- Itens, quantidades, descontos
- Cliente e filial
- Cancelamento de venda
- Paginação
- Regras de negócio aplicadas por item:
  - 4+ itens: 10% de desconto
  - 10-20 itens: 20% de desconto
  - >20 itens: proibido

---

## 📐 Arquitetura & Padrões

### Domain
- **Entity**, **Value Object**
- **Domain Events** (ex: `SaleCreatedEvent`, `SaleCancelledEvent`)
- Lógica de negócio encapsulada na entidade `Sale`

### Application
- **CQRS** com MediatR
- **Handlers** para comandos e queries
- **FluentValidation** para validações

### Infra
- **Repository Pattern**
- EF Core com PostgreSQL

### WebApi
- **Controllers** (como `SaleController`)
- **Middlewares**:
  - `DomainExceptionMiddleware`
  - `ValidationExceptionMiddleware`

### Common
- HealthChecks
- Logging
- Response padrão (via `ApiResponse`)

### IoC
- **Service Registration**
- Isolamento da injeção de dependências

---

## ✍️ Convenções e Nomeclaturas

| Tipo          | Sufixo     | Exemplo                        |
|---------------|------------|--------------------------------|
| Command       | `Command`  | `CreateSaleCommand`            |
| Query         | `Query`    | `GetSaleByIdQuery`             |
| Handler       | `Handler`  | `CreateSaleCommandHandler`     |
| DTO           | `Request`, `Response` | `SaleRequest`, `SaleResponse` |
| Middleware    | `Middleware` | `ValidationExceptionMiddleware` |

---

## 🧪 Testes

✅ Priorizados **testes unitários**, devido ao tempo limitado.

- Testes para `Application` (handlers, validações)
- Testes para `Common` (healthcheck, responses)

🔜 Testes de integração e controllers podem ser adicionados facilmente.

---

## ✅ Conclusão

Esse projeto demonstra:
- Arquitetura DDD limpa
- Separação clara de responsabilidades
- Validações robustas com cobertura de testes
- API estruturada e documentada

📁 Estrutura preparada para escalar, integrar eventos e publicar em brokers no futuro.

---

> Desenvolvido por [Vinicius Oliveira](https://github.com/vinialexandre) • `backend/` contém o projeto principal

Se quiser, posso já salvar esse novo conteúdo no `README.md`. Deseja?