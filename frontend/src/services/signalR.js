import {HubConnectionBuilder, LogLevel} from "@microsoft/signalr";
import {useRoom} from "../stores/roomStore";

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

    connection.on("JoinRoom", (spectatorModel) => handleNewSpectator(spectatorModel));
    connection.on("KnowWinner", (winnerId) => handleWinner(winnerId));
    connection.on("LeaveRoom", (leaveRoomModel) => handleLeavingRoom(leaveRoomModel));
    connection.on("JoinGame", (message) => handleNewPlayer(message));

    try {
        await connection.start();
        console.log("✅ Подключено к комнате");
    } catch (err) {
        console.error("❌ Ошибка подключения к комнате:", err);
    }

    return connection;
};

const handleNewSpectator = (spectatorModel) => {
    const {addSpectatorToRoom} = useRoom();
    addSpectatorToRoom(spectatorModel);
};

const handleWinner = (winnerId) => {
    const {setWinner} = useRoom();
    setWinner(winnerId);
};

const handleLeavingRoom = (leaveRoomModel) => {
    const {leaveRoom} = useRoom();
    leaveRoom(leaveRoomModel);
};

const handleNewPlayer = (spectator) => {
    const {JoinGame} = useRoom();
    JoinGame(spectator);
};

export const disconnectFromRoom = async () => {
    if (connection) {
        await connection.stop();
        console.log("❌ Отключено от комнаты");
        connection = null;
    }
};