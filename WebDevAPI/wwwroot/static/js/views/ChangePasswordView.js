import { changePassword } from '../helpers/verifyForm.js';
import AbstractView from './AbstractView.js';

export default class extends AbstractView {
  constructor(params) {
    super(params);
    this.setTitle('Change password');

    window.addEventListener('submit', changePassword);
  }

  async getHtml() {
    return `
   <div class="wrapper">
            <div class="form-box">
                <h2>Forgot password</h2>
                <form id="Forgot" action="javascript:void(0);">
                    
                <div class="input-box">
                        <input type="text" id="token" required>
                        <label>Token</label>
                    </div>
                <div class="input-box">
                        <input type="password" id="password" minlength="8 required">
                        <span id="strength">New Password</span>
                        <label>Password</label>
                    </div>
                     <div class="input-box">
                        <input type="password" id="password2" minlength="8 required">
                        <span id="validate">Retype The New Password</span>
                        <label>Password</label>
                    </div>
                    <button type="submit" class="btn" id="Changebutton">Reset Password</button>
                    </form>
            </div>
        </div>
        `;
  }
}
