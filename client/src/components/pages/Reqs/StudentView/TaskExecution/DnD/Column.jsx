import React from "react";
import { Droppable } from "react-beautiful-dnd";
import s from "./dnd.module.scss";
import clx from "classnames";
import DraggableCard from "./DraggableCard";

const Column = ({ prefix, elements, shouldValiate = false }) => {
	return (
		<div className={clx(shouldValiate ? s.columnToValidate : s.column)}>
			<h3 className={s.header}>{prefix}</h3>
			{Array.isArray(elements) && (
				<Droppable droppableId={`${prefix}`}>
					{(provided) => (
						<div
							{...provided.droppableProps}
							ref={provided.innerRef}
							className={s.draggableArea}
						>
							{elements.map((item, index) => (
								<DraggableCard
									key={item.id}
									item={item}
									index={index}
									currentPrefix={prefix}
								/>
							))}
							{provided.placeholder}
						</div>
					)}
				</Droppable>
			)}
		</div>
	);
};

export default Column;
