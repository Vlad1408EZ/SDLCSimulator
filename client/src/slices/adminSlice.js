import { createSlice } from "@reduxjs/toolkit";
import { getAllGroupsAPI } from "../api/adminAPI";
import { createUserAPI, deleteUserAPI, getUsersAPI } from "../api/userAPI";
import { enqueueSnackbar, handleError, variants } from "./notificationsSlice";
import { AVAILABLE_MODALS, toggleModal } from "./uiSlice";


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

export const createUser = (data) => (dispatch, getState) => {
    dispatch(setIsUCreationLoading(true));
    createUserAPI(data)
        .then((res) => {
            const users = getState().admin.users;
            dispatch(setUsers([...users, res.data]));
            dispatch(enqueueSnackbar("Користувача успішно створено", variants.SUCCESS));
            dispatch(toggleModal({
                [AVAILABLE_MODALS.CREATE_USER]: false,
            }));
        })
        .catch((res) => {
            handleError(res.response)
        })
        .finally(() => dispatch(setIsUCreationLoading(false)));
};


export const deleteUser = (uid) => (dispatch, getState) => {
    dispatch(setIsUCreationLoading(true));
    deleteUserAPI(uid)
        .then(() => {
            const users = getState().admin.users;
            dispatch(setUsers(users.filter(u => u.id !== uid)));
            dispatch(enqueueSnackbar("Користувача успішно видалено", variants.SUCCESS));
        })
        .catch((res) => {
            handleError(res.response)
        })
        .finally(() => dispatch(setIsUCreationLoading(false)));
};