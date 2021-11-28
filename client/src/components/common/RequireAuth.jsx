import React from "react";
import { useSelector } from "react-redux";
import { Navigate, Outlet } from "react-router";

const RequireAuth = (props) => {
	const isAuthenticated = useSelector((state) => state.auth.isAuthenticated);
	if (!isAuthenticated) {
		return (
			<Navigate
				to={{
					pathname: "/",
					state: { from: props.location, visible: false },
				}}
			/>
		);
	}
	return <Outlet />;
};

export default RequireAuth;
