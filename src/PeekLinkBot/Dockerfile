FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIG=Release
WORKDIR /base
COPY ["src/PeekLinkBot/PeekLinkBot.csproj", "PeekLinkBot/"]
RUN dotnet restore "./PeekLinkBot/PeekLinkBot.csproj"
COPY . .
WORKDIR /base/src/PeekLinkBot
RUN dotnet build "./PeekLinkBot.csproj" -c $BUILD_CONFIG -o /app/build

FROM build AS publish
ARG BUILD_CONFIG=Release
RUN dotnet publish "./PeekLinkBot.csproj" -c $BUILD_CONFIG -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:9.0

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PeekLinkBot.dll"]
