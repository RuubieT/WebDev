import {jwtToken, navigateTo} from '../index.js';
import { Contactform } from "../../models/Contactform.js";
import { createGame } from './gamelogic.js';


function createSubmitFormButton() {
    var submitFormButton = document.getElementById("contactFormButtonDiv");
    if (!submitFormButton) {    
        const div = document.createElement('div');
        div.id = 'contactFormButtonDiv';
        
        const btn = createCustomButtons("submitFormButton", "Submit!");
        btn.addEventListener("click", async () => {
            const inputFields = document.querySelectorAll("input");
            const validInputs = Array.from(inputFields).filter(input => input.value !== "");
            const contactForm = new Contactform();
            validInputs.forEach(input =>{
                switch(input.id){
                    case "name":
                        contactForm.name = input.value;
                        break;
                    case "email":
                        contactForm.email = input.value;
                        break;
                    case "subject":
                        contactForm.subject = input.value;
                        break;
                    case "description":
                        contactForm.description = input.value;
                        break;
                }
            })

            
            let response = await fetch('api/Contactform', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({name: contactForm.name, email: contactForm.email, subject: contactForm.subject, description: contactForm.description})
            });
                
            let data = await response.json();
            alert(JSON.stringify(data));
            
        })

        div.appendChild(btn);
        return div;
    }
}

function deleteSubmitFormButton() {
    deleteCustomButtons("contactFormButtonDiv")
}

function createPlayButton() {
    var playButton = document.getElementById("playButtonDiv");
    if (!playButton) {    
        const div = document.createElement('div');
        div.id = 'playButtonDiv';
        
        const btn = createCustomButtons("playButton", "Play Game"); 
        btn.dataset["link"] = "";
        btn.addEventListener("click", async () => {
            navigateTo("/game");
        })

        div.appendChild(btn);
        document.body.appendChild(div);
          
    }
}

function deletePlayButton() {
    deleteCustomButtons("playButtonDiv");
}

function createGameButtons(){
    var playButtons = document.getElementById("gameButtons");
    if (!playButtons){
        const div = document.createElement('div');
        div.id = 'gameButtons';

        const createbutton = createCustomButtons("createButton", "Create");
        createbutton.classList.add("game");
        createbutton.dataset["link"] = "";
        createbutton.addEventListener("click", () => {
            var deck = createGame();

            console.log(deck);
            
            //navigateTo('/table');
        })
        
        const joinbutton = createCustomButtons("joinButton", "Join");
        joinbutton.classList.add("game");
        joinbutton.dataset["link"] = "";
        joinbutton.addEventListener("click", () => {
           console.log("Join table?")
        })
        

        div.appendChild(createbutton);
        div.appendChild(joinbutton);
        document.body.appendChild(div);
    }
}

function deleteGameButtons(){
    deleteCustomButtons("gameButtons");
}

function createPokerButtons(){
    var pokerButtons = document.getElementById("pokerButtons");
    if (!pokerButtons){
        const div = document.createElement('div');
        div.id = 'pokerButtons';

        const checkButton = createCustomButtons("checkButton", "Check");
        checkButton.addEventListener("click", () => {
            alert("CHECK");
        })

        const foldButton = createCustomButtons("foldButton", "Fold");
        foldButton.addEventListener("click", () => {
            alert("FOLD");
        })

        const callButton = createCustomButtons("callButton", "Call");
        callButton.addEventListener("click", () => {
            alert("CALL");
        })

        const betButton = createCustomButtons("betButton", "Bet");
        betButton.addEventListener("click", () => {
            alert("BET");
        })
        
        
        div.appendChild(checkButton);
        div.appendChild(foldButton);
        div.appendChild(callButton);
        div.appendChild(betButton);
        document.body.appendChild(div);
    }
}

function deletePokerButtons(){
    deleteCustomButtons("pokerButtons");
}

function createCustomButtons(name, text){
    const button = document.createElement('button');
    button.id = name;
    button.innerText = text;

    return button;
}

function deleteCustomButtons(name){
    var buttons = document.getElementById(name);
    if(buttons){
        document.body.removeChild(buttons);
    }
}

export {
    createSubmitFormButton,
    deleteSubmitFormButton,
    createPlayButton,
    deletePlayButton,
    createGameButtons,
    deleteGameButtons,
    createPokerButtons,
    deletePokerButtons
}
