import React from "react";
import { Provider } from "react-redux";
import store, { history } from "./store";
import { Route } from "react-router";
import "./scss/styles.scss";
import { SnackbarProvider } from "notistack";
import RequireAuth from "./components/common/RequireAuth";
import LandingPage from "./components/pages/Landing/";
import { BrowserRouter, Routes } from "react-router-dom";
import Home from "./components/pages/Home";
import PrivatePage from "./components/common/PrivateRoute";
import Reqs from "./components/pages/Reqs";
import ReqsCreate from "./components/pages/ReqsCreate";
import ErrorPage from "./components/pages/404";
import StudentAccount from "./components/pages/UserAccount";
import AdminPage from "./components/pages/Admin";
import Notifier from "./components/common/layout/Notifier";

function App() {
	return (
		<Provider store={store}>
			<BrowserRouter history={history}>
				<SnackbarProvider>
					<Notifier />
					<Routes>
						<Route path="/" element={<LandingPage />} />
						<Route element={<RequireAuth />}>
							<Route path="/home" element={<PrivatePage component={Home} />} />
							<Route path="/reqs" element={<PrivatePage component={Reqs} />} />
							<Route path="/reqs/create" element={<PrivatePage component={ReqsCreate} />} />
							<Route
								path="/account"
								element={<PrivatePage component={StudentAccount} />}
							/>
						</Route>
						<Route path="/admin" element={<PrivatePage component={AdminPage} />} />
						<Route path="*" element={<ErrorPage />} />
					</Routes>
				</SnackbarProvider>

			</BrowserRouter>
		</Provider>
	);
}

export default App;
