# peek-link-bot-v2

A new version of peek-link-bot, the reddit bot that gives a preview of a parent comment's linked URLs so you don't get rickrolled anymore.

## Running the code

First, you have to create a reddit account and add the peek-link-bot application to the authorized applications for the account. You can do this on https://old.reddit.com/prefs/apps/.

After you register the application, add the following informations into your environment variables:

``` bash
    export PeekLinkBot__Username="The bot's reddit account username"
    export PeekLinkBot__Password="The bot's reddit account password"
    export PeekLinkBot__ClientID="The client ID for your reddit application"
    export PeekLinkBot__Secret="The secret of your reddit application"
```

And then run the service with the following command:

``` bash
    dotnet run ./bin/Debug/net6.0/PeekLinkBot.dll
```