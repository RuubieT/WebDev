import AbstractView from './AbstractView.js';
import { createPlayButton } from '../helpers/buttons.js';

export default class extends AbstractView {
  constructor(params) {
    super(params);
    this.setTitle('Dashboard');

    createPlayButton();
  }

  async getHtml() {
    return `
       
        `;
  }
}
