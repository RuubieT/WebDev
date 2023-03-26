export const SUITS = ["♥","♠","♦","♣"]
export const VALUES = ["2","3","4","5","6","7","8","9","10","J","Q","K","A"]

export class Game {
    constructor(id, cards, players){
        this.id = id;
        this.cards = cards;
        this.players = players;
    }
}

class Card {
    constructor(suit, value){
        this.suit = suit;
        this.value = value;
    }

    get color(){
        return this.suit === '♥' || this.suit === '♦' ? 'red' : 'black';
    }

    getCardHTML(){
        const cardDiv = document.createElement('div');
        cardDiv.innerText = this.suit;
        cardDiv.classList.add("card", this.color)
        cardDiv.dataset.value = `${this.value} ${this.suit}`;
        return cardDiv;
    }
}