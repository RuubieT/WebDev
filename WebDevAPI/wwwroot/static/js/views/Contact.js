import AbstractView from "./AbstractView.js";

window.addEventListener("input", (event) => { checkInput(event.target.id) });

window.addEventListener('load', function () {
    console.log("loaded Contact.js");
}); 



export default class extends AbstractView {
    constructor(params) {
        super(params);
        this.setTitle("Contact");
        document.querySelector("#recaptcha").hidden =false;
        document.querySelector("#contactform").hidden =false;
        document.querySelector("#submit").hidden =false;

        document.querySelector("#submit").addEventListener("submit", async (event) => {
            console.log("Form submitted now ");
            console.log(event);
            // Then we prevent the form from being sent by canceling the event
            event.preventDefault();
        
            /* if the name, subject or description is not filled in show the appropriate message
            if (!thename.value || !subject.value || !description.value || !email.validity.valid) {
                showError();
                return;
            }*/
        
            /*
                let response = await fetch('http://localhost:3000/form', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({email: email.value})
                });
            
                let data = await response.json();
                alert(JSON.stringify(data))
            */
        
        })
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
              <input type="email" name="email" id="email" minlength="8">
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
        </form> 
      </section>
      
      <script>
        var recaptcha_response = '';
        function submitUserForm() {
            console.log("Submitted");
         }
        </script>
        `;
    }

}

function checkInput(field) {
    
    const html = document.getElementById(field)
    const errorField = document.querySelector("#" + html.id + " + span.error");
    html.addEventListener("input", (event) => {
        if (html.validity.valid && html.value != "") {
            errorField.textContent = "";
            errorField.className = "error";
        } else {
            showError(html, errorField);
        }
    })

}

function showError(field, errorField) {
    //For each input field a check and a error to display
    if (!field.value) {
        switch (field.id) {
            case 'name':
                errorField.textContent = "Fill in a name";
                errorField.className = "error active";
                break;
            case 'email':
                errorField.textContent = "Fill in an email";
                errorField.className = "error active";
                break;
            case 'subject':
                errorField.textContent = "Fill in a subject";
                errorField.className = "error active";
                break;
            case 'description':
                errorField.textContent = "Fill in description";
                errorField.className = "error active";
                break;
        }
    } else if (field.id == 'email' && field.validity.typeMismatch) {
        errorField.textContent = "Entered value needs to be an e-mail address.";
        errorField.className = "error active";
    }
}
