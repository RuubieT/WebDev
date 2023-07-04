import AbstractView from './AbstractView.js';
import { createLeaderboard } from '../helpers/leaderboard.js';

export default class extends AbstractView {
  constructor(params) {
    super(params);
    this.setTitle('Leaderboard');

    const uri = 'api/Player/Leaderboard';

    fetch(uri)
      .then((response) => response.json())
      .then((data) => createLeaderboard(data))
      .catch((error) => console.error('Unable to get items.', error));
  }

  async getHtml() {
    return `

        `;
  }
}