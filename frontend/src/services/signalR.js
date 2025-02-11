import {HubConnectionBuilder, LogLevel} from "@microsoft/signalr";

let connection = null;

export const connectToRoom = async (eventHandlers) => {
    if (connection) {
        console.warn("⚠️ SignalR уже подключён");
        return connection;
    }

    connection = new HubConnectionBuilder()
        .withUrl(process.env.REACT_APP_RPS_SIGNALR, {
            accessTokenFactory: () => localStorage.getItem("token"),
        })
        .configureLogging(LogLevel.Information)
        .build();

    connection.on("JoinRoom", eventHandlers.onNewSpectator);
    connection.on("KnowWinner", eventHandlers.onWinner);
    connection.on("LeaveRoom", eventHandlers.onLeavingRoom);
    connection.on("JoinGame", eventHandlers.onNewPlayer);

    try {
        await connection.start();
        console.log("✅ Подключено к комнате");
    } catch (err) {
        console.error("❌ Ошибка подключения к комнате:", err);
    }

    return connection;
};

export const disconnectFromRoom = async () => {
    if (connection) {
        await connection.stop();
        console.log("❌ Отключено от комнаты");
        connection = null;
    }
};