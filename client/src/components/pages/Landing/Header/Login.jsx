import React from 'react'
import { Field, Form } from "react-final-form";
import s from "./forms.module.scss";
import { useDispatch, useSelector } from "react-redux";
import clx from "classnames"
import cs from "../../../../scss/common.module.scss"
import { validateEmail, validatePassword } from '../../../common/utils/validation';
import { InputAdapter } from '../../../common/ui-parts/finalFormAdapters';
import Loading from '../../../common/ui-parts/Loading';
import { login } from '../../../../slices/authSlice';

const LoginForm = () => {
    const dispatch = useDispatch();
    const isLoading = useSelector((state) => state.auth.isLoading);

    const onSubmit = (values) => {
        dispatch(login(values.email, values.password));
    };

    return (
        <Form
            onSubmit={onSubmit}
            render={({ handleSubmit }) => (
                <form
                    onSubmit={handleSubmit}>
                    <Field
                        validate={validateEmail}
                        component={InputAdapter}
                        className={s.field}
                        name="email"
                        variant="filled"
                        autoComplete="off"
                        placeholder="Email"
                        aria-describedby="usernameHelp"
                    />
                    <Field
                        validate={validatePassword}
                        component={InputAdapter}
                        className={s.field}
                        type="password"
                        name="password"
                        variant="filled"
                        autoComplete="off"
                        placeholder="Password"
                    />
                    <div className={s.loading}>
                        {isLoading ? (
                            <Loading color="#d5d5d5" position="absolute" />
                        ) : (
                            <>
                                <button type="submit" className={s.submit}>
                                    Enter
                                </button>

                            </>
                        )}
                    </div>
                </form>
            )}
        />

    )
}

export default LoginForm
