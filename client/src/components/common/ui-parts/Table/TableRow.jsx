import React from 'react'
import s from "./table.module.scss";
import cs from "../../../../scss/common.module.scss";
import clx from "classnames";
import FlexBox from '../FlexBox';
import { Tooltip } from '@mui/material';

const TableRow = ({
    sectionName = "",
    showBorderBottom = true,
    removeSectionName = false,
    coloredBackground = false,
    rowWidth = 1000,
    children
}) => {
    return (
        <FlexBox>
            <FlexBox className={clx(
                s.tableRow,
                cs[`width${rowWidth}`],
                showBorderBottom && s.withBorderBottom,
                coloredBackground && s.coloredBackground
            )}>
                {!removeSectionName && (
                    (sectionName && sectionName.length > 22) ? (
                        <Tooltip title={sectionName}>
                            <div className={s.sectionName}>
                                {sectionName}
                            </div>
                        </Tooltip>
                    )
                        : (
                            <div className={s.sectionName}>
                                {sectionName}
                            </div>
                        )
                )}
                {children}
            </FlexBox>
        </FlexBox>
    )
}

export default TableRow
