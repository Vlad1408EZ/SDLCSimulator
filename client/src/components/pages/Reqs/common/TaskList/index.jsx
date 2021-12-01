import React from "react";
import FlexBox from "../../../../common/ui-parts/FlexBox";

const TaskList = ({ listLength = 0, children }) => {
	return (

		<FlexBox
			flexDirection="column"
			alignItems="center"
		>
			{listLength === 0 ? (
				<h4>В базі не знайдено жодного завдання</h4>
			) : (
				children
			)}
		</FlexBox>
	);
};

export default TaskList;
