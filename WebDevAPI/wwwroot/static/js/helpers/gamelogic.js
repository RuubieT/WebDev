import { jwtToken } from '../index.js';
import { CreatePokerTableDto } from '../../models/Dto/PokerTable/CreatePokerTableDto.js';
import { getCookie, setCookie } from './cookieHelper.js';
import { getData, postData } from './services/apiCallTemplates.js';
import { Card, SUITS, VALUES } from '../../models/Game.js';
import { PlayerHand } from '../../models/PlayerHand.js';
import {
  createPokerTable,
  startPokertable,
  getPokertablePlayers,
  getPlayerhand,
} from './services/pokertable.js';

async function createGame() {
  const username = getCookie('username');
  const createData = new CreatePokerTableDto(username);

  let createdGame = await createPokerTable(createData);

  if (createdGame) {
    setCookie('pokerTableId', createdGame.id, 1);
    alert('Game created for ' + username);
  }
}

async function startGame() {
  const table = getCookie('pokerTableId');

  let startedGame = await startPokertable(table);
  if (startedGame) {
    alert('Cards have been dealt.');
  }
}

async function getPlayers() {
  const table = getCookie('pokerTableId');

  let players = await getPokertablePlayers(table);
  if (players) {
    console.log(players);
  }
}

async function getHand() {
  const username = getCookie('username');
  let hand = await getPlayerhand(username);
  if (hand) {
    var firstCard = new Card(
      SUITS[hand.firstCard.suit],
      CARD_VALUE_MAP[VALUES[hand.firstCard.value]],
    );
    var secondCard = new Card(
      SUITS[hand.secondCard.suit],
      CARD_VALUE_MAP[VALUES[hand.secondCard.value]],
    );
    var playerhand = new PlayerHand(firstCard, secondCard);

    var cardsDiv = document.getElementById('cards');
    var handDiv = document.getElementById('cards ' + username);
    if (!handDiv) {
      const div = document.createElement('div');
      div.id = 'cards ' + username;
      div.appendChild(firstCard.getCardHTML());
      div.appendChild(secondCard.getCardHTML());
      cardsDiv.appendChild(div);
    }

    return playerhand;
  }
}

async function getTableCards() {
  let cards = await getData(`api/test/tablecards`);
  if (cards) {
    var tableCardsDiv = document.getElementById('tableCardsDiv');
    var tablecards = document.getElementById('tablecards');
    if (!tablecards) {
      const div = document.createElement('div');
      div.id = 'tablecards';
      for (const i in cards) {
        var tablecard = new Card(
          SUITS[cards[i].mySuit],
          CARD_VALUE_MAP[VALUES[cards[i].myValue]],
        );
        div.appendChild(tablecard.getCardHTML());
      }
      tableCardsDiv.appendChild(div);
    }
  }
}

const CARD_VALUE_MAP = {
  2: 2,
  3: 3,
  4: 4,
  5: 5,
  6: 6,
  7: 7,
  8: 8,
  9: 9,
  10: 10,
  J: 11,
  Q: 12,
  K: 13,
  A: 14,
};

export { createGame, startGame, getPlayers, getHand, getTableCards };
