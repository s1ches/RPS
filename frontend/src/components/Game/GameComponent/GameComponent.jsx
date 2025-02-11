import {useState} from "react";
import {useUserStore} from "../../../stores/userStore";
import {PlayerChoice} from "../../../models/Shared/playerChoice";
import {RoomStatus} from "../../../models/Shared/roomStatus";

const GameComponent = ({room, game}) => {
    const [playerChoice, setPlayerChoice] = useState(null);
    const [roundResult, setRoundResult] = useState(null);
    const [loading, setLoading] = useState(false);
    const {user} = useUserStore();
    // const [gameData, setGameData] = useState(null);

    //
    // useEffect(() => {
    //     const fetchGameData = async () => {
    //         try {
    //             const response = await api.get(`/rooms/${roomId}`);
    //             setGameData(response.data);
    //         } catch (error) {
    //             console.error('Ошибка при загрузке данных игры', error);
    //         }
    //     };
    //     fetchGameData();
    // }, [roomId]);
    //
    // const handleRoundEnd = (winner) => {
    //     // Обновляем результат раунда
    //     setRoundResult(winner);
    //     // Присваиваем рейтинги
    //     api.post(`/games/${roomId}/end-round`, { winner });
    // };
    //
    // useEffect(() => {
    //     if (roundResult) {
    //         setTimeout(() => {
    //             setRoundResult(null); // Ожидание перед новым раундом
    //         }, 3000); // 3 секунды на перерыв
    //     }
    // }, [roundResult]);
    //

    const handleJoinGame = () => {
        // Логика для присоединения к игре
        if (game._player1 === null || game._player2 === null) {
            // setIsJoined(true);
            // Пример: обновление состояния игры (например, через API)
            // game.updatePlayer2(user.id);  // Псевдокод для обновления второго игрока
            alert("Ты теперь игрок")
        } else {
            alert("Комната уже полностью заполнена!");
        }
    };

    const handleChoice = (choice) => {
        if (loading) return;

        setLoading(true);
        setPlayerChoice(choice);

        setTimeout(() => {
            const winnerId = determineWinner(choice, game._player2Choice);
            game.updateScore(winnerId);
            setRoundResult(winnerId);
            setLoading(false);
        }, 1000);
    };

    // Функция для определения победителя
    const determineWinner = (player1Choice, player2Choice) => {
        if (player1Choice === player2Choice) return null; // Ничья
        if (
            (player1Choice === PlayerChoice.Rock && player2Choice === PlayerChoice.Scissors) ||
            (player1Choice === PlayerChoice.Scissors && player2Choice === PlayerChoice.Paper) ||
            (player1Choice === PlayerChoice.Paper && player2Choice === PlayerChoice.Rock)
        ) {
            return game._player1;
        }
        return game._player2;
    };

    if (!game) {
        return <div className="game-component">Игра не найдена</div>;
    }

    return (
        <div className="game-container">
            <div className="game-info">
                <h2>Комната {game._roomId}</h2>
                <p><strong>Игрок 1:</strong> {game._player1 || "Ожидание игрока..."}</p>
                <p><strong>Игрок 2:</strong> {game._player2 || "Ожидание игрока..."}</p>
                <p><strong>Статус:</strong> {game._status}</p>
            </div>

            <div className="game-board">
                <h3>Текущий раунд</h3>
                <p><strong>Выбор игроков:</strong> {game._player1Choice} - {game._player2Choice}</p>

                {(game._player1 === user.id || game._player2 === user.id) &&
                    <>
                        <div className="choices">
                            <button onClick={() => handleChoice('Rock')} disabled={loading}>Камень</button>
                            <button onClick={() => handleChoice('Paper')} disabled={loading}>Бумага</button>
                            <button onClick={() => handleChoice('Scissors')} disabled={loading}>Ножницы</button>
                        </div>
                        <p className="waiting-text">
                            {loading ? 'Ожидайте...' : 'Выберите ваш ход'}
                        </p>
                    </>
                }

                {roundResult && (
                    <p><strong>Результат:</strong> <span className="game-result">Победа {roundResult}</span></p>
                )}


            </div>

            <div className="game-score">
                <h4>Счёт</h4>
                <p>{game._player1}: {game._victories1}</p>
                <p>{game._player2}: {game._victories2}</p>
            </div>
            {room.status === RoomStatus.WaitingForPlayer && (game._player1 === null || game._player2)
                &&
                <div className="join-game">
                    <button onClick={handleJoinGame} className="join-button">Присоединиться к игре</button>
                </div>}
        </div>
    );
};

export default GameComponent;
