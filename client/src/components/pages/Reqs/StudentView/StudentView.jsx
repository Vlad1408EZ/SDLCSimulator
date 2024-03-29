import React, { useCallback, useEffect, useMemo, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useNavigate } from "react-router";
import clx from "classnames";

import cs from "../../../../scss/common.module.scss";
import CSelect from "../../../common/ui-parts/Select";
import {
	FILTER_BY,
	FILTER_OPTIONS,
	FILTER_OPTION_TYPE,
	findStudentTasksBySearchValue,
	getStudentTasks,
	setFilterBy,
	setFilterOption,
} from "../../../../slices/tasksSlice";
import Loading from "../../../common/ui-parts/Loading";
import Task from "../common/Task";
import TaskList from "../common/TaskList";
import TaskExecution from "./TaskExecution/TaskExecutionPage";
import FlexBox from "../../../common/ui-parts/FlexBox";
import SearchInput from "../../../common/ui-parts/SearchInput";

const getFilteredTasks = (taskFilterOption, taskFilterBy, tasks) => {
	if (!taskFilterOption) return tasks;
	if (taskFilterBy === FILTER_BY.DEFAULT.value) return tasks;
	if (taskFilterBy === FILTER_BY.DIFFICULTY.value)
		return tasks.filter(
			(el) => el[taskFilterBy] === FILTER_OPTION_TYPE[taskFilterOption]
		);
	if (taskFilterBy === FILTER_BY.TYPE.value)
		return tasks.filter(
			(el) => el[taskFilterBy] === FILTER_OPTION_TYPE[taskFilterOption]
		);
};

const StudentView = () => {
	const dispatch = useDispatch();
	const navigate = useNavigate();
	const location = useLocation();
	const { isLoading, studentTasks, taskFilterBy, taskFilterOption, taskSearchValue } =
		useSelector((state) => state.tasks);
	const [taskId, setTaskId] = useState(null);

	const filteredStudentTasks = useMemo(
		() => getFilteredTasks(taskFilterOption, taskFilterBy, studentTasks),
		[taskFilterOption, taskFilterBy, studentTasks]
	);

	const handleTaskClick = (id) => navigate(`?taskId=${id}`);

	const handleSearchValueChange = useCallback((newValue) => {
		dispatch(findStudentTasksBySearchValue(newValue));
	}, [])

	const handleFilterByChange = (newValue) => dispatch(setFilterBy(newValue));

	const handleFilterOptionChange = (newValue) =>
		dispatch(setFilterOption(newValue));

	useEffect(() => {
		if (location.search) {
			const taskId = new URLSearchParams(location.search).get("taskId");
			taskId && setTaskId(+taskId);
		} else if (!location.search && taskId !== null) setTaskId(null);
	}, [location.search, taskId]);

	useEffect(() => {
		dispatch(getStudentTasks());
	}, []);


	return taskId ? (
		<TaskExecution
			taskId={taskId}
			readonly={location.state?.readonlyExec}
			execResult={location.state?.result}
		/>
	) : (
		<div className={clx(cs.width680, cs.marginAutoHorizontal)}>
			<FlexBox alignItems="end" justifyContent="spaceBetween" className={clx(cs.marginTop50, cs.marginBottom20)}>
				<SearchInput
					initValue={taskSearchValue}
					onChange={(val) => handleSearchValueChange(val)}
					placeholder="Пошук по назві.." />
				<FlexBox>
					<CSelect
						label="Фільтрувати"
						value={taskFilterBy}
						className={cs.marginRight10}
						onChange={handleFilterByChange}
						options={Object.values(FILTER_BY)}
					/>
					<CSelect
						label="Опції фільтру"
						value={taskFilterOption}
						onChange={handleFilterOptionChange}
						options={FILTER_OPTIONS[taskFilterBy.toUpperCase()]}
					/>
				</FlexBox>
			</FlexBox>
			{isLoading ? (
				<Loading />
			) : (
				<TaskList listLength={filteredStudentTasks.length}>
					{filteredStudentTasks.map((task) => (
						<Task
							key={task.id}
							{...task}
							onClick={() => handleTaskClick(task.id)}
						/>
					))}
				</TaskList>
			)}
		</div>
	);
};

export default StudentView;
