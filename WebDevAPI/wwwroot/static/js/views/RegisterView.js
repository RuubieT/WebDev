import { registerVerify } from "../helpers/verifyForm.js";
import AbstractView from "./AbstractView.js";

export default class extends AbstractView {
    constructor(params) {
        super(params);
        this.setTitle("Register");
        

        document.getElementById("contact").style.display = 'none';
        
        window.addEventListener("submit", registerVerify)
        
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
                        <input type="password" id="password" required>
                        <label>Password</label>
                    </div>
                    <div class="remember-forgot">
                        <label>
                        <input type="checkbox" id="agreeCheck" required>
                        Agree to the terms & conditions (Captcha?)</label>
                    </div>
                        <button type="submit" class="btn" id="btn">Register</button>
                    <div class="login-register">
                        <p>Already have an account?<a href="/login" class="register-link" data-link>   Log in</a></p>
                    </div>
                </form>
            </div>
        </div>
        `;
    }

}

