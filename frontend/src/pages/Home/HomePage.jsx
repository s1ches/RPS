import React, {useCallback, useEffect, useState} from 'react';
import './styles/HomePage.css'
import Room from "../../models/Room";
import RoomCard from "../../components/Room/RoomCard/RoomCard";
import {RoomStatus} from "../../models/Shared/roomStatus";

const HomePage = () => {
    const [rooms, setRooms] = useState([]);
    const [loading, setLoading] = useState(false);
    const [offset, setOffset] = useState(0);
    const [totalRooms, setTotalRooms] = useState(0);
    const [maxRating, setMaxRating] = useState(0);
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


    // // Функция загрузки игр с использованием limit и offset
    // const fetchRooms = useCallback(async () => {
    //     if (loading) return; // Предотвращаем множественные запросы
    //     setLoading(true);
    //     try {
    //         const response = await api.get(`/rooms?limit=${limit}&offset=${offset}`);
    //         setRooms((prevRooms) => [...prevRooms, ...response.data.rooms]); // Добавляем новые комнаты
    //         setTotalRooms(response.data.totalRooms);
    //     } catch (error) {
    //         console.error('Ошибка при загрузке комнат', error);
    //         alert('Ошибка при загрузке комнат');
    //     }
    //     setLoading(false);
    // }, [loading, offset, limit]);

    // Обработчик скролла
    const handleScroll = useCallback(() => {
        const bottom = window.innerHeight + document.documentElement.scrollTop === document.documentElement.offsetHeight;
        if (bottom && !loading && offset + limit < totalRooms) {
            setOffset((prevOffset) => prevOffset + limit);  // Переход к следующей порции данных
        }
    }, [loading, offset, totalRooms]);

    // useEffect(() => {
    //     if (isAuth) {
    //         fetchRooms(); // Загружаем комнаты, если пользователь авторизован
    //     }
    // }, [isAuth, offset, fetchRooms]); // Загружаем комнаты при изменении offset

    useEffect(() => {
        window.addEventListener('scroll', handleScroll);
        return () => {
            window.removeEventListener('scroll', handleScroll);  // Очистка обработчика при размонтировании
        };
    }, [handleScroll]);

    const handleCreateRoom = () => {
        if (maxRating) {
            alert("Создали комнату")
            return;
            const newRoom = new Room();
            newRoom.maxRating = parseInt(maxRating, 10);
            newRoom.status = RoomStatus.WaitingForPlayer;
            setRooms((prevRooms) => [...prevRooms, newRoom]);
            setTotalRooms((prevTotalRooms) => prevTotalRooms + 1);
            setMaxRating(''); // Очистить поле ввода
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
