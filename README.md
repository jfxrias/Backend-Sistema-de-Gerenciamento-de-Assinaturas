# Desafio Técnico Backend - Sistema de Gerenciamento de Assinaturas

Este é um projeto desenvolvido como resolução de um desafio técnico público para fins de estudo e demonstração de habilidades arquiteturais.

API RESTful em .NET 8 para gerenciamento de assinaturas e dependentes. 
Desenvolvida utilizando os princípios do DDD (Domain-Driven Design) e acesso a dados via Dapper.

## 🚀 Tecnologias Utilizadas
- .NET 8 (C#)
- DDD (Domain, Application, Infrastructure)
- Dapper (Micro-ORM)
- PostgreSQL
- xUnit & Moq (Testes Unitários)
- JWT (Autenticação)

## ⚙️ Configuração e Execução

### 1. Banco de Dados
1. Crie um banco de dados no PostgreSQL (recomendado nomear como `T2M`).
2. Importe o script contido no arquivo `Database/T2M.sql` para criar as tabelas e popular os dados iniciais.
3. Em seguida, configure sua string de conexão e a chave do JWT no arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=T2M;Username=postgres;Password=sua_senha"
  },
  "Jwt": {
    "Key": "ExemploDeChaveEnormeTemQueTer32CaracteresOuMais"
  }
}
```

### 2. Rodando o Projeto
Na raiz da solução, execute os comandos no terminal:

```bash
dotnet restore
dotnet run
```

### 3. Executando os Testes Unitários
A aplicação possui testes unitários cobrindo Entidades de Domínio, Repositórios e Serviços de Aplicação. Para rodá-los:

```bash
dotnet test
```
