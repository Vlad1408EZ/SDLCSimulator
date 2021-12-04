import network from "../network";

export const getStudentTasksAPI = (config) =>
	network.get(`/Task/StudentTasks`, config);

export const saveTaskExecutionResultAPI = (data) =>
	network.post("/TaskResult/SetTaskResult", data);
