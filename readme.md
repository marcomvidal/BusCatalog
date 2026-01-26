# BusCatalog
Application ecosystem for bus lines management. Development under DDD (Domain-Driven Design), TDD (Test-Driven Development) concepts and event-driven architecture.

## Disclaimer
This application is non-official and it has no relationship with companies or transport official entities. It was development exclusively for learning purposes. It can't be distributed commercially.

## Components
All the application components were compartmented on Docker containers. Each component has a `Dockerfile` inside their own directory. A general `docker-compose.yml` orchestrates the entire ecosystem execution together.
- SPA
- Scraper
- API

![Topology](https://raw.githubusercontent.com/marcomvidal/BusCatalog/refs/heads/master/Images/Topology.png "Topology")

## SPA - Single Page Application
Front-end, a configuration painel of the application. It allows visualize, create, edit, and delete data through a friendly web user interface that interacts with the <b>API</b> microservice.

### Tech specifications
- <b>Language:</b> TypeScript 5.3.2
- <b>Runtime:</b> Node 20.11.0
- <b>Framework:</b> Angular 17.1.1
- <b>Main libraries:</b> Tailwind, Ng-Select, Karma, Jasmine, Spectator

## Scraper
Back-end, a microservice for lines information extraction from the EMTU website via webscrapping. It allows persistence through REST or events (using Kafka) to the <b>API</b> microservice.

### Tech specifications
- <b>Language:</b> Java 21
- <b>Runtime:</b> JRE 21
- <b>Framework:</b> Spring Boot 3.2.4
- <b>Main libraries:</b> Maven, Spring Kafka, Spring Test, Spring Kafka, Lombok, JUnit
- <b>Persistence:</b> SQLite via Spring Data JPA (Hibernate)

## API
Back-end, a microservice for lines information consolidation and provision that were persisted through <b>Scraper</b> or <b>SPA</b>.

### Tech specifications
- <b>Language:</b> C# 12
- <b>Runtime:</b> .NET 8
- <b>Framework:</b> ASP.NET Core 8.0.0
- <b>Main libraries:</b> AutoMapper, Confluent.Kafka, FluentValidation, xUnit, FluentAssertions
- <b>Persistence:</b> SQLite via Entity Framework Core

## Execution
It is possible to execute each of the components independently using the runtimes tools that each one were built. However, given the solution is very heterogenous, the most straighforward way to execute the whole ecosystem is using <b>Docker Compose<b> through the following steps:

1. Be sure that Docker and Docker Compose are installed
2. In the project root directory, run the command `docker compose up --build`
3. Access the application via web browser:
- <b>API</b>: http://localhost:5001/swagger
- <b>SPA</b>: http://localhost:5002
- <b>Scraper</b>: http://localhost:5003/swagger-ui/index.html

![LinesSummary](https://raw.githubusercontent.com/marcomvidal/BusCatalog/refs/heads/master/Images/LinesSummary.png "Lines Summary")

![LinesEdit](https://raw.githubusercontent.com/marcomvidal/BusCatalog/refs/heads/master/Images/LinesEdit.png "Lines Edit")

![DockerContainers](https://raw.githubusercontent.com/marcomvidal/BusCatalog/refs/heads/master/Images/Docker.png "Docker Containers")

![ScraperSwagger](https://raw.githubusercontent.com/marcomvidal/BusCatalog/refs/heads/master/Images/ScraperSwagger.png "Scraper Swagger")

![ApiSwagger](https://raw.githubusercontent.com/marcomvidal/BusCatalog/refs/heads/master/Images/ApiSwagger.png "API Swagger")