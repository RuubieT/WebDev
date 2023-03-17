import { navigateTo } from "../index.js";
import AbstractView from "./AbstractView.js";

export default class extends AbstractView {
    constructor(params) {
        super(params);
        this.setTitle("Dashboard");

        document.getElementById("contact").style.display = 'none';

        window.onload = function () {
            console.log("onload"); 

            var button = document.getElementById("playButton");
            console.log(button);
            button.onclick = async function () {
                navigateTo("/game");
            }
        }
    }   

    async getHtml() {
        return `
         <div style="background-image: url('/static/images/Dashboard_background.jpg'); background-size: cover; height:750px">
            <h1>Home</h1>

            <p>
               <button id="playButton">Play</button>
            </p>        
        </p>
        </div>
        `;
    }

}

