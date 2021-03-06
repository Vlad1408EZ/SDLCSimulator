import { createSlice } from "@reduxjs/toolkit";

export const AVAILABLE_MODALS = {
	TASK_RESULT: "taskResult",
	TASK_TIME_WARNING: "taskTimeWarning",
	CREATE_USER: "createUser",
	STUDENT_RESULT: "studentResult"
};

const initialState = {
	sidebarOpen: false,
	modals: {},
};

export const uiSlice = createSlice({
	name: "ui",
	initialState,
	reducers: {
		resetState: () => initialState,
		toggleSidebar: (state, action) => {
			state.sidebarOpen = action.payload;
		},
		toggleModal: (state, action) => {
			state.modals = { ...state.modals, ...action.payload };
		},
	},
});

export const { resetState, toggleSidebar, toggleModal } = uiSlice.actions;

export default uiSlice.reducer;
