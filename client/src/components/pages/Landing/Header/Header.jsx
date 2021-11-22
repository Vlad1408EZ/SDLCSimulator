import React, { useRef, useState, useEffect } from "react";
import { useSelector } from "react-redux";
import { useLocation, Navigate } from "react-router";
import s from "./header.module.scss";
import clx from "classnames";
import useClickOutside from "../../../common/hooks/useClickOutside";
import LoginForm from "./Login";

const Header = () => {
  const location = useLocation();
  const modalRef = useRef();
  const buttonRef = useRef();
  const [visible, setVisible] = useState(
    location.state
      ? location.state.visible
      : location.pathname && location.pathname.includes("login"));
  const [redirect, setRedirect] = useState(false);
  const isAuthenticated = useSelector((state) => state.auth.isAuthenticated);

  useClickOutside(
    () => {
      setVisible(false);
    },
    modalRef,
    buttonRef
  );

  useEffect(() => {
    if (isAuthenticated) {
      setRedirect(true);
    }
  }, [isAuthenticated, setRedirect]);


  const visibilityHandler = () => {
    setVisible(!visible);
  };

  const modalClass = clx({
    [s.modal]: true,
    [s.active]: visible,
  });

  if (redirect) {
    return <Navigate to="/home" />;
  }

  return (
    <header className={s.headerContainer}>
      <span>VLPI</span>
      <div className={s.navigation}>
        <button
          type="button"
          className="buttonOk"
          ref={buttonRef}
          onClick={visibilityHandler}>
          Sign in
        </button>
        {visible && <div className={modalClass} ref={modalRef}>
          <LoginForm />
        </div>}
      </div>

    </header>
  )
}

export default Header
