import React, { useEffect, useMemo, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { useLocation, useNavigate } from 'react-router'
import { getStudentTasks } from '../../../../slices/tasksSlice';
import Loading from '../../../common/ui-parts/Loading';
import Task from '../common/Task';
import TaskList from '../common/TaskList';
import TaskExecution from './TaskExecution/TaskExecutionPage';

const StudentView = () => {
    const dispatch = useDispatch()
    const navigate = useNavigate();
    const location = useLocation();
    const { isLoading, studentTasks } = useSelector(state => state.tasks);
    const [taskId, setTaskId] = useState(null);


    useEffect(() => {
        if (location.search) {
            const taskId = new URLSearchParams(location.search).get('taskId');
            taskId && setTaskId(+taskId);
        } else if (!location.search && taskId !== null) setTaskId(null);
    }, [location.search, taskId])

    useEffect(() => {
        dispatch(getStudentTasks());
    }, [])

    const handleTaskClick = (id) => {
        navigate(`?taskId=${id}`);
    }

    if (isLoading) return <Loading />

    return (taskId
        ? <TaskExecution taskId={taskId} />
        : <TaskList listLength={studentTasks.length}>
            {studentTasks.map(task => (
                <Task key={task.id} {...task} onClick={() => handleTaskClick(task.id)} />
            ))}
        </TaskList>
    )
}

export default StudentView
