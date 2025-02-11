import React, {createContext, useContext, useState} from 'react';
import Room from '../models/Room';
import Game from "../models/Game"; // Подключаем класс Room

const RoomContext = createContext();

export const useRoom = () => {
    return useContext(RoomContext);
};

export const RoomProvider = ({children}) => {
    const [room, setRoom] = useState(new Room());
    const [game, setGame] = useState(new Game());

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

    return (
        <RoomContext.Provider value={{room, addSpectatorToRoom, setWinner, leaveRoom}}>
            {children}
        </RoomContext.Provider>
    );
};