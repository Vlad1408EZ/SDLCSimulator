import { createSlice } from "@reduxjs/toolkit";
import { getAllGroupsAPI, getUsersAPI } from "../api/adminAPI";
import { handleError } from "./notificationsSlice";


const initialState = {
    isLoading: false,
    users: [],
    userCreation: {
        isCreationLoading: false,
        groups: [],
        roles: [
            { value: 0, label: "Student" },
            { value: 1, label: "Teacher" },
            { value: 2, label: "Admin" },
        ]
    }
};


export const adminSlice = createSlice({
    name: "admin",
    initialState,
    reducers: {
        resetState: () => initialState,
        setIsLoading: (state, action) => {
            state.isLoading = action.payload;
        },
        setUsers: (state, action) => {
            state.users = action.payload;
        },
        setIsUCreationLoading: (state, action) => {
            state.userCreation.isCreationLoading = action.payload;
        },
        setUCreationGroups: (state, action) => {
            state.userCreation.groups = action.payload;
        },
    },
});


export const {
    resetState,
    setIsLoading,
    setUsers,
    setIsUCreationLoading,
    setUCreationGroups
} = adminSlice.actions;

export default adminSlice.reducer;


// Actions

export const getUsers = () => (dispatch) => {
    dispatch(setIsLoading(true));
    getUsersAPI()
        .then((res) => {
            dispatch(setUsers(res.data));
        })
        .catch((err) => handleError(err))
        .finally(() => dispatch(setIsLoading(false)));
};

export const getAllGroups = () => (dispatch) => {
    dispatch(setIsUCreationLoading(true));
    getAllGroupsAPI()
        .then((res) => {
            const groups = res.data.map(obj => ({ label: obj.groupName, value: obj.groupName, id: obj.id }));
            dispatch(setUCreationGroups(groups));
        })
        .catch((err) => handleError(err))
        .finally(() => dispatch(setIsUCreationLoading(false)));
};