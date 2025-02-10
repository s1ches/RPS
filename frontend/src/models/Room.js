// models/Room.js

class Room {
    constructor(roomId) {
        this.roomId = roomId;  // Идентификатор комнаты
        this.player1 = null; // Два игрока
        this.player2 = null;  // Два игрока
        this.spectators = [];  // Список зрителей
        this.rounds = [];  // История раундов
        this.status = 'Ожидает игроков';  // Статус комнаты
        this.result = '';  // Результат игры
    }

    // Установить игрока
    addPlayer(playerId) {
        if (this.player1 === null) {
            this.player1 = playerId;
        } else if (this.player2 === null) {
            this.player2 = playerId;
        }
    }

    // Добавить зрителя
    addSpectator(spectatorId) {
        if (this.spectators.length < 10) {  // Ограничение на количество зрителей
            this.spectators.push(spectatorId);
        } else {
            console.log("В комнате уже слишком много зрителей");
        }
    }

    // Добавить раунд
    addRound(roundResult) {
        this.rounds.push(roundResult);
    }

    // Изменить статус на "Игра началась"
    startGame() {
        if (this.players.player1 && this.players.player2) {
            this.status = 'Игра началась';
        } else {
            console.log("Невозможно начать игру, необходимо два игрока");
        }
    }

    // Завершить игру и задать результат
    endGame(result) {
        this.status = 'Игра завершена';
        this.result = result;
    }

    // Проверка, можно ли начать игру
    canStartGame() {
        return this.players.player1 && this.players.player2 && this.status === 'Ожидает начала игры';
    }

    // Проверка на доступность присоединения
    canJoinGame() {
        if (this.status === 'Игра началась') {
            return this.spectators.length < 10; // Можно быть зрителем, если игра началась
        } else if (this.status === 'Ожидает игроков' && (this.players.player1 === null || this.players.player2 === null)) {
            return true;  // Можно присоединиться, если есть свободные места
        }
        return false;
    }
}

export default Room;
