import { createSlice } from '@reduxjs/toolkit'
import { loginAPI } from '../api/userAPI';
import { handleError } from './notificationsSlice';


// Slice

const initialState = {
    isLoading: false,
    user: JSON.parse(localStorage.getItem("userData")),
    token: null,
    isError: false,
    isAuthenticated: localStorage.getItem("jwtToken") != null,
}

export const authSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        resetState: () => initialState,
        setIsLoading: (state, action) => {
            state.isLoading = action.payload;
        },
        setUser: (state, action) => {
            state.user = action.payload;
            state.isError = false;
        },
        setAuthError: (state, action) => {
            state.isError = action.payload;
        },
        setIsAuthencticated: (state, action) => {
            state.isAuthenticated = action.payload;
        },
    },
});

export const {
    resetState,
    setIsLoading,
    setUser,
    setAuthError,
    setIsAuthencticated
} = authSlice.actions;

export default authSlice.reducer;


//Actions

export const login = (email, password) => (dispatch) => {
    dispatch(setIsLoading(true));
    loginAPI({ email, password })
        .then(res => {
            localStorage.setItem("jwtToken", res.data.token);
            localStorage.setItem("userData", JSON.stringify(res.data));
            dispatch(setUser(res.data));
            dispatch(setIsAuthencticated(true));
        })
        .catch(err => handleError(err))
        .finally(() => dispatch(setIsLoading(false)));
}

export const logout = () => (dispatch) => {
    localStorage.removeItem("jwtToken");
    localStorage.removeItem("userData");
    dispatch(setUser(null));
    dispatch(setIsAuthencticated(false));
}