# BusCatalog
Ecossistema de aplicações para gerenciamento de linhas de ônibus. Desenvolvido sob preceitos de DDD (Domain-Driven Design), TDD (Test-Driven Development) e arquitetura orientada a eventos.

## Disclaimer
Este aplicativo não é oficial e não possui nenhuma ligação com empresas ou órgãos oficiais de transportes.
Foi desenvolvido unicamente para fins de aprendizagem e não pode ser distribuído comercialmente.

## Componentes
Todas as aplicações componentes foram compartimentadas em contêineres Docker, estando cada `Dockerfile` dentro de seu respectivo diretório. Um `docker-compose.yml` geral orquestra a execução das aplicações juntas.
- SPA
- Scraper
- API

## SPA - Single Page Application
Front-end de painel de configuração da aplicação. Permite visualizar, criar, editar e excluir linhas através de uma interface web amigável.

### Especificidades técnicas
- <b>Linguagem:</b> TypeScript 5.3.2
- <b>Runtime:</b> Node 20.11.0
- <b>Framework:</b> Angular 17.1.1
- <b>Bibliotecas importantes:</b> Tailwind, Ng-Select, Karma, Jasmine, Spectator

## Scraper
Back-end para extração de informações das informações das linhas pelo site da EMTU via webscrapping. Possibilita persistí-las através de REST ou por eventos usando Kafka.

### Especificidades técnicas
- <b>Linguagem:</b> Java 21
- <b>Runtime:</b> JRE 21
- <b>Framework:</b> Spring Boot 3.2.4
- <b>Bibliotecas importantes:</b> Maven, Spring Kafka, Spring Data JPA, Spring Test, Spring Kafka, Lombok, JUnit

## API
Back-end para consolidação e disponibilização das informações das linhas produzidas por <b>Scraper</b> ou criadas no <b>SPA</b>.

### Especificidades técnicas
- <b>Linguagem:</b> C# 12
- <b>Runtime:</b> .NET 8
- <b>Framework:</b> ASP.NET Core 8.0.0
- <b>Bibliotecas importantes:</b> AutoMapper, Confluient.Kafka, FluentValidation, Entity Framework Core, xUnit, FluentAssertions

## Execução
É possível executar cada uma das aplicações de forma independente e isolada utilizando as ferramentas de cada dos runtimes em que foram desenvolvidas. Contudo, dada a heterogeneidade da solução, a forma mais prática de executar o conjunto todo através do <b>Docker Compose</b>, através dos seguintes passos:

1. Certifique-se de que Docker e Docker Compose estejam instalados
2. No diretório raiz do projeto, execute o comando `docker compose up --build`
3. Acesse as aplicações via interface web:
- <b>API</b>: http://localhost:5001/swagger
- <b>SPA</b>: http://localhost:5002
- <b>Scraper</b>: http://localhost:5003/swagger-ui/index.html