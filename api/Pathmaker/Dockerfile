#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
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
ARG ENVIRONMENT
ENV ASPNETCORE_ENVIRONMENT ${ENVIRONMENT}
ENTRYPOINT ["dotnet", "Pathmaker.Api.dll"]