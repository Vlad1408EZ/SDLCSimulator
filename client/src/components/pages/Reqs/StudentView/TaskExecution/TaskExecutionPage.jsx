import React, { useMemo } from "react";
import { useSelector } from "react-redux";
import Board from "./DnD/Board";
import ExecutionResultModal from "./ExecutionResultModal";

const getTaskConfig = (type, readonly = false) => {
	if (!type) return;

	const config = { header: "", limitations: null, readonly };
	if (type === "RequirementsTypeAndOrderByImportance")
		config.header =
			"Перетягуйте блоки у відповідні секції, які вважаєте вірними. Враховуйте важливість і порядок.";
	else if (type === "SystemsTypeAndFindMostImportant") {
		config.header =
			"Перетягуйте блоки у відповідні секції. Враховуйте лише найвіжливіші блоки(макс. 3 в секції).";
		config.limitations = {
			blocksInCol: 3,
		};
	}
	return config;
};

const TaskExecution = ({ taskId, readonly = false, execResult = null }) => {
	const studentTasks = useSelector((state) => state.tasks.studentTasks);
	const requiredTask = useMemo(
		() => studentTasks.find((task) => task.id === taskId),
		[taskId, studentTasks]
	);

	const parsedTask = useMemo(() => {
		if (!requiredTask) return null;
		const standard = JSON.parse(requiredTask.standard);
		const description = JSON.parse(requiredTask.description);
		description.Blocks = description.Blocks.map((value) => ({
			value,
			requiredPrefix: Object.keys(standard.StandardOrResult).find(
				(prefix) =>
					!!standard.StandardOrResult[prefix].find(
						(prefVal) => prefVal === value
					)
			),
		}));
		return {
			...requiredTask,
			description,
			standard,
			...(execResult ? { result: JSON.parse(execResult) } : {}),
			author: `${requiredTask.teacherFirstName} ${requiredTask.teacherLastName}`,
		};
	}, [requiredTask, execResult]);

	if (!parsedTask) return null;

	return (
		parsedTask && (
			<>
				<ExecutionResultModal />
				<Board
					task={parsedTask}
					config={getTaskConfig(parsedTask.type, readonly)}
				/>
			</>
		)
	);
};

export default TaskExecution;
