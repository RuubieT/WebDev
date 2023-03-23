import AbstractView from "./AbstractView.js";

export default class extends AbstractView {
    constructor(params) {
        super(params);
        this.setTitle("Register");
        

        document.getElementById("contact").style.display = 'none';
        
        var registerSubmitDiv = document.getElementById("signupDiv");
        if (registerSubmitDiv) {     
            const btn = document.createElement("button");
            btn.innerText = "Register";
            btn.id = "registerSubmitButton";
            btn.addEventListener("click", () => {
                console.log("Register me")
            })
            registerSubmitDiv.appendChild(btn);
        }
        
    }   

    async getHtml() {
        return `
        <div class="wrapper register">
            <div class="form-box register">
                <h2>Registration</h2>
                <form id="LoginForm" action="#" method="post" class="form-contactpagina">
                    
                    <div class="input-box">
                        <input type="text" required>
                        <label>First name</label>
                    </div>
                    <div class="input-box">
                    <input type="text" required>
                    <label>Last name</label>
                    </div>
                    <div class="input-box">
                        <input type="email" required>
                        <label>Email</label>
                    </div>
                    <div class="input-box">
                        <input type="password" required>
                        <label>Password</label>
                    </div>
                    <div class="remember-forgot">
                        <label>
                        <input type="checkbox">
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

