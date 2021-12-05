import React, { useEffect, useState } from "react";
import { Paper } from "@mui/material";
import s from "./account.module.scss";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router";
import DefaultUser from "../../../assets/images/default-user.png";
import FlexBox from "../../common/ui-parts/FlexBox";
import {
    TableHeader,
    Table,
    TableRow,
    TableCell,
    TablePagination,
} from "../../common/ui-parts/Table";
import { getStudentTasks } from "../../../slices/tasksSlice";
import { getUserRole } from "../../common/utils";

const UserAccount = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const user = useSelector((state) => state.auth.user);
    const studentTasks = useSelector((state) => state.tasks.studentTasks);
    const [paginationOffset, setPaginationOffset] = useState([0, 5]);

    const taskLength = studentTasks.length;

    const uRole = getUserRole(user.role);

    const handleRowClick = (taskId, taskResult) =>
        navigate(`/reqs?taskId=${taskId}`, { state: { readonlyExec: true, result: taskResult } });

    useEffect(() => dispatch(getStudentTasks()), []);

    return (
        <Paper elevation={1} className={s.container}>
            <FlexBox>
                <img src={DefaultUser} className={s.avatar} alt="default user" />
                <div className={s.userData}>
                    <p>
                        {`${uRole}: `}
                        <b>
                            {user.firstName} {user.lastName}
                        </b>
                    </p>
                    <p>
                        Пошта: <b>{user.email}</b>
                    </p>
                </div>
            </FlexBox>
            {user.role !== "Admin" && (
                <>
                    <h4 className={s.statsHeader}>Сатистика виконання завдань</h4>
                    <Table>
                        <TableHeader
                            cells={["Спроба", "Кількість помилок", "Результат у відсотках", "Заліковий бал"]}
                        />
                        {studentTasks.slice(paginationOffset).map((task) =>
                            !!task.studentsTaskResults.length ? (
                                <div key={task.id}>
                                    <TableRow
                                        sectionName={task.topic}
                                        coloredBackground
                                        showBorderBottom={false}
                                    />
                                    {task.studentsTaskResults.map((taskResult, index) => (
                                        <TableRow key={taskResult.id} onSeeMoreClick={() => handleRowClick(task.id, taskResult.result)}>
                                            <TableCell value={`№ ${index + 1}`} />
                                            <TableCell value={taskResult.errorCount} />
                                            <TableCell value={taskResult.percentage * 100} />
                                            <TableCell value={taskResult.finalMark} />
                                        </TableRow>
                                    ))}
                                </div>
                            ) : null
                        )}
                        {taskLength > 5 && (
                            <TablePagination
                                itemsCount={taskLength}
                                setOffset={setPaginationOffset}
                            />
                        )}
                    </Table>
                </>
            )}
        </Paper>
    );
};

export default UserAccount;
