import { Player } from '../../models/Player.js';
import { UserLoginDto } from '../../models/Dto/Auth/UserLoginDto.js';
import { PlayerRegisterDto } from '../../models/Dto/Auth/PlayerRegisterDto.js';
import { currentUser, jwtToken, navigateTo } from '../index.js';
import { setCookie } from './cookieHelper.js';
import { GetUser, Login, Register } from './services/auth.js';
import { FindUser } from './services/player.js';

function checkInput() {
  const inputFields = document.querySelectorAll('input');
  return Array.from(inputFields).filter((input) => input.value !== '');
}

const loginVerify = async () => {
  var inputs = checkInput();
  const user = new UserLoginDto();
  inputs.forEach((input) => {
    switch (input.id) {
      case 'email':
        user.email = input.value;
        break;
      case 'password':
        user.password = input.value;
        break;
    }
  });

  await Login(user);
  CurrentUser = await GetUser();

  if (CurrentUser) {
    setCookie('username', CurrentUser.username, 1);
    alert('Logged in');
    navigateTo('/');
  } else alert('User not found');
};

const registerVerify = async () => {
  var inputs = checkInput();
  const newUser = new PlayerRegisterDto();
  inputs.forEach((input) => {
    switch (input.id) {
      case 'firstname':
        newUser.firstname = input.value;
        break;
      case 'lastname':
        newUser.lastname = input.value;
        break;
      case 'username':
        newUser.username = input.value;
        break;
      case 'email':
        newUser.email = input.value;
        break;
      case 'password':
        newUser.password = input.value;
        break;
    }
  });

  await Register(newUser);
  let data = await GetUser();

  if (data) {
    currentUser.email = data.email;

    setCookie('username', data.username, 1);
    alert('Registered succesfully');
    navigateTo('/');
  } else alert('User not found');
};

function removeEventListeners() {
  window.removeEventListener('submit', loginVerify);
  window.removeEventListener('submit', registerVerify);
}

export { loginVerify, registerVerify, checkInput, removeEventListeners };
