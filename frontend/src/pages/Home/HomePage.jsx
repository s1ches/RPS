import React, {useCallback, useEffect, useState} from 'react';
import './styles/HomePage.css'
import Room from "../../models/Room";
import RoomCard from "../../components/Room/RoomCard/RoomCard";
import {RoomStatus} from "../../models/Shared/roomStatus";
import {createRoom, getRooms, joinRoom} from "../../services/room";
import {useNavigate} from "react-router-dom";
import {connectToRoom} from "../../services/signalR";

const HomePage = () => {
    const [rooms, setRooms] = useState([]);
    const [loading, setLoading] = useState(false);
    const [offset, setOffset] = useState(0);
    const [totalRooms, setTotalRooms] = useState(0);
    const [maxRating, setMaxRating] = useState(0);
    let navigate = useNavigate();
    const limit = 10;

    useEffect(() => {
        const rooms = [
            new Room({roomId: 'room-001'}),
            new Room({roomId: 'room-002'}),
            new Room({roomId: 'room-003'}),
        ];

        rooms[0].addPlayer('player1');
        rooms[0].addPlayer('player2');
        rooms[0].addSpectator('spectator1');
        rooms[0].addSpectator('spectator2');
        rooms[0].maxRating = 1000;
        rooms[0].status = RoomStatus.GameInProgress;

        // Пример для room2
        rooms[1].addPlayer('player3');
        rooms[1].addSpectator('spectator3');
        rooms[1].maxRating = 2000;
        rooms[1].status = RoomStatus.WaitingForPlayer;

        // Пример для room3
        rooms[2].maxRating = 1500;
        rooms[2].status = RoomStatus.Closed;


        setRooms(rooms);
        setTotalRooms(rooms.length);
    }, []);

    // Обработчик скролла
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
        connectToRoom().then((connection) => {
            fetchRooms()
            console.log(connection)
        })


    }, [offset]);

    useEffect(() => {
        window.addEventListener('scroll', handleScroll);
        return () => {
            window.removeEventListener('scroll', handleScroll);  // Очистка обработчика при размонтировании
        };
    }, [handleScroll]);

    const handleCreateRoom = () => {
        if (maxRating) {
            createRoom(maxRating).then(({roomId, error}) => {
                if (error) {
                    console.log(error)
                    alert(error);
                } else {
                    joinRoom(roomId).then((error) => {
                        if (error) {
                            console.log(error)
                            alert(error);
                        } else {
                            navigate(`/rooms/${roomId}`)
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
