import { navigateTo } from "../index.js";
import AbstractView from "./AbstractView.js";

export default class extends AbstractView {
    constructor(params) {
        super(params);
        this.setTitle("Dashboard");
        
        
        const appDiv = document.getElementById("app");
        console.log(appDiv.childNodes);

        document.getElementById("contact").style.display = 'none';

        const playButton = document.createElement("button");
        const node = document.createTextNode("Play");
        playButton.appendChild(node);
        appDiv.appendChild(playButton);
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

