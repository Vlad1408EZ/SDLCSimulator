import { createSlice } from "@reduxjs/toolkit";

export const variants = {
	SUCCESS: "success",
	INFO: "info",
	ERROR: "error",
	WARNING: "warning",
};

const initialState = {
	notificationList: [],
};

export const notificationsSlice = createSlice({
	name: "notifications",
	initialState,
	reducers: {
		resetState: () => initialState,
		setNotifications: (state, action) => {
			state.notificationList = action.payload;
		},
		addNotification: (state, action) => {
			state.notificationList = [...state.notificationList, action.payload];
		},
		removeNotifications: (state, action) => {
			const key = action.payload;
			if (!key)
				state.notificationList = [];  // dismiss all if no key has been defined
			else
				state.notificationList = state.notificationList.filter((notification) => notification.key !== key);
		},
	},
});

export const {
	resetState,
	setNotifications,
	addNotification,
	removeNotifications,
} = notificationsSlice.actions;

export default notificationsSlice.reducer;

// Actions

export const enqueueSnackbar = (message, variant) => (dispatch) => {
	dispatch(addNotification(({
		message,
		options: { variant },
		key: new Date().getTime() + Math.random(),
	})));
};

export const handleError = (err, dispatch) => {
	const errData = err?.response?.data;
	const msg = extractErrorMessage(errData);
	if (msg) {
		dispatch(enqueueSnackbar(msg, variants.ERROR));
	}
};

const extractErrorMessage = (err) => {
	if (typeof err === "object" && err.hasOwnProperty("errors")) {
		const errors = err.errors;
		return Object.keys(errors)
			.map((key) => `${key} => ${errors[key][0]}\n`)
			.join("");
	}
	return null;
};