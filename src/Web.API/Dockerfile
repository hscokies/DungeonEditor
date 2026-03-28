FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Web.API/Web.API.csproj", "Web.API/"]
COPY ["Infrastructure.AppSettings/Infrastructure.AppSettings.csproj", "Infrastructure.AppSettings/"]
COPY ["Infrastructure.Persistence/Infrastructure.Persistence.csproj", "Infrastructure.Persistence/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Application.API/Application.API.csproj", "Application.API/"]
COPY ["Infrastructure.Messaging/Infrastructure.Messaging.csproj", "Infrastructure.Messaging/"]
COPY ["Infrastructure.Logging/Infrastructure.Logging.csproj", "Infrastructure.Logging/"]
COPY ["Infrastructure.Services/Infrastructure.Services.csproj", "Infrastructure.Services/"]
COPY ["Infrastructure.Storage/Infrastructure.Storage.csproj", "Infrastructure.Storage/"]
RUN dotnet restore "Web.API/Web.API.csproj"
COPY . .
WORKDIR "/src/Web.API"
RUN dotnet build "./Web.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Web.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.API.dll"]
