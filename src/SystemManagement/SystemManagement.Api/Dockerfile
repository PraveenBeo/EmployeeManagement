FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["src/BuildingBlocks/BuildingBlocks.Abstractions/BuildingBlocks.Abstractions.csproj", "src/BuildingBlocks/BuildingBlocks.Abstractions/"]
COPY ["src/BuildingBlocks/BuildingBlocks.Messaging/BuildingBlocks.Messaging.csproj", "src/BuildingBlocks/BuildingBlocks.Messaging/"]
COPY ["src/SystemManagement/Modules/AccessCardManagement/AccessCardManagement.Domain/AccessCardManagement.Domain.csproj", "src/SystemManagement/Modules/AccessCardManagement/AccessCardManagement.Domain/"]
COPY ["src/SystemManagement/Modules/AccessCardManagement/AccessCardManagement.Application/AccessCardManagement.Application.csproj", "src/SystemManagement/Modules/AccessCardManagement/AccessCardManagement.Application/"]
COPY ["src/SystemManagement/Modules/AccessCardManagement/AccessCardManagement.Infrastructure/AccessCardManagement.Infrastructure.csproj", "src/SystemManagement/Modules/AccessCardManagement/AccessCardManagement.Infrastructure/"]
COPY ["src/SystemManagement/Modules/AccessCardManagement/AccessCardManagement.Presentation/AccessCardManagement.Presentation.csproj", "src/SystemManagement/Modules/AccessCardManagement/AccessCardManagement.Presentation/"]
COPY ["src/SystemManagement/Modules/LeaveManagement/LeaveManagement.Domain/LeaveManagement.Domain.csproj", "src/SystemManagement/Modules/LeaveManagement/LeaveManagement.Domain/"]
COPY ["src/SystemManagement/Modules/LeaveManagement/LeaveManagement.Application/LeaveManagement.Application.csproj", "src/SystemManagement/Modules/LeaveManagement/LeaveManagement.Application/"]
COPY ["src/SystemManagement/Modules/LeaveManagement/LeaveManagement.Infrastructure/LeaveManagement.Infrastructure.csproj", "src/SystemManagement/Modules/LeaveManagement/LeaveManagement.Infrastructure/"]
COPY ["src/SystemManagement/Modules/LeaveManagement/LeaveManagement.Presentation/LeaveManagement.Presentation.csproj", "src/SystemManagement/Modules/LeaveManagement/LeaveManagement.Presentation/"]
COPY ["src/SystemManagement/Modules/ReportManagement/ReportManagement.Domain/ReportManagement.Domain.csproj", "src/SystemManagement/Modules/ReportManagement/ReportManagement.Domain/"]
COPY ["src/SystemManagement/Modules/ReportManagement/ReportManagement.Application/ReportManagement.Application.csproj", "src/SystemManagement/Modules/ReportManagement/ReportManagement.Application/"]
COPY ["src/SystemManagement/Modules/ReportManagement/ReportManagement.Infrastructure/ReportManagement.Infrastructure.csproj", "src/SystemManagement/Modules/ReportManagement/ReportManagement.Infrastructure/"]
COPY ["src/SystemManagement/Modules/ReportManagement/ReportManagement.Presentation/ReportManagement.Presentation.csproj", "src/SystemManagement/Modules/ReportManagement/ReportManagement.Presentation/"]
COPY ["src/SystemManagement/SystemManagement.Api/SystemManagement.Api.csproj", "src/SystemManagement/SystemManagement.Api/"]
RUN dotnet restore "src/SystemManagement/SystemManagement.Api/SystemManagement.Api.csproj"
COPY . .
RUN dotnet publish "src/SystemManagement/SystemManagement.Api/SystemManagement.Api.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SystemManagement.Api.dll"]
