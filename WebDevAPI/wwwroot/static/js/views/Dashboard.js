import AbstractView from "./AbstractView.js";

export default class extends AbstractView {

    constructor(params) {
        super(params);
        this.setTitle("Dashboard");

        document.querySelector("#recaptcha").hidden = true;
        document.querySelector("#contactform").hidden = true;
        document.querySelector("#submit").hidden = true;


        
        const button = document.getElementById("contactform");
        console.log(button);
           // button.addEventListener('click', () => console.log("Start game"))
        
    }

   

    async getHtml() {
        return `
        <h1>Home</h1>
        <p>
           <button onclick="show();" id="playButton">Play</button>
        </p>        
        `;
    }

}

