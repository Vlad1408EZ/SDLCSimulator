import React from 'react'
import clx from "classnames";
import s from "./table.module.scss";

const HeadCell = ({ title, isFirstCell = false }) => {
    return (
        <span className={clx(s.headerCell, isFirstCell && s.first)}>{title}</span>
    )
}

export default HeadCell
