import React from 'react';
import renderer from 'react-test-renderer';

import TableCell from "../TableCell"

it("Table cell with number has 2 values after 0", () => {
    const tree = renderer
        .create(<TableCell value={123.1454} />)
        .toJSON();
    expect(tree).toMatchSnapshot();
})

it("Table cell with empty string equal to \"-/-\"", () => {
    const tree = renderer
        .create(<TableCell value="" />)
        .toJSON();
    expect(tree).toMatchSnapshot();
})