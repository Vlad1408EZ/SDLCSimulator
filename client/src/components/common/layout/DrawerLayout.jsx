import React, { useEffect, useMemo, useCallback, useRef } from "react";
import classnames from "classnames";
import { useSelector, useDispatch } from "react-redux";
import Drawer from '@mui/material/Drawer';
// import { getUserData } from "../../actions/login";
import s from "./styles.module.scss";
import Sidebar from "./MSidebar";
import Header from "./Header";
// import { toggleSidebar } from "../../actions/sidebar";
import Box from '@mui/material/Box';
import { toggleSidebar } from "../../../slices/uiSlice";


export default function DrawerLayout({ children }) {
    const auth = useSelector((state) => state.AuthReducer);
    const sidebarOpen = useSelector((state) => state.ui.sidebarOpen);
    const dispatch = useDispatch();
    const drawerRef = useRef();
    useEffect(() => {
        // dispatch(getUserData());
    }, []);
    const dispatchToggleSidebar = useCallback(
        () => dispatch(toggleSidebar(!sidebarOpen)),
        [dispatch, sidebarOpen]
    );
    const drawerClasses = useMemo(() => ({ paper: s.drawerPaper }), []);
    const mainClassName = useMemo(
        () => classnames(s.content, { [s.contentShift]: sidebarOpen }),
        [sidebarOpen]
    );

    return (
        <Box display="flex">
            <Header auth={auth} toggleSidebar={dispatchToggleSidebar} />
            <Drawer
                ref={drawerRef}
                className={s.drawer}
                anchor="left"
                open={sidebarOpen}
                classes={drawerClasses}
                onClose={dispatchToggleSidebar}
                onClick={dispatchToggleSidebar}
            >
                <Sidebar auth={auth} sidebar={sidebarOpen} />
            </Drawer>
            <main className={mainClassName}>
                <div className={s.drawerHeader} />
                {children}
            </main>
        </Box>
    );
}
