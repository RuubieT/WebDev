import { Player } from '../../models/Player.js';
import { UserLoginDto } from '../../models/Dto/Auth/UserLoginDto.js';
import { PlayerRegisterDto } from '../../models/Dto/Auth/PlayerRegisterDto.js';
import { navigateTo } from '../index.js';
import { getCookie, setCookie } from './cookieHelper.js';
import {
  ChangePw,
  ForgotPw,
  GetUser,
  Login,
  Register,
} from './services/auth.js';

let CurrentUser;

function inputsAreNotNull() {
  const inputFields = document.querySelectorAll('input');
  return Array.from(inputFields).filter((input) => input.value !== '');
}

const loginVerify = async () => {
  var inputs = inputsAreNotNull();
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
  var inputs = inputsAreNotNull();

  var strength = document.getElementById('strength');
  if (strength.innerText != 'Strong!') {
    return;
  }

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
  CurrentUser = await GetUser();

  if (CurrentUser) {
    setCookie('username', CurrentUser.username, 1);
    alert('Registered succesfully');
    navigateTo('/');
  } else alert('User not found');
};

const forgotPassword = async () => {
  var inputs = inputsAreNotNull();
  let data = await ForgotPw({ email: inputs[0].value });

  if (data) {
    console.log(data);
    setCookie('email', inputs[0].value, 1);
    setCookie('forgotpwtoken', data.token, 1);
    navigateTo('/changepw');
  } else alert('User not found');
};

const changePassword = async () => {
  var inputs = inputsAreNotNull();
  let newPassword = {};
  inputs.forEach((input) => {
    switch (input.id) {
      case 'token':
        newPassword.token = input.value;
        break;
      case 'password':
        newPassword.password = input.value;
        break;
      case 'password2':
        newPassword.password2 = input.value;
        break;
    }
  });
  if (newPassword.password != newPassword.password2) {
    let validate = document.getElementById('validate');
    validate.innerHTML =
      '<span style="color:red">Passwords are not the same!</span>';
  } else {
    let email = getCookie('email');

    let data = await ChangePw({
      email: email,
      password: newPassword.password,
      token: newPassword.token,
    });

    if (data) {
      console.log(data);
      navigateTo('/');
    } else alert('User not found');
  }
};

function assignCaptchaDiv() {
  var catpcha = document.getElementById('captcha');
  catpcha.style.display = 'block';

  let capscriptdiv = document.getElementById('captchaScript');
  let scriptcap = document.createElement('script');
  scriptcap.type = 'text/javascript';
  scriptcap.text = ` function verifyCaptcha(token) {
        var buttons = document.getElementsByTagName('button');
        for (let i = 0; i < buttons.length; i++) {
          let button = buttons[i];
          button.disabled = false;
        }
      }`;
  capscriptdiv.appendChild(scriptcap);
  return 'accept the captcha';
}

function removeEventListeners() {
  window.removeEventListener('submit', loginVerify);
  window.removeEventListener('submit', registerVerify);
  window.removeEventListener('submit', forgotPassword);
  window.removeEventListener('submit', changePassword);
  window.removeEventListener('input', (event) => {
    inputsAreNotNull(event.target.id);
  });
}

export {
  loginVerify,
  registerVerify,
  forgotPassword,
  inputsAreNotNull,
  removeEventListeners,
  changePassword,
  assignCaptchaDiv,
};
