import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { registerUser } from '../../services/auth';
import { useUserStore } from '../../stores/userStore';
import './styles/RegisterPage.css'

const RegisterPage = () => {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const navigate = useNavigate();
    const { login } = useUserStore();

    const handleRegister = (e) => {
        e.preventDefault();

        if (password !== confirmPassword) {
            alert('Пароли не совпадают');
            return;
        }

        registerUser(username, email, password, confirmPassword)
            .then(({ user, error }) => {
                if (error) {
                    console.log(error)
                    alert(error);
                } else if (user) {
                    login(user);
                    navigate('/');
                } else {
                    alert('Невозможно зарегистрироваться. Попробуйте позже.');
                }
            });
    };


    return (
        <div className="registerPage__container">
            <h2 className="registerPage__header">Регистрация</h2>
            <form className="registerPage__form" onSubmit={handleRegister}>
                <input
                    className="registerPage__input"
                    type="text"
                    placeholder="Username"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                    required
                />
                <input
                    className="registerPage__input"
                    type="email"
                    placeholder="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    required
                />
                <input
                    className="registerPage__input"
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                />
                <input
                    className="registerPage__input"
                    type="password"
                    placeholder="Confirm Password"
                    value={confirmPassword}
                    onChange={(e) => setConfirmPassword(e.target.value)}
                    required
                />
                <button className="registerPage__button" type="submit">Зарегистрироваться</button>
            </form>
            <button className="registerPage__button" onClick={() => navigate('/login')}>Есть аккаунт? Войти</button>
        </div>
    );

};

export default RegisterPage;
