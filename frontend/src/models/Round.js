import {GameStatus} from "./Shared/gameStatus";
import {PlayerChoice} from "./Shared/playerChoice";

class Round {
    constructor(roundId, gameId, player1Id, player2Id, player1Choice, player2Choice, winnerId) {
        this._roundId = roundId;
        this._gameId = gameId
        this._status = GameStatus.Started;
        this._player1Id = player1Id;
        this._player2Id = player2Id;
        this._player1Choice = player1Choice;
        this._player2Choice = player2Choice;
        this._winnerId = winnerId;
    }

    determineWinner() {
        if (this._player1Choice === this._player2Choice) {
            this._winnerId = null;
        } else if (
            (this._player1Choice === PlayerChoice.Rock && this._player2Choice === PlayerChoice.Scissors) ||
            (this._player1Choice === PlayerChoice.Scissors && this._player2Choice === PlayerChoice.Paper) ||
            (this._player1Choice === PlayerChoice.Paper && this._player2Choice === PlayerChoice.Rock)
        ) {
            this._winnerId = this._player1Id;
        } else {
            this._winnerId = this._player2Id;
        }

        this._status = GameStatus.Ended;
    }
}

export default Round;
