import {useNavigate, useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import GameComponent from "../../components/Game/GameComponent/GameComponent";
import {useUserStore} from "../../stores/userStore";
import {RoomStatus} from "../../models/Shared/roomStatus";
import './styles/RoomPage.css'
import Game from "../../models/Game";
import {GameStatus} from "../../models/Shared/gameStatus";
import {PlayerChoice} from "../../models/Shared/playerChoice";
import {getRoom, leaveRoom} from "../../services/room";

const RoomPage = () => {
    const {roomId} = useParams();
    const {user} = useUserStore();
    const player1Id = 'player-001';
    const player2Id = 'player-002';
    const [room, setRoom] = useState(null);
    const [game, setGame] = useState(new Game({
        id: "game-001",
        roomId: roomId,
        player1: player1Id,
        player2: null,
        rounds: [{player1Choice: PlayerChoice.Rock, player2Choice: PlayerChoice.Scissors, winner: player1Id}],
        status: GameStatus.Started,
        winnerId: null,
        victories1: 0,
        victories2: 0,
    }));

    const [gameStarted, setGameStarted] = useState(false);
    let navigate = useNavigate();

    useEffect(() => {

        if (room.player1 && room.player2 && room.status === RoomStatus.WaitingForPlayer) {
            room.startGame();
            setGameStarted(true);
        }
    }, [room, user]);

    const joinGameOnClick = () => {
        if (room && user) {
            if (room.status !== RoomStatus.WaitingForPlayer) {
                alert("Игра уже началась.");
            } else if (user.rating > room.maxRating) {
                alert("Ваш рейтинг слишком высок для этой комнаты.");
            } else if (room.player1 && room.player2) {
                alert("В комнате уже два игрока, невозможно присоединиться.");
            } else {


                if (room.player1 && room.player2) {
                    room.startGame();
                    setGameStarted(true);
                }
            }
        }
    };

    const leaveRoomOnClick = () => {
        if (room && user) {
            if (room.player1 === user.id || room.player2 === user.id) {
                if (!gameStarted) {
                    leaveRoom(room.roomId).then(() => navigate('/'))
                } else {
                    alert("Вы не можете выйти из комнаты, пока игра не завершена.");
                }
            } else {
                leaveRoom(room.roomId).then(() => navigate('/'))
            }
        }
    };

    useEffect(() => {
        const fetchRoomDetails = async () => {
                getRoom(roomId).then(({room, error}) => {
                    if(error) {
                        console.log(error);
                        alert(error);
                    }
                    else{
                        setRoom(room);
                    }
            });
        };
        fetchRoomDetails();
    }, []);

    // const joinRoom = () => {
    //     if (userRating >= room.minRating && userRating <= room.maxRating) {
    //         setIsJoined(true);
    //         $gameApi.post(`/rooms/${roomId}/join`, { user: 'player' });
    //     } else {
    //         alert('Ваш рейтинг не соответствует требованиям для этой комнаты');
    //     }
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
                    !gameStarted) &&
                    user
                    (
                    <button onClick={joinGameOnClick}>Присоединиться</button>
                )}
                {(
                    <button onClick={leaveRoomOnClick}>Выйти из комнаты</button>
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
