FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
LABEL version="1.2" maintainer="Marco Vidal"

# Environment variables
ENV POSTGRESQLCONNSTR_PostgreSqlDatabase "User ID=USERNAME;Password=PASSWORD;Host=SERVERNAME;Port=5432;Database=DATABASE;sslmode=Require;Trust Server Certificate=true"
ENV ASPNETCORE_JwtSecret "JWTTOKEN"

# Tasks
WORKDIR /app
COPY ./src/dist .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet SantoAndreOnBus.dll
