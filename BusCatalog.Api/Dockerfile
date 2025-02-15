# Fetches the build image, restore the dependencies & publish it
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Fetches the execution image & copies the built files
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Copy SQLite database file
COPY ./BusCatalog.Api/Database.db /app/Database.db

# Sets the application to expose ports & run it
EXPOSE 80
ENV ASPNETCORE_URLS=http://0.0.0.0:80
ENTRYPOINT ["dotnet", "BusCatalog.Api.dll"]