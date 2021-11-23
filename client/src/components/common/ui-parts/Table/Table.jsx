import React from 'react'
import FlexBox from '../FlexBox'

const Table = ({ children }) => {
    return (
        <FlexBox flexDirection="column">{children}</FlexBox>
    )
}

export default Table
