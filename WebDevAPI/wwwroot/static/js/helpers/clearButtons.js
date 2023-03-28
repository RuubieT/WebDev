import { deleteGameButtons, deletePlayButton, deletePokerButtons, deleteSubmitFormButton } from "./buttons.js";
import { deleteLeaderboard } from "./leaderboard.js";


function deleteAllButtons(){
    deleteGameButtons();
    deleteLeaderboard();
    deletePlayButton();
    deletePokerButtons();
    deleteSubmitFormButton();
}

export {
    deleteAllButtons
}
