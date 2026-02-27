FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["src/BuildingBlocks/BuildingBlocks.Abstractions/BuildingBlocks.Abstractions.csproj", "src/BuildingBlocks/BuildingBlocks.Abstractions/"]
COPY ["src/BuildingBlocks/BuildingBlocks.Messaging/BuildingBlocks.Messaging.csproj", "src/BuildingBlocks/BuildingBlocks.Messaging/"]
COPY ["src/UserManagement/UserManagement.Domain/UserManagement.Domain.csproj", "src/UserManagement/UserManagement.Domain/"]
COPY ["src/UserManagement/UserManagement.Application/UserManagement.Application.csproj", "src/UserManagement/UserManagement.Application/"]
COPY ["src/UserManagement/UserManagement.Infrastructure/UserManagement.Infrastructure.csproj", "src/UserManagement/UserManagement.Infrastructure/"]
COPY ["src/UserManagement/UserManagement.Api/UserManagement.Api.csproj", "src/UserManagement/UserManagement.Api/"]
RUN dotnet restore "src/UserManagement/UserManagement.Api/UserManagement.Api.csproj"
COPY . .
RUN dotnet publish "src/UserManagement/UserManagement.Api/UserManagement.Api.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UserManagement.Api.dll"]
