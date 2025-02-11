import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { loginUser } from '../../services/auth';
import { useUserStore } from '../../stores/userStore';
import './styles/LoginPage.css'

const LoginPage = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();
    const { login } = useUserStore();

    let handleLogin = (e) => {
        e.preventDefault();
        loginUser(email, password)
            .then(({ user, error }) => {
                if (error) {
                    console.log(error)
                    alert(error);
                } else if (user) {
                    login(user);
                    navigate('/');
                } else {
                    alert('Невозможно авторизоваться. Попробуйте позже.');
                }
            });
    };

    return (
        <div className="loginPage__container">
            <h2 className="loginPage__header">Авторизация</h2>
            <form className="loginPage__form" onSubmit={e => handleLogin(e)}>
                <input
                    className="loginPage__input"
                    type="email"
                    placeholder="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    required
                />
                <input
                    className="loginPage__input"
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                />
                <button className="loginPage__button" type="submit">Войти</button>
            </form>
            <button className="loginPage__button" onClick={() => navigate('/register')}>Зарегистрироваться</button>
        </div>
    );
};

export default LoginPage;
