export class UserRegisterDto {
    constructor(firstname, lastname, email, password) {
        this.firstname = firstname;
        this.lastname = lastname;
        this.email = email;
        this.passwordHash = password;
    }
}