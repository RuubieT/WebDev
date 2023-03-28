import { Game } from "../../models/Game.js";
import { jwtToken } from "../index.js";
import { getCookie } from "./cookieHelper.js";

export let game = new Game();

async function createGame(){

    const data = JSON.parse(getCookie("userData"));
    const createdata = {
        username: data.username,
        email: data.email
    }

    game = await fetch('/api/Pokertable/Create', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(createdata)
    }).then((response)=>{
        return response.json(); 
    }).catch(err => console.log(err));

    console.log(game);

       
}

export {
    createGame,
}
