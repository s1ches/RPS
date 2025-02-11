import {GameStatus} from "./Shared/gameStatus";

class Game {
    constructor({
                    gameId,
                    roomId,
                    player1,
                    player2,
                    rounds = [],
                    status = GameStatus.Started,
                    winnerId = null,
                    victories1 = 0,
                    victories2 = 0,
                }) {
        this._gameId = gameId
        this._roomId = roomId;
        this._player1 = player1;
        this._player2 = player2;
        this._rounds = rounds;
        this._status = status;
        this._winnerId = winnerId;
        this._victories1 = victories1;
        this._victories2 = victories2;
    }

    addRound(roundResult) {
        this._rounds.push(roundResult);
    }

    updateScore(winnerId) {
        if (winnerId === this._player1) {
            this._victories1 += 1;
        } else if (winnerId === this._player2) {
            this._victories2 += 1;
        }
    }

    endGame() {
        this._status = GameStatus.Ended;

        if (this._victories1 > this._victories2) {
            this._winnerId = this._player1;
        } else if (this._victories1 < this._victories2) {
            this._winnerId = this._player2;
        } else {
            this._winnerId = null;
        }
    }
}


export default Game;
