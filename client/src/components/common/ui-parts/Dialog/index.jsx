import _ from "lodash";
import React from "react";
import clx from "classnames";
import {
	Dialog as MuiDialog,
	DialogContent as MuiDialogContent,
	DialogActions as MuiDialogActions,
} from "@mui/material";
import cs from "../../../../scss/common.module.scss";
import s from "./dialog.module.scss";
import FlexBox from "../FlexBox";
import { X } from "react-feather";

export const Dialog = (props) => {
	const newProps = {
		classes: {
			root: s.dialogRoot,
			paper: s.dialogPaper,
			...(props.classes || {}),
		},
		PaperProps: { square: true },
		maxWidth: "md",
		..._.omit(props, ["classes"]),
	};

	return <MuiDialog {...newProps} />;
};

export const DialogTitle = (props) => {
	const { children, onClose } = props;
	const newProps = {
		className: s.dialogTitle,
		justifyContent: "center",
		alignItems: "center",
		..._.omit(props, ["children", "onClose"]),
	};
	const handleClose = () => onClose && onClose();

	return (
		<FlexBox {...newProps}>
			{children}
			<X onClick={handleClose} className={s.closeIcon} />
		</FlexBox>
	);
};

export const DialogContent = (props) => {
	const { children, className } = props;
	const newProps = {
		className: clx(
			cs.padding70x60,
			cs.paddingBottom0,
			s.dialogContent,
			className
		),
		flexDirection: "column",
		..._.omit(props, ["children", "className"]),
	};
	return (
		<FlexBox {...newProps} clone>
			<MuiDialogContent>{children}</MuiDialogContent>
		</FlexBox>
	);
};

export const DialogActions = (props) => {
	const { children } = props;
	const newProps = {
		className: cs.padding40x60,
		justifyContent: "spaceBetween",
		disableSpacing: true,
		..._.omit(props, ["children"]),
	};
	return (
		<FlexBox {...newProps} clone>
			<MuiDialogActions>{children}</MuiDialogActions>
		</FlexBox>
	);
};
