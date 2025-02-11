import React, {useEffect} from 'react';
import {useNavigate} from 'react-router-dom';
import {useUserStore} from '../../stores/userStore';
import './styles/HomePage.css'
import RoomList from "../../components/Game/RoomList/RoomList";

const HomePage = () => {
    const {isAuth} = useUserStore();
    const navigate = useNavigate();

    useEffect(() => {
        if (!isAuth) {
            navigate('/login');
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
