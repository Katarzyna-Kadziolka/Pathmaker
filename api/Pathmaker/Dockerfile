# Build ASP.NET API
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Pathmaker.Api/Pathmaker.Api.csproj", "Pathmaker.Api/"]
COPY ["Pathmaker.Application/Pathmaker.Application.csproj", "Pathmaker.Application/"]
COPY ["Pathmaker.Persistence/Pathmaker.Persistence.csproj", "Pathmaker.Persistence/"]
COPY ["Pathmaker.Shared/Pathmaker.Shared.csproj", "Pathmaker.Shared/"]
COPY ["Pathmaker.Infrastructure/Pathmaker.Infrastructure.csproj", "Pathmaker.Infrastructure/"]
RUN dotnet restore "Pathmaker.Api/Pathmaker.Api.csproj"
COPY . .
WORKDIR "/src/Pathmaker.Api"
RUN dotnet build "Pathmaker.Api.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Pathmaker.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Adds support for ASPNETCORE_ENVIRONMENT in Dockerfile
ARG ENVIRONMENT
ENV ASPNETCORE_ENVIRONMENT ${ENVIRONMENT}
# For Heroku hosting:
# Dockerfile cannot have EXPOSE
# Application must be using $PORT variable given by Heroku
ARG PORT
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Pathmaker.Api.dll
