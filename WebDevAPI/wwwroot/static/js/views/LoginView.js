import AbstractView from "./AbstractView.js";

export default class extends AbstractView {
    constructor(params) {
        super(params);
        this.setTitle("Login");
        

        document.getElementById("contact").style.display = 'none';
    }   

    test(){
                console.log("implement action");
    }


    async getHtml() {
        return`
        <div class="wrapper">
            <div class="form-box login">
                <h2>Login</h2>
                <form id="LoginForm" action="#" method="post">
                    <div class="input-box">
                        <span class="icon">
                            <ion-icon name="mail"></ion-icon>
                        </span>
                        <input type="email" required>
                        <label>Email</label>
                    </div>
                    <div class="input-box">
                        <span class="icon">
                            <ion-icon name="lock-closed"></ion-icon>
                        </span>
                        <input type="password" required>
                        <label>Password</label>
                    </div>
                    <div class="remember-forgot">
                        <label><input type="checkbox">
                        Remember me</label>
                        <a href="/forgotpw" data-link>Forgot Password?</a>
                    </div>
                    <button type="submit" class="btn" id="btn">Login</button>
                    <div class="login-register">
                        <p>Don't have an account?<a href="/register" class="register-link" data-link>Register</a>
                    </div>
                </form>
            </div>
        </div>
        `;
    }

}

