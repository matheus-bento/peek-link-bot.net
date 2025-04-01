# peek-link-bot

A new version of peek-link-bot, the reddit bot that gives a preview of a parent comment's linked URLs so you don't get
rickrolled anymore.

## Running the code

First, you have to create a reddit account and add the peek-link-bot application to the authorized applications for the account. You can do this on https://old.reddit.com/prefs/apps/.

After you register the application, add the following informations into your environment variables:

``` bash
export PeekLinkBot__ClientID="The client ID for your reddit application"
export PeekLinkBot__Secret="The secret of your reddit application"
export PeekLinkBot__Username="The bot's reddit account username"
export PeekLinkBot__Password="The bot's reddit account password"
export PeekLinkBot__MessageCheckInterval=15
```

And then run the service with the following command:

``` bash
dotnet run
```

Or alternatively, you can use docker to run the script into a container. Make sure to edit the Dockerfile and register the environment variables required by the application.

```Dockerfile
ENV PeekLinkBot__ClientID="The client ID for your reddit application"
ENV PeekLinkBot__Secret="The secret of your reddit application"
ENV PeekLinkBot__Username="The bot's reddit account username"
ENV PeekLinkBot__Password="The bot's reddit account password"
ENV PeekLinkBot__MessageCheckInterval=15
```

Then build the image

```bash
docker build -t peek-link-bot:dev -f ./src/PeekLinkBot/Dockerfile .
```

and run the container

```bash
docker run -d --name peek-link-bot peek-link-bot:dev
```
