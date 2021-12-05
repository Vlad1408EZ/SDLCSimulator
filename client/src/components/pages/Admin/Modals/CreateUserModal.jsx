import React, { useCallback, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import clx from "classnames";
import { IconButton, InputAdornment } from "@mui/material";
import {
    EyeOff,
    Eye
} from "react-feather";

import cs from "../../../../scss/common.module.scss";
import s from "./modals.module.scss";
import { AVAILABLE_MODALS, toggleModal } from "../../../../slices/uiSlice";
import {
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
} from "../../../common/ui-parts/Dialog";
import Button, { BTN_TYPE } from "../../../common/ui-parts/Button";
import { OutlinedInput } from "../../../common/ui-parts/Input";
import MultipleSelectCheckmarks from "../../../common/ui-parts/MultiSelect";
import CSelect from "../../../common/ui-parts/Select";
import FlexBox from "../../../common/ui-parts/FlexBox";
import { createUser, getAllGroups } from "../../../../slices/adminSlice";

const defaultFormState = {
    firstName: "",
    lastName: "",
    password: "",
    email: "@lpnu.ua",
    role: 0,
    groups: []
};

const getUserRoleForEmail = (role) => {
    if (role === 0) return "пз";
    if (role === 1) return "викладач";
    if (role === 2) return "адмін";
    else return role;
}

const CreateUserModal = () => {
    const dispatch = useDispatch();
    const modal = useSelector(
        (state) => state.ui.modals[AVAILABLE_MODALS.CREATE_USER]
    );
    const { roles, groups } = useSelector((state) => state.admin.userCreation);

    const { open } = modal ? modal : { open: false };
    const [userData, setUserData] = useState(defaultFormState);
    const [passwordVisible, setPasswordVisible] = useState(false);

    const handleDataChange = (key, value) => {

        setUserData(prev => {
            const email = key === "firstName"
                ? `${value.toLowerCase()}.${prev.lastName.toLowerCase()}.${getUserRoleForEmail(prev.role)}@lpnu.ua`
                : key === "lastName"
                    ? `${prev.firstName.toLowerCase()}.${value.toLowerCase()}.${getUserRoleForEmail(prev.role)}@lpnu.ua`
                    : key === "role"
                        ? `${prev.firstName.toLowerCase()}.${prev.lastName.toLowerCase()}.${getUserRoleForEmail(value)}@lpnu.ua`
                        : prev.email;
            return {
                ...prev,
                email,
                [key]: value,
            }
        })
    }

    const handleShowPasswordClick = () =>
        setPasswordVisible(passVisible => !passVisible);

    const handleMouseDownPassword = (event) => event.preventDefault();

    const handleSaveUser = () => {
        dispatch(createUser(userData));
    }

    const closeModal = useCallback(() => {
        dispatch(toggleModal({
            [AVAILABLE_MODALS.CREATE_USER]: false,
        }));
    }, [dispatch]);


    useEffect(() => {
        open && dispatch(getAllGroups());
        !open && setUserData(defaultFormState);
    }, [open, dispatch])

    return (
        <Dialog open={open}>
            <DialogTitle onClose={closeModal}>
                Створення користувача
            </DialogTitle>
            <DialogContent className={clx(cs.padding30x50, s.container)}>
                <OutlinedInput
                    label="Ім'я"
                    value={userData.firstName}
                    onChange={(e) => handleDataChange("firstName", e.target.value)}
                    className={cs.marginBottom20}
                />
                <OutlinedInput
                    label="Прізвище"
                    value={userData.lastName}
                    onChange={(e) => handleDataChange("lastName", e.target.value)}
                    className={cs.marginBottom20}
                />
                <OutlinedInput
                    label="Пароль"
                    type={passwordVisible ? 'text' : 'password'}
                    value={userData.password}
                    onChange={(e) => handleDataChange("password", e.target.value)}
                    className={cs.marginBottom20}
                    InputProps={{
                        endAdornment: <InputAdornment position="end">
                            <IconButton
                                aria-label="toggle password visibility"
                                onClick={handleShowPasswordClick}
                                onMouseDown={handleMouseDownPassword}
                                edge="end"
                            >
                                {passwordVisible ? <EyeOff /> : <Eye />}
                            </IconButton>
                        </InputAdornment>
                    }}
                />
                <OutlinedInput
                    label="Пошта"
                    value={userData.email}
                    onChange={(e) => handleDataChange("email", e.target.value)}
                    className={cs.marginBottom20}
                />
                <FlexBox alignItems="center" justifyContent="spaceBetween">
                    <CSelect
                        label="Роль"
                        value={userData.role}
                        variant="outlined"
                        onChange={(value) => handleDataChange("role", value)}
                        options={roles}
                    />
                    <MultipleSelectCheckmarks
                        label="Група"
                        options={groups}
                        multiple={userData.role === 1} //is teacher
                        onChange={(value) => handleDataChange("groups", value)}
                    />
                </FlexBox>

            </DialogContent>
            <DialogActions>
                <Button buttonType={BTN_TYPE.CANCEL} onClick={closeModal}>
                    Скасувати
                </Button>
                <Button onClick={handleSaveUser}>Збергети</Button>
            </DialogActions>
        </Dialog>
    );
};

export default CreateUserModal;
