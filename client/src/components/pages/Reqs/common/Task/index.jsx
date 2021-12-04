import React from "react";
import s from "./task.module.scss";
import cs from "../../../../../scss/common.module.scss";
import clx from "classnames";
import Paper from "@mui/material/Paper";
import Button from "../../../../common/ui-parts/Button";
import FlexBox from "../../../../common/ui-parts/FlexBox";

const SECONDS_IN_MIN = 60;

const Task = ({ onClick, difficulty, topic, maxGrade, taskTime }) => {
	const taskExecTime = taskTime ? taskTime / SECONDS_IN_MIN : "-/-";

	const handleClick = () => onClick && onClick();

	return (
		<Paper elevation={1} className={s.container}>
			<FlexBox flexDirection="column">
				<span className={s.taskTitle}>{topic}</span>
				<p className={s.taskDescription}>
					Складність: {difficulty}
					<br />
					Максимальний бал: {maxGrade}
					<br />
					Час на виконання: {taskExecTime} хвилин
				</p>
				<Button
					onClick={handleClick}
					wrapperClassName={clx(cs.flex, cs.justifyContentEnd)}
				>
					Переглянути
				</Button>
			</FlexBox>
		</Paper>
	);
};

export default Task;
