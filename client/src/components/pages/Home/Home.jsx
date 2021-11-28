import React from "react";
import clx from "classnames";
import s from "./home.module.scss";
import FlexBox from "../../common/ui-parts/FlexBox";

const Home = () => {
	return (
		<div>
			<div className={s.mainModule}>
				<h5>Модуль аналізу вимог</h5>
				<div className={s.modulesDescription}>
					Модуль аналізу вимог призначений для практичної підготовки студента з
					допомогою завдань, пов'язаних із добуванням вимог та бізнес-аналізом.
					Завдання розроблені таким чином, щоб виробити у студента практичні
					навички, що стануть в пригоді під час роботи із вимогами до продукту.
					Кожне завдання має еталонний варіант розв'язку, який студент може
					розглянути після здачі та оцінювання виконаного завдання.
				</div>
			</div>

			<FlexBox
				className={s.secondaryModules}
				flexWrap="wrap"
				justifyContent="spaceBetween"
			>
				<div className={s.secondaryModule}>
					<h5>Модуль проектування</h5>
					<div className={s.modulesDescription}>
						Модуль міститиме практичні завдання, що стосуються проектування
						високорівневої архітектури програмної системи. Ці модулі будуть
						реалізовані з допомого інструментів візуалізації даних.
					</div>
				</div>
				<div className={s.secondaryModule}>
					<h5>Модуль кодування</h5>
					<div className={clx(s.modulesDescription, s.descriptionAlignLeft)}>
						Модуль міститиме практичні завдання із написанням реального коду
						певною мовою програмування.
					</div>
				</div>
				<div className={s.secondaryModule}>
					<h5>Модуль моделювання</h5>
					<div className={s.modulesDescription}>
						Модуль призначений для того, щоб студент міг набути практичні
						навички у моделюванні систем з використанням шаблонів проектування.
						Тут застосовуватимуться UML-діаграми, на більш нижчому рівні
						представлення програмної системи – діаграми класів.
					</div>
				</div>
				<div className={s.secondaryModule}>
					<h5>Модуль тестування</h5>
					<div className={clx(s.modulesDescription, s.descriptionAlignLeft)}>
						Модуль призначений для набуття практичних навичок із побудови
						тестових сценаріїв для перевірки якості ПЗ.
					</div>
				</div>
			</FlexBox>
		</div>
	);
};

export default Home;
