import React, { useEffect, useState } from 'react'
import { IconButton, InputBase, Paper } from '@mui/material'
import { Search } from "react-feather"
import cs from "../../../../scss/common.module.scss";
import useDebounce from '../../hooks/useDebounce';

const SearchInput = ({
    initValue,
    onChange,
    placeholder = "Пошук..",
    searchDelay = 400
}) => {
    const [value, setValue] = useState(initValue);
    const debouncedValue = useDebounce(value, searchDelay);

    const handleSearchChange = (e) => setValue(e.target.value);

    useEffect(() => {
        if (debouncedValue !== initValue)
            onChange(debouncedValue);
    }, [debouncedValue, initValue])

    return (
        <Paper className={cs.paddingLeft10}>
            <InputBase
                placeholder={placeholder}
                value={value}
                onChange={handleSearchChange}
            />
            <IconButton aria-label="search">
                <Search />
            </IconButton>
        </Paper>
    )
}


const areEqual = (prev, next) =>
    next.initValue === prev.initValue

export default React.memo(SearchInput, areEqual);
