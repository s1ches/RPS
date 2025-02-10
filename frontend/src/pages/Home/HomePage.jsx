import React, {useEffect} from 'react';
import {useNavigate} from 'react-router-dom';
import {useUserStore} from '../../stores/userStore';
import './styles/HomePage.css'
import RoomList from "../../components/Game/RoomList/RoomList";

const HomePage = () => {
    let {isAuth} = useUserStore();
    const navigate = useNavigate();

    useEffect(() => {
        if (!isAuth) {
            isAuth = true;
            // navigate('/login');
        }
    }, [isAuth]); // Добавляем зависимости

    return (
        <div>
            <h2>Выбор игры</h2>
            <div>
                <RoomList/>
            </div>
        </div>
    );
};

export default HomePage;
