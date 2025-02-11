const RoomPage = ({ userRating }) => {
    // const { roomId } = useParams(); // Получаем id комнаты из URL
    // const [room, setRoom] = useState(null);
    // const [isJoined, setIsJoined] = useState(false);
    // const [isCreator, setIsCreator] = useState(false);
    // const [gameStarted, setGameStarted] = useState(false);
    //
    // useEffect(() => {
    //     // Загружаем информацию о комнате
    //     const fetchRoomDetails = async () => {
    //         try {
    //             const response = await api.get(`/rooms/${roomId}`); //TODO переделать
    //             setRoom(response.data);
    //             setIsCreator(response.data.creator === 'player');
    //         } catch (error) {
    //             console.error('Ошибка при загрузке данных комнаты', error);
    //         }
    //     };
    //     fetchRoomDetails();
    // }, [roomId]);
    //
    // const joinRoom = () => {
    //     if (userRating >= room.minRating && userRating <= room.maxRating) {
    //         setIsJoined(true);
    //         api.post(`/rooms/${roomId}/join`, { user: 'player' }); //TODO переделать
    //     } else {
    //         alert('Ваш рейтинг не соответствует требованиям для этой комнаты');
    //     }
    // };
    //
    // const startGame = () => {
    //     // Логика запуска игры
    //     setGameStarted(true);
    //     api.post(`/rooms/${roomId}/start`); //TODO переделать
    // };
    //
    // return (
    //     <div>
    //         <h1>Комната {roomId}</h1>
    //         {room && (
    //             <>
    //                 <p>Создатель: {room.creator}</p>
    //                 <p>Минимальный рейтинг: {room.minRating}</p>
    //                 <p>Максимальный рейтинг: {room.maxRating}</p>
    //                 <p>Игроки: {room.players.join(', ')}</p>
    //
    //                 {isCreator && !gameStarted && (
    //                     <button onClick={startGame}>Начать игру</button>
    //                 )}
    //
    //                 {!isJoined && !gameStarted && (
    //                     <button onClick={joinRoom}>Присоединиться</button>
    //                 )}
    //
    //                 {gameStarted && <GameComponent roomId={roomId} />}
    //             </>
    //         )}
    //     </div>
    // );
};

export default RoomPage;
