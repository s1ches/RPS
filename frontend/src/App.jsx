import React, {useEffect, useState} from 'react';
import {BrowserRouter, Navigate, Route, Routes} from 'react-router-dom';
import HomePage from './pages/Home/HomePage';
import LoginPage from './pages/Login/LoginPage';
import RegisterPage from './pages/Register/RegisterPage';
import RoomPage from './pages/Room/RoomPage';
import Navbar from "./components/Shared/Navbar/Navbar";
import './App.css'
import {useUserStore} from "./stores/userStore";
import {getCurrentUser} from "./services/users";

const App = () => {
    const {user, login} = useUserStore();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (token) {
            getCurrentUser().then(({ user, error }) => {
                if (error) {
                    console.log(error);
                    alert(error);
                } else if (user) {
                    login(user);
                } else {
                    alert('Время жизни токена истекло. Пожалуйста, войдите снова.');
                }
                setLoading(false);
            });
        } else {
            setLoading(false);
        }
    }, []);

    if (loading) {
        return <div>Загрузка...</div>;
    }

    return (
        <BrowserRouter>
            <div className="App">
                {user.isAuth && <Navbar/>}
                <Routes>
                    <Route path="/" element={user.isAuth ? <HomePage/> : <Navigate to="/login" replace/>}/>
                    <Route path="/login" element={user.isAuth ? <Navigate to="/" replace/> : <LoginPage/>}/>
                    <Route path="/register" element={user.isAuth ? <Navigate to="/" replace/> : <RegisterPage/>}/>
                    <Route path="/room/:id" element={user.isAuth ? <RoomPage /> : <Navigate to="/login" replace />}
                    />
                </Routes>
            </div>
        </BrowserRouter>
    );
};

export default App;
