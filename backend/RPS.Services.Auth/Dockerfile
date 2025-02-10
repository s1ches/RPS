FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RPS.Services.Auth/RPS.Services.Auth.csproj", "RPS.Services.Auth/"]
RUN dotnet restore "RPS.Services.Auth/RPS.Services.Auth.csproj"
COPY . .
WORKDIR "/src/RPS.Services.Auth"
RUN dotnet build "RPS.Services.Auth.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RPS.Services.Auth.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RPS.Services.Auth.dll"]
