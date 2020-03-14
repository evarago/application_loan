# API GraphQL ASPNetCore 

Principais abordagens

 DDD (Domain Driven Design) hexagonal architecture
 Swagger
 Version
 OAuth
 AWS Lamdba
 AWS SQS
 Dependency Injection
 Entity Framework Repository
 TDD (Unit Tests)
 Visual Studio 2017
 Logs
 Docker

## Ambiente de Desenvolvimento

- .NET Core 2.1 SDK
- Visual Studio Code v1.42.1 (Ou VS2017)
- MySql 5.6 (Nesse cenário foi o Aurora RDS)
 
## Setup do projeto

O projeto pode ser executado pelo VS code ou VS2012, no IIS ou Docker.
Simplesmente inicie o depurador a partir do IDE ou execute-o diretamente usando o comando CLI dotnet run a partir da raiz da pasta \ApplicationLoan.Api.
Para uso do MySql será necessário direcionar para uma instância criada previamente, os scripts disponíveis na pasta "Scripts\" do projeto podem ser executados para criar as tabelas e inserir os dados iniciais.

Ainda não está disponível o "migration"  para criar o banco de dados e inserir os dados iniciais na primeira execução do aplicativo.

A API está configurada para executar na porta 52582, se isso entrar em conflito com algum outro serviço no seu computador, você poderá alterá-la no arquivo de xxx.

Para executar no docker, ajustar o arquivo Dockerfile conforme a necessidade.

## Contato

everton.varago@gmail.com