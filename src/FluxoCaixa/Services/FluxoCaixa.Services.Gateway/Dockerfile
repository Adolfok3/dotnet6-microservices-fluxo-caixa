#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/FluxoCaixa.Services.Gateway/FluxoCaixa.Services.Gateway.csproj", "Services/FluxoCaixa.Services.Gateway/"]
COPY ["Chassis/FluxoCaixa.Chassis.Vault/FluxoCaixa.Chassis.Vault.csproj", "Chassis/FluxoCaixa.Chassis.Vault/"]
COPY ["Chassis/FluxoCaixa.Chassis.Utils/FluxoCaixa.Chassis.Utils.csproj", "Chassis/FluxoCaixa.Chassis.Utils/"]
COPY ["Chassis/FluxoCaixa.Chassis.Persistence/FluxoCaixa.Chassis.Persistence.csproj", "Chassis/FluxoCaixa.Chassis.Persistence/"]
COPY ["Chassis/FluxoCaixa.Chassis.Authentication/FluxoCaixa.Chassis.Authentication.csproj", "Chassis/FluxoCaixa.Chassis.Authentication/"]
COPY ["Chassis/FluxoCaixa.Chassis.AutoMapper/FluxoCaixa.Chassis.AutoMapper.csproj", "Chassis/FluxoCaixa.Chassis.AutoMapper/"]
RUN dotnet restore "Services/FluxoCaixa.Services.Gateway/FluxoCaixa.Services.Gateway.csproj"
COPY . .
WORKDIR "/src/Services/FluxoCaixa.Services.Gateway"
RUN dotnet build "FluxoCaixa.Services.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FluxoCaixa.Services.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FluxoCaixa.Services.Gateway.dll"]
