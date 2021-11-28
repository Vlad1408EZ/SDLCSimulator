import network from "../network";

export const loginAPI = (data) => network.post("/User/Login", data);

export const allUsersAPI = () => network.get("/User/AllUsers");

export const createUserAPI = (data) => network.post("/User/CreateUser", data);

export const updateUserAPI = (data) =>
	network.put("/User/UpdateUserInfo", data);

export const changeUserPasswordAPI = (data) =>
	network.put("/User/ChangePassword", data);
