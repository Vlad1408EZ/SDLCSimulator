import network from "../network";

export const loginAPI = (data) => network.post("/User/Login", data);

export const getUsersAPI = () => network.get("/User/AllUsers");

export const createUserAPI = (data) => network.post("/User/CreateUser", data);

export const deleteUserAPI = (uid) => network.delete(`/User/DeleteUser?userId=${uid}`);

export const updateUserAPI = (data) =>
	network.post("/User/UpdateUserInfo", data);

export const changeUserPasswordAPI = (data) =>
	network.post("/User/ChangePassword", data);
