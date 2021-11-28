export const extractErrorMessage = (err) => {
	if (typeof err === "object" && err.hasOwnProperty("errors")) {
		const errors = err.errors;
		return Object.keys(errors).map((key) => [key, errors[key][0]]);
	}
	return null;
};
