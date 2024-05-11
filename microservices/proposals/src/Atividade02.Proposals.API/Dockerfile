FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./microservices/proposals/src/Atividade02.Proposals.API/Atividade02.Proposals.API.csproj", "proposals/src/Atividade02.Proposals.API/"]
COPY ["./microservices/BuildingBlocks/Atividade02.Core/Atividade02.Core.csproj", "BuildingBlocks/Atividade02.Core/"]
COPY ["./microservices/proposals/src/Atividade02.Proposals.Application/Atividade02.Proposals.Application.csproj", "proposals/src/Atividade02.Proposals.Application/"]
COPY ["./microservices/proposals/src/Atividade02.Proposals.Domain/Atividade02.Proposals.Domain.csproj", "proposals/src/Atividade02.Proposals.Domain/"]
COPY ["./microservices/proposals/src/Atividade02.Proposals.Domain.Services/Atividade02.Proposals.Domain.Services.csproj", "proposals/src/Atividade02.Proposals.Domain.Services/"]
COPY ["./microservices/proposals/src/Atividade02.Proposals.Infrastructure/Atividade02.Proposals.Infrastructure.csproj", "proposals/src/Atividade02.Proposals.Infrastructure/"]
COPY ["./microservices/proposals/src/Atividade02.Proposals.Infrastructure.ExternalServices.CreditAnalysisEngine/Atividade02.Proposals.Infrastructure.ExternalServices.CreditAnalysisEngine.csproj", "proposals/src/Atividade02.Proposals.Infrastructure.ExternalServices.CreditAnalysisEngine/"]
COPY ["./microservices/BuildingBlocks/Atividade02.Core.MessageBus/Atividade02.Core.MessageBus.csproj", "BuildingBlocks/Atividade02.Core.MessageBus/"]
RUN dotnet restore "proposals/src/Atividade02.Proposals.API/Atividade02.Proposals.API.csproj"
COPY ./microservices/ .

RUN dotnet build "proposals/src/Atividade02.Proposals.API/Atividade02.Proposals.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "proposals/src/Atividade02.Proposals.API/Atividade02.Proposals.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Atividade02.Proposals.API.dll"]
