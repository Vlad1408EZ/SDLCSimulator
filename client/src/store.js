import { configureStore } from "@reduxjs/toolkit";
import { createBrowserHistory } from "history";
import adminSlice from "./slices/adminSlice";
import authSlice from "./slices/authSlice";
import notificationsSlice from "./slices/notificationsSlice";
import tasksSlice from "./slices/tasksSlice";
import uiSlice from "./slices/uiSlice";

export const history = createBrowserHistory();

const store = configureStore({
	reducer: {
		auth: authSlice,
		ui: uiSlice,
		notifications: notificationsSlice,
		tasks: tasksSlice,
		admin: adminSlice
	},
});

export default store;
