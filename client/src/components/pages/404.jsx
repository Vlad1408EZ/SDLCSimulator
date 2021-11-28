import React from "react";
import { NavLink } from "react-router-dom";
import FlexBox from "../common/ui-parts/FlexBox";

const ErrorPage = () => (
	<FlexBox flexDirection="column" justifyContent="center" alignItems="center">
		<h1 className>404</h1>
		<h3 className>Sorry but we could not find this page. It does not exist!</h3>
		<NavLink to="/home">Go Home!</NavLink>
	</FlexBox>
);

export default ErrorPage;
