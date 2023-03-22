import AbstractView from "./AbstractView.js";
import { createGameButtons, deletePlayButton, deletePokerButtons } from "../helpers/buttons.js";

const uri = 'api/PokerTable';

export default class extends AbstractView {
   

    constructor(params) {
        super(params);
        this.setTitle("Game");

        document.getElementById("contact").style.display = 'none';

        deletePlayButton();
        createGameButtons();
        deletePokerButtons();

        window.onload = function() {

            function create() {
                fetch(uri + "/Create")
                    .then(response => response.json())
                    .catch(error => console.error('Unable to create.', error));
            }
            function join() {
                fetch(uri + "/Join")
                    .then(response => response.json())
                    .catch(error => console.error('Unable to create.', error));
            }
        }
    }   

    async getHtml() {
        return `
            <h1>Game</h1>    
        
        
        `;
    }

}

