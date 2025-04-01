FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:9.0
WORKDIR /app
COPY --from=build-env /app/out .

ENV PeekLinkBot__ClientID="PeekLinkBot__ClientID"
ENV PeekLinkBot__Secret="PeekLinkBot__Secret"
ENV PeekLinkBot__Username="PeekLinkBot__Username"
ENV PeekLinkBot__Password="PeekLinkBot__Password"
ENV PeekLinkBot__MessageCheckInterval=15

ENTRYPOINT ["dotnet", "PeekLinkBot.dll"]