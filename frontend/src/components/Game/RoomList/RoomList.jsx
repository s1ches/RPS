import React, { useState, useEffect, useCallback } from 'react';
import { api } from '../../../services/api';
import RoomCard from '../RoomCard';
import Room from "../../../models/Room"; // Компонент для отображения комнаты
import './styles/RoomList.css'

const RoomList = ({ isAuth, limit = 5 }) => {
    const [rooms, setRooms] = useState([]);
    const [loading, setLoading] = useState(false);
    const [offset, setOffset] = useState(0);
    const [totalRooms, setTotalRooms] = useState(0);

    // Моковые данные должны быть загружены только один раз
    useEffect(() => {
        const mockRooms = [
            new Room('room-001'),
            new Room('room-002'),
            new Room('room-003'),
            new Room('room-004'),
            new Room('room-005'),
            new Room('room-006'),
            new Room('room-007'),
            new Room('room-008'),
            new Room('room-009'),
            new Room('room-010'),
        ];

        mockRooms[0].addPlayer('player1');
        mockRooms[0].addPlayer('player2');
        mockRooms[0].addSpectator('viewer1');
        mockRooms[0].addSpectator('viewer2');
        mockRooms[0].addRound('Player1 выбрал камень, Player2 выбрал ножницы');
        mockRooms[0].addRound('Player1 выбрал ножницы, Player2 выбрал бумагу');
        mockRooms[0].endGame('Победа игрока 1');

        mockRooms[1].addPlayer('player3');
        mockRooms[1].addPlayer('player4');
        mockRooms[1].addSpectator('viewer3');
        mockRooms[1].addSpectator('viewer4');
        mockRooms[1].addRound('Player3 выбрал бумагу, Player4 выбрал камень');
        mockRooms[1].endGame('Победа игрока 3');

        mockRooms[2].addPlayer('player5');
        mockRooms[2].addPlayer('player6');
        mockRooms[2].addSpectator('viewer5');
        mockRooms[2].addSpectator('viewer6');
        mockRooms[2].addRound('Player5 выбрал камень, Player6 выбрал ножницы');
        mockRooms[2].addRound('Player5 выбрал бумагу, Player6 выбрал ножницы');
        mockRooms[2].endGame('Победа игрока 6');

        mockRooms[3].addPlayer('player7');
        mockRooms[3].addPlayer('player8');
        mockRooms[3].addSpectator('viewer7');
        mockRooms[3].addSpectator('viewer8');
        mockRooms[3].addRound('Player7 выбрал ножницы, Player8 выбрал бумагу');
        mockRooms[3].addRound('Player7 выбрал камень, Player8 выбрал ножницы');
        mockRooms[3].endGame('Победа игрока 8');

        mockRooms[4].addPlayer('player9');
        mockRooms[4].addPlayer('player10');
        mockRooms[4].addSpectator('viewer9');
        mockRooms[4].addSpectator('viewer10');
        mockRooms[4].addRound('Player9 выбрал бумагу, Player10 выбрал камень');
        mockRooms[4].addRound('Player9 выбрал ножницы, Player10 выбрал бумагу');
        mockRooms[4].endGame('Победа игрока 9');

        mockRooms[5].addPlayer('player11');
        mockRooms[5].addPlayer('player12');
        mockRooms[5].addSpectator('viewer11');
        mockRooms[5].addSpectator('viewer12');
        mockRooms[5].addRound('Player11 выбрал камень, Player12 выбрал ножницы');
        mockRooms[5].addRound('Player11 выбрал бумагу, Player12 выбрал ножницы');
        mockRooms[5].endGame('Победа игрока 11');

        mockRooms[6].addPlayer('player13');
        mockRooms[6].addPlayer('player14');
        mockRooms[6].addSpectator('viewer13');
        mockRooms[6].addSpectator('viewer14');
        mockRooms[6].addRound('Player13 выбрал камень, Player14 выбрал ножницы');
        mockRooms[6].addRound('Player13 выбрал ножницы, Player14 выбрал бумагу');
        mockRooms[6].endGame('Победа игрока 13');

        mockRooms[7].addPlayer('player15');
        mockRooms[7].addPlayer('player16');
        mockRooms[7].addSpectator('viewer15');
        mockRooms[7].addSpectator('viewer16');
        mockRooms[7].addRound('Player15 выбрал бумагу, Player16 выбрал камень');
        mockRooms[7].addRound('Player15 выбрал ножницы, Player16 выбрал бумагу');
        mockRooms[7].endGame('Победа игрока 16');

        mockRooms[8].addPlayer('player17');
        mockRooms[8].addPlayer('player18');
        mockRooms[8].addSpectator('viewer17');
        mockRooms[8].addSpectator('viewer18');
        mockRooms[8].addRound('Player17 выбрал камень, Player18 выбрал ножницы');
        mockRooms[8].addRound('Player17 выбрал ножницы, Player18 выбрал бумагу');
        mockRooms[8].endGame('Победа игрока 17');

        mockRooms[9].addPlayer('player19');
        mockRooms[9].addPlayer('player20');
        mockRooms[9].addSpectator('viewer19');
        mockRooms[9].addSpectator('viewer20');
        mockRooms[9].addRound('Player19 выбрал бумагу, Player20 выбрал камень');
        mockRooms[9].addRound('Player19 выбрал ножницы, Player20 выбрал бумагу');
        mockRooms[9].endGame('Победа игрока 20');

        setRooms(mockRooms); // Устанавливаем моковые данные
        setTotalRooms(mockRooms.length);
    }, []); // Этот эффект выполнится только при монтировании компонента


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
    }, [loading, offset, limit, totalRooms]);

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

    return (
        <div className="room-list-container">
            <h2>Выбор комнаты</h2>
            <div className="rooms">
                {rooms.map((room) => (
                    <RoomCard key={room.roomId} room={room} />
                ))}
            </div>
            {loading && <p className="loading-text">Загрузка...</p>}
        </div>
    );
};

export default RoomList;
