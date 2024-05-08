![choco](https://i.ibb.co/VjySGrT/45-2-4.png)

## Architecture

Choco operates with two main Docker services:

1. **PostgreSQL Docker Service**: Manages all data-related operations, ensuring data persistence and structured access. Data-related services are initiated through a Docker container configured with the entry point ```docker-entrypoint-initdb.d ```
2. **Bot Application**: Handles interactions with Discord, executing commands and services based on user interactions or predefined conditions through handling commands

## Services

Choco's capabilities are distributed across several services, each designed to perform specific functions:

1. **ServiceCommandRpgService**: Manages the creation of embedded content for RPG commands, enhancing user interaction with visually rich messages.
2. **ServicesGames**: A general service hub for game-related functionalities. Currently includes:
   - **ServiceChannelGameMenu**: Service to call up the menu. Initially 3 messages of any kind must be created from the bot in it.
   - **ServiceGameNovella**: Handles the execution and management of a novel-style game. 
3. **ServicesLogsDiscord**: Logs various events and activities directly into a designated Discord server channel for monitoring and auditing purposes. At the moment, this channel displays logs for interaction with the visual novel's play button
4. **ServiceChannelReactionEmoji**: Automatically adds reaction emojis to messages in specified Discord channels.
5. **ServiceSendEmojiToChat**: Sends random emojis at random intervals to specified channels, adding a fun and unpredictable element to the interactions. The desired emoji is changed in the emojiesNames list.

## Configuration

Configuration of service channels and database connections are managed through environmental variables specified in a `.env` file located at the project root:

```plaintext
POSTGRES_HOST=postgres
POSTGRES_PORT=5432
POSTGRES_USER=choco
POSTGRES_PASSWORD=kalmari
POSTGRES_DB=choco
ID_CHANNEL_GAME_LOGS=<SELECT ID>
ID_CHANNEL_GAME_UPDATE_MENU=<SELECT ID>
ID_CHANNEL_SEND_EMOJI=<SELECT ID>
ID_CHANNEL_SEND_REACTION1=<SELECT ID>
ID_CHANNEL_SEND_REACTION2=<SELECT ID>
```

## Token/Prefix Configuration

The bot token and prefix, crucial for bot authentication and operations, is sensitive and should be securely managed. Change the token in the `config.json` file located in the `Config` folder:

```
Config/
│
└─── config.json
```

Update the file with your Discord bot token to ensure that Choco connects and interacts with Discord servers correctly.

## Running Choco

To run Choco, ensure that you have Docker installed and configured, as the PostgreSQL service runs on a Docker container. Run the bot via ```docker-compose.yaml``` don't forget to change the necessary variables in ```.env```.
