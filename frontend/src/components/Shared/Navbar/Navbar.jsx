// components/Navbar.jsx
import React from 'react';
import {Link, useNavigate} from 'react-router-dom';
import {useUserStore} from '../../../stores/userStore'; // Если используете Zustand или другой state
import './styles/Navbar.css';

const Navbar = () => {
    let {isAuth, logout, username, rating} = useUserStore(); // Добавляем username и rating из состояния
    const navigate = useNavigate();
    isAuth = true;
    username = 'fuzikort';
    rating = 5;

    const handleLogout = () => {
        logout(); // Очистить состояние пользователя в хранилище
        localStorage.removeItem('token'); // Удалить токен из localStorage
        navigate('/login'); // Перенаправить на страницу логина
    };

    return (
        <nav className="navbar">
            <div className="navbar__title">Камень, Ножницы, Бумага</div>
            <ul>
                {isAuth ? (
                    <>
                        <li className="navbar__user-info">
                            <span>{username}</span> {/* Отображаем ник пользователя */}
                            <span>Рейтинг: {rating}</span> {/* Отображаем рейтинг */}
                        </li>
                        <li>
                            <button onClick={handleLogout}>Выйти</button>
                        </li>
                    </>
                ) : (
                    <>
                        <li>
                            <Link to="/login">Войти</Link>
                        </li>
                        <li>
                            <Link to="/register">Зарегистрироваться</Link>
                        </li>
                    </>
                )}
            </ul>
        </nav>
    );
};

export default Navbar;
