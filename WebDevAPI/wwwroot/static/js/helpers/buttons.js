import { jwtToken, navigateTo, s } from '../index.js';
import { Contactform } from '../../models/Contactform.js';
import { createGame, getHand, joinGame, startGame } from './gamelogic.js';
import { createContactform } from './services/contactform.js';
import { joinPokertable } from './services/pokertable.js';
import { test, test2 } from './services/player.js';
import { loginVerify } from './verifyForm.js';
import { GetUser } from './services/auth.js';
import { getCookie } from './cookieHelper.js';

const appDiv = document.getElementById('app');

function createSubmitFormButton() {
  var submitFormButton = document.getElementById('contactFormButtonDiv');
  if (!submitFormButton) {
    const div = document.createElement('div');
    div.id = 'contactFormButtonDiv';

    const btn = createCustomButtons('submitFormButton', 'Submit!');
    btn.addEventListener('click', async () => {
      const inputFields = document.querySelectorAll('input');
      const validInputs = Array.from(inputFields).filter(
        (input) => input.value !== '',
      );
      const contactForm = new Contactform();
      validInputs.forEach((input) => {
        switch (input.id) {
          case 'name':
            contactForm.name = input.value;
            break;
          case 'email':
            contactForm.email = input.value;
            break;
          case 'subject':
            contactForm.subject = input.value;
            break;
          case 'description':
            contactForm.description = input.value;
            break;
        }
      });

      let response = await createContactform(contactForm);

      let data = await response.json();
      alert(JSON.stringify(data));
    });
    btn.disabled = true;
    div.appendChild(btn);
    return div;
  }
}

function deleteSubmitFormButton() {
  deleteCustomButtons('contactFormButtonDiv');
}

function createPlayButton() {
  var playButton = document.getElementById('playButtonDiv');
  if (!playButton) {
    const div = document.createElement('div');
    div.id = 'playButtonDiv';

    const btn = createCustomButtons('playButton', 'Play Game');
    btn.addEventListener('click', async () => {
      s._connection.send('SendMessage', 'game');
      s._connection.on('ReceiveMessage', (value) => {
        console.log(value);
      });

      navigateTo('/game');
    });

    div.appendChild(btn);

    document.body.appendChild(div);
    console.log(appDiv);
  }
}

function deletePlayButton() {
  deleteCustomButtons('playButtonDiv');
}

function createGameButtons() {
  var playButtons = document.getElementById('gameButtons');
  if (!playButtons) {
    const div = document.createElement('div');
    div.id = 'gameButtons';

    const createbutton = createCustomButtons('createButton', 'Create');
    createbutton.classList.add('game');

    createbutton.addEventListener('click', async () => {
      //Setup game
    });

    const joinbutton = createCustomButtons('joinButton', 'Join');
    joinbutton.classList.add('game');

    joinbutton.addEventListener('click', async () => {
      await joinGame();
    });

    const startbutton = createCustomButtons('startButton', 'Start');
    startbutton.classList.add('game');

    startbutton.addEventListener('click', async () => {
      navigateTo('/table');
    });

    var x = document.createElement('input');
    x.setAttribute('type', 'text');
    x.id = 'idpokertable';

    div.appendChild(createbutton);
    div.appendChild(x);
    div.appendChild(joinbutton);
    div.appendChild(startbutton);
    document.body.appendChild(div);
  }
}

function deleteGameButtons() {
  deleteCustomButtons('gameButtons');
}

async function createPokerButtons() {
  var pokerButtons = document.getElementById('pokerButtons');
  if (!pokerButtons) {
    const div = document.createElement('div');
    div.id = 'pokerButtons';

    const checkButton = createCustomButtons('checkButton', 'Check');
    checkButton.addEventListener('click', () => {
      s._connection.send('SendMessage', `CHECK`);
      s._connection.on('ReceiveMessage', (value) => {
        console.log(value);
      });
    });

    const foldButton = createCustomButtons('foldButton', 'Fold');
    foldButton.addEventListener('click', async () => {
      s._connection.send('SendMessage', 'FOLD');
      s._connection.on('ReceiveMessage', (value) => {
        console.log(value);
      });
    });

    const callButton = createCustomButtons('callButton', 'Call');
    callButton.addEventListener('click', () => {
      s._connection.send('SendMessage', 'Call');
      s._connection.on('ReceiveMessage', (value) => {
        console.log(value);
      });
    });

    const betButton = createCustomButtons('betButton', 'Bet');
    betButton.addEventListener('click', () => {
      s._connection.send('SendMessage', 'BET + {VALUE}');
      s._connection.on('ReceiveMessage', (value) => {
        console.log(value);
      });
    });

    div.appendChild(checkButton);
    div.appendChild(foldButton);
    div.appendChild(callButton);
    div.appendChild(betButton);
    document.body.appendChild(div);
  }
}

function deletePokerButtons() {
  deleteCustomButtons('pokerButtons');
}

function createCustomButtons(name, text) {
  const button = document.createElement('button');
  button.id = name;
  button.innerText = text;

  return button;
}

function deleteCustomButtons(name) {
  var buttons = document.getElementById(name);
  if (buttons) {
    document.body.removeChild(buttons);
  }
}

export {
  createSubmitFormButton,
  deleteSubmitFormButton,
  createPlayButton,
  deletePlayButton,
  createGameButtons,
  deleteGameButtons,
  createPokerButtons,
  deletePokerButtons,
};
