import React from 'react';

const RoomCard = ({ room }) => {
    return (
        <div className="room-card">
            <h3>Комната: {room.roomId}</h3>
            <p>Игрок 1: {room.player1 || 'Нет игрока'}</p>
            <p>Игрок 2: {room.player2 || 'Нет игрока'}</p>
            <p>Зрители: {room.spectators.join(', ') || 'Нет зрителей'}</p>
            <p>Статус: {room.status}</p>
            <p>Результат игры: {room.result}</p>
            <button>Присоединиться</button>
        </div>
    );
};

export default RoomCard;
