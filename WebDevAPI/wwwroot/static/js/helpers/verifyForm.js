import { Player } from "../../models/Player.js";
import { UserLoginDto } from "../../models/Dto/UserLoginDto.js";
import { PlayerRegisterDto } from "../../models/Dto/PlayerRegisterDto.js";
import { jwtToken, navigateTo } from "../index.js";
import { setCookie } from "./cookieHelper.js";

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
       
    //#TODO duplicate code
    let data = await response.json();
    jwtToken.token = data.token;

    let userData = await fetch(`/api/Player/Find/${user.email}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + jwtToken.token
        },
    }).then((response)=>{
        return response.json(); 
    }).catch(err => console.log(err));

    setCookie("userData", JSON.stringify(userData), 1);
    
    alert("Logged in");
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
    }).then((response) => {
        return response.json();
    }).catch(err => console.log(err));
        
    jwtToken.token = response.token;
    
    let userData = await fetch(`/api/Player/Find/${newUser.email}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + jwtToken.token
        },
    }).then((response)=>{
        return response.json(); 
    }).catch(err => console.log(err));

    setCookie("userData", JSON.stringify(userData), 1);

    alert("Registered succesfully");
    navigateTo("/");
}

function removeEventListeners(){
    window.removeEventListener("submit", loginVerify);
    window.removeEventListener("submit", registerVerify);
}

export {
    loginVerify,
    registerVerify,
    checkInput,
    removeEventListeners
}
