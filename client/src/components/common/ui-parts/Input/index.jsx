import _ from "lodash";
import React from "react";
import clx from "classnames";
import MuiInput from "@mui/material/Input";
import s from "./input.module.scss";
import { TextField } from "@mui/material";

export const Input = (props) => {
  const newProps = {
    color: "secondary",
    notched: "false",
    classes: {
      root: clx(
        props.classes && props.classes.root ? props.classes.root : null,
        s.root
      ),
      ...(props.classes ? _.omit(props.classes, ["root"]) : {}),
    },
    ..._.omit(props, ["classes"]),
  };

  return <MuiInput  {...newProps} />;
};

export const InputStandard = (props) => {
  const newProps = {
    notched: "false",
    classes: {
      root: clx(
        s.rootStandard,
        props.classes && props.classes.root ? props.classes.root : null
      ),
      ...(props.classes ? _.omit(props.classes, ["root"]) : {}),
    },
    ..._.omit(props, ["classes"]),
  };

  return <MuiInput {...newProps} />;
};
