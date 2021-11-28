import React, { useState } from "react";
import {
	LocalizationProvider,
	DateTimePicker as MuiDateTimePicker,
	DatePicker as MuiDatePicker,
} from "@mui/lab";
import DateAdapter from "@mui/lab/AdapterMoment";

function DateTimePicker(props) {
	const newProps = {
		views: ["date", "hours"],
		autoOk: true,
		hideTabs: true,
		minutesStep: 60,
		label: "",
		inputVariant: "standard",
		...props,
	};

	return (
		<LocalizationProvider dateAdapter={DateAdapter}>
			<MuiDateTimePicker {...newProps} />
		</LocalizationProvider>
	);
}

export function DatePicker(props) {
	const [selectedDate, handleDateChange] = useState(null);

	const newProps = {
		autoOk: true,
		label: "",
		inputVariant: "standard",
		value: selectedDate,
		onChange: handleDateChange,
		...props,
	};

	return (
		<LocalizationProvider dateAdapter={DateAdapter}>
			<MuiDatePicker {...newProps} />
		</LocalizationProvider>
	);
}

export default DateTimePicker;
