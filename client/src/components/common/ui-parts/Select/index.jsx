import React from 'react'
import Select from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';
import { FormControl, InputLabel } from '@mui/material';

//options = [{value: val, label: "String"}]
const CSelect = ({
    label = "",
    value,
    onChange,
    options = []
}) => {
    const handleChange = (event) => {
        onChange(event.target.value);
    };

    return (
        <FormControl variant="standard" sx={{ m: 1, minWidth: 120 }}>
            <InputLabel id="select-standard-label">{label}</InputLabel>
            <Select
                labelId="custom-select-labelId"
                id={`custom-select-id$-${label}`}
                value={value}
                onChange={handleChange}
                label={label}
            >{options.map(option => (
                <MenuItem key={option.value} value={option.value}>
                    {option.label}
                </MenuItem>
            ))}
            </Select>
        </FormControl>

    )
}

export default CSelect
