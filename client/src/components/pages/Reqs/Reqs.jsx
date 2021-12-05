import React from "react";
import { useSelector } from 'react-redux'

import StudentView from "./StudentView/StudentView";
import TeacherView from "./TeacherView";

const Reqs = () => {
	const { user } = useSelector((state) => state.auth);

	return (
		<div>
			{user.role == "Student" && <StudentView />}
			{user.role == "Teacher" && <TeacherView />}
		</div>
	);
};

export default Reqs;
