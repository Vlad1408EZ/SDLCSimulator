import { createSlice } from "@reduxjs/toolkit";
import {
	getStudentTasksAPI,
	getTeacherTasksAPI,
	getTasksTypesAPI,
	getTeacherGroupsAPI,
	saveTaskExecutionResultAPI,
	createTaskAPI,
} from "../api/taskAPI";
import { handleError } from "./notificationsSlice";
import { AVAILABLE_MODALS, toggleModal } from "./uiSlice";

export const DEFAULT_COLUMN = "Доступні варіанти";

export const FILTER_BY = {
	DEFAULT: { value: "default", label: "По замовчуванню" },
	DIFFICULTY: { value: "difficulty", label: "Складність" },
	TYPE: { value: "type", label: "Тип завдання" },
}

export const FILTER_OPTIONS = {
	DEFAULT: [],
	DIFFICULTY: [{ value: "HARD", label: "Hard" }, { value: "MEDIUM", label: "Medium" }, { value: "EASY", label: "Easy" }],
	TYPE: [{ value: "ORDER_BY_IMPORTANCE", label: "DnD-порядок і важливість" }, { value: "MOST_IMPORTANT", label: "DnD-найважливіші" },]
}

export const FILTER_OPTION_TYPE = {
	HARD: "Hard",
	MEDIUM: "Medium",
	EASY: "Easy",
	ORDER_BY_IMPORTANCE: "RequirementsTypeAndOrderByImportance",
	MOST_IMPORTANT: "SystemsTypeAndFindMostImportant",
}

const initialState = {
	isLoading: false,
	studentTasks: [],
	teacherTasks: [],
	taskFilterBy: FILTER_BY.DEFAULT.value,
	taskFilterOption: null,
	taskSearchValue: "",
	taskExecution: {
		taskId: null,
		errorCount: 0,
		isSavingResult: false,
		isExecutionFinished: false,
		isExecutionTimerRunning: false
	},
	tasksTypes: [],
	teacherGroups: [],
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
		setIsExecutionTimerRunning: (state, action) => {
			state.taskExecution.isExecutionTimerRunning = action.payload;
		},
		setFilterBy: (state, action) => {
			state.taskFilterBy = action.payload;
		},
		setFilterOption: (state, action) => {
			state.taskFilterOption = action.payload;
		},
		setTaskSearchValue: (state, action) => {
			state.taskSearchValue = action.payload;
		},
		setTasksTypes: (state, action) => {
			state.tasksTypes = action.payload;
		},
		setTeacherGroups: (state, action) => {
			state.teacherGroups = action.payload;
		},
		setTaskCreated: (state, action) => {
			state.isTaskCreated = action.payload;
		},
	},
});

export const {
	resetState,
	resetTaskExecutionState,
	setIsLoading,
	setStudentTasks,
	setTeacherTasks,
	setTasksTypes,
	initTaskExecution,
	finishTaskExecution,
	incrementExecutingTaskErrors,
	setIsSavingResult,
	setIsExecutionTimerRunning,
	setFilterBy,
	setFilterOption,
	setTaskSearchValue,
	setTeacherGroups,
	setTaskCreated,
} = tasksSlice.actions;

export default tasksSlice.reducer;

//Actions

export const getStudentTasks = () => (dispatch) => {
	dispatch(setIsLoading(true));
	getStudentTasksAPI()
		.then((res) => {
			dispatch(setStudentTasks(res.data));
		})
		.catch((err) => handleError(err, dispatch))
		.finally(() => dispatch(setIsLoading(false)));
};

export const getTeacherTasks = () => (dispatch) => {
	dispatch(setIsLoading(true));
	getTeacherTasksAPI()
		.then((res) => {
			dispatch(setTeacherTasks(res.data));
		})
		.catch((err) => handleError(err))
		.finally(() => dispatch(setIsLoading(false)));
};

export const getTasksTypes = () => (dispatch) => {
	dispatch(setIsLoading(true));
	getTasksTypesAPI()
		.then((res) => {
			dispatch(setTasksTypes(res.data));
		})
		.catch((err) => handleError(err))
		.finally(() => dispatch(setIsLoading(false)));
};

export const getTeacherGroups = () => (dispatch) => {
	dispatch(setIsLoading(true));
	getTeacherGroupsAPI()
		.then((res) => {
			dispatch(setTeacherGroups(res.data));
		})
		.catch((err) => handleError(err))
		.finally(() => dispatch(setIsLoading(false)));
};

export const saveTaskExecutionResult = (results) => (dispatch, getState) => {
	if (!results) return;
	dispatch(setIsSavingResult(true));
	dispatch(saveTaskExecutionResult(false));
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
		.catch((err) => handleError(err, dispatch))
		.finally(() => dispatch(setIsSavingResult(false)));
};

export const createTask = (task) => (dispatch) => {
	if (!task) return;

	createTaskAPI(task)
		.then(() => {
			dispatch(setTaskCreated(true));
		})
		.catch((err) => handleError(err, dispatch))
};


export const findStudentTasksBySearchValue = (value) => (dispatch) => {
	dispatch(setIsLoading(true));
	dispatch(setTaskSearchValue(value));
	getStudentTasksAPI({
		params: new URLSearchParams(`Topic=${value}`)
	})
		.then(res => {
			dispatch(setStudentTasks(res.data));
		})
		.catch((err) => handleError(err, dispatch))
		.finally(() => dispatch(setIsLoading(false)));
}