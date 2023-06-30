export class PokerTable {
  constructor(
    ante,
    smallBlind,
    bigBlind,
    maxSeats,
    players,
    cards,
    seats,
    gamestate,
    pot,
    activePlayer,
    tableCards,
    bet,
  ) {
    this.ante = ante;
    this.smallBlind = smallBlind;
    this.bigBlind = bigBlind;
    this.maxSeats = maxSeats;
    this.players = players;
    this.cards = cards;
    this.seats = seats;
    this.gamestate = gamestate;
    this.pot = pot;
    this.activePlayer = activePlayer;
    this.tableCards = tableCards;
    this.bet = bet;
  }
}
