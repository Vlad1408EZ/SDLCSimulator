import React from "react";
import DrawerLayout from "./layout/";

const PrivatePage = (props) => {
	const { component: Component, ...rest } = props;

	return (
		<DrawerLayout>
			<Component {...rest} />
		</DrawerLayout>
	);
};

export default PrivatePage;
