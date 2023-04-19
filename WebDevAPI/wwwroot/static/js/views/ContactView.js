import AbstractView from './AbstractView.js';
import { createSubmitFormButton } from '../helpers/buttons.js';
import { GetUser } from '../helpers/services/auth.js';
import { assignCaptchaDiv } from '../helpers/verifyForm.js';

export default class extends AbstractView {
  constructor(params) {
    super(params);
    this.setTitle('Contact');

    // window.addEventListener('input', (event) => {
    //   checkInput(event.target.id);
    // });

    // let person = this.getCurrentUser().then((data) => {
    //   return data;
    // });
  }

  async getHtml() {
    return `
        <div class="wrapper register">
            <div class="form-box contact">
                <h2>Contact Ruben</h2>
                <form id="Contactform" action="" method="post">
                    
                    <div class="input-box">
                        <input type="text" name="name" id="name" required>
                        <label for="name">Name</label>
                        <span class="error" aria-live="polite"></span>
                    </div>
                    <div class="input-box">
                        <input type="email" name="email" id="email" minlength="8" required>
                        <label for="email">Email</label>
                        <span class="error" aria-live="polite"></span>
                    </div> 
                    <div class="input-box">
                        <input type="text" name="subject" id="subject" maxlength="200">
                        <label for="subject">Subject</label>
                        <span class="error" aria-live="polite"></span>
                    </div>
                    <div class="input-box">
                        <input type="text" name="description" id="description" maxlength="600">
                        <label for="description">Description</label>
                        <span class="error" aria-live="polite"></span>
                    </div>
                    <div class="remember-forgot" id="captchaDiv">
                        ${assignCaptchaDiv()}
                    </div>
                        ${createSubmitFormButton().innerHTML}
                </form>
            </div>
        </div>
      
        `;
  }
}

function checkInput(field) {
  const input = document.getElementById(field);
  input.addEventListener('input', (event) => {
    const errorField = document.querySelector('#' + input.id + ' + span.error');
    if (input.validity.valid && input.value != '') {
      errorField.textContent = '';
      errorField.className = 'error';
    } else {
      showError(input, errorField);
    }
  });
}

function showError(field, errorField) {
  //For each input field a check and a error to display
  if (!field.value) {
    switch (field.id) {
      case 'name':
        errorField.textContent = 'Fill in a name';
        errorField.className = 'error active';
        break;
      case 'email':
        errorField.textContent = 'Fill in an email';
        errorField.className = 'error active';
        break;
      case 'subject':
        errorField.textContent = 'Fill in a subject';
        errorField.className = 'error active';
        break;
      case 'description':
        errorField.textContent = 'Fill in description';
        errorField.className = 'error active';
        break;
    }
  } else if (field.id == 'email' && field.validity.typeMismatch) {
    errorField.textContent = 'Entered value needs to be an e-mail address.';
    errorField.className = 'error active';
  }
}
