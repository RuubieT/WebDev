import { jwtToken, s } from '../index.js';
import { CreatePokerTableDto } from '../../models/Dto/PokerTable/CreatePokerTableDto.js';
import { getCookie, setCookie } from './cookieHelper.js';
import { getData, postData } from './services/apiCallTemplates.js';
import {
  Card,
  GameStates,
  PlayerActions,
  SUITS,
  VALUES,
} from '../../models/Game.js';
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
import { clearChildNodes, createPokerButtons } from './buttons.js';
import { Player } from '../../models/Player.js';

export const pokertable = new PokerTable();
let username;
let pokertableId;

async function createGame() {
  username = getCookie('username');
  const createData = new CreatePokerTableDto(username);

  let createdGame = await createPokertable(createData, jwtToken.token);

  if (createdGame) {
    s.joinTable({ id: createdGame.id, name: username });
    setCookie('pokerTableId', createdGame.id, 1);
    alert('Game created for ' + username);
  }
}

async function dealCards(hand) {
  pokertableId = getCookie('pokerTableId');
  username = getCookie('username');
  // createPlayerDivs();
  let count = 0;

  var firstcard = new Card();
  var secondcard = new Card();

  pokertable.players.forEach(async (i) => {
    //#TODO begin onderaan bij currentuser en draai kaarten recht.
    const carddiv = document.createElement('div');
    carddiv.id = 'cards ' + i.username;
    if (i.username == username) {
      if (hand) {
        firstcard = new Card(
          SUITS[hand.firstCard.mySuit],
          VALUES[hand.firstCard.myValue],
        );
        secondcard = new Card(
          SUITS[hand.secondCard.mySuit],
          VALUES[hand.secondCard.myValue],
        );
      }
      carddiv.appendChild(firstcard.getCardHTML());
      carddiv.appendChild(secondcard.getCardHTML());
    } else {
      var cardBack = document.createElement('img');
      cardBack.src = '../static/images/cardBack.png';
      cardBack.classList.add('card');
      var cardBack2 = document.createElement('img');
      cardBack2.src = '../static/images/cardBack.png';
      cardBack2.classList.add('card');
      carddiv.appendChild(cardBack);
      carddiv.appendChild(cardBack2);
    }
    i.seat = count; //?
    count++;

    var div = document.getElementById('player ' + i.username);
    div.appendChild(carddiv);
  });
  alert('Cards have been dealt.');
}

function generatecardback() {
  var cardBack = document.createElement('img');
  cardBack.src = '../static/images/cardBack.png';
  cardBack.classList.add('card');
  tableCardsDiv.appendChild(cardBack);
}

async function tableCards(cards, gamestate) {
  //Tablecards 1-3 4 5
  var tableCardsDiv = document.getElementById('tableCardsDiv');
  if (tableCardsDiv.hasChildNodes) {
    tableCardsDiv.innerHTML = '';
  }
  for (const i in cards) {
    switch (gamestate) {
      case GameStates.PRE_FLOP:
        generatecardback();

        break;
      case GameStates.FLOP:
        for (let i = 0; i < 3; i++) {
          var tablecard = new Card(
            SUITS[cards[i].mySuit],
            VALUES[cards[i].myValue],
          );

          tableCardsDiv.appendChild(tablecard.getCardHTML());
        }
        generatecardback();

        break;
      case GameStates.TURN:
        for (let i = 0; i < 4; i++) {
          var tablecard = new Card(
            SUITS[cards[i].mySuit],
            VALUES[cards[i].myValue],
          );

          tableCardsDiv.appendChild(tablecard.getCardHTML());
        }
        generatecardback();
        break;
      case GameStates.RIVER:
        for (let i = 0; i < 5; i++) {
          var tablecard = new Card(
            SUITS[cards[i].mySuit],
            VALUES[cards[i].myValue],
          );

          tableCardsDiv.appendChild(tablecard.getCardHTML());
        }
        generatecardback();
        break;
    }
  }
}

//main function //startpokertable is solo <-- delete playerhands
async function assignPokertable() {
  pokertableId = getCookie('pokerTableId');
  if (!pokertableId) {
    await createGame();
    pokertableId = getCookie('pokerTableId');
  }

  let data = await findPokertable(pokertableId, jwtToken.token);
  if (data) {
    let data = await getPokertablePlayers(pokertableId, jwtToken.token);
    pokertable.players = await getPokertablePlayers(
      pokertableId,
      jwtToken.token,
    );
    // pokertable.players.forEach((player, i) => {
    //   pokertable.players[i] = new Player(
    //     player.username,
    //     player.chips,
    //     undefined,
    //     pokertableId,
    //     undefined,
    //   );
    // });
    pokertable.ante = data.ante;
    pokertable.smallBlind = data.smallBlind;
    pokertable.bigBlind = data.bigBlind;
    pokertable.maxSeats = data.maxSeats;
    pokertable.pot = 0;
    pokertable.gamestate = GameStates.PRE_FLOP;
    pokertable.activePlayer = 0;
    createPlayerDivs();
    createPokerButtons();
  }
}

function createTableDivs(){
    var tablenameDiv = document.getElementById('tablename');
    clearChildNodes(tablenameDiv);
    tablenameDiv.innerText = "Pokertable: " + getCookie('pokerTableId');

    var joincodeDiv = document.getElementById('joincode');
    joincodeDiv.innerText = "Invite your friends by sending them this code: " + getCookie('pokerTableId');



}

function createPlayerDivs() {
  var playersDiv = document.getElementById('players');
  clearChildNodes(playersDiv);
  let count = 0;

  pokertable.players.forEach(async (i) => {
    const div = document.createElement('div');
    div.id = 'player ' + i.username;
    div.classList.add('player', 'player-' + count);

    const playername = document.createElement('div');
    playername.innerText = i.username;
    playername.classList.add('name');

    const carddiv = document.createElement('div');
    carddiv.id = 'cards ' + i.username;

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

  createTableDivs();
}

async function joinGame() {
  var input = document.getElementById('idpokertable').value;
  if (input) {
    var joindata = new JoinPokertable();
    joindata.pokertableId = input;
    joindata.username = getCookie('username');
    let data = await joinPokertable(joindata, jwtToken.token);
    if (data) {
      s.joinTable({ id: joindata.pokertableId, name: joindata.username });
      setCookie('pokerTableId', data.pokerTableId, 1);
      alert('Game ' + data.pokerTableId + ' joined');
    }
  }
}

async function playerAction(action) {
  var player = pokertable.players[pokertable.activePlayer];

  switch (action) {
    case PlayerActions.FOLD:
      player.action = player.FOLD;
      break;
    case PlayerActions.CHECK:
      player.action = player.CHECK;
      break;
    case PlayerActions.RAISE:
      player.action = player.FOLD;
      player.chips -= action.value;
      pokertable.bet = action.value;
      pokertable.pot += action.value;
      break;
    case PlayerActions.CALL:
      player.action = player.CALL;
      player.chips -= action.value;
      pokertable.pot += action.value;
      break;
  }
  pokertable.activePlayer =
    (pokertable.activePlayer + 1) % pokertable.players.length;

  let actionsLeft = 0;
  var allPlayersActed = false;
  pokertable.players.forEach((player) => {
    if (player.action == null) {
      actionsLeft++;
    }
  });
  if (actionsLeft == 0) {
    allPlayersActed = true;
  }

  if (allPlayersActed) {
    switch (pokertable.gamestate) {
      case GameStates.PRE_FLOP:
        currentState = GameStates.FLOP;
        break;
      case GameStates.FLOP:
        currentState = GameStates.TURN;
        break;
      case GameStates.TURN:
        currentState = GameStates.RIVER;
        break;
      case GameStates.RIVER:
        // Determine the winner and distribute the pot
        break;
    }
    pokertable.players.forEach((player) => {
      player.action = null;
    });
    pokertable.bet = 0;
  }
}

export {
  createGame,
  dealCards,
  joinGame,
  assignPokertable,
  tableCards,
  createPlayerDivs,
  playerAction,
};
