import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate, useLocation } from "react-router";

import { getTeacherTasks } from "../../../../slices/tasksSlice";

import Loading from "../../../common/ui-parts/Loading";

import Task from "../common/Task";
import TaskList from "../common/TaskList";
import TaskView from "./TaskView";

import cs from "../../../../scss/common.module.scss";

const TeacherView = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const location = useLocation();
    const { isLoading, teacherTasks } = useSelector((state) => state.tasks);

    const [taskId, setTaskId] = useState(null);

    useEffect(() => {
        dispatch(getTeacherTasks());
    }, []);

    useEffect(() => {
		if (location.search) {
			const taskId = new URLSearchParams(location.search).get("taskId");
			taskId && setTaskId(+taskId);
		} else if (!location.search && taskId !== null) setTaskId(null);
	}, [location.search, taskId]);

    const handleTaskClick = (id) => navigate(`?taskId=${id}`);

    if (isLoading) return <Loading />;

    if (taskId) return <TaskView taskId={taskId}/>

    return <div>
        <TaskList listLength={teacherTasks.length}>
            {teacherTasks.map((task) => (
                <Task
                    key={task.id}
                    {...task}
                    onClick={() => handleTaskClick(task.id)}
                />
            ))}
        </TaskList>
    </div>
};

export default TeacherView;
