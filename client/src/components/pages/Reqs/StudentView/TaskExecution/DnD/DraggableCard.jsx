import React, { useEffect } from "react";
import clx from "classnames";
import s from "./dnd.module.scss";
import { Draggable } from "react-beautiful-dnd";
import { Paper } from "@mui/material";
import {
	DEFAULT_COLUMN,
	incrementExecutingTaskErrors,
} from "../../../../../../slices/tasksSlice";
import { useDispatch } from "react-redux";

const DraggableCard = ({ item, index, currentPrefix }) => {
	const dispatch = useDispatch();
	const isValid = item.requiredPrefix === currentPrefix;

	useEffect(() => {
		if (
			currentPrefix !== item.requiredPrefix &&
			currentPrefix !== DEFAULT_COLUMN
		)
			dispatch(incrementExecutingTaskErrors());
	}, [currentPrefix, item, dispatch]);

	return (
		<Draggable draggableId={item.id} index={index}>
			{(provided, snapshot) => {
				return (
					<Paper
						className={clx(s.card, isValid ? s.valid : s.error)}
						ref={provided.innerRef}
						snapshot={snapshot}
						{...provided.draggableProps}
						{...provided.dragHandleProps}
					>
						<span>{item.content}</span>
					</Paper>
				);
			}}
		</Draggable>
	);
};

export default DraggableCard;
