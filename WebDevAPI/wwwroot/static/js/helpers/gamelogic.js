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
    var playersDiv = document.getElementById('players');
    if (playersDiv.hasChildNodes) {
      playersDiv.innerHTML = '';
    }
    for (const p in players) {
      const div = document.createElement('div');
      div.id = 'player ' + players[p].username;
      div.classList.add('player', 'player-' + p);

      const playername = document.createElement('div');
      playername.innerText = players[p].username;
      playername.classList.add('name');

      let playerHand = await getHand(players[p].username);
      const carddiv = document.createElement('div');
      carddiv.id = 'cards ' + players[p].username;
      carddiv.appendChild(playerHand.firstCard.getCardHTML());
      carddiv.appendChild(playerHand.secondCard.getCardHTML());

      const chipsDiv = document.createElement('div');
      chipsDiv.classList.add('chips');
      const chipsCount = document.createElement('div');
      chipsCount.classList.add('chips-value');
      chipsCount.innerText = players[p].chips;

      chipsDiv.appendChild(chipsCount);
      div.appendChild(chipsDiv);
      div.appendChild(carddiv);
      div.appendChild(playername);
      playersDiv.appendChild(div);
    }
  }
}

async function getHand(username) {
  let hand = await getPlayerhand(username);
  if (hand) {
    var firstCard = new Card(
      SUITS[hand.firstCard.suit],
      VALUES[hand.firstCard.value],
    );
    var secondCard = new Card(
      SUITS[hand.secondCard.suit],
      VALUES[hand.secondCard.value],
    );
    var playerhand = new PlayerHand(firstCard, secondCard);

    return playerhand;
  }
}

async function getTableCards() {
  let cards = await getData(`api/test/tablecards`);
  if (cards) {
    var tableCardsDiv = document.getElementById('tableCardsDiv');
    if (tableCardsDiv.hasChildNodes) {
      tableCardsDiv.innerHTML = '';
    }
    for (const i in cards) {
      var tablecard = new Card(
        SUITS[cards[i].mySuit],
        VALUES[cards[i].myValue],
      );
      tableCardsDiv.appendChild(tablecard.getCardHTML());
    }
  }
}

export { createGame, startGame, getPlayers, getHand, getTableCards };
