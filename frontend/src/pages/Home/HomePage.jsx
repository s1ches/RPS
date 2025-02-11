import React, {useCallback, useEffect, useState} from 'react';
import './styles/HomePage.css'
import RoomCard from "../../components/Room/RoomCard/RoomCard";
import {createRoom, getRooms, joinRoom} from "../../services/room";
import {useNavigate} from "react-router-dom";
import {connectToRoom} from "../../services/signalR";
import {useRoom} from "../../stores/roomStore";

const HomePage = () => {
    const [rooms, setRooms] = useState([]);
    const [loading, setLoading] = useState(false);
    const [offset, setOffset] = useState(0);
    const [totalRooms, setTotalRooms] = useState(0);
    const [maxRating, setMaxRating] = useState(0);
    const {addSpectatorToRoom, setWinner, leaveRoom, joinGame} = useRoom();
    let navigate = useNavigate();
    const limit = 10;

    const handleScroll = useCallback(() => {
        const bottom = window.innerHeight + document.documentElement.scrollTop === document.documentElement.offsetHeight;
        if (bottom && !loading && offset + limit < totalRooms) {
            setOffset((prevOffset) => prevOffset + limit);
        }
    }, [loading, offset, totalRooms]);

    const fetchRooms = useCallback(async () => {
        if (loading)
            return;

        setLoading(true);
        const roomsData = await getRooms(limit, offset);

        if (roomsData.totalCount > 0) {
            setRooms((prevRooms) => [...prevRooms, ...roomsData.rooms]);
            setTotalRooms(roomsData.totalCount);
        } else {
            alert("Комнат нет")
        }

        setLoading(false);

    }, [loading, offset]);

    useEffect(() => {
        const eventHandlers = {
            onNewSpectator: addSpectatorToRoom,
            onWinner: setWinner,
            onLeavingRoom: leaveRoom,
            onNewPlayer: joinGame,
        };

        connectToRoom(eventHandlers).then(() => {
            fetchRooms()
        })


    }, [offset]);

    useEffect(() => {
        window.addEventListener('scroll', handleScroll);
        return () => {
            window.removeEventListener('scroll', handleScroll);
        };
    }, [handleScroll]);

    const handleCreateRoom = () => {
        if (maxRating) {
            createRoom(maxRating).then(({roomId, error}) => {
                if (error) {
                    console.log(error)
                    alert(error);
                } else {
                    joinRoom(roomId).then(({error}) => {
                        if (error) {
                            console.log('hello')
                            console.log(error)
                            alert(error);
                        } else {
                            navigate(`/room/${roomId}`)
                        }
                    })
                }
            })
        } else {
            alert('Пожалуйста, введите максимальный рейтинг!');
        }
    };

    return (
        <div className="room-list-container">
            <h2>Выбор комнаты</h2>
            <div className="create-room">
                <input
                    type="number"
                    placeholder="Введите максимальный рейтинг"
                    value={maxRating}
                    onChange={(e) => setMaxRating(e.target.value ? parseInt(e.target.value, 10) : '')}
                />
                <button onClick={handleCreateRoom}>Создать комнату</button>
            </div>
            <div className="rooms">
                {rooms.map((room) => (
                    <RoomCard key={room.roomId} room={room}/>
                ))}
            </div>
            {loading && <p className="loading-text">Загрузка...</p>}
        </div>
    );
};

export default HomePage;
