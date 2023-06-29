export const SUITS = ['♥', '♠', '♦', '♣'];
export const VALUES = [
  '2',
  '3',
  '4',
  '5',
  '6',
  '7',
  '8',
  '9',
  '10',
  'J',
  'K',
  'Q',
  'A',
];

export const GameStates = {
  PRE_FLOP: "pre-flop",
  FLOP: "flop",
  TURN: "turn",
  RIVER: "river",
};

export const PlayerActions = {
  FOLD: "fold",
  CALL: "call",
  RAISE: "raise",
  CHECK: "check",
};


export class Game {
  constructor(id, cards, players) {
    this.id = id;
    this.cards = cards;
    this.players = players;
  }
}

export class Card {
  constructor(suit, value) {
    this.suit = suit;
    this.value = value;
  }

  get color() {
    return this.suit === '♥' || this.suit === '♦' ? 'red' : 'black';
  }

  getCardHTML() {
    const cardDiv = document.createElement('div');
    cardDiv.classList.add('card', this.color);
    cardDiv.dataset.value = `${this.value} ${this.suit}`;
    return cardDiv;
  }
}

