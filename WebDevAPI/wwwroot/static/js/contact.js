const form = document.querySelector("form");
const email = document.getElementById("email");
const thename = document.getElementById("name");
const subject = document.getElementById("subject");
const description = document.getElementById("description");
const emailError = document.querySelector("#email + span.error");
const thenameError = document.querySelector("#name + span.error");
const subjectError = document.querySelector("#subject + span.error");
const descriptionError = document.querySelector("#description + span.error");

email.addEventListener("input", (event) => {
    // Each time the user types something, we check if the
    // form fields are valid.

    if (email.validity.valid) {
        // In case there is an error message visible, if the field
        // is valid, we remove the error message.
        emailError.textContent = ""; // Reset the content of the message
        emailError.className = "error"; // Reset the visual state of the message
    } else {
        // If there is still an error, show the correct error
        showError();
    }
});

thename.addEventListener("input", (event) => {
    if (thename.validity.valid) {
        thenameError.textContent = ""; // Reset the content of the message
        thenameError.className = "error"; // Reset the visual state of the message
    } else {
        showError();
    }
});

subject.addEventListener("input", (event) => {
    if (subject.validity.valid) {
        subjectError.textContent = ""; // Reset the content of the message
        subjectError.className = "error"; // Reset the visual state of the message
    } else {
        showError();
    }
});

description.addEventListener("input", (event) => {
    if (description.validity.valid) {
        descriptionError.textContent = ""; // Reset the content of the message
        descriptionError.className = "error"; // Reset the visual state of the message
    } else {
        showError();
    }
});

form.addEventListener("submit", async (event) => {
    // Then we prevent the form from being sent by canceling the event
    event.preventDefault();

    // if the name, subject or description is not filled in show the appropriate message
    if(!thename.value || !subject.value || !description.value || !email.validity.valid){
        showError();
        return;
    }

/*
    let response = await fetch('http://localhost:3000/form', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({email: email.value})
    });

    let data = await response.json();
    alert(JSON.stringify(data))
*/
});

function showError(){
    if(!thename.value){
        thenameError.textContent = "no name";
        thenameError.className = "error active";
    }
    if(!subject.value){
        subjectError.textContent = "no subject";
        subjectError.className = "error active";
    }
    if(!description.value){
        descriptionError.textContent = "no description";
        descriptionError.className = "error active";
    }
    if (!email.value) {
        // If the field is empty,
        // display the following error message.
        emailError.textContent = "You need to enter an e-mail address.";
        emailError.className = "error active";
    } else if (email.validity.typeMismatch) {
        // If the field doesn't contain an email address,
        // display the following error message.
        emailError.textContent = "Entered value needs to be an e-mail address.";
        emailError.className = "error active";
    // } else if (email.validity.tooShort) {
    //     // If the data is too short,
    //     // display the following error message.
    //     emailError.textContent = `E-mail should be at least ${email.minLength} characters; you entered ${email.value.length}.`;
    // }
    }
}