import React, { useMemo } from 'react'
import { useSelector } from 'react-redux';
import Board from './DnD/Board'
import ExecutionResultModal from './ExecutionResultModal';

const TaskExecution = ({ taskId }) => {
    const studentTasks = useSelector(state => state.tasks.studentTasks);
    const requiredTask = useMemo(() => studentTasks.find(task => task.id === taskId), [taskId, studentTasks])

    const parsedTask = useMemo(() => {
        if (!requiredTask) return null;
        const standard = JSON.parse(requiredTask.standard);
        const description = JSON.parse(requiredTask.description);
        description.Blocks = description.Blocks.map(value => ({
            value,
            requiredPrefix: Object.keys(standard.StandardOrResult)
                .find(prefix => !!standard.StandardOrResult[prefix].find(prefVal => prefVal === value))
        }))
        return {
            ...requiredTask,
            description,
            standard,
            author: `${requiredTask.teacherFirstName} ${requiredTask.teacherLastName}`
        }
    }, [requiredTask])

    if (!parsedTask) return null;

    return parsedTask && (
        <>
            <ExecutionResultModal />
            <Board task={parsedTask} />
        </>
    )
}

export default TaskExecution
