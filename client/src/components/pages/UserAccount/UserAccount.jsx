import React, { useEffect, useState } from 'react'
import { Paper } from '@mui/material'
import s from "./account.module.scss"
import { useDispatch, useSelector } from 'react-redux'
import DefaultUser from "../../../assets/images/default-user.png"
import FlexBox from '../../common/ui-parts/FlexBox'
import { TableHeader, Table, TableRow, TableCell, TablePagination } from '../../common/ui-parts/Table'
import { getStudentTasks } from '../../../slices/tasksSlice'

const getUserRole = (role) => {
    if (role === "Student") return "Студент";
    if (role === "Teacher") return "Викладач";
}

const UserAccount = () => {
    const dispatch = useDispatch();
    const user = useSelector(state => state.auth.user);
    const studentTasks = useSelector(state => state.tasks.studentTasks);
    const [paginationOffset, setPaginationOffset] = useState([0, 5]);

    const taskLength = studentTasks.length;

    useEffect(() => dispatch(getStudentTasks()), []);

    return (
        <Paper elevation={1} className={s.container}>

            <FlexBox>
                <img src={DefaultUser} className={s.avatar} alt="default user" />
                <div className={s.userData}>
                    <p>{`${getUserRole(user.role)}: `}<b>{user.firstName} {user.lastName}</b></p>
                    <p>Пошта: <b>{user.email}</b></p>
                </div>
            </FlexBox>
            <h4 className={s.statsHeader}>Сатистика виконання завдань</h4>
            <Table>
                <TableHeader cells={["errorCount", "percentage", "finalMark"]} />
                {studentTasks.slice(paginationOffset)
                    .map(task => (
                        !!task.studentsTaskResults.length ? (
                            <div key={task.id} >
                                <TableRow sectionName={task.topic} coloredBackground showBorderBottom={false} />
                                {task.studentsTaskResults.map(taskResult => (
                                    <TableRow key={taskResult.id}>
                                        <TableCell value={taskResult.errorCount} />
                                        <TableCell value={taskResult.percentage} />
                                        <TableCell value={taskResult.finalMark} />
                                    </TableRow>
                                ))}
                            </div>
                        ) : null
                    ))}
                {taskLength > 5 && (
                    <TablePagination
                        itemsCount={taskLength}
                        setOffset={setPaginationOffset}
                    />
                )}
            </Table>
        </Paper>
    )
}

export default UserAccount
