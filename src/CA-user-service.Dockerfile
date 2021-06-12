FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/UserService/API/CA.Services.UserService.API.csproj", "Services/UserService/API/"]
COPY ["Services/UserService/Infrastructure/CA.Services.UserService.Infrastructure.csproj", "Services/UserService/Infrastructure/"]
COPY ["Services/UserService/Domain/CA.Services.UserService.Domain.csproj", "Services/UserService/Domain/"]
RUN dotnet restore "Services/UserService/API/CA.Services.UserService.API.csproj"
COPY . .
WORKDIR "/src/Services/UserService/API"
RUN dotnet build "CA.Services.UserService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CA.Services.UserService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CA.Services.UserService.API.dll"]