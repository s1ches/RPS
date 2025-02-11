import React from 'react';
import './styles/HomePage.css'
import RoomList from "../../components/Game/RoomList/RoomList";

const HomePage = () => {

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
