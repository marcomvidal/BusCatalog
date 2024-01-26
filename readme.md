# BusCatalog

API RESTful de consulta de informações de linhas de ônibus da cidade de Santo André.

## Disclaimer
Este aplicativo não é oficial e não possui nenhuma ligação com empresas ou órgãos oficiais de transportes.
Foi desenvolvido unicamente para fins de aprendizagem e não pode ser distribuído comercialmente.

## Ferramentas utilizadas
- C# 12
- .NET 8.0
- Entity Framework Core 8.0
- SQLite
- Docker

## Componentes
- `/`: Dockerfile e inicialização;
- `Adapters`: Interfaces da aplicação com fontes de dados externas;
- `Domain`: Lógica de negócio;
- `Infrastructure`: Configuração da aplicação;
- `Migrations`: Atualizações de schema do banco de dados.

## Execução
Utilize o comando `dotnet run`. Será iniciada uma instância do servidor Kestrel do .NET Core. Esta aplicação é compatível com Windows, Linux e Mac OS X, desde que o .NET Core esteja instalado.

## Arquitetura da solução
![Principal](https://raw.githubusercontent.com/marcomvidal/SantoAndreOnBus/master/arquitetura.png)
