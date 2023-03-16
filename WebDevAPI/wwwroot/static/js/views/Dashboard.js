import AbstractView from "./AbstractView.js";

export default class extends AbstractView {

    constructor(params) {
        super(params);
        this.setTitle("Dashboard");

        document.querySelector("#recaptcha").hidden = true;
        document.querySelector("#contactform").hidden = true;
        document.querySelector("#submit").hidden = true;
        var backgroundImage = new Image()
        backgroundImage.src = "./../../images/Dashboard_background.jpg";
        console.log(backgroundImage);        
    }

   

    async getHtml() {
        return `
        <h1>Home</h1>
        <img src=\'./../../../images/Dashboard_background.jpg\' width=\'400px\' height=\'150px\'>
        <p style="background-image: url($(backgroundImage);">
           <button id="playButton">Play</button>
        </p>        
        `;
    }

}

