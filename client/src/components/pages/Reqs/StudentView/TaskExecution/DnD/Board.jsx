import React, { useEffect, useMemo, useState } from "react";
import { DragDropContext } from "react-beautiful-dnd";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router";
import s from "./dnd.module.scss";
import cs from "../../../../../../scss/common.module.scss";
import FlexBox from "../../../../../common/ui-parts/FlexBox";
import Column from "./Column";
import { CircularProgress, Paper } from "@mui/material";
import Button, { BTN_TYPE } from "../../../../../common/ui-parts/Button";
import {
	DEFAULT_COLUMN,
	initTaskExecution,
	resetTaskExecutionState,
	saveTaskExecutionResult,
} from "../../../../../../slices/tasksSlice";
import { resetState as resetModalsState } from "../../../../../../slices/uiSlice";
import { enqueueSnackbar, variants } from "../../../../../../slices/notificationsSlice";
import { AVAILABLE_MODALS, toggleModal } from "../../../../../../slices/uiSlice";
import { useTimer } from "../../../../../common/hooks/useTimer";

const getItems = (blocks, prefix) =>
	Array.isArray(blocks)
		? blocks.map((b, i) => ({
			id: `item-${i}`,
			prefix,
			requiredPrefix: b.requiredPrefix,
			content: b.value,
		}))
		: [];

const removeFromList = (list, index) => {
	const result = Array.from(list);
	const [removed] = result.splice(index, 1);
	return [removed, result];
};

const addToList = (list, index, element) => {
	const result = Array.from(list);
	result.splice(index, 0, element);
	return result;
};

const generateColumn = (columnTitles, blocks) =>
	columnTitles.reduce(
		(acc, listKey, index) => ({
			...acc,
			[listKey]: index === 0 ? getItems(blocks, listKey) : [],
		}),
		{}
	);

const Board = ({ task, config, isTeacherView = false, disableInfo = false }) => {
	const columns = useMemo(
		() => [DEFAULT_COLUMN, ...task.description.Columns],
		[task]
	);
	const initElementsState = useMemo(
		() => generateColumn(columns, task.description.Blocks),
		[columns, task]
	);

	const dispatch = useDispatch();
	const navigate = useNavigate();
	const [elements, setElements] = useState(initElementsState);
	const [initReadonlyExec, setInitReadonlyExec] = useState(false);
	const isExecutionFinished = useSelector(
		(state) => state.tasks.taskExecution.isExecutionFinished
	);
	const isExecutionTimerRunning = useSelector(
		(state) => state.tasks.taskExecution.isExecutionTimerRunning
	);

	const onDragEnd = (result) => {
		if (!result.destination) return;

		if (isExecutionFinished || config?.readonly) {
			dispatch(enqueueSnackbar("Ви не можете це змінювати", variants.ERROR));
			return;
		}

		const listCopy = { ...elements };

		const sourceList = listCopy[result.source.droppableId];
		const [removedElement, newSourceList] = removeFromList(
			sourceList,
			result.source.index
		);
		listCopy[result.source.droppableId] = newSourceList;
		const destinationList = listCopy[result.destination.droppableId];

		//add limitations on column insert
		if (
			destinationList.length >= config?.limitations?.blocksInCol &&
			result.destination.droppableId !== DEFAULT_COLUMN
		)
			return;

		listCopy[result.destination.droppableId] = addToList(
			destinationList,
			result.destination.index,
			removedElement
		);

		setElements(listCopy);
	};

	const handleSubmitExecResult = () => dispatch(saveTaskExecutionResult(elements));

	const handleClear = () => {
		// eslint-disable-next-line no-restricted-globals
		const shouldClear = confirm("Ви точно хочете очистити поточне рішення?");
		shouldClear && setElements(initElementsState);
	};

	const handleGoBack = () => navigate(-1);

	const [timer, startTimer, clearTimer] = useTimer(task.taskTime, handleSubmitExecResult);


	useEffect(() => {
		isExecutionTimerRunning && startTimer();
	}, [isExecutionTimerRunning])


	useEffect(() => {
		if (!config.readonly || !task.result || !task.result.StandardOrResult || initReadonlyExec) return;
		const result = Object.entries(task.result.StandardOrResult)
			.reduce((ac, [key, valsArr]) => ({
				...ac,
				[key]: valsArr.map(value => ({
					id: `item-${Math.floor(Math.random() * 1000)}`,
					requiredPrefix: task.standard.StandardOrResult[key].includes(value) ? key : "Incorrect",
					content: value
				}))
			}), {});
		setElements(result);
		setInitReadonlyExec(true);
	}, [task, config, initReadonlyExec])

	useEffect(() => {
		isExecutionFinished && clearTimer();
	}, [isExecutionFinished])

	useEffect(() => {
		dispatch(initTaskExecution(task.id));
		if (task.taskTime && !config?.readonly) dispatch(toggleModal({
			[AVAILABLE_MODALS.TASK_TIME_WARNING]: {
				open: true,
			}
		}));

		return () => {
			dispatch(resetTaskExecutionState());
			dispatch(resetModalsState());
		}
	}, []);

	return (
		<DragDropContext onDragEnd={onDragEnd}>
			<div className={!disableInfo ? s.boardWrapper : ''}>
				<Paper elevation={1} className={s.board}>
					{elements ? (
						<>
							{!disableInfo && <FlexBox className={s.taskDescription} flexDirection="column">
								<div>
									<p className={s.header}>
										{config?.header ?? "Опис завдання не знайдено"}
									</p>
									<p className={s.description}>
										<span className={s.value}>Складність:</span>{" "}
										{task.difficulty} <br />
										<span className={s.value}>Максимальний бал:</span>{" "}
										{task.maxGrade} <br />
										<span className={s.value}>Автор:</span> {task.author} <br />
									</p>
									{!isTeacherView && task?.taskTime && (
										<pre className={s.timeData}>
											<span className={s.value}>Часу залишилось:</span><br />
											Години: {timer.time.h}<br />
											Хвилини: {timer.time.m}<br />
											Секунди: {timer.time.s}<br />
										</pre>
									)}

								</div>
								{!isTeacherView && <Column
									elements={elements[columns[0]]}
									key={columns[0]}
									prefix={columns[0]}
								/>}
							</FlexBox>}
							<FlexBox className={s.columns}>
								{columns.slice(1).map((listKey) => (
									<Column
										shouldValiate
										elements={elements[listKey]}
										key={listKey}
										prefix={listKey}
									/>
								))}
							</FlexBox>
						</>
					) : (
						<CircularProgress />
					)}
				</Paper>
				{!disableInfo && <FlexBox justifyContent="spaceBetween" className={cs.marginTop20}>
					<Button onClick={handleGoBack} buttonType={BTN_TYPE.CANCEL}>
						Повернутися назад
					</Button>
					{(!isExecutionFinished && !config?.readonly) && (
						<FlexBox justifyContent="end">
							<Button onClick={handleClear} buttonType={BTN_TYPE.CANCEL}>
								Очистити
							</Button>
							<Button wrapperClassName={cs.marginLeft30} onClick={handleSubmitExecResult}>
								Зберегти результат
							</Button>
						</FlexBox>
					)}
				</FlexBox>}
			</div>
		</DragDropContext>
	);
};

export default Board;
