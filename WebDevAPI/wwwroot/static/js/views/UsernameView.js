import { registerPlayer } from "../helpers/verifyForm.js";
import AbstractView from "./AbstractView.js";

export default class extends AbstractView {
    constructor(params) {
        super(params);
        this.setTitle("Register");
        

        document.getElementById("contact").style.display = 'none';
        
        window.addEventListener("submit", registerPlayer)
        
    }   

    async getHtml() {
        return `
        <div class="wrapper username">
            <div class="form-box register">
                <h2>Fill in your username</h2>
                <form id="Register" action="javascript:void(0);">
                    
                    <div class="input-box">
                        <input type="text" id="username" required>
                        <label>Username</label>
                    </div>
                        <button type="submit" class="btn" id="btn">Confirm</button>
                </form>
            </div>
        </div>
        `;
    }

}

