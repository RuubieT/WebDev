import AbstractView from "./AbstractView.js";
import { createPlayButton } from "../helpers/buttons.js";

export default class extends AbstractView {
    constructor(params) {
        super(params);
        this.setTitle("Dashboard");
        
        createPlayButton();

        document.getElementById("contact").style.display = 'none';

        
    }   

    async getHtml() {
        return `
        <!--<div style="background-image: url('/static/images/Dashboard_background.jpg'); background-size: cover; height:750px">-->
       
            <h1>Home</h1>

       
        `;
    }

}

