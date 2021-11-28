import React from "react";
import Header from "./Header";
import s from "./landing.module.scss";

const LandingPage = () => {
	return (
		<div className={s.landingPageContainer}>
			<Header />
			<div className={s.landingPage}>
				<div className={s.descriptionBlock}>
					<h3>
						"Віртуальна лабораторія розробки програмного забезпечення" - VLPI
					</h3>
					<h4>
						- це тренажер, який допоможе отримати практичні навички з кожного
						етапу життєвого циклу розробки програмного забезпечення.
					</h4>
				</div>
			</div>
		</div>
	);
};

export default LandingPage;
