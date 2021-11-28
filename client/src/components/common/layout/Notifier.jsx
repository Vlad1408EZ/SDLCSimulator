// eslint-disable-next-line no-unused-vars
import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useSnackbar } from "notistack";
import { removeNotifications } from "../../../slices/notificationsSlice";

let displayed = [];

const Notifier = () => {
    const dispatch = useDispatch();
    const notifications = useSelector(
        (store) => store.notifications.notificationList
    );
    const { enqueueSnackbar, closeSnackbar } = useSnackbar();

    const storeDisplayed = (id) => {
        displayed = [...displayed, id];
    };

    const removeDisplayed = (id) => {
        displayed = [...displayed.filter((key) => id !== key)];
    };

    useEffect(() => {
        notifications.forEach((notification) => {
            if (notification.dismissed) {
                closeSnackbar(notification.key);
                return;
            }

            if (displayed.includes(notification.key)) return;
            const { message, key, options = {} } = notification;
            const handleClick = () => {
                closeSnackbar(key);
            };
            enqueueSnackbar(message, {
                anchorOrigin: {
                    vertical: "bottom",
                    horizontal: "center",
                },
                autoHideDuration: 7000,
                preventDuplicate: true,
                onClick: handleClick,
                ContentProps: {
                    classes: { root: "alertRoot" },
                },
                key,
                ...options,
                onClose: (event, reason, myKey) => {
                    if (options.onClose) {
                        options.onClose(event, reason, myKey);
                    }
                },
                onExited: (event, myKey) => {
                    // removen this snackbar from redux store
                    dispatch(removeNotifications(myKey));
                    removeDisplayed(myKey);
                },
            });
            // keep track of snackbars that we've displayed
            storeDisplayed(key);
        });
    }, [notifications, closeSnackbar, enqueueSnackbar, dispatch]);

    return null;
};

export default Notifier;
