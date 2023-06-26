import { createUserList } from '../helpers/management.js';

import AbstractView from './AbstractView.js';

export default class extends AbstractView {
  constructor(params) {
    super(params);
    this.setTitle('Moderator Page');

    createUserList();
  }

  async getHtml() {
    return `
          
        
        
        `;
  }
}
