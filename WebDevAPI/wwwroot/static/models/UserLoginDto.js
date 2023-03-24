export class UserLoginDto {
    constructor(email, password) {
        this.email = email;
        this.passwordHash = password;
    }
}