const userNameRegex = /^[a-zA-Z0-9._@+-]+$/;
const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{12,}$/;

export function validUserName(value: string) {
    return userNameRegex.test(value);
}

export function validPassword(value: string) {
    return passwordRegex.test(value);
}
