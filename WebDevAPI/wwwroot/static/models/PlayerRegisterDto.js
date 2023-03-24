export class PlayerRegisterDto {
    constructor(firstname, lastname, username, email, password) {
        this.firstname = firstname;
        this.lastname = lastname;
        this.username = username
        this.email = email;
        this.passwordHash = password;
    }
}