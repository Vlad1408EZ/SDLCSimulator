import React from "react";
import { NavLink } from "react-router-dom";
import FlexBox from "../common/ui-parts/FlexBox";

const ErrorPage = () => (
	<FlexBox flexDirection="column" justifyContent="center" alignItems="center">
		<h1 className>404</h1>
		<h3 className>Упс, ми не знайшли цю сторінку. Скоріше за все, вона не існує.</h3>
		<NavLink to="/home">На головну</NavLink>
	</FlexBox>
);

export default ErrorPage;
