import { forgotPassword } from '../helpers/verifyForm.js';
import AbstractView from './AbstractView.js';

export default class extends AbstractView {
  constructor(params) {
    super(params);
    this.setTitle('Forgot Password');

    window.addEventListener('submit', forgotPassword);

    var all = document.getElementsByTagName('*');
  }

  async getHtml() {
    return `
         <div class="wrapper">
            <div class="form-box">
             <form id="Forgot" action="javascript:void(0);">
                <h2>Forgot password</h2>
                    <div class="input-box">
                        <input type="email" id="email" required>
                        <label>Email</label>
                    </div>
                    <button type="submit" class="btn" id="ForgotButton">Reset Password</button>
                    </form>
            </div>
        </div>
        `;
  }
}
