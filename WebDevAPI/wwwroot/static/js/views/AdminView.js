import { createRoleList } from '../helpers/management.js';
import AbstractView from './AbstractView.js';

export default class extends AbstractView {
  constructor(params) {
    super(params);
    this.setTitle('Admin Page');

    createRoleList();
  }

  async getHtml() {
    return `
       
        
        
        `;
  }
}
