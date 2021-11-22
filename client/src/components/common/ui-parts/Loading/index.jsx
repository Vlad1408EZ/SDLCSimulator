import React from 'react'
import clx from "classnames"
import s from "./loading.module.scss"
import { CircularProgress } from '@mui/material';

const Loading = ({
    inner,
    color,
    position,
    style,
    containerStyle,
    className,
}) => {
    const spinnerStyle = {
        width: "2rem",
        height: "2rem",
        position: position || "relative",
        left: "0",
        right: "0",
        top: "0",
        bottom: "0",
        margin: "auto",
        color: color || "#47D8C2",
    };

    const innerSpinnerStyle = {
        width: "25px",
        height: "25px",
        margin: "0 10px",
        color: color || "#47D8C2",
    };

    const resultStyle = inner
        ? { ...innerSpinnerStyle, ...style }
        : { ...spinnerStyle, ...style };

    return (
        <div
            className={clx(s.spinnerContainer, className)}
            style={containerStyle}
        >
            <CircularProgress style={resultStyle} />
        </div>
    );
}

export default Loading
