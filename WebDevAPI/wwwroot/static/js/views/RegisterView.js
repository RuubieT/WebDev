import { assignCaptchaDiv, registerVerify } from '../helpers/verifyForm.js';
import AbstractView from './AbstractView.js';

export default class extends AbstractView {
  constructor(params) {
    super(params);
    this.setTitle('Register');

    window.addEventListener('submit', registerVerify);
    window.addEventListener('input', (event) => {
      this.checkInput(event.target.id);
    });
  }

  async getHtml() {
    return `
        <div class="wrapper register">
            <div class="form-box register">
                <h2>Registration</h2>
                <form id="Register" action="javascript:void(0);">
                    
                    <div class="input-box">
                        <input type="text" id="firstname" required>
                        <label>First name</label>
                    </div>
                    <div class="input-box">
                        <input type="text" id="lastname" required>
                        <label>Last name</label>
                    </div>
                    <div class="input-box">
                        <input type="text" id="username" required>
                        <label>Username</label>
                    </div>
                    <div class="input-box">
                        <input type="email" id="email" required>
                        <label>Email</label>
                    </div>
                    <div class="input-box">
                        <input type="password" id="password" minlength="8 required">
                        <span id="strength">Type Password</span>
                        <label>Password</label>
                    </div>
                    <div>
                    Check the captcha
                     ${assignCaptchaDiv()}
                    </div>
                
                       
                       
                   
                        <button type="submit" class="btn" id="btn" disabled>Register</button>
                    <div class="login-register">
                        <p>Already have an account?<a href="/login" class="register-link" data-link>   Log in</a></p>
                    </div>
                </form>
            </div>
        </div>
        `;
  }

  checkInput(field) {
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
}
