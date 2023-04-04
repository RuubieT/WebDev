export class PokerTable {
  constructor(ante, smallBlind, bigBlind, maxSeats, players, cards, seats) {
    this.ante = ante;
    this.smallBlind = smallBlind;
    this.bigBlind = bigBlind;
    this.maxSeats = maxSeats;
    this.players = players;
    this.cards = cards;
    this.seats = seats;
  }
}
