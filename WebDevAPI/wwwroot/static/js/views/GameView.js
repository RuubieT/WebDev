import AbstractView from './AbstractView.js';
import { createGameButtons } from '../helpers/buttons.js';
import { test, test2 } from '../helpers/services/player.js';

const uri = 'api/PokerTable';

export default class extends AbstractView {
  constructor(params) {
    super(params);
    this.setTitle('Game');

    createGameButtons();
  }

  async getHtml() {
    return `
            <h1>Game</h1>    
        
        
        `;
  }
}
