import {navigateTo} from '../index.js';

function createPlayButton() {
    var playButton = document.getElementById("playButton");
    if (!playButton) {    
        const div = document.createElement('div');
        div.id = 'playButton';
        
        const btn = document.createElement("button");
        btn.innerText = "Play game";
        btn.id = "playButton";
        btn.addEventListener("click", () => {
            navigateTo('/game');
        })

        div.appendChild(btn);
        document.body.appendChild(div);
    }
}

function deletePlayButton() {
    var playButton = document.getElementById("playButton");
    if (playButton) {
        document.body.removeChild(playButton);
    } 
}

function createGameButtons(){
    var playButtons = document.getElementById("gameButtons");
    if (!playButtons){
        const div = document.createElement('div');
        div.id = 'gameButtons';

        const createbutton = document.createElement('button');
        createbutton.id = "createButton";
        createbutton.innerText = "Create";
        createbutton.classList.add("game");
        createbutton.addEventListener("click", () => {
            navigateTo('/table');
        })
        
        const joinbutton = document.createElement('button');
        joinbutton.id = "joinButton";
        joinbutton.innerText = "Join";
        joinbutton.classList.add("game");
        joinbutton.addEventListener("click", () => {
            alert("JOIN A GAME");
        })

        div.appendChild(createbutton);
        div.appendChild(joinbutton);
        document.body.appendChild(div);
    }
}

function deleteGameButtons(){
    var playButtons = document.getElementById("gameButtons");
    if (playButtons) {
        document.body.removeChild(playButtons);
    } 
}

function createPokerButtons(){
    var pokerButtons = document.getElementById("pokerButtons");
    if (!pokerButtons){
        const div = document.createElement('div');
        div.id = 'pokerButtons';

        const checkButton = document.createElement('button');
        checkButton.id = "checkButton";
        checkButton.innerText = "Check";
        checkButton.addEventListener("click", () => {
            alert("CHECK");
        })

        const foldButton = document.createElement('button');
        foldButton.id = "foldButton";
        foldButton.innerText = "Fold";
        foldButton.addEventListener("click", () => {
            alert("FOLD");
        })

        const callButton = document.createElement('button');
        callButton.id = "callButton";
        callButton.innerText = "Call";
        callButton.addEventListener("click", () => {
            alert("CALL");
        })

        const betButton = document.createElement('button');
        betButton.id = "betButton";
        betButton.innerText = "Bet";
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
    var pokerButtons = document.getElementById("pokerButtons");
    if (pokerButtons) {
        document.body.removeChild(pokerButtons);
    } 
}

export {
    createPlayButton,
    deletePlayButton,
    createGameButtons,
    deleteGameButtons,
    createPokerButtons,
    deletePokerButtons
}
