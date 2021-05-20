#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/RemarkService/API/CA.Services.RemarkService.API/CA.Services.RemarkService.API.csproj", "Services/RemarkService/API/CA.Services.RemarkService.API/"]
RUN dotnet restore "Services/RemarkService/API/CA.Services.RemarkService.API/CA.Services.RemarkService.API.csproj"
COPY . .
WORKDIR "/src/Services/RemarkService/API/CA.Services.RemarkService.API"
RUN dotnet build "CA.Services.RemarkService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CA.Services.RemarkService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CA.Services.RemarkService.API.dll"]