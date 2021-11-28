import { validateEmail, validatePassword } from "./validation";

test("Email should be invalid", () => {
	expect(validateEmail("email@gmail.")).toBe("Invalid email");
});

test("Password is too short", () => {
	expect(validatePassword("132")).toBe("Invalid password");
});

test("Password is valid", () => {
	expect(validatePassword("1324567")).toBeUndefined();
});
