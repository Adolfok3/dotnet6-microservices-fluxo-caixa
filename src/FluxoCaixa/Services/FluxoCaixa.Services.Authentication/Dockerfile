#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/FluxoCaixa.Services.Authentication/FluxoCaixa.Services.Authentication.csproj", "Services/FluxoCaixa.Services.Authentication/"]
COPY ["Chassis/FluxoCaixa.Chassis.Startup/FluxoCaixa.Chassis.Startup.csproj", "Chassis/FluxoCaixa.Chassis.Startup/"]
COPY ["Chassis/FluxoCaixa.Chassis.Logging/FluxoCaixa.Chassis.Logging.csproj", "Chassis/FluxoCaixa.Chassis.Logging/"]
COPY ["Chassis/FluxoCaixa.Chassis.Messaging/FluxoCaixa.Chassis.Messaging.csproj", "Chassis/FluxoCaixa.Chassis.Messaging/"]
COPY ["Chassis/FluxoCaixa.Chassis.Utils/FluxoCaixa.Chassis.Utils.csproj", "Chassis/FluxoCaixa.Chassis.Utils/"]
COPY ["Chassis/FluxoCaixa.Chassis.ExceptionHandler/FluxoCaixa.Chassis.ExceptionHandler.csproj", "Chassis/FluxoCaixa.Chassis.ExceptionHandler/"]
COPY ["Chassis/FluxoCaixa.Chassis.Persistence/FluxoCaixa.Chassis.Persistence.csproj", "Chassis/FluxoCaixa.Chassis.Persistence/"]
COPY ["Chassis/FluxoCaixa.Chassis.Authentication/FluxoCaixa.Chassis.Authentication.csproj", "Chassis/FluxoCaixa.Chassis.Authentication/"]
COPY ["Chassis/FluxoCaixa.Chassis.Caching/FluxoCaixa.Chassis.Caching.csproj", "Chassis/FluxoCaixa.Chassis.Caching/"]
COPY ["Chassis/FluxoCaixa.Chassis.HealthCheck/FluxoCaixa.Chassis.HealthCheck.csproj", "Chassis/FluxoCaixa.Chassis.HealthCheck/"]
COPY ["Chassis/FluxoCaixa.Chassis.AutoMapper/FluxoCaixa.Chassis.AutoMapper.csproj", "Chassis/FluxoCaixa.Chassis.AutoMapper/"]
COPY ["Chassis/FluxoCaixa.Chassis.HttpLogging/FluxoCaixa.Chassis.HttpLogging.csproj", "Chassis/FluxoCaixa.Chassis.HttpLogging/"]
COPY ["Chassis/FluxoCaixa.Chassis.Swagger/FluxoCaixa.Chassis.Swagger.csproj", "Chassis/FluxoCaixa.Chassis.Swagger/"]
COPY ["Chassis/FluxoCaixa.Chassis.Vault/FluxoCaixa.Chassis.Vault.csproj", "Chassis/FluxoCaixa.Chassis.Vault/"]
RUN dotnet restore "Services/FluxoCaixa.Services.Authentication/FluxoCaixa.Services.Authentication.csproj"
COPY . .
WORKDIR "/src/Services/FluxoCaixa.Services.Authentication"
RUN dotnet build "FluxoCaixa.Services.Authentication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FluxoCaixa.Services.Authentication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FluxoCaixa.Services.Authentication.dll"]
