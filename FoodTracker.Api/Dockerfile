FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FoodTracker.Api/FoodTracker.Api.csproj", "FoodTracker.Api/"]
COPY ["FoodTracker.Application/FoodTracker.Application.csproj", "FoodTracker.Application/"]
COPY ["FoodTracker.Domain/FoodTracker.Domain.csproj", "FoodTracker.Domain/"]
COPY ["FoodTracker.Infrastructure/FoodTracker.Infrastructure.csproj", "FoodTracker.Infrastructure/"]
COPY ["FoodTracker.Telegram/FoodTracker.Telegram.csproj", "FoodTracker.Telegram/"]
COPY ["FoodTracker.sln", "./"]
RUN dotnet restore "FoodTracker.Api/FoodTracker.Api.csproj"
COPY . .
WORKDIR "/src/FoodTracker.Api"
RUN dotnet build "FoodTracker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FoodTracker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodTracker.Api.dll"]
