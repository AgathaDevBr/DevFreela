# DevFreela

DevFreela e uma plataforma para conectar clientes a desenvolvedores freelancers.

## Stack

- **ASP.NET Core**: API HTTP em `DevFreela.API`.
- **Entity Framework Core**: acesso a dados no projeto `DevFreela.Infrastructure`.
- **JWT**: autenticacao e autorizacao nos endpoints protegidos.
- **Docker**: containerizacao da API, SQL Server e RabbitMQ.
- **RabbitMQ**: publicacao do evento `project-created` quando um projeto e criado.
- **xUnit**: testes unitarios em `DevFreela.UnitTests`.
- **Azure**: template Bicep em `infra/azure/main.bicep` para hospedar a API em Azure Container Apps com Azure SQL.

## Estrutura

- `DevFreela.API`: controllers, configuracao HTTP e autenticacao JWT.
- `DevFreela.Application`: casos de uso e contratos de servico.
- `DevFreela.Core`: entidades, eventos, interfaces de repositorio e mensageria.
- `DevFreela.Infrastructure`: EF Core, repositorios e RabbitMQ.
- `DevFreela.UnitTests`: testes unitarios com xUnit e Moq.

## Executar com Docker

```bash
docker compose up --build
```

A API fica disponivel em `http://localhost:8080`.

## Gerar token JWT

```http
POST http://localhost:8080/api/auth/login
Content-Type: application/json

{
  "email": "cliente@devfreela.com",
  "password": "devfreela123",
  "role": "Client"
}
```

Use o `accessToken` retornado como Bearer token nos endpoints protegidos.

## Testes

```bash
dotnet test DevFreela.UnitTests/DevFreela.UnitTests.csproj
```

## Azure

O arquivo `infra/azure/main.bicep` provisiona a base para Azure Container Apps e Azure SQL. Informe a imagem Docker publicada em um registry e os segredos de producao durante o deploy.
