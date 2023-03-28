export class PokerTable {
    constructor(ante, smallBlind, bigBlind, maxSeats, players){
        this.ante = ante;
        this.smallBlind = smallBlind;
        this.bigBlind = bigBlind;
        this.maxSeats = maxSeats;
        this.players = players;
    }
}