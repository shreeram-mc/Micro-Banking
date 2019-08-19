FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["MicroHard.Banking.Api/MicroHard.Banking.Api.csproj", "MicroHard.Banking.Api/"]
COPY ["MicroHard.Infra.Ioc/MicroHard.Infra.Ioc.csproj", "MicroHard.Infra.Ioc/"]
COPY ["Microhard.Infra.Bus/Microhard.Infra.Bus.csproj", "Microhard.Infra.Bus/"]
COPY ["Microhard/MicroHard.Domain.Core/MicroHard.Domain.Core.csproj", "Microhard/MicroHard.Domain.Core/"]
COPY ["Microhard.Transfer.Data/Microhard.Transfer.Data.csproj", "Microhard.Transfer.Data/"]
COPY ["Microhard.Transfer.Domain/Microhard.Transfer.Domain.csproj", "Microhard.Transfer.Domain/"]
COPY ["MicroHard.Banking.Domain/MicroHard.Banking.Domain.csproj", "MicroHard.Banking.Domain/"]
COPY ["MicroHard.Banking.Application/MicroHard.Banking.Application.csproj", "MicroHard.Banking.Application/"]
COPY ["MicroHard.Banking.Data/MicroHard.Banking.Data.csproj", "MicroHard.Banking.Data/"]
COPY ["Microhard.Transfer.Application/Microhard.Transfer.Application.csproj", "Microhard.Transfer.Application/"]
RUN dotnet restore "MicroHard.Banking.Api/MicroHard.Banking.Api.csproj"
COPY . .
WORKDIR "/src/MicroHard.Banking.Api"
RUN dotnet build "MicroHard.Banking.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MicroHard.Banking.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MicroHard.Banking.Api.dll"]
