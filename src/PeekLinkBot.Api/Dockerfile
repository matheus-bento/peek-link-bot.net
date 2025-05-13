FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIG=Release
WORKDIR /base
COPY ["src/PeekLinkBot.Api/PeekLinkBot.Api.csproj", "PeekLinkBot.Api/"]
RUN dotnet restore "./PeekLinkBot.Api/PeekLinkBot.Api.csproj"
COPY . .
WORKDIR /base/src/PeekLinkBot.Api
RUN dotnet build "./PeekLinkBot.Api.csproj" -c $BUILD_CONFIG -o /app/build

FROM build AS publish
ARG BUILD_CONFIG=Release
RUN dotnet publish "./PeekLinkBot.Api.csproj" -c $BUILD_CONFIG -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PeekLinkBot.Api.dll"]
