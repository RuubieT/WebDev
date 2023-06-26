import { navigateTo } from '../index.js';
import {
  createCustomButtons,
  deleteGameButtons,
  deletePlayButton,
  deletePokerButtons,
  deleteSubmitFormButton,
} from './buttons.js';
import { deleteAllCookies, getCookie } from './cookieHelper.js';
import { deleteLeaderboard } from './leaderboard.js';
import { GetUser, Logout } from './services/auth.js';

async function removeRegAndLog() {
  await GetUser().then((data) => {
    if (data && getCookie('username') != null) {
      console.log(data);
      var div = document.getElementById('login');
      div.classList.add('disabled-link');

      div.innerText = 'Welcome back ' + data.player.userName;

      var div2 = document.getElementById('register');
      div2.href = ""; 
      const logoutButton = createCustomButtons('logoutButton', 'Logout');
      logoutButton.addEventListener('click', async () => {
        navigateTo('/');
        await Logout();
        deleteAllCookies();
      });
      div2.innerText = '';
      div2.appendChild(logoutButton);


      if (data.role == 'Moderator'){
        const moderatorButton = createCustomButtons('moderatorButton', 'Manage users');
        moderatorButton.addEventListener('click', async () => {
        navigateTo('/register'); 
        });
        
        div2.appendChild(moderatorButton);
      }

      if (data.role == 'Admin'){
        const adminButton = createCustomButtons('adminButton', 'Manage roles');
        adminButton.addEventListener('click', async () => {
        navigateTo('/admin'); 
        });
        div2.appendChild(adminButton);
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
  deletePlayButton();
  deletePokerButtons();
  deleteSubmitFormButton();
}

export { deleteAllButtons, removeRegAndLog };
