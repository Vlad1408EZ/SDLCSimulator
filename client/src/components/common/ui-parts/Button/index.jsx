import React, { useMemo, useState } from "react";
import _ from "lodash";
import s from "./button.module.scss";
import clx from "classnames";
import { CircularProgress } from "@mui/material";

export const BTN_TYPE = {
	OK: "Ok",
	CANCEL: "Cancel",
};

const Button = (props) => {
	const [loading, setLoading] = useState(false);
	const {
		onClick,
		disabled,
		withLoader,
		debounceTime = 200,
		className: customClassName,
		wrapperClassName,
		buttonType = BTN_TYPE.OK,
	} = props;
	const onLoadingButtonClick = useMemo(() => {
		if (!onClick) return undefined;

		const onClickWithLoader = () =>
			Promise.resolve()
				.then(() => {
					setLoading(true);
					return onClick();
				})
				.finally(() => {
					setLoading(false);
				});
		const internalOnClick = withLoader ? onClickWithLoader : onClick;
		return _.debounce(internalOnClick, debounceTime, {
			leading: true,
			trailing: false,
		});
	}, [onClick, withLoader, debounceTime]);
	const newProps = {
		disabled: loading || disabled,
		onClick: onLoadingButtonClick,
		..._.omit(props, [
			"loading",
			"disabled",
			"debounceTime",
			"onClick",
			"className",
			"wrapperClassName",
			"buttonType",
		]),
	};

	return (
		<div className={clx(s.wrapper, wrapperClassName)}>
			<button
				type="button"
				className={clx(`button${buttonType}`, customClassName)}
				{...newProps}
			/>
			{loading && <CircularProgress className={s.buttonProgress} size={24} />}
		</div>
	);
};

export default Button;
