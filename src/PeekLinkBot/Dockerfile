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

ENV PEEK_LINK_BOT_CLIENT_ID="PEEK_LINK_BOT_CLIENT_ID"
ENV PEEK_LINK_BOT_SECRET="PEEK_LINK_BOT_SECRET"
ENV PEEK_LINK_BOT_USERNAME="PEEK_LINK_BOT_USERNAME"
ENV PEEK_LINK_BOT_PASSWORD="PEEK_LINK_BOT_PASSWORD"
ENV PEEK_LINK_BOT_MESSAGE_CHECK_INTERVAL=15
ENV PEEK_LINK_BOT_MONGO_DB_CONNECTION_STRING="PEEK_LINK_BOT_MONGO_DB_CONNECTION_STRING"

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PeekLinkBot.dll"]
