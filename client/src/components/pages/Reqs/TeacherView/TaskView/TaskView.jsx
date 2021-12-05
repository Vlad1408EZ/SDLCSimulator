import React, { useMemo } from "react";
import { useSelector, useDispatch} from "react-redux";

import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';

import Board from "../../StudentView/TaskExecution/DnD/Board";
import Button from "../../../../common/ui-parts/Button";
import FlexBox from "../../../../common/ui-parts/FlexBox";

import { AVAILABLE_MODALS, toggleModal } from "../../../../../slices/uiSlice";

import StudentResultModal from "./StudentResultModal";

import cs from "../../../../../scss/common.module.scss";

const getTaskConfig = (type) => {
	if (!type) return;

	const config = { header: "", limitations: null, readonly: true };
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

const TaskView = ({ taskId }) => {
    const { auth: { user }, tasks: { teacherTasks }} = useSelector((state) => state);
	const dispatch = useDispatch();

	const requiredTask = useMemo(
		() => teacherTasks.find((task) => task.id === taskId),
		[taskId, teacherTasks]
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
			result: standard,
			author: `${user.firstName} ${user.lastName}`,
		};
	}, [requiredTask]);

	const clickDetailViewHandler = ({ taskId: tId, resultId: rId }) => {
		dispatch(toggleModal({
			[AVAILABLE_MODALS.STUDENT_RESULT]: {
				open: true,
				data: { taskId: tId, resultId: rId },
			},
		}));
	};

	if (!parsedTask) return null;

	return (
		parsedTask && (
			<>
				<StudentResultModal />
				<Board
					task={parsedTask}
                    isTeacherView
					config={getTaskConfig(parsedTask.type)}
				/>
				<FlexBox
					flexDirection="column"
					alignItems="center"
					className={cs.marginTop20}
				>
					<TableContainer sx={{ maxWidth: 1200 }} component={Paper}>
						<Table aria-label="simple table">
							<TableHead>
							<TableRow>
								<TableCell>Студент</TableCell>
								<TableCell align="right">Група</TableCell>
								<TableCell align="right">Кількість помилок</TableCell>
								<TableCell align="right">Результат у відсотках</TableCell>
								<TableCell align="right">Заліковий бал</TableCell>
								<TableCell align="right"></TableCell>
							</TableRow>
							</TableHead>
							<TableBody>
							{requiredTask.teachersTaskResults.map((studentsResult) => (
								<TableRow
								key={studentsResult.id}
								sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
								>
								<TableCell component="th" scope="row">
									{studentsResult.studentFirstName}&nbsp;{studentsResult.studentLastName}
								</TableCell>
								<TableCell align="right">{studentsResult.groupName}</TableCell>
								<TableCell align="right">{(studentsResult.errorCount)}</TableCell>
								<TableCell align="right" >{(studentsResult.percentage * 100).toFixed(2)}</TableCell>
								<TableCell align="right">{(studentsResult.finalMark)}</TableCell>
								<TableCell align="right">
									<div style={{ display: 'inline-table'}}>
										<Button onClick={() => clickDetailViewHandler({ taskId, resultId: studentsResult.id })}>Деталі</Button>
									</div>
								</TableCell>
								</TableRow>
							))}
							</TableBody>
						</Table>
					</TableContainer>
				</FlexBox>
			</>
		)
	);
}

export default TaskView;
