import React from 'react';
import {useNavigate} from "react-router-dom";
import {RoomStatus} from "../../../models/Shared/roomStatus";
import './styles/RoomCard.css';

const RoomCard = ({room}) => {
    let navigate = useNavigate();
    const joinRoom = () => {
        if (room.status !== RoomStatus.Closed) {
            navigate(`/room/${room.roomId}`);
        }
    }

    return (
        <div className="room-card">
            <h3 className="room-card__title">Комната: {room.roomId}</h3>
            <p className="room-card__players">
                Игрок 1: {room.player1 || 'Нет игрока'}
            </p>
            <p className="room-card__players">
                Игрок 2: {room.player2 || 'Нет игрока'}
            </p>
            <p className={`room-card__status ${room.status === RoomStatus.Closed ? 'room-card__status--closed' : ''}`}>
                Статус: {room.status}
            </p>
            <button
                className="room-card__button"
                onClick={joinRoom}
                disabled={room.status === RoomStatus.Closed}
            >
                Присоединиться
            </button>
        </div>
    );
};

export default RoomCard;
