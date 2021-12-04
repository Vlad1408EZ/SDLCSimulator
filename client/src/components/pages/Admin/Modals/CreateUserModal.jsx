import React, { useCallback, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router";
import clx from "classnames";

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
import { getAllGroups } from "../../../../slices/adminSlice";


const CreateUserModal = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const modal = useSelector(
        (state) => state.ui.modals[AVAILABLE_MODALS.CREATE_USER]
    );
    const { roles, groups } = useSelector((state) => state.admin.userCreation);

    const { open } = modal ? modal : { open: false };
    const [userData, setUserData] = useState({
        name: "",
        surname: "",
        email: "@lpnu.ua",
        role: 0
    })

    const handleDataChange = (key, value) => {
        setUserData(prev => ({
            ...prev,
            [key]: value
        }))
    }

    const handleSaveUser = () => {
        //call save func
        closeModal();
    }

    const closeModal = useCallback(() => {
        dispatch(toggleModal({
            [AVAILABLE_MODALS.CREATE_USER]: false,
        }));
    }, [dispatch]);


    useEffect(() => {
        open && dispatch(getAllGroups());
    }, [open, dispatch])

    return (
        <Dialog open={open}>
            <DialogTitle onClose={closeModal}>Створення користувача</DialogTitle>
            <DialogContent className={clx(cs.padding30x50, s.container)}>
                <OutlinedInput label="Прізвище" value={userData.surname} className={cs.marginBottom20} />
                <OutlinedInput label="Ім'я" value={userData.name} className={cs.marginBottom20} />
                <OutlinedInput label="Пошта" value={userData.email} className={cs.marginBottom20} />
                <FlexBox alignItems="center" justifyContent="spaceBetween">
                    <CSelect
                        label="Роль"
                        value={userData.role}
                        variant="outlined"
                        // className={cs.marginRight10}
                        onChange={(value) => handleDataChange("role", value)}
                        options={roles}
                    />
                    <MultipleSelectCheckmarks label="Група" options={groups} />
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
