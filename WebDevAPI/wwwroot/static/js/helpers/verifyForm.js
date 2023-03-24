import { Player } from "../../models/Player.js";
import { UserLoginDto } from "../../models/UserLoginDto.js";
import { UserRegisterDto } from "../../models/UserRegisterDto.js";
import { navigateTo } from "../index.js";

function checkInput (){
    const inputFields = document.querySelectorAll("input");
    return Array.from(inputFields).filter(input => input.value !== "");
}

const loginVerify = async () => {
            var inputs = checkInput();
            const user = new UserLoginDto();
            inputs.forEach(input =>{
                switch(input.id){
                    case "email":
                        user.email = input.value;
                        break;
                    case "password":
                        user.password = input.value;
                        break;
                }
            })

            let response = await fetch('api/Auth/Login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(user)
            });
                
            let data = await response.json();
            var token = JSON.stringify(data);
            console.log(token);
            navigateTo("/");

}

const registerVerify = async () => {
    var inputs = checkInput();
    const newUser = new UserRegisterDto();
    inputs.forEach(input =>{
        switch(input.id){
            case "firstname":
                newUser.firstname = input.value;
                break;
            case "lastname":
                newUser.lastname = input.value;
                break;
            case "email":
                newUser.email = input.value;
                break;
            case "password":
                newUser.password = input.value;
                break;
        }
    })

    let response = await fetch('api/Auth/Register', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(newUser)
    });
        
    let data = await response.json();
    console.log(JSON.stringify(data));
    navigateTo("/username");
}

const registerPlayer = async () => {
    var inputs = checkInput();
    const newPlayer = new Player();
    inputs.forEach(input =>{
        switch(input.id){
            case "username":
                newUser.username = input.value;
                break;
        }
    })

    let response = await fetch('api/Auth/Player', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(newPlayer)
    });
        
    let data = await response.json();
    console.log(JSON.stringify(data));
    navigateTo("/");
}

function removeEventListeners(){
    window.removeEventListener("submit", loginVerify);
    window.removeEventListener("submit", registerVerify);
}


export {
    loginVerify,
    registerVerify,
    registerPlayer,
    checkInput,
    removeEventListeners
}