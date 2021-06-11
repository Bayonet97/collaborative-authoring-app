  
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/AuthorizationService/API/CA.Services.AuthorizationService.API.csproj", "Services/AuthorizationService/API/"]
COPY ["Services/AuthorizationService/Infrastructure/CA.Services.AuthorizationService.Infrastructure.csproj", "Services/AuthorizationService/Infrastructure/"]
COPY ["Services/AuthorizationService/Domain/CA.Services.AuthorizationService.Domain.csproj", "Services/AuthorizationService/Domain/"]
RUN dotnet restore "Services/AuthorizationService/API/CA.Services.AuthorizationService.API.csproj"
COPY . .
WORKDIR "/src/Services/AuthorizationService/API"
RUN dotnet build "CA.Services.AuthorizationService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CA.Services.AuthorizationService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CA.Services.AuthorizationService.API.dll"]