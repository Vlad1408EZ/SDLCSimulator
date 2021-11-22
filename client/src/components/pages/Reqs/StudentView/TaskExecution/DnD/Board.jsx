import React, { useEffect, useMemo, useState } from 'react'
import { DragDropContext } from "react-beautiful-dnd";
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router';
import s from "./dnd.module.scss";
import cs from "../../../../../../scss/common.module.scss";
import FlexBox from '../../../../../common/ui-parts/FlexBox';
import Column from './Column';
import { CircularProgress, Paper } from '@mui/material';
import Button, { BTN_TYPE } from "../../../../../common/ui-parts/Button"
import { DEFAULT_COLUMN, initTaskExecution, resetTaskExecutionState, saveTaskExecutionResult } from '../../../../../../slices/tasksSlice';


const getItems = (blocks, prefix) => Array.isArray(blocks)
    ? blocks.map(b => ({
        id: `item-${Math.floor(Math.random() * 1000)}`,
        prefix,
        requiredPrefix: b.requiredPrefix,
        content: b.value
    }))
    : [];

const removeFromList = (list, index) => {
    const result = Array.from(list);
    const [removed] = result.splice(index, 1);
    return [removed, result];
};

const addToList = (list, index, element) => {
    const result = Array.from(list);
    result.splice(index, 0, element);
    return result;
};


const generateColumn = (columnTitles, blocks) =>
    columnTitles.reduce(
        (acc, listKey, index) => ({ ...acc, [listKey]: index === 0 ? getItems(blocks, listKey) : [] }),
        {}
    );


const Board = ({ task }) => {
    const list = useMemo(() => [DEFAULT_COLUMN, ...task.description.Columns], [task]);
    const initElementsState = useMemo(() => generateColumn(list, task.description.Blocks), [list, task]);

    const dispatch = useDispatch();
    const navigate = useNavigate();
    const [elements, setElements] = useState(initElementsState);
    const isExecutionFinished = useSelector(state => state.tasks.taskExecution.isExecutionFinished);

    console.log({ elements })
    const onDragEnd = (result) => {
        if (!result.destination || isExecutionFinished) {
            return;
        }
        const listCopy = { ...elements };

        const sourceList = listCopy[result.source.droppableId];
        const [removedElement, newSourceList] = removeFromList(
            sourceList,
            result.source.index
        );
        listCopy[result.source.droppableId] = newSourceList;
        const destinationList = listCopy[result.destination.droppableId];
        listCopy[result.destination.droppableId] = addToList(
            destinationList,
            result.destination.index,
            removedElement
        );

        setElements(listCopy);
    };

    const handleSubmit = () =>
        dispatch(saveTaskExecutionResult(elements));

    const handleClear = () => {
        // eslint-disable-next-line no-restricted-globals
        const shouldClear = confirm("Ви точно хочете очистити поточне рішення?");
        shouldClear && setElements(initElementsState);
    }

    const handleGoBack = () => navigate("/reqs");

    useEffect(() => {
        dispatch(initTaskExecution(task.id))

        return () => dispatch(resetTaskExecutionState());
    }, [])

    return (
        <DragDropContext onDragEnd={onDragEnd} >
            <div className={s.boardWrapper}>
                <Paper elevation={1} className={s.board}>
                    {elements
                        ? (<><FlexBox className={s.taskDescription} flexDirection="column">
                            <div>
                                <p className={s.header}>Перетягуйте блоки у відповідні секції, які вважаєте вірними. Враховуйте важливість і порядок. </p>
                                <p className={s.description}>
                                    <span className={s.value}>Складність:</span> {task.difficulty} <br />
                                    <span className={s.value}>Максимальний бал:</span> {task.maxGrade} <br />
                                    <span className={s.value}>Автор:</span> {task.author} <br />
                                </p>
                            </div>
                            <Column
                                elements={elements[list[0]]}
                                key={list[0]}
                                prefix={list[0]}
                            />
                        </FlexBox>
                            <FlexBox className={s.columns}>
                                {list.slice(1).map((listKey) => (
                                    <Column
                                        shouldValiate
                                        elements={elements[listKey]}
                                        key={listKey}
                                        prefix={listKey}
                                    />
                                ))}
                            </FlexBox></>)
                        : <CircularProgress />
                    }
                </Paper>
                <FlexBox justifyContent="spaceBetween" className={cs.marginTop20}>
                    <Button
                        onClick={handleGoBack}
                        buttonType={BTN_TYPE.CANCEL}
                    >
                        На головну
                    </Button>
                    {!isExecutionFinished && (
                        <FlexBox justifyContent="end" >
                            <Button
                                onClick={handleClear}
                                buttonType={BTN_TYPE.CANCEL}
                            >
                                Очистити
                            </Button>
                            <Button onClick={handleSubmit}>Зберегти результат</Button>
                        </FlexBox>
                    )}
                </FlexBox>

            </div>


        </DragDropContext>
    )
}

export default Board
