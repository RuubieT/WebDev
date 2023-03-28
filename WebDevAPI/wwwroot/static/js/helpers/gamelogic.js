import { Game } from "../../models/Game.js";
import { jwtToken } from "../index.js";
import { CreatePokerTableDto } from "../../models/Dto/PokerTable/CreatePokerTableDto.js";
import { StartPokerGameDto } from "../../models/Dto/PokerTable/StartPokerGameDto.js";
import { getCookie, setCookie } from "./cookieHelper.js";
import { getData, postData } from "./apiCallTemplates.js";

export let game = new Game();
export let startPokerTable = new StartPokerGameDto();

async function createGame(){

    const username = getCookie("username");
    const createData = new CreatePokerTableDto(username);

    game = await postData('api/Pokertable/Create', createData);

    if (game) {
        setCookie("pokerTableId", game.id, 1);
    }
}

async function startGame() {
    const table = getCookie("pokerTableId");

    let startedGame = await getData(`/api/Pokertable/Start/${table}`);
    console.log(startedGame);

}

export {
    createGame,
    startGame
}
