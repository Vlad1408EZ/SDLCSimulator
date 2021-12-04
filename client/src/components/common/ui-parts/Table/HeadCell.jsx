import React from "react";
import clx from "classnames";
import s from "./table.module.scss";
import cs from "../../../../scss/common.module.scss";

const HeadCell = ({ title, cellWidth, isFirstCell = false }) => {
	return (
		<span className={clx(s.headerCell, isFirstCell && s.first, cellWidth ? cs[`width${cellWidth}`] : s.flex1)}>{title}</span>
	);
};

export default HeadCell;
