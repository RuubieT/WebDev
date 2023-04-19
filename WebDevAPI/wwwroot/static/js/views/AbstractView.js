import { GetUser } from '../helpers/services/auth.js';

export default class {
  constructor(params) {
    this.params = params;
  }

  setTitle(title) {
    document.title = title;
  }

  async getCurrentUser() {
    let userdata = await GetUser();
    return userdata;
  }

  async getHtml() {
    return '';
  }
}
