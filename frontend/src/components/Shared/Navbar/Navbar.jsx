// components/Navbar.jsx
import React from 'react';
import {useNavigate} from 'react-router-dom';
import {useUserStore} from '../../../stores/userStore';
import './styles/Navbar.css';

const Navbar = () => {
    let {user, logout} = useUserStore();
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        navigate('/login');
    };

    return (
        <>
            {user.isAuth ? (<nav className="navbar">
                <div className="navbar__title">Камень, Ножницы, Бумага</div>
                <ul>
                        <li className="navbar__user-info">
                            <span>{user.username}</span>
                            <span>Рейтинг: {user.rating}</span>
                        </li>
                        <li>
                            <button onClick={handleLogout}>Выйти</button>
                        </li>
                </ul>
            </nav>
            ) : <></>
            }
        </>
    );
};

export default Navbar;
