import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router";
import { DragDropContext, Droppable, Draggable } from "react-beautiful-dnd";

import { useTheme } from "@mui/material/styles";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import Grid from "@mui/material/Grid";
import TextField from "@mui/material/TextField";
import OutlinedInput from "@mui/material/OutlinedInput";
import Container from '@mui/material/Container';

import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemText from "@mui/material/ListItemText";

import AddIcon from "@mui/icons-material/Add";
import IconButton from "@mui/material/IconButton";
import DeleteIcon from "@mui/icons-material/Delete";

import Fab from "@mui/material/Fab";

import TaskBlock from './TaskBlock';

import Loading from "../../common/ui-parts/Loading";

import {
	createTask,
	getTasksTypes,
	getTeacherGroups,
	setTaskCreated,
} from "../../../slices/tasksSlice";
import {
	tasksTypeToEnumMapper,
	getTasksTypeName,
	getDifficultyName,
} from "../../../helpers/tasks.helper";

const AVAILABLE_ITEMS_ID = "availableItems";
const ITEM_HEIGHT = 48;
const ITEM_PADDING_TOP = 8;
const MenuProps = {
  PaperProps: {
    style: {
      maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
      width: 250,
    },
  },
};

const reorder = (list, startIndex, endIndex) => {
	const result = Array.from(list);
	const [removed] = result.splice(startIndex, 1);
	result.splice(endIndex, 0, removed);
	return result;
};

const moveBetween = ({
	list1,
	list2,
	source,
	destination,
  }) => {
	const newFirst = Array.from(list1.values);
	const newSecond = Array.from(list2.values);

	const moveFrom = source.droppableId === list1.id ? newFirst : newSecond;
	const moveTo = moveFrom === newFirst ? newSecond : newFirst;

	const [moved] = moveFrom.splice(source.index, 1);
	moveTo.splice(destination.index, 0, moved);

	return {
		list1: {
			...list1,
			values: newFirst,
		},
		list2: {
			...list2,
			values: newSecond,
		},
	};
}

function getStyles(name, personName, theme) {
	return {
	  fontWeight:
		personName.indexOf(name) === -1
		  ? theme.typography.fontWeightRegular
		  : theme.typography.fontWeightMedium,
	};
}

const ReqsCreate = () => {
	const theme = useTheme();
	const dispatch = useDispatch();
	const navigate = useNavigate();
	const { isLoading, tasksTypes, teacherGroups, isTaskCreated } = useSelector((state) => state.tasks);

	const [taskName, setTaskName] = useState("");
	const [selectedTaskType, setSelectedTaskType] = useState(null);
	const [selectedDifficulty, setSelectedDifficulty] = useState(null);
	const [selectedGroups, setSelectedGroups] = useState([]);

	const [items, setItems] = useState([]);
	const [itemName, setItemName] = useState("");

	const [blocks, setBlocks] = useState([]);
	const [blockName, setBlockName] = useState("");

	useEffect(() => {
		dispatch(getTasksTypes());
		dispatch(getTeacherGroups());
	}, []);

	const handleDifficultyChange = (event) => {
		setSelectedDifficulty(event.target.value);
	};
	const handleItemAdd = () => {
		setItems([...items, { name: itemName, id: crypto.randomUUID() }]);
		setItemName("");
	};
	const handleItemDelete = (itemIndex) => {
		const i = [...items];
		i.splice(itemIndex, 1);
		setItems(i);
	};
	const handleTaskNameChange = (event) => {
		setTaskName(event.target.value);
	};
	const handleTypeChange = (event) => {
		setSelectedTaskType(event.target.value);
	};
	const handleBlockNameChange = (event) => {
		setBlockName(event.target.value);
	};
	const handleBlockAdd = () => {
		const newBlock = {
			name: blockName,
			items: [],
			itemName: '',
		};
		setBlocks([
			...blocks,
			newBlock,
		]);
		setBlockName("");
	};
	const handleBlockItemDelete = ({ blockIndex, itemIndex }) => {
		const b = [...blocks];
		b[blockIndex].items.splice(itemIndex, 1);
		setBlocks(b);
	};
	const handleBlockDelete = (blockIndex) => {
		const b = [...blocks];
		b.splice(blockIndex, 1);
		setBlocks(b);
	};
	const handleGroupSelect = (event) => {
		const {
		  target: { value },
		} = event;
		setSelectedGroups(
		  typeof value === "string" ? value.split(",") : value,
		);
	};

	const onDragEnd = (result) => {
		const { destination, source } = result;

		if (!destination) return;

		if (source.droppableId === destination.droppableId) {
			if (source.droppableId === AVAILABLE_ITEMS_ID) {
				setItems(reorder(items, source.index, destination.index));
			} else {
				const blockIndex = blocks.findIndex(({ name }) => name === source.droppableId);
				blocks[blockIndex].items = reorder(blocks[blockIndex].items, source.index, destination.index);
				setBlocks(blocks);
			}
			return;
		}

		const sourceBlockIndex = blocks.findIndex(({ name }) => name === source.droppableId);
		const destinationBlockIndex = blocks.findIndex(({ name }) => name === destination.droppableId);

		const firstList = source.droppableId === AVAILABLE_ITEMS_ID ? items : blocks[sourceBlockIndex].items;
		const secondList = destination.droppableId === AVAILABLE_ITEMS_ID ? items : blocks[destinationBlockIndex].items;

		const { list1, list2 } = moveBetween({
			list1: {
				id: source.droppableId,
				values: firstList,
			},
			list2: {
				values: secondList,
			},
			source,
			destination,
		});

		if (source.droppableId === AVAILABLE_ITEMS_ID) {
			setItems(list1.values);
		} else {
			blocks[sourceBlockIndex].items = list1.values;
			setBlocks(blocks);
		}

		if (destination.droppableId === AVAILABLE_ITEMS_ID) {
			setItems(list2.values);
		} else {
			blocks[destinationBlockIndex].items = list2.values;
			setBlocks(blocks);
		}
	};

	const handleCreateTask = () => {
		const task = {
			topic: taskName,
			type: tasksTypeToEnumMapper[selectedTaskType],
			difficulty: selectedDifficulty + 1,
			description: {
				columns: blocks.map(({ name }) => name),
				blocks: [
					...blocks.flatMap(({ items }) => items.map(({ name }) => name)),
					...items.map(({ name }) => name),
				],
			},
			standard: {
				standardOrResult: blocks.reduce((acc, block) => ({
					[block.name]: block.items.map(({ name }) => name),
					...acc
				}), {}),
			},
			groupNames: selectedGroups,
		};
		dispatch(createTask(task));
	};

	useEffect(() => {
		if (isTaskCreated) {
			dispatch(setTaskCreated(null));
			navigate('/reqs');
		}
	}, [isTaskCreated]);

	const createTaskValid = () => {
		if (
			!taskName.length || selectedDifficulty === null
			|| selectedTaskType === null || !selectedGroups.length
			|| !blocks.length
		) return false;

		if(blocks.find(({ items }) => !items.length)) return false;

		const allItems = [
			...blocks.flatMap(({ items }) => items.map(({ name }) => name)),
			...items.map(({ name }) => name),
		];

		if (!allItems.length) return false;

		return true;
	};


	if (isLoading) return <Loading />;

	return (
		<Container maxWidth="lg">
			<DragDropContext onDragEnd={onDragEnd}>
				<Grid container spacing={2} sx={{ marginTop: '20px' }}>
					<Grid item xs={12}>
						<FormControl fullWidth>
							<TextField
								id="outlined-basic"
								label="Назва завдання"
								variant="outlined"
								value={taskName}
								onChange={handleTaskNameChange}
							/>
						</FormControl>
					</Grid>
					<Grid item xs={4}>
						<FormControl fullWidth>
							<Select
								value={selectedDifficulty}
								displayEmpty
								renderValue={(selected) => {
									if (selected === null) {
										return <em>Складність</em>;
									}
									return getDifficultyName({ difficulty: selected })
								}}
								onChange={handleDifficultyChange}
								inputProps={{ "aria-label": "Without label" }}
							>
								<MenuItem disabled value="">
									<em>Складність</em>
								</MenuItem>
								{[...new Array(3).keys()].map((difficulty) => (
									<MenuItem value={difficulty}>{getDifficultyName({ difficulty })}</MenuItem>
								))}
							</Select>
						</FormControl>
					</Grid>
					<Grid item xs={4}>
						<FormControl fullWidth>
							<Select
								value={selectedTaskType}
								displayEmpty
								renderValue={(selected) => {
									if (!selected) {
									return <em>Тип завдання</em>;
									}
									return getTasksTypeName({ type: selected });
								}}
								onChange={handleTypeChange}
								inputProps={{ "aria-label": "Without label" }}
							>
								<MenuItem disabled value="">
									<em>Тип завдання</em>
								</MenuItem>
								{tasksTypes.map((taskType) => (
									<MenuItem value={taskType}>{getTasksTypeName({ type: taskType })}</MenuItem>
								))}
							</Select>
						</FormControl>
					</Grid>
					<Grid item xs={4}>
						<FormControl fullWidth>
							<Select
								multiple
								displayEmpty
								value={selectedGroups}
								onChange={handleGroupSelect}
								input={<OutlinedInput />}
								renderValue={(selected) => {
									if (selected.length === 0) {
										return <em>Обрати групи</em>;
									}
									return selected.join(", ");
								}}
								MenuProps={MenuProps}
								inputProps={{ "aria-label": "Without label" }}
							>
								<MenuItem disabled value="">
									<em>Обрати групи</em>
								</MenuItem>
								{teacherGroups.map(({ groupName, id }) => (
									<MenuItem
										key={id}
										value={groupName}
										style={getStyles(groupName, selectedGroups, theme)}
									>
										{groupName}
									</MenuItem>
								))}
							</Select>
						</FormControl>
					</Grid>
					<Grid item xs={12}>
						<Droppable droppableId={AVAILABLE_ITEMS_ID}>
							{(providedDroppable) => (
								<div
									{...providedDroppable.droppableProps}
									ref={providedDroppable.innerRef}
								>
									<List
										sx={{ width: "100%", bgcolor: "background.paper" }}
										component="nav"
										aria-labelledby="nested-list-subheader"
										subheader={
											<ListItem>
												Доступні варіанти
											</ListItem>
										}
									>
										{items.map((item, itemIndex) => (
											<Draggable index={itemIndex} draggableId={`${AVAILABLE_ITEMS_ID}-${item.name}-${item.id}`} key={item.id}>
												{(providedDraggable) => (
													<div
														{...providedDraggable.draggableProps}
														{...providedDraggable.dragHandleProps}
														ref={providedDraggable.innerRef}
														style={providedDraggable.draggableProps.style}
													>
														<ListItem
															secondaryAction={
																<IconButton aria-label="delete" edge="end" size="small" onClick={() => handleItemDelete(itemIndex)}>
																	<DeleteIcon fontSize="inherit" />
																</IconButton>
															}
														>
															<ListItemText primary={item.name} />
														</ListItem>
													</div>
												)}
											</Draggable>
										))}
										{providedDroppable.placeholder}
										<ListItem
											secondaryAction={
												<IconButton aria-label="delete" edge="end" size="small" onClick={handleItemAdd}>
													<AddIcon fontSize="inherit" />
												</IconButton>
											}
										>
											<TextField
												fullWidth
												hiddenLabel
												value={itemName}
												onChange={(event) => setItemName(event.target.value)}
												id="filled-hidden-label-small"
												variant="filled"
												size="small"
											/>
										</ListItem>
									</List>
								</div>
							)}
						</Droppable>
					</Grid>
					<Grid item xs={9}>
						<FormControl fullWidth>
							<TextField
								id="outlined-basic"
								label="Назва блоку"
								variant="outlined"
								value={blockName}
								onChange={handleBlockNameChange}
							/>
						</FormControl>
					</Grid>
					<Grid item xs={3}>
						<FormControl fullWidth>
							<Fab
								onClick={handleBlockAdd}
								disabled={!blockName.length || blocks.findIndex(({ name }) => name === blockName) != -1}
								color="primary"
								variant="extended"
								aria-label="add"
							>
								<AddIcon sx={{ mr: 1 }} />
								Блок
							</Fab>
						</FormControl>
					</Grid>
					{blocks.map((block, blockIndex) => (
						<Grid item xs={4} key={block.name}>
							<Droppable droppableId={block.name}>
								{(providedDroppable) => (
									<div
										{...providedDroppable.droppableProps}
										ref={providedDroppable.innerRef}
									>
										<TaskBlock
											block={block}
											blockIndex={blockIndex}
											onDelete={handleBlockDelete}
											onItemDelete={handleBlockItemDelete}
											dndPlaceholder={providedDroppable.placeholder}
										/>
									</div>
								)}
							</Droppable>
						</Grid>
					))}
					<Grid item xs={12}>
						<FormControl fullWidth>
							<Fab
								onClick={handleCreateTask}
								disabled={!createTaskValid()}
								color="primary"
								variant="extended"
								aria-label="add"
							>
								<AddIcon sx={{ mr: 1 }} />
								Створити
							</Fab>
						</FormControl>
					</Grid>
				</Grid>
			</DragDropContext>
		</Container>
	);
};

export default ReqsCreate;
