FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/AuthoringService/API/CA.Services.AuthoringService.API.csproj", "Services/AuthoringService/API/"]
COPY ["Services/AuthoringService/Infrastructure/CA.Services.AuthoringService.Infrastructure.csproj", "Services/AuthoringService/Infrastructure/"]
COPY ["Services/AuthoringService/Domain/CA.Services.AuthoringService.Domain.csproj", "Services/AuthoringService/Domain/"]
RUN dotnet restore "Services/AuthoringService/API/CA.Services.AuthoringService.API.csproj"
COPY . .
WORKDIR "/src/Services/AuthoringService/API"
RUN dotnet build "CA.Services.AuthoringService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CA.Services.AuthoringService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CA.Services.AuthoringService.API.dll"]