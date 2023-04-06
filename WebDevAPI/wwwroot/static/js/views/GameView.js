import AbstractView from './AbstractView.js';
import { createGameButtons } from '../helpers/buttons.js';

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
