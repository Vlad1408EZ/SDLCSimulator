const emailRE = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

const isValueNotGiven = (value) => value === "" || value === null || value === undefined;

export const validateEmail = (value) => {
    return !emailRE.test(String(value).toLowerCase())
        || isValueNotGiven(value)
        ? "Invalid email"
        : undefined;
}

export const validatePassword = (value) => {
    return isValueNotGiven(value) || value.length < 6
        ? "Invalid password"
        : undefined;
}