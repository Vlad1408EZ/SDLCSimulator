const getTaskConfig = ({ type, readonly }) => {
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

const tasksTypeToEnumMapper = {
	RequirementsTypeAndOrderByImportance: 1,
	SystemsTypeAndFindMostImportant: 2,
}

const getTasksTypeName = ({ type }) => {
	switch (type) {
		case "RequirementsTypeAndOrderByImportance":
			return "Тип вимог і порядок за важливістю"
		case "SystemsTypeAndFindMostImportant":
			return "Тип системи та найголовніше"
		default:
			return "";
	}
}

const getDifficultyName = ({ difficulty }) => {
	const dNames = [
		'Easy',
		'Medium',
		'Hard',
	];

	return dNames[difficulty];
}

export {
	getTaskConfig,
	tasksTypeToEnumMapper,
	getTasksTypeName,
	getDifficultyName,
};
