// models/Round.js

class Round {
    constructor(roundId, player1Id, player2Id, player1Choice, player2Choice, result) {
        this._roundId = roundId; // ID раунда
        this._player1Id = player1Id; // ID игрока 1
        this._player2Id = player2Id; // ID игрока 2
        this._player1Choice = player1Choice; // Выбор игрока 1 (камень, ножницы, бумага)
        this._player2Choice = player2Choice; // Выбор игрока 2 (камень, ножницы, бумага)
        this._result = result; // Результат раунда (например, "Игрок 1 победил", "Ничья", "Игрок 2 победил")
    }

    // Геттеры
    get roundId() {
        return this._roundId;
    }

    get player1Id() {
        return this._player1Id;
    }

    get player2Id() {
        return this._player2Id;
    }

    get player1Choice() {
        return this._player1Choice;
    }

    get player2Choice() {
        return this._player2Choice;
    }

    get result() {
        return this._result;
    }
}

export default Round;
