import * as signalR from "@microsoft/signalr";
import {HubConnectionBuilder, LogLevel} from "@microsoft/signalr";

let connection = null;

export const connectToRoom = async () => {
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

export const getConnection = () => connection;
