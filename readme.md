# Santo André On Bus

API RESTful de consulta de informações de linhas de ônibus da cidade de Santo André.

## Disclaimer
Este aplicativo não é oficial e não possui nenhuma ligação com empresas ou órgãos oficiais de transportes.
Foi desenvolvido unicamente para fins de aprendizagem e não pode ser distribuído comercialmente.

## Ferramentas utilizadas
- C# 8.0
- .NET Core 3.0
- Entity Framework Core 3.0
- ASP.NET Identity com JSON Web Token
- PostgreSQL 9.4
- Docker

## Componentes
- `/`: Dockerfile e inicialização;
- `Contexts`: Mapeamento relacional-objeto com acesso a banco de dados através do Entity Framework;
- `Controllers`: Ouvintes e manipuladores de requisições HTTPS;
- `Filters`: Armazenamento de dados de validação em sessão, usados como anotações para simplificar os Controllers;
- `Helpers`: Classes de utilidades variadas, não aplicáveis aos demais diretórios;
- `Migrations`: Atualizações de schema do banco de dados;
- `Models`: Classes de negócio, objetos de transporte de requisição (DTOs) e mapeamentos Model-DTO;
- `Services`: Servem aos `Controllers` com a execução de tarefas e consultas complexas.

## Ambientes
Todas as credenciais de desenvolvimento estão armazenadas em `appsettings.Development.json`. Para o ambiente de produção, além do arquivo `appsettings.json`, a aplicação foi preparada para lidar com dados sensíveis através de variáveis de ambiente. São elas:
- `POSTGRESQLCONNSTR_PostgreSqlDatabase`: Credenciais do banco de dados;
- `ASPNETCORE_JwtSecret`: Hash JWT para geração de token de autenticação da API.
O local recomendado de armazenamento desta credenciais é o Dockerfile.

## Fluxo de execução
1. A requisição chega ao `Controller` através de uma mensagem HTTP. Caso seja um POST ou PUT, os dados são desserializados em um `DTO`;
2. A autenticação é realizada verificando o conteúdo do token JWT. Caso falhe, é enviado ao cliente o código HTTP 403;
3. Dados são armazenados e / ou carregados pelo Entity Framework Core diretamente ou indiretamente (através de `Services`), caso operações mais complexas sejam exigidas;
4. São enviados `DTOs` de resposta com os dados solicitados.

## Autenticação
Não são utilizados cookies. Toda a autenticação ocorre por tokens e seguem o seguinte fluxo:
1. Cadastro do usuário na rota `api/authentication/register`. Por padrão, ela está fechada. Para liberá-la, basta remover a anotação `[Authorize]` do método `AuthenticationController@Register`;
2. Autenticação do usuário cadastrado através da rota `api/authentication/login`, enviando um objeto com nome e senha. Se estiverem corretos, é retornado o token JWT;
3. Todas as requisições para rotas de negócio deverão conter no header o `Bearer Token`.
A persistência e transporte da autenticação são realizadas usando a infraestrutura do ASP.NET Identity.

## Roteamento
Todas as rotas possuem o prefixo `/api`, seguidas do nome do `Controller` e eventuais parâmetros. Para visualizar linha I-01, por exemplo, a rota seria `https://localhost:5001/api/lines/I-01`.

## Deployment
A aplicação real foi hospedada em um servidor do Heroku com sucesso. Caso deseje fazê-lo também, atente-se para os seguintes passos:
1. Recompile e gere a versão executável da aplicação em `/dist` com os comandos `dotnet build && dotnet restore && dotnet publish -o ./dist`;
2. Crie um diretório chamado `/src` e insira nele o projeto inteiro, exceto o Dockerfile;
3. Insira dados válidos nas variáveis de ambiente do `Dockerfile`;
4. Publique no serviço de núvem de sua preferência.

## Execução
Utilize o comando `dotnet run`. Será iniciada uma instância do servidor Kestrel do .NET Core. Esta aplicação é compatível com Windows, Linux e Mac OS X, desde que o .NET Core esteja instalado.

## Arquitetura da solução
![Principal](https://raw.githubusercontent.com/marcomvidal/SantoAndreOnBus/master/arquitetura.png)
