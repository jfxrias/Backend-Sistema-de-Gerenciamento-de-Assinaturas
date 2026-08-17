# Desafio Tecnico Backend - Sistema de Gerenciamento de Assinaturas 

API RESTful em .NET 8 para gerenciamento de assinaturas e dependentes. Desenvolvida utilizando os principios do DDD (Domain-Driven Design) e acesso a dados via Dapper. 

### eo Tecnologias. Utilizadasene 

###### #° 

- ¢ .NET 8 (C#) 

- DDD (Domain, Application, Infrastructure) 

- Dapper (Micro-ORM) 

- PostgreSQL 

- xUnit & Mog (Testes Unitarios) 

- JWT (Autenticagao) 

## © Configuragao e Execucao 

##### 1. Banco de Dados 

1. Crie um banco de dados no PostgreSQL (recomendado nomear como T2M ). 

2. Importe o script contido no arquivo Database/T2M.sql para criar as tabelas e popular os dados iniciais[cite: 2]. 

3. Em seguida, configure sua string de conexdo e a chave do JWT no arquivo 

   - appsettings.json: 

{ "ConnectionStrings": { "DefaultConnection": "“Host=localhost;Port=5432;Database=T2M;Username=postgres;Passworc hs "Jwt": { "Key": "“ExemploDeChaveEnormeTemQueTer32CaracteresOuMais" } } 

#### 2. Rodando o Projeto 

Na raiz da solugao, execute os comandos no terminal: 

dotnet restore 

dotnet run 

###### 3. Executando os Testes Unitarios 

A aplicagao possui testes unitarios cobrindo Entidades de Dominio, Repositorios e Servicos de Aplicacao. Para roda-los: 

dotnet test 

