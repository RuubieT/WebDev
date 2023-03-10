import AbstractView from "./AbstractView.js";

export default class extends AbstractView {
    constructor(params) {
        super(params);
        this.setTitle("Contact");
    }

    async getHtml() {
        return `
        <section class="contact">
      
        <h1>Contact Ruben Tharner</h1>
    
        <form action="" method="post" class="form-contactpagina" onsubmit="return submitUserForm();">
    
          <div class="form-contactpagina__inputelement fx-col">
            <label for="name">Naam: </label>
            <input type="text" name="name" id="name">
            <span class="error" aria-live="polite"></span>
          </div>
    
          <div class="form-contactpagina__inputelement fx-col">
              <label for="email">Emailadres: </label>
              <input type="email" name="email" id="email">
              <span class="error" aria-live="polite"></span>
          </div>
    
          <div class="form-contactpagina__inputelement fx-col">
            <label for="email">Onderwerp: </label>
            <input type="text" name="subject" id="subject" maxlength="200">
            <span class="error" aria-live="polite"></span>
          </div>
    
          <div class="form-contactpagina__inputelement fx-col">
            <label for="email">Beschrijving: </label>
            <input type="text" name="description" id="description" maxlength="600">
            <span class="error" aria-live="polite"></span>
          </div>
    
          <div class="g-recaptcha" data-sitekey="6LeRgakkAAAAAIf6C1lJ9oNJDhK_fRSSjBu6ItJx" data-callback="verifyCaptcha"></div>
          <div id="g-recaptcha-error"></div>

          <div class="form-contactpagina__inputelement">
            <input type="submit" name="submit" value="Verstuur!">
          </div>
        </form> 
      </section>
      
      <script>
        var recaptcha_response = '';
        function submitUserForm() {
            if(recaptcha_response.length == 0) {
                document.getElementById('g-recaptcha-error').innerHTML = '<span style="color:red;">This field is required.</span>';
                return false;
            }
            return true;
        }
         
        function verifyCaptcha(token) {
            recaptcha_response = token;
            document.getElementById('g-recaptcha-error').innerHTML = '';
        }
        </script>
        `;
    }
}