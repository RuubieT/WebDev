import { Player } from '../../models/Player.js';
import { UserLoginDto } from '../../models/Dto/Auth/UserLoginDto.js';
import { PlayerRegisterDto } from '../../models/Dto/Auth/PlayerRegisterDto.js';
import { jwtToken, navigateTo } from '../index.js';
import { getCookie, setCookie } from './cookieHelper.js';
import {
  ChangePw,
  ForgotPw,
  GetUser,
  Login,
  Register,
  ValidateCode,
} from './services/auth.js';

let CurrentUser;

function inputsAreNotNull() {
  const inputFields = document.querySelectorAll('input');
  return Array.from(inputFields).filter((input) => input.value !== '');
}

function checkInput(field) {
  const input = document.getElementById(field).value;
  if (field == 'password') {
    var strength = document.getElementById('strength');

    const criteria = {
      length: false,
      uppercase: false,
      lowercase: false,
      number: false,
      specialChar: false,
    };

    // Check if the password meets each criteria
    if (input.length >= 8) {
      criteria.length = true;
    }

    if (/[A-Z]/.test(input)) {
      criteria.uppercase = true;
    }

    if (/[a-z]/.test(input)) {
      criteria.lowercase = true;
    }

    if (/\d/.test(input)) {
      criteria.number = true;
    }

    if (/[$@!%*?&]/.test(input)) {
      criteria.specialChar = true;
    }

    // Calculate the number of criteria met
    const numCriteriaMet = Object.values(criteria).filter(Boolean).length;

    // Return the strength of the password based on the number of criteria met
    switch (numCriteriaMet) {
      case 1:
        strength.innerHTML = '<span style="color:red">Weak!</span>';
        return;
      case 2:
        strength.innerHTML = '<span style="color:red">Weak!</span>';
      case 3:
        strength.innerHTML = '<span style="color:orange">Medium!</span>';
        return;
      case 4:
        strength.innerHTML = '<span style="color:orange">Medium!</span>';
        return;
      case 5:
        strength.innerHTML = '<span style="color:green">Strong!</span>';
        return;
      default:
        return '';
    }
  }
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

  var result = false;
  var modal = document.getElementById('myModal');
  var span = document.getElementsByClassName('close')[0];
  modal.style.display = 'block';

  span.onclick = function () {
    modal.style.display = 'none';
    navigateTo('/');
  };

  window.onclick = function (event) {
    if (event.target == modal) {
      modal.style.display = 'none';
      navigateTo('/');
    }
  };

  var modalcontent = document.getElementById('modalcontent');
  if (modalcontent.hasChildNodes) {
    modalcontent.innerHTML = '';
  }
  var form = document.createElement('form');
  form.action = 'javascript:void(0);';
  var input = document.createElement('input');
  input.type = 'text';
  input.id = 'code';
  var button = document.createElement('button');
  button.id = 'submitCode';
  button.innerText = 'Submit';
  button.addEventListener('click', async (e) => {
    e.preventDefault();
    const value = document.getElementById('code').value;
    const data = {
      Email: user.email,
      Code: value,
    };
    result = await ValidateCode(data);
    if (result.message == 'Success') {
      let tokenJson = await Login(user);
      CurrentUser = await GetUser();

      if (CurrentUser) {
        if (tokenJson) {
          jwtToken.token = tokenJson.token;
        }
        setCookie('username', CurrentUser.userName, 1);
        alert('Logged in');
        navigateTo('/');
        if (modalcontent.hasChildNodes) {
          modalcontent.innerHTML = '';
        }
      }
    }
  });

  form.appendChild(input);
  form.appendChild(button);
  modalcontent.appendChild(form);
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

  let registerData = await Register(newUser);
  CurrentUser = await GetUser();

  if (CurrentUser) {
    if (registerData) {
      jwtToken.token = registerData.token;
    }
    setCookie('username', CurrentUser.userName, 1);
    alert('Registered succesfully');

    var modal = document.getElementById('myModal');
    var span = document.getElementsByClassName('close')[0];
    modal.style.display = 'block';

    span.onclick = function () {
      modal.style.display = 'none';
      navigateTo('/');
    };

    window.onclick = function (event) {
      if (event.target == modal) {
        modal.style.display = 'none';
        navigateTo('/');
      }
    };

    var modalcontent = document.getElementById('modalcontent');
    modalcontent.innerHTML = `
  <img src=${registerData.qrCodeImageUrl} >
    <p>${registerData.manualEntrySetupCode}</p>`;
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
  window.removeEventListener('input', (event) => {
    checkInput(event.target.id);
  });
}

export {
  loginVerify,
  registerVerify,
  forgotPassword,
  inputsAreNotNull,
  checkInput,
  removeEventListeners,
  changePassword,
  assignCaptchaDiv,
};
