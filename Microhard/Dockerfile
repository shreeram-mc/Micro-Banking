FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 83
EXPOSE 444

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Microhard/Microhard.csproj", "Microhard/"]
RUN dotnet restore "Microhard/Microhard.csproj"
COPY . .
WORKDIR "/src/Microhard"
RUN dotnet build "Microhard.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Microhard.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Microhard.dll"]