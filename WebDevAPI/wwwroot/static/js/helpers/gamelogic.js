import { Game } from "../../models/Game.js";
import { jwtToken } from "../index.js";

export let game = new Game();

function createGame(){
    
        fetch("/api/Pokertable/Create", {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'bearer ' + jwtToken.token
            },
        }).then(res => res.json())
            .then((o) => {game = o})
            .catch(err => console.log(err));
        console.log(game)
        return game;
}



export {
    createGame,
}