import React from "react";
import { Link } from "react-router-dom";
// import { logout } from "../../actions/login";
// import { toggleSidebar } from "../../actions/sidebar";
import AppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
// import { FiLogOut } from "react-icons/all";
import s from "./styles.module.scss";
import Box from "@mui/material/Box";
import { StylesProvider } from "@mui/styles";

const Header = ({ auth, toggleSidebar }) => {
	// const { isAuthenticated } = auth;
	const isAuthenticated = true;
	// const { visible } = sidebar;

	//   const dispatchLogout = useCallback(() => dispatch(logout()), [dispatch]);

	const guestLinks = (
		<Link className={s.iconLink} to="/login">
			Login
		</Link>
	);

	const authLinks = (
		<Link className={s.iconLink} onClick={() => {} /*dispatchLogout*/} to="/">
			{/* <FiLogOut size={iconSize} /> */}
		</Link>
	);

	return (
		<StylesProvider injectFirst>
			<AppBar component={AppBar} position="fixed" className={s.appBar}>
				<Box display="flex" justifyContent="space-between" clone>
					<Toolbar
						classes={{ dense: s.toolbarDense }}
						variant="dense"
						disableGutters
					>
						<Box display="flex" alignItems="center" onClick={toggleSidebar}>
							{" "}
							<span className={s.title}>VLPI</span>
						</Box>
						<Box display="flex" alignItems="center">
							{isAuthenticated ? authLinks : guestLinks}
						</Box>
					</Toolbar>
				</Box>
			</AppBar>
		</StylesProvider>
	);
};

export default Header;
