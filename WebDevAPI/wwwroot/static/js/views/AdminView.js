export default class extends AbstractView {
  constructor(params) {
    super(params);
    this.setTitle('Admin Page');
  }

  async getHtml() {
    return `
            <h1>Admin</h1>    
        
        
        `;
  }
}
