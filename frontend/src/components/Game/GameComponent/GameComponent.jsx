
const GameComponent = ({ roomId }) => {
    // const [gameData, setGameData] = useState(null);
    // const [roundResult, setRoundResult] = useState(null);
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
    // return (
    //     <div>
    //         <h2>Текущий раунд</h2>
    //         {gameData && (
    //             <>
    //                 <p>Игроки: {gameData.players.join(' vs ')}</p>
    //                 <p>Выбор игроков: {gameData.choices.join(' - ')}</p>
    //                 <p>Результат раунда: {roundResult ? <span style={{ color: 'green' }}>{roundResult}</span> : 'Игра в процессе'}</p>
    //                 {roundResult && <p>Ожидайте нового раунда...</p>}
    //                 <button onClick={() => handleRoundEnd('player1')}>Победа игрока 1</button>
    //                 <button onClick={() => handleRoundEnd('player2')}>Победа игрока 2</button>
    //             </>
    //         )}
    //     </div>
    // );
};

export default GameComponent;
