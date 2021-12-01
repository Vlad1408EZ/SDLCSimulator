import React, { useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import cs from "../../../../../../scss/common.module.scss";
import { AVAILABLE_MODALS, toggleModal } from "../../../../../../slices/uiSlice";
import {
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
} from "../../../../../common/ui-parts/Dialog";
import Button, { BTN_TYPE } from "../../../../../common/ui-parts/Button";
import { useNavigate } from "react-router";
import { setIsExecutionTimerRunning } from "../../../../../../slices/tasksSlice";

const ExecutionResultModal = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const modal = useSelector(
        (state) => state.ui.modals[AVAILABLE_MODALS.TASK_TIME_WARNING]
    );

    const { open } = modal ? modal : { open: false };

    const handleStartExec = useCallback(() => {
        dispatch(toggleModal({
            [AVAILABLE_MODALS.TASK_TIME_WARNING]: false,
        }));
        dispatch(setIsExecutionTimerRunning(true));
    }, [dispatch]);

    const handleGoBack = useCallback(() => navigate("/reqs"), [navigate]);

    return (
        <Dialog open={open}>
            <DialogTitle>Увага</DialogTitle>
            <DialogContent className={cs.padding30x50}>
                <p>Час виконання завдання обмежений.</p>
                <p>Як тільки час сплине, завдання автоматично відправиться на перевірку.</p>
            </DialogContent>
            <DialogActions>
                <Button buttonType={BTN_TYPE.CANCEL} onClick={handleGoBack}>
                    На головну
                </Button>
                <Button onClick={handleStartExec}>Розпочати виконання</Button>
            </DialogActions>
        </Dialog>
    );
};

export default ExecutionResultModal;
