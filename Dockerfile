FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build_backend
WORKDIR /src
COPY backend/. .
RUN dotnet restore "UrlShortener.sln"
RUN dotnet build "UrlShortener.sln" -c Release

FROM build_backend AS publish
WORKDIR "/src/UrlShortener.Api"
RUN dotnet publish "UrlShortener.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM node:18-alpine AS build_frontend
WORKDIR /frontend
COPY frontend/package.json .
COPY frontend/package-lock.json .
RUN npm install
COPY frontend/. .
RUN npm run build

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build_frontend /frontend/dist ./wwwroot
ENTRYPOINT ["dotnet", "UrlShortener.Api.dll"]