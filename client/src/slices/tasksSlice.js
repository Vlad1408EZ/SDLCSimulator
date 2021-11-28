import { createSlice } from "@reduxjs/toolkit";
import { getStudentTasksAPI, saveTaskExecutionResultAPI } from "../api/taskAPI";
import { handleError } from "./notificationsSlice";
import { AVAILABLE_MODALS, toggleModal } from "./uiSlice";

export const DEFAULT_COLUMN = "Доступні варіанти";

const initialState = {
	isLoading: false,
	studentTasks: [],
	teacherTasks: [],
	taskExecution: {
		taskId: null,
		errorCount: 0,
		isSavingResult: false,
		isExecutionFinished: false,
	},
};

export const tasksSlice = createSlice({
	name: "tasks",
	initialState,
	reducers: {
		resetState: () => initialState,
		resetTaskExecutionState: (state) => {
			state.taskExecution = initialState.taskExecution;
		},
		setIsLoading: (state, action) => {
			state.isLoading = action.payload;
		},
		setStudentTasks: (state, action) => {
			state.studentTasks = action.payload;
		},
		setTeacherTasks: (state, action) => {
			state.teacherTasks = action.payload;
		},
		initTaskExecution: (state, action) => {
			state.taskExecution.taskId = action.payload;
			state.taskExecution.errorCount = 0;
		},
		finishTaskExecution: (state) => {
			state.taskExecution.isExecutionFinished = true;
		},
		incrementExecutingTaskErrors: (state) => {
			state.taskExecution.errorCount = ++state.taskExecution.errorCount;
		},
		setIsSavingResult: (state, action) => {
			state.taskExecution.isSavingResult = action.payload;
		},
	},
});

export const {
	resetState,
	resetTaskExecutionState,
	setIsLoading,
	setStudentTasks,
	setTeacherTasks,
	initTaskExecution,
	finishTaskExecution,
	incrementExecutingTaskErrors,
	setIsSavingResult,
} = tasksSlice.actions;

export default tasksSlice.reducer;

//Actions

export const getStudentTasks = () => (dispatch) => {
	dispatch(setIsLoading(true));
	getStudentTasksAPI()
		.then((res) => {
			dispatch(setStudentTasks(res.data));
		})
		.catch((err) => handleError(err))
		.finally(() => dispatch(setIsLoading(false)));
};

export const saveTaskExecutionResult = (results) => (dispatch, getState) => {
	if (!results) return;
	dispatch(setIsSavingResult(true));
	const fkeys = Object.keys(results).filter((key) => key !== DEFAULT_COLUMN);
	const standardOrResult = Object.fromEntries(
		fkeys.map((key) => [key, results[key].map((obj) => obj.content)])
	);
	const { taskId, errorCount } = getState().tasks.taskExecution;
	saveTaskExecutionResultAPI({
		taskId,
		result: {
			standardOrResult,
		},
		errorCount,
	})
		.then((res) => {
			dispatch(finishTaskExecution());
			dispatch(
				toggleModal({
					[AVAILABLE_MODALS.TASK_RESULT]: {
						open: true,
						data: res.data,
					},
				})
			);
		})
		.catch((err) => handleError(err))
		.finally(() => dispatch(setIsSavingResult(false)));
};
