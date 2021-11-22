import { createSlice } from '@reduxjs/toolkit'

export const variants = {
    SUCCESS: "success",
    INFO: "info",
    ERROR: "error",
    WARNING: "warning",
};

const initialState = {
    notifications: [],
}

export const notificationsSlice = createSlice({
    name: "notifications",
    initialState,
    reducers: {
        resetState: () => initialState,
        setNotifications: (state, action) => {
            state.notifications = action.payload;
        },
        addNotification: (state, action) => {
            state.notifications = [...state.notifications, action.payload];
        },
        removeNotifications: (state, action) => {
            const { key } = action.payload;
            if (!key) // dismiss all if no key has been defined
                state.notifications = [];
            else state.notifications = state.notifications.filter((n, i) => i !== key);
        }
    },
});


export const {
    resetState,
    setNotifications,
    addNotification,
    removeNotifications
} = notificationsSlice.actions

export default notificationsSlice.reducer;


// Actions

export const handleError = (err) => (dispatch) => {
    const msg = extractErrorMessage(err);
    if (msg) {
        dispatch(addNotification({
            msg,
            key: new Date().getTime() + Math.random(),
        }))
    }
}

const extractErrorMessage = (err) => {
    if (typeof err === "object" && err.hasOwnProperty("errors")) {
        const errors = err.errors;
        return Object.keys(errors).map(key => `${key} => ${errors[key][0]}\n`).join("");
    }
    return null;
}