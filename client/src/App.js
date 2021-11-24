import React from 'react';
import { Provider } from 'react-redux';
import store, { history } from './store';
import { Route } from "react-router";
import "./scss/styles.scss";
import RequireAuth from './components/common/RequireAuth';
import LandingPage from './components/pages/Landing/';
import { BrowserRouter, Routes } from 'react-router-dom';
import Home from './components/pages/Home';
import PrivatePage from './components/common/PrivateRoute';
import Reqs from './components/pages/Reqs';
import ErrorPage from './components/pages/404';
import StudentAccount from './components/pages/UserAccount';

function App() {
  return (
    <Provider store={store}>
      <BrowserRouter history={history}>
        <Routes>
          <Route path="/" element={<LandingPage />} />
          <Route element={<RequireAuth />}>
            <Route path="/home" element={<PrivatePage component={Home} />} />
            <Route path="/reqs" element={<PrivatePage component={Reqs} />} />
            <Route path="/account" element={<PrivatePage component={StudentAccount} />} />
          </Route>
          <Route path="*" element={<ErrorPage />} />
        </Routes>
      </BrowserRouter>
    </Provider>
  );
}

export default App;
