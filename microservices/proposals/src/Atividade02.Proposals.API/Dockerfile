#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["proposals/src/Atividade02.Proposals.API/Atividade02.Proposals.API.csproj", "proposals/src/Atividade02.Proposals.API/"]
COPY ["BuildingBlocks/Atividade02.Core/Atividade02.Core.csproj", "BuildingBlocks/Atividade02.Core/"]
COPY ["proposals/src/Atividade02.Proposals.Application/Atividade02.Proposals.Application.csproj", "proposals/src/Atividade02.Proposals.Application/"]
COPY ["proposals/src/Atividade02.Proposals.Domain/Atividade02.Proposals.Domain.csproj", "proposals/src/Atividade02.Proposals.Domain/"]
COPY ["proposals/src/Atividade02.Proposals.Domain.Services/Atividade02.Proposals.Domain.Services.csproj", "proposals/src/Atividade02.Proposals.Domain.Services/"]
COPY ["proposals/src/Atividade02.Proposals.Infrastructure/Atividade02.Proposals.Infrastructure.csproj", "proposals/src/Atividade02.Proposals.Infrastructure/"]
COPY ["proposals/src/Atividade02.Proposals.Infrastructure.ExternalServices.CreditAnalysisEngine/Atividade02.Proposals.Infrastructure.ExternalServices.CreditAnalysisEngine.csproj", "proposals/src/Atividade02.Proposals.Infrastructure.ExternalServices.CreditAnalysisEngine/"]
COPY ["BuildingBlocks/Atividade02.Core.MessageBus/Atividade02.Core.MessageBus.csproj", "BuildingBlocks/Atividade02.Core.MessageBus/"]
RUN dotnet restore "proposals/src/Atividade02.Proposals.API/Atividade02.Proposals.API.csproj"
COPY . .
WORKDIR "/src/proposals/src/Atividade02.Proposals.API"
RUN dotnet build "Atividade02.Proposals.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Atividade02.Proposals.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Atividade02.Proposals.API.dll"]
