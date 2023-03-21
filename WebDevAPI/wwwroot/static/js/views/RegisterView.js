import AbstractView from "./AbstractView.js";
import { deletePlayButton, deleteGameButtons, deletePokerButtons } from "../helpers/buttons.js";

export default class extends AbstractView {
    constructor(params) {
        super(params);
        this.setTitle("Register");
        
        deletePlayButton();
        deleteGameButtons();
        deletePokerButtons();

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
            <div class="form signup">
                <header>Signup</header>
                <div id="signupDiv"></div>
            </div>
        `;
    }

}

