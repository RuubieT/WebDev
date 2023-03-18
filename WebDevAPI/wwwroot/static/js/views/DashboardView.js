import AbstractView from "./AbstractView.js";
import { createPlayButton, deleteGameButtons } from "../helpers/buttons.js";

export default class extends AbstractView {
    constructor(params) {
        super(params);
        this.setTitle("Dashboard");
        
        createPlayButton();
        deleteGameButtons();

        document.getElementById("contact").style.display = 'none';

        
    }   

    async getHtml() {
        return `
        <!--<div style="background-image: url('/static/images/Dashboard_background.jpg'); background-size: cover; height:750px">-->
            <h1>Home</h1>

        </div>
        `;
    }

}

