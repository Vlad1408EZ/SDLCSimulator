import React from "react";
import clx from "classnames";
import s from "./table.module.scss";
import cs from "../../../../scss/common.module.scss";
import { Tooltip } from "@mui/material";

const TableCell = ({ value, cellWidth, isFirstCell = false }) => {
	const newVal = typeof value === "number"
		? value.toFixed(2)
		: Array.isArray(value)
			? value.join(", ")
			: !value ? "-/-" : value;
	return (
		<div className={clx(s.tableCell, isFirstCell && s.first, cellWidth ? cs[`width${cellWidth}`] : s.flex1)}>
			<Tooltip title={newVal}>
				<span>{newVal}</span>
			</Tooltip>
		</div>
	);
};

export default TableCell;
