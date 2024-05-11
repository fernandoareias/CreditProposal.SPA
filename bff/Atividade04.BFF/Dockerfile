#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./bff/Atividade04.BFF/Atividade04.BFF.csproj", "../bff/Atividade04.BFF/"]
COPY ["./microservices/BuildingBlocks/Atividade02.Core.MessageBus/Atividade02.Core.MessageBus.csproj", "BuildingBlocks/Atividade02.Core.MessageBus/"]
COPY ["./microservices/BuildingBlocks/Atividade02.Core/Atividade02.Core.csproj", "BuildingBlocks/Atividade02.Core/"]
RUN dotnet restore "../bff/Atividade04.BFF/Atividade04.BFF.csproj"
COPY . .
RUN dotnet build "./bff/Atividade04.BFF/Atividade04.BFF.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./bff/Atividade04.BFF/Atividade04.BFF.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Atividade04.BFF.dll"]
