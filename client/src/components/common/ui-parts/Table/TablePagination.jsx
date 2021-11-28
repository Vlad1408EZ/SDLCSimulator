import React, { useMemo } from "react";
import { TablePagination } from "@mui/material";
import cs from "../../../../scss/common.module.scss";

const CustomTablePagination = ({ itemsCount = 0, setOffset }) => {
	const [page, setPage] = React.useState(0);
	const [rowsPerPage, setRowsPerPage] = React.useState(5);

	const rowsPerPageOptions = useMemo(
		() => [5, 10, 15, 35, itemsCount].sort((a, b) => a - b),
		[itemsCount]
	);

	const handleChangePage = (event, newPage) => {
		setPage(newPage);
		const start = rowsPerPage * newPage;
		setOffset && setOffset([start, start + rowsPerPage]);
	};

	const handleChangeRowsPerPage = (event) => {
		setRowsPerPage(parseInt(event.target.value, 10));
		setPage(0);
	};
	return (
		<TablePagination
			component="div"
			className={cs.marginRight50}
			count={itemsCount}
			page={page}
			onPageChange={handleChangePage}
			rowsPerPage={rowsPerPage}
			rowsPerPageOptions={rowsPerPageOptions}
			onRowsPerPageChange={handleChangeRowsPerPage}
		/>
	);
};

export default CustomTablePagination;
