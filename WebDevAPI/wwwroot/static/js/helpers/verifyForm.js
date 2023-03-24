import { Player } from "../../models/Player.js";
import { UserLoginDto } from "../../models/UserLoginDto.js";
import { PlayerRegisterDto } from "../../models/PlayerRegisterDto.js";
import { jwtToken, navigateTo } from "../index.js";

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
            jwtToken.token = data.token;
            navigateTo("/");

}

const registerVerify = async () => {
    var inputs = checkInput();
    const newUser = new PlayerRegisterDto();
    inputs.forEach(input =>{
        switch(input.id){
            case "firstname":
                newUser.firstname = input.value;
                break;
            case "lastname":
                newUser.lastname = input.value;
                break;
            case "username":
                newUser.username = input.value;
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
    navigateTo("/");
}

const registerPlayer = async () => {
    var inputs = checkInput();
    const newPlayer = new Player();
    inputs.forEach(input =>{
        switch(input.id){
            case "username":
                newPlayer.username = input.value;
                break;
        }
    })

    let response = await fetch('api/Auth/Username', {
        method: 'POST',
        headers: { 
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + jwtToken.token
         },
        body: JSON.stringify(newPlayer)
    }).then(res => res.json())
    .then(x => console.log(x))
    .catch(err => alert(err));

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