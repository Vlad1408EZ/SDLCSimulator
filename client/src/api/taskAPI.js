import network from "../network";

export const getStudentTasksAPI = (config) => network.get("/Task/StudentTasks", config);
export const getTeacherTasksAPI = () => network.get("/Task/TeacherTasks");
export const getTasksTypesAPI = () => network.get("/Type");
export const getTeacherGroupsAPI = () => network.get('/Group/TeacherGroups');

export const saveTaskExecutionResultAPI = (data) =>
	network.post("/TaskResult/SetTaskResult", data);

export const createTaskAPI = (data) => network.post("/Task/CreateTask", data);
