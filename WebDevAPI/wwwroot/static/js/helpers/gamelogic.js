import { jwtToken } from '../index.js';
import { CreatePokerTableDto } from '../../models/Dto/PokerTable/CreatePokerTableDto.js';
import { getCookie, setCookie } from './cookieHelper.js';
import { getData, postData } from './services/apiCallTemplates.js';
import { Card, SUITS, VALUES } from '../../models/Game.js';
import { PlayerHand } from '../../models/PlayerHand.js';
import {
  createPokertable,
  startPokertable,
  getPokertablePlayers,
  getPlayerhand,
  findPokertable,
  joinPokertable,
} from './services/pokertable.js';
import { PokerTable } from '../../models/PokerTable.js';
import { JoinPokertable } from '../../models/Dto/PokerTable/JoinPokertable.js';

async function createGame() {
  const username = getCookie('username');
  const createData = new CreatePokerTableDto(username);

  let createdGame = await createPokertable(createData);

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

//main function //startpokertable is solo <-- delete playerhands
async function assignPokertable() {
  var table = getCookie('pokerTableId');
  if (!table) {
    table = await createGame();
  }

  let data = await findPokertable(table);
  if (data) {
    let pokertable = new PokerTable();
    pokertable.players = await getPokertablePlayers(table);
    pokertable.ante = data.ante;
    pokertable.smallBlind = data.smallBlind;
    pokertable.bigBlind = data.bigBlind;
    pokertable.maxSeats = data.maxSeats;
    pokertable.cards = await getData(`api/test/tablecards`);

    await startPokertable(table);

    var playersDiv = document.getElementById('players');
    if (playersDiv.hasChildNodes) {
      playersDiv.innerHTML = '';
    }
    let count = 0;
    var firstcard = new Card();
    var secondcard = new Card();
    pokertable.players.forEach(async (i) => {
      let hand = await getPlayerhand(i.username);
      if (hand) {
        firstcard = new Card(
          SUITS[hand.firstCard.suit],
          VALUES[hand.firstCard.value],
        );
        secondcard = new Card(
          SUITS[hand.secondCard.suit],
          VALUES[hand.secondCard.value],
        );
      }
      const div = document.createElement('div');
      div.id = 'player ' + i.username;
      div.classList.add('player', 'player-' + count);

      const playername = document.createElement('div');
      playername.innerText = i.username;
      playername.classList.add('name');

      const carddiv = document.createElement('div');
      carddiv.id = 'cards ' + i.username;
      carddiv.appendChild(firstcard.getCardHTML());
      carddiv.appendChild(secondcard.getCardHTML());

      const chipsDiv = document.createElement('div');
      chipsDiv.classList.add('chips');
      const chipsCount = document.createElement('div');
      chipsCount.classList.add('chips-value');
      chipsCount.innerText = i.chips;

      chipsDiv.appendChild(chipsCount);
      div.appendChild(chipsDiv);
      div.appendChild(carddiv);
      div.appendChild(playername);
      playersDiv.appendChild(div);
      count++;
    });
  }
}

async function getHand(username) {}

//TODO
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

async function joinGame() {
  var input = document.getElementById('idpokertable').value;
  if (input) {
    var joindata = new JoinPokertable();
    joindata.pokertableId = input;
    joindata.username = getCookie('username');
    let data = await joinPokertable(joindata, jwtToken.token);
    if (data) {
      setCookie('pokerTableId', data.pokerTableId, 1);
      alert('Game ' + data.pokerTableId + 'joined');
    }
  }
}

export {
  createGame,
  startGame,
  joinGame,
  getHand,
  getTableCards,
  assignPokertable,
};
