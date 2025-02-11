import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { loginUser } from '../../services/auth';
import { useUserStore } from '../../stores/userStore';
import './styles/LoginPage.css'

const LoginPage = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();
    const { login } = useUserStore();

    const handleLogin = async (e) => {
        e.preventDefault();
        try {
            const userData = await loginUser(username, password);
            login(userData);
            navigate('/');
        } catch (error) {
            alert('Ошибка при входе: ' + error.message);
        }
    };

    return (
        <div className="loginPage__container">
            <h2 className="loginPage__header">Авторизация</h2>
            <form className="loginPage__form" onSubmit={handleLogin}>
                <input
                    className="loginPage__input"
                    type="text"
                    placeholder="Email"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
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
