import network from "../network";

export const getUsersAPI = () => network.get(`/User/AllUsers`);

export const getAllGroupsAPI = () => network.get(`/Group/Allgroups`);