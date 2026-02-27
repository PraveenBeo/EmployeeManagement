FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["src/Web/EmployeeManagement.Web/EmployeeManagement.Web.csproj", "src/Web/EmployeeManagement.Web/"]
RUN dotnet restore "src/Web/EmployeeManagement.Web/EmployeeManagement.Web.csproj"
COPY . .
RUN dotnet publish "src/Web/EmployeeManagement.Web/EmployeeManagement.Web.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EmployeeManagement.Web.dll"]
