import React, { useCallback } from "react";
import {
	Home,
	LogOut,
	Code,
	Activity,
	Box as BoxIcon,
	Grid,
	Clipboard,
	User,
} from "react-feather";
import { Link, useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import Box from "@mui/material/Box";
// import { logout } from "../../actions/login";
import s from "./styles.module.scss";
import { StylesProvider } from "@mui/styles";
import { toggleSidebar } from "../../../slices/uiSlice";
import { logout } from "../../../slices/authSlice";

const iconSize = 20;

const LinkWrapper = (props = {}) => {
	const { onClick, ...restProps } = props;
	const dispatch = useDispatch();
	const dispatchToggleSidebar = useCallback(
		(...args) => {
			dispatch(toggleSidebar(false));
			if (onClick) onClick(...args);
		},
		[dispatch, onClick]
	);
	return (
		<Link
			onClick={dispatchToggleSidebar}
			className={s.sidebarItem}
			{...restProps}
		/>
	);
};

const Icon = ({ component: Component }) => (
	<Component className={s.icon} size={iconSize} />
);

const VerticalSidebar = () => {
	const isAuthenticated = useSelector((state) => state.auth.isAuthenticated);
	const user = null,
		isAdminOrManager = true;
	const dispatch = useDispatch();
	const dispatchLogout = () => dispatch(logout());

	return (
		<StylesProvider injectFirst>
			<Box
				display="flex"
				flexDirection="column"
				justifyContent="space-between"
				flexGrow={1}
				className={s.mSidebar}
			>
				<div className={s.linksContainer}>
					<LinkWrapper to="/home">
						<Icon component={Home} />
						Головна
					</LinkWrapper>
					<LinkWrapper to="/reqs">
						<Icon component={Clipboard} />
						Аналіз вимог
					</LinkWrapper>
					<LinkWrapper to="/design">
						<Icon component={Grid} />
						Проектування
					</LinkWrapper>
					<LinkWrapper to="/modelling">
						<Icon component={BoxIcon} />
						Моделювання
					</LinkWrapper>
					<LinkWrapper to="/code">
						<Icon component={Code} />
						Кодування
					</LinkWrapper>
					<LinkWrapper to="/testing">
						<Icon component={Activity} />
						Тестування
					</LinkWrapper>
				</div>

				<div>
					{isAuthenticated ? (
						<>
							<LinkWrapper to="/account">
								<Icon component={User} />
								Кабінет користувача
							</LinkWrapper>
							<LinkWrapper
								className={s.sidebarItem}
								to="/"
								onClick={dispatchLogout}
							>
								<LogOut className={s.icon} size={iconSize} />
								Покинути систему
							</LinkWrapper>
						</>
					) : (
						<img alt="" src="/noAva.png" />
					)}
				</div>
			</Box>
		</StylesProvider>
	);
};

export default VerticalSidebar;
