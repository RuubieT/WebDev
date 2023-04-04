import AbstractView from './AbstractView.js';
import {
  deleteGameButtons,
  deletePlayButton,
  createPokerButtons,
} from '../helpers/buttons.js';
import {
  getHand,
  getTableCards,
  assignPokertable,
} from '../helpers/gamelogic.js';

export default class extends AbstractView {
  constructor(params) {
    super(params);
    this.setTitle('Table');

    createPokerButtons();
    //getPlayers();
    // getTableCards();
    assignPokertable();
    document.getElementById('contact').style.display = 'none';
  }

  async getHtml() {
    return `
    <div class="container">
        <div class="table">
            <div class="card-place" id="tableCardsDiv">
        </div>
        <div class="players" id="players">
            <div id="cards"></div>
        </div>
    </div>
        `;
  }
}
