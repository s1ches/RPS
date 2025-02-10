import React, { useEffect, useState } from 'react';
import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom';
import { useUserStore } from './stores/userStore';
import HomePage from './pages/Home/HomePage';
import LoginPage from './pages/Login/LoginPage';
import RegisterPage from './pages/Register/RegisterPage';
import RoomPage from './pages/Room/RoomPage';
import Navbar from "./components/Shared/Navbar/Navbar";
import './App.css'

const App = () => {
    const {isAuth} = useUserStore();
    const [isLoggedIn, setIsLoggedIn] = useState(isAuth);

    useEffect(() => {
        const token = localStorage.getItem('token');
        setIsLoggedIn(true);
        // if (token) {
        //     setIsLoggedIn(true);
        // }
    }, []);

    return (
        <BrowserRouter>
            <div className="App">
                {isLoggedIn && <Navbar />}
                <Routes>
                    {/* Главная страница, доступна только после авторизации */}
                    <Route
                        path="/"
                        element={
                            // isLoggedIn ? (
                                <HomePage />
                            // ) : (
                                // <Navigate to="/login" replace />
                            // )
                        }
                    />

                    {/* Страница логина */}
                    <Route path="/login" element={<LoginPage />} />

                    {/* Страница регистрации */}
                    <Route path="/register" element={<RegisterPage />} />

                    {/* Страница игры */}
                    <Route path="/game/:id" element={<RoomPage />} />
                </Routes>
            </div>
        </BrowserRouter>
    );
};

export default App;
