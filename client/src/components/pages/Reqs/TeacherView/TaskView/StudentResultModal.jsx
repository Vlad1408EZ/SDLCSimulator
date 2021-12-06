import React, { useCallback, useMemo } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router";

import { AVAILABLE_MODALS, toggleModal } from "../../../../../slices/uiSlice";
import {
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
} from "../../../../common/ui-parts/Dialog";
import Button, { BTN_TYPE } from "../../../../common/ui-parts/Button";
import Board from "../../StudentView/TaskExecution/DnD/Board";

import cs from "../../../../../scss/common.module.scss";

const getTaskConfig = (type) => {
	if (!type) return;

	const config = { header: "", limitations: null, readonly: true };
	if (type === "RequirementsTypeAndOrderByImportance")
		config.header =
			"Перетягуйте блоки у відповідні секції, які вважаєте вірними. Враховуйте важливість і порядок.";
	else if (type === "SystemsTypeAndFindMostImportant") {
		config.header =
			"Перетягуйте блоки у відповідні секції. Враховуйте лише найвіжливіші блоки(макс. 3 в секції).";
		config.limitations = {
			blocksInCol: 3,
		};
	}
	return config;
};

const ExecutionResultModal = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const modal = useSelector(
        (state) => state.ui.modals[AVAILABLE_MODALS.STUDENT_RESULT]
    );
    const { auth: { user }, tasks: { teacherTasks }} = useSelector((state) => state);

    const { open } = modal ? modal : { open: false };

    const closeModal = useCallback(() => {
        dispatch(toggleModal({
            [AVAILABLE_MODALS.STUDENT_RESULT]: {
                ...modal,
                open: false,
            },
        }));
    }, [dispatch, modal]);

    const target = useMemo(() => {
        if (!modal?.data) return null;

        const targetTask = teacherTasks.find((task) => task.id === modal.data.taskId);
        if (!targetTask) return null;

        const targetResult = targetTask.teachersTaskResults.find((result) => result.id == modal.data.resultId);
        return { result: targetResult, task: targetTask };
    }, [modal]);

    const parsedTask = useMemo(() => {
		if (!target) return null;
		const standard = JSON.parse(target.task.standard);
		const description = JSON.parse(target.task.description);
		description.Blocks = description.Blocks.map((value) => ({
			value,
			requiredPrefix: Object.keys(standard.StandardOrResult).find(
				(prefix) =>
					!!standard.StandardOrResult[prefix].find(
						(prefVal) => prefVal === value
					)
			),
		}));
		return {
			...target.task,
			description,
			standard,
			result: JSON.parse(target.result.result),
		};
	}, [target]);

    return (
        <Dialog open={open}>
            <DialogTitle onClose={closeModal}></DialogTitle>
            <DialogContent className={cs.padding30x50}>
                {parsedTask && <Board
					task={parsedTask}
                    disableInfo
                    isTeacherView
					config={getTaskConfig(parsedTask.type)}
				/>}
            </DialogContent>
            <DialogActions>
                <Button buttonType={BTN_TYPE.CANCEL} onClick={closeModal}>
                    Назад
                </Button>
            </DialogActions>
        </Dialog>
    );
};

export default ExecutionResultModal;
