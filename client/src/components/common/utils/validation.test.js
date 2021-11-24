import { validateEmail, validatePassword } from "./validation";

test('Email should be invalid', () => {
    // expect(2 + 2).toBe(4);
    expect(validateEmail("email@gmail.")).toBe("Invalid email");
})

test('Password is too short', () => {
    // expect(2 + 2).toBe(4);
    expect(validatePassword("132")).toBe("Invalid password");
})

test('Password is valid', () => {
    // expect(2 + 2).toBe(4);
    expect(validatePassword("1324567")).toBeUndefined();
})
