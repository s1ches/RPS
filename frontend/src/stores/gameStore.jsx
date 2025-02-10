// stores/GameStore.js
import { createContext, useContext, useState } from 'react';
import Game from '../models/Game';
import { useUserStore } from './userStore';

// Создаем контекст для управления состоянием игры
const GameContext = createContext();

// Провайдер контекста для управления состоянием игры
export const GameProvider = ({ children }) => {
    const [games, setGames] = useState([]);
    const { user, updateUserRating } = useUserStore();

    // Создание новой игры
    const createGame = (player1) => {
        const newGame = new Game(Date.now(), player1.id, null); // Начинаем игру с первым игроком
        setGames((prevGames) => [...prevGames, newGame]);
    };

    // Присоединение второго игрока
    const joinGame = (gameId, player2) => {
        const game = games.find((game) => game.id === gameId);
        if (game && game.status === 'Не начата') {
            game._player2 = player2.id; // Присоединяем второго игрока
            game.startGame(); // Начинаем игру
            setGames([...games]);
        }
    };

    // Добавление раунда в игру
    const addRound = (gameId, roundResult) => {
        const game = games.find((game) => game.id === gameId);
        if (game && game.status === 'Начата') {
            game.addRound(roundResult); // Добавляем результат раунда
            setGames([...games]);
        }
    };

    // Завершение игры и обновление рейтинга игроков
    const endGame = (gameId, winnerId) => {
        const game = games.find((game) => game.id === gameId);
        if (game && game.status === 'Начата') {
            let result;
            if (winnerId === game.player1) {
                result = `Победа игрока 1 (${game.player1})`;
                updateUserRating(user.rating + 3); // Победитель
            } else if (winnerId === game.player2) {
                result = `Победа игрока 2 (${game.player2})`;
                updateUserRating(user.rating + 3); // Победитель
            } else {
                result = 'Ничья';
            }
            game.endGame(result); // Завершаем игру
            setGames([...games]);
        }
    };

    return (
        <GameContext.Provider value={{ games, createGame, joinGame, addRound, endGame }}>
            {children}
        </GameContext.Provider>
    );
};

// Хук для использования GameStore
export const useGameStore = () => useContext(GameContext);
