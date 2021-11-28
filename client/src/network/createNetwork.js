import axios from "axios";

const createNetwork = (baseURL) => {
	const defaultOptons = {
		baseURL,
		headers: {
			"Content-Type": "application/json",
		},
	};

	const instance = axios.create(defaultOptons);

	instance.interceptors.request.use(
		(config) => {
			const token = localStorage.getItem("jwtToken");
			if (
				token &&
				config &&
				!config.url.endsWith("token") &&
				!config.url.includes("/Login")
			) {
				config.headers.Authorization = `Bearer ${token}`;
			}
			return config;
		},
		(error) => Promise.reject(error)
	);

	return instance;
};

export default createNetwork;
