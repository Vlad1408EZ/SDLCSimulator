import React, { useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import { AVAILABLE_MODALS, toggleModal } from "../../../../../slices/uiSlice";
import {
	Dialog,
	DialogActions,
	DialogContent,
	DialogTitle,
} from "../../../../common/ui-parts/Dialog";
import Button, { BTN_TYPE } from "../../../../common/ui-parts/Button";
import { useNavigate } from "react-router";

const ExecutionResultModal = () => {
	const dispatch = useDispatch();
	const navigate = useNavigate();
	const modal = useSelector(
		(state) => state.ui.modals[AVAILABLE_MODALS.TASK_RESULT]
	);

	const { open, data } = modal ? modal : { open: false, data: null };

	const onClose = useCallback(
		() =>
			dispatch(
				toggleModal({
					[AVAILABLE_MODALS.TASK_RESULT]: false,
				})
			),
		[dispatch]
	);

	const onHome = useCallback(() => navigate("/reqs"), [navigate]);

	return (
		<Dialog open={open}>
			<DialogTitle onClose={onClose}>Результат виконання завдання</DialogTitle>
			{data && (
				<DialogContent>
					<p>Кількість помилок: {data.errorCount}</p>
					<p>Результат у відсотках: {data.percentage}%</p>
					<p>Заліковий бал: {data.finalMark}</p>
				</DialogContent>
			)}
			<DialogActions>
				<Button buttonType={BTN_TYPE.CANCEL} onClick={onClose}>
					Повернутися до завдання
				</Button>
				<Button onClick={onHome}>На головну</Button>
			</DialogActions>
		</Dialog>
	);
};

export default ExecutionResultModal;
