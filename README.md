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

---

📊 Veja o relatório de cobertura de testes completo:  
👉 [Clique aqui](https://vinialexandre.github.io/abi-gth-omnia-developer-evaluation)

---

## 🧠 Sobre o Desafio

O desafio consiste em criar uma API para registro de vendas com:
- CRUD completo
- Aplicação de regras de negócio por item
- Suporte a cancelamento de venda
- Estrutura com separação em camadas e foco em escalabilidade

> Regras implementadas detalhadas em [`README_DESAFIO.md`](../README_DESAFIO.md)

---

## 📮 Testando com Postman

Para testar a API rapidamente:

1. Abra o Postman
2. Vá em `Import` > `Upload Files`
3. Selecione o arquivo `Developer Evaluation - Sales API.postman_collection.json` disponível na **raiz do repositório**

✅ A collection contém todos os endpoints de CRUD e cenários de negócio prontos para execução.

> ⚠️ Lembre-se de garantir que a API esteja rodando em `https://localhost:5000`.
> ⚠️ Divirta-se

---

## 🧱 Estrutura e Patterns

### Domain
- Entity (`Sale`, `SaleItem`)
- Value Object
- Domain Events

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

## ✅ Testes

✔️ Foram priorizados testes **unitários** devido ao curto prazo:
- Alta cobertura em `Application` (Handlers, Validações)
- Testes em `Common` (HealthCheck, Responses)

> Desenvolvido por [Vinicius Oliveira](https://github.com/vinialexandre)