// models/Game.js

class Game {
    constructor(id, roomId, player1, player2) {
        this._id = id;
        this._roomId = roomId;
        this._player1 = player1; // ID игрока 1
        this._player2 = player2; // ID игрока 2
        this._rounds = []; // Список раундов
        this._status = 'Не начата'; // Статус игры (например, "Не начата", "Начата", "Закончена")
        this._result = ''; // Результат игры (например, "Победа игрока 1", "Победа игрока 2", "Ничья")
    }

    // Геттеры
    get id() {
        return this._id;
    }

    get roomId() {
        return this._roomId;
    }

    get player1() {
        return this._player1;
    }

    get player2() {
        return this._player2;
    }

    get rounds() {
        return this._rounds;
    }

    get status() {
        return this._status;
    }

    get result() {
        return this._result;
    }

    // Добавление раунда
    addRound(roundResult) {
        this._rounds.push(roundResult);
    }

    // Изменение статуса игры
    startGame() {
        this._status = 'Начата';
    }

    endGame(result) {
        this._status = 'Закончена';
        this._result = result;
    }

    // Проверка, завершена ли игра
    isGameOver() {
        return this._status === 'Закончена';
    }
}

export default Game;
