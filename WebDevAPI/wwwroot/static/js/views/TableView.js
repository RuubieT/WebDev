import AbstractView from "./AbstractView.js";
import { deleteGameButtons, deletePlayButton, createPokerButtons } from "../helpers/buttons.js";

export default class extends AbstractView {
    constructor(params) {
        super(params);
        this.setTitle("Table");
        
        createPokerButtons();

        document.getElementById("contact").style.display = 'none';

        
    }   

    async getHtml() {
        return `
        <h1>Table</h1>
    

        `;
    }

}

