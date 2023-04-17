import { navigateTo } from '../index.js';
import {
  deleteGameButtons,
  deletePlayButton,
  deletePokerButtons,
  deleteSubmitFormButton,
} from './buttons.js';
import { getCookie } from './cookieHelper.js';
import { deleteLeaderboard } from './leaderboard.js';
import { GetUser, Logout } from './services/auth.js';

async function removeRegAndLog() {
  await GetUser()
    .then((data) => {
      if (data) {
        var div = document.getElementById('login');
        div.classList.add('disabled-link');

        const username = getCookie('username');

        div.innerText = 'Welcome back ' + username;

        var div2 = document.getElementById('register');
        var button = document.createElement('button');
        button.innerText = 'Logout';
        button.addEventListener('click', async () => {
          await Logout();
          navigateTo('/');
        });
        div2.innerText = '';
        div2.appendChild(button);
      }
    })
    .catch((e) => {
      console.log(e);
    });
}

function deleteAllButtons() {
  document.getElementById('captcha').style.display = 'none';
  let capscriptdiv = document.getElementById('captchaScript');
  if (capscriptdiv.hasChildNodes) {
    capscriptdiv.innerHTML = '';
  }

  deleteGameButtons();
  deleteLeaderboard();
  deletePlayButton();
  deletePokerButtons();
  deleteSubmitFormButton();
}

export { deleteAllButtons, removeRegAndLog };
