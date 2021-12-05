import network from "../network";

export const getStudentTasksAPI = () => network.get("/Task/StudentTasks");
export const getTeacherTasksAPI = () => network.get("/Task/TeacherTasks");

export const saveTaskExecutionResultAPI = (data) =>
	network.post("/TaskResult/SetTaskResult", data);
