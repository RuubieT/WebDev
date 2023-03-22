import AbstractView from "./AbstractView.js";
import { deletePlayButton, deleteGameButtons, deletePokerButtons } from "../helpers/buttons.js";

export default class extends AbstractView {
    constructor(params) {
        super(params);
        this.setTitle("Login");
        
        deletePlayButton();
        deleteGameButtons();
        deletePokerButtons();

        document.getElementById("contact").style.display = 'none';

        var loginSubmitDiv = document.getElementById("loginSubmit");
        if (loginSubmitDiv) {     
            const btn = document.createElement("button");
            btn.innerText = "Login";
            btn.id = "loginSubmitButton";
            btn.addEventListener("click", () => {
                console.log("Logging in")
            })
            loginSubmitDiv.appendChild(btn);
        }
       
    }   

    hey() {
        console.log("Hey");
    }

    async getHtml() {
        return `
            <div class="form">
                <form method='post' action='/api/User/Create'> 
                <input type='text' id='textid' name='subdomain' value='' /> 
                <input type='submit' id='button2' name='submit2' value='submit'/>
                </form>
            </div>
        `;
    }

}

