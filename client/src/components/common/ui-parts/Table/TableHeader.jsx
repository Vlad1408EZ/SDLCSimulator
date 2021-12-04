import React from "react";
import { HeadCell, TableRow } from ".";


const TableHeader = ({ rowWidth, cells = [] }) => {

	return (
		<TableRow rowWidth={rowWidth} showBorderBottom={false}>
			{cells.map((header, ind) => (
				<HeadCell key={ind} title={header} />
			))}
		</TableRow>
	);
};

export default TableHeader;
