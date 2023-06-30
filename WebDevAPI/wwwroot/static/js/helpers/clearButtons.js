import { jwtToken, navigateTo } from '../index.js';
import {
  createCustomButtons,
  deleteGameButtons,
  deletePlayButton,
  deletePokerButtons,
  deleteSubmitFormButton,
} from './buttons.js';
import { deleteAllCookies, getCookie, setCookie } from './cookieHelper.js';
import { deleteLeaderboard } from './leaderboard.js';
import { deleteRoleList, deleteUserList } from './management.js';
import { GetUser, Logout } from './services/auth.js';

async function removeRegAndLog() {
  await GetUser().then((data) => {
    if (data != undefined) {
      if (getCookie('username') == null) {
        setCookie('username', data.player.userName, 1);
        if (data.playerInformation.pokerTableId) {
          setCookie('pokerTableId', data.playerInformation.pokerTableId, 1);
        }
        if (data.refreshedToken) {
          jwtToken.token = data.refreshedToken;
        }
      }
      // && getCookie('username') != null
      var div = document.getElementById('login');
      div.classList.add('disabled-link');

      div.innerText = 'Welcome back ' + data.player.userName;

      var div2 = document.getElementById('register');
      div2.href = '';
      const logoutButton = createCustomButtons('logoutButton', 'Logout');
      logoutButton.addEventListener('click', async () => {
        deleteAllCookies();
        await Logout(jwtToken.token);

        navigateTo('/');
      });
      div2.innerText = '';
      div2.appendChild(logoutButton);

      if (data.role == 'Moderator') {
        var modButton = document.getElementById('modPanel');
        modButton.removeAttribute('hidden');
      }

      if (data.role == 'Admin') {
        var adminButton = document.getElementById('adminPanel');
        adminButton.removeAttribute('hidden');
      }
    }
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
  deleteRoleList();
  deleteUserList();
  deletePlayButton();
  deletePokerButtons();
  deleteSubmitFormButton();
}

export { deleteAllButtons, removeRegAndLog };
