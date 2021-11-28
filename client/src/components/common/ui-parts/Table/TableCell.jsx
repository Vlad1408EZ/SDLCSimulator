import React from "react";
import clx from "classnames";
import s from "./table.module.scss";

const TableCell = ({ value, isFirstCell = false }) => {
	const newVal =
		typeof value === "number" ? value.toFixed(2) : !value ? "-/-" : value;
	return (
		<div className={clx(s.tableCell, isFirstCell && s.first)}>
			<span>{newVal}</span>
		</div>
	);
};

export default TableCell;
