import React from "react";
import { Input } from "../Input";
import DateTimePicker, { DatePicker } from "../Picker";

const generateInputAdapter = (InputComponent) => ({ input, meta, ...rest }) => (
  <InputComponent
    {...input}
    {...rest}
    error={(rest.shouldValidate || meta.touched) && meta.invalid}
    helperText={(rest.shouldValidate || meta.touched) && meta.error}
    onChange={(event) => input.onChange(event.target.value)}
  />
);

const generatePickerAdapter = (PickerComponent) => ({
  input,
  meta,
  ...rest
}) => (
  <PickerComponent
    {...input}
    {...rest}
    error={(rest.shouldValidate || meta.touched) && meta.invalid}
    helperText={(rest.shouldValidate || meta.touched) && meta.error}
    value={input.value || null}
    onChange={(value) => input.onChange(value)}
  />
);

export const InputAdapter = generateInputAdapter(Input);
export const DateTimePickerAdapter = generatePickerAdapter(DateTimePicker);
export const DatePickerAdapter = generatePickerAdapter(DatePicker);
