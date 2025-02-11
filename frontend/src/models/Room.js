import {RoomStatus} from "./Shared/roomStatus";

class Room {
    constructor({
                    roomId,
                    player1 = null,
                    player2 = null,
                    maxRating = 0,
                    status = RoomStatus.WaitingForPlayer,
                    spectators = [],
                    lastActivityTime = Date.now()
                }) {
        this.roomId = roomId;
        this.player1 = player1;
        this.player2 = player2;
        this.maxRating = maxRating;
        this.status = status;
        this.spectators = spectators;
        this.lastActivityTime = lastActivityTime;
    }

    addPlayer(playerId) {
        if (this.player1 === null) {
            this.player1 = playerId;
        } else if (this.player2 === null) {
            this.player2 = playerId;
        }

        this.lastActivityTime = Date.now();
    }

    leavePlayer(playerId) {
        if (this.player1 === playerId) {
            this.player1 = null;
        } else if (this.player2 === playerId) {
            this.player2 = null;
        }
        this.lastActivityTime = Date.now();
    }

    addSpectator(spectator) {
        this.spectators.push(spectator);
        this.lastActivityTime = Date.now();
    }

    leaveSpectator(spectatorId) {
        const index = this.spectators.indexOf(spectatorId);
        if (index !== -1) {
            this.spectators.splice(index, 1);
            this.lastActivityTime = Date.now();
        }
    };

    startGame() {
        if (this.player1 && this.player2) {
            this.status = RoomStatus.WaitingForPlayer;
        } else {
            console.log("Невозможно начать игру, необходимо два игрока");
        }
    }
}


export default Room;
