import React, {createContext, useContext, useState} from 'react';
import Game from "../models/Game";
import Room from "../models/Room";
import {RoomStatus} from "../models/Shared/roomStatus";
import {GameStatus} from "../models/Shared/gameStatus";

const RoomContext = createContext();

export const useRoom = () => {
    return useContext(RoomContext);
};

export const RoomProvider = ({children}) => {
    const [room, setRoom] = useState(new Room({
        roomId: null,
        player1: null,
        player2: null,
        maxRating: 0,
        status: RoomStatus.WaitingForPlayer,
        spectators: [],
        lastActivityTime: Date.now()
    }));
    const [game, setGame] = useState(new Game({
        gameId: null,
        roomId: null,
        player1: null,
        player2: null,
        rounds: [],
        status: GameStatus.Started,
        winnerId: null,
        victories1: 0,
        victories2: 0,
    }));

    const addSpectatorToRoom = (spectatorModel) => {
        room.addSpectator(spectatorModel);
        setRoom({...room});
    };

    const setWinner = (winnerId) => {
        game.winnerId = winnerId
        setGame({...game})
    };

    const leaveRoom = (leaveRoomModel) => {
        if (leaveRoomModel.isPlayer) {
            if (game._player1 === leaveRoomModel.userId) {
                game._player1 = null;
            } else {
                game._player2 = null;
            }
        } else {
            room.leaveSpectator(leaveRoomModel.userId);
        }

        setGame({...game})
        setRoom({...room});
    };

    const joinGame = (joinGameModel) => {
        if (game._player1 === null) {
            game._player1 = joinGameModel.playerId;
        } else {
            game._player2 = joinGameModel.playerId;
        }

        game.id = joinGameModel.gameId;
        game.status = joinGameModel.gameStatus;
        setGame({...game})
    };

    return (
        <RoomContext.Provider
            value={{room, setRoom, game, setGame, addSpectatorToRoom, setWinner, leaveRoom, joinGame}}>
            {children}
        </RoomContext.Provider>
    );
};