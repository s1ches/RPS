import {useLocation, useNavigate} from "react-router-dom";
import {useEffect, useState} from "react";
import GameComponent from "../../components/Game/GameComponent/GameComponent";
import {useUserStore} from "../../stores/userStore";
import {RoomStatus} from "../../models/Shared/roomStatus";
import './styles/RoomPage.css'
import Room from "../../models/Room";
import Game from "../../models/Game";
import {GameStatus} from "../../models/Shared/gameStatus";
import {PlayerChoice} from "../../models/Shared/playerChoice";

const RoomPage = () => {
    const {state} = useLocation();
    const room = new Room(state?.room);
    const {user} = useUserStore();
    const player1Id = 'player-001';
    const player2Id = 'player-002';
    const [game, setGame] = useState(new Game({
        id: "game-001",
        roomId: room.roomId,
        player1: player1Id,
        player2: null,
        rounds: [{ player1Choice: PlayerChoice.Rock, player2Choice: PlayerChoice.Scissors, winner: player1Id }],
        status: GameStatus.Started,
        winnerId: null,
        victories1: 0,
        victories2: 0,
    }));

    // const [isJoined, setIsJoined] = useState(false);
    const [gameStarted, setGameStarted] = useState(true);//TODO: mock
    let navigate = useNavigate();

    useEffect(() => {
        // if (room.player1 === user.id || room.player2 === user.id) {
        //     setIsJoined(true);
        // }

        if (room.player1 && room.player2 && room.status === RoomStatus.WaitingForPlayer) {
            room.startGame();
            setGameStarted(true);
        }
    }, [room, user]);

    const joinGame = () => {
        if (room && user) {
            // Проверка, можно ли присоединиться как игрок
            if (room.status !== RoomStatus.WaitingForPlayer) {
                alert("Игра уже началась.");
            } else if (user.rating > room.maxRating) {
                alert("Ваш рейтинг слишком высок для этой комнаты.");
            } else if (room.player1 && room.player2) {
                alert("В комнате уже два игрока, невозможно присоединиться.");
            } else {
                room.addPlayer(user.id);
                // setIsJoined(true);

                if (room.player1 && room.player2) {
                    room.startGame();
                    setGameStarted(true);
                }
            }
        }
    };

    const leaveRoom = () => {
        if (room && user) {
            if (room.player1 === user.id || room.player2 === user.id) {
                if (!gameStarted) {
                    // room.leavePlayer(user.id);
                    navigate('/')
                    // setIsJoined(false);
                } else {
                    alert("Вы не можете выйти из комнаты, пока игра не завершена.");
                }
            } else {
                // room.leaveSpectator(user.id);
                navigate('/')
                // setIsJoined(false);
            }
        }
    };

    // useEffect(() => {
    //     // Загружаем информацию о комнате
    //     const fetchRoomDetails = async () => {
    //         try {
    //             const response = await $gameApi.get(`/rooms/${roomId}`);
    //             setRoom(response.data);
    //             setIsCreator(response.data.creator === 'player');
    //         } catch (error) {
    //             console.error('Ошибка при загрузке данных комнаты', error);
    //         }
    //     };
    //     fetchRoomDetails();
    // }, [roomId]);

    // const joinRoom = () => {
    //     if (userRating >= room.minRating && userRating <= room.maxRating) {
    //         setIsJoined(true);
    //         $gameApi.post(`/rooms/${roomId}/join`, { user: 'player' });
    //     } else {
    //         alert('Ваш рейтинг не соответствует требованиям для этой комнаты');
    //     }
    // };

    // const startGame = () => {
    //     // Логика запуска игры
    //     setGameStarted(true);
    //     $gameApi.post(`/rooms/${roomId}/start`);
    // };

    return (
        <div className="game-room-container">
            <h1 className="room-header">Комната {room.roomId}</h1>

            <div className="room-info">
                <p><strong>Максимальный рейтинг:</strong> {room.maxRating}</p>
                <p>
                    <strong>Игроки:</strong> {room.player1 ? room.player1 : 'Нет игрока 1'}, {room.player2 ? room.player2 : 'Нет игрока 2'}
                </p>
            </div>

            <div className="room-buttons">
                {(
                    // !isJoined ||
                    !gameStarted) && (
                    <button onClick={joinGame}>Присоединиться</button>
                )}
                {(
                    // isJoined &&
                    // !gameStarted) && (
                    <button onClick={leaveRoom}>Выйти из комнаты</button>
                )}
            </div>

            {room.spectators.length > 0 && (
                <div className="spectators-list">
                    <h3>Зрители</h3>
                    <ul>
                        {room.spectators.map((spectator, index) => (
                            <li key={index}>{spectator}</li>
                        ))}
                    </ul>
                </div>
            )}

            {gameStarted && (
                <div className="game-field">
                    <h2>Игра началась!</h2>
                    <GameComponent room={room} game={game}/>
                </div>
            )}
        </div>
    );
};

export default RoomPage;
