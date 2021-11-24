import React, { useMemo } from 'react'
import { HeadCell, TableRow } from '.'

const getCellName = (fileName) => {
    if (!fileName) return;
    if (fileName === "errorCount") return "Кількість помилок";
    if (fileName === "finalMark") return "Заліковий бал";
    if (fileName === "percentage") return "Результат у відсотках";
    else return fileName;
}

const getHeaderCells = (availableFiles) =>
    availableFiles.map(fileName => getCellName(fileName));


const TableHeader = ({ cells }) => {
    const headers = useMemo(() => getHeaderCells(cells), [cells]);

    return (
        <TableRow showBorderBottom={false}>
            {headers.map((header, ind) => (
                <HeadCell key={ind} title={header} />
            ))}
        </TableRow>
    )
}

export default TableHeader
