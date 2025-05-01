# peek-link-bot

A new version of peek-link-bot, the reddit bot that gives a preview of a parent comment's linked URLs so you don't get
rickrolled anymore.

## Running the code

First, you have to create a reddit account and add the peek-link-bot application to the authorized applications for the account. You can do this on https://old.reddit.com/prefs/apps/.

After you register the application, add the following informations into your environment variables:

``` bash
export PEEK_LINK_BOT_CLIENT_ID="The client ID for your reddit application"
export PEEK_LINK_BOT_SECRET="The secret of your reddit application"
export PEEK_LINK_BOT_USERNAME="The bot's reddit account username"
export PEEK_LINK_BOT_PASSWORD="The bot's reddit account password"
export PEEK_LINK_BOT_MESSAGE_CHECK_INTERVAL=15 # Seconds
export PEEK_LINK_BOT_MONGO_DB_CONNECTION_STRING="Mongo database that will store the bot's interactions"
```

And then run the service with the following command:

``` bash
dotnet run
```

Or alternatively, you can use docker-compose to create an environment ready to run the application.

```bash
docker-compose -p peek-link-bot up -d
```

Make sure to register the required environment variables before starting the docker compose project.
