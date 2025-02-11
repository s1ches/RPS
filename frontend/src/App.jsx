import React, {useEffect} from 'react';
import {BrowserRouter, Navigate, Route, Routes} from 'react-router-dom';
import HomePage from './pages/Home/HomePage';
import LoginPage from './pages/Login/LoginPage';
import RegisterPage from './pages/Register/RegisterPage';
import RoomPage from './pages/Room/RoomPage';
import Navbar from "./components/Shared/Navbar/Navbar";
import './App.css'
import {useUserStore} from "./stores/userStore";
import User from "./models/User";

const App = () => {
    const { user, login } = useUserStore();

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (token) {
            let user = new User('1', 'fuzikort', 0, true) //TODO заменить
            if (user) {
                login(user);
            }
        }
    }, []);

    return (
        <BrowserRouter>
            <div className="App">
                {user.isAuth && <Navbar />}
                <Routes>
                    <Route path="/" element={user.isAuth ? <HomePage /> : <Navigate to="/login" replace />} />
                    <Route path="/login" element={user.isAuth ? <Navigate to="/" replace /> : <LoginPage />} />
                    <Route path="/register" element={user.isAuth ? <Navigate to="/" replace /> : <RegisterPage />} />
                    <Route path="/room/:id" element={user.isAuth ? <Navigate to="/" replace /> : <RoomPage />} />
                </Routes>
            </div>
        </BrowserRouter>
    );
};

export default App;
