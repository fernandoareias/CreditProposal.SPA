#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./microservices/portador/src/Atividade02.Portador.API/Atividade02.Portador.API.csproj", "portador/src/Atividade02.Portador.API/"]
COPY ["./microservices/BuildingBlocks/Atividade02.Core/Atividade02.Core.csproj", "BuildingBlocks/Atividade02.Core/"]
COPY ["./microservices/BuildingBlocks/Atividade02.Core.MessageBus/Atividade02.Core.MessageBus.csproj", "BuildingBlocks/Atividade02.Core.MessageBus/"]
RUN dotnet restore "portador/src/Atividade02.Portador.API/Atividade02.Portador.API.csproj"
COPY ./microservices/ .

RUN dotnet build "portador/src/Atividade02.Portador.API/Atividade02.Portador.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "portador/src/Atividade02.Portador.API/Atividade02.Portador.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Atividade02.Portador.API.dll"]
