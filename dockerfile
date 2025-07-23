# Étape 1 : Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Étape 2 : Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Config Render
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# ✅ Remplace ce nom si ton fichier s'appelle différemment
ENTRYPOINT ["dotnet", "abaBackOffice.dll"]
