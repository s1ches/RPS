import {api} from './api';
import User from "../models/User";

export const registerUser = async (username, email, password, confirmPassword) => {
    try {
        const response = await api.post('/auth/register', {username, email, password, confirmPassword});
        const token = response.data;
        localStorage.setItem('token', token);
        //TODO: запрос на юзера
        return new User('1', 'fuzikort', 0);
    } catch (error) {
        console.error('Ошибка регистрации:', error.response?.data?.message || error.message);
        return {error: 'Ошибка при регистрации'};  // Возвращаем ошибку, а не выбрасываем исключение
    }
};

export const loginUser = async (email, password) => {
    try {
        const response = await api.post('/auth/login', {username: email, password});
        const token = response.data;
        localStorage.setItem('token', token);
        //TODO: запрос на юзера
        let user = new User('1', 'fuzikort', 0)
        return {user};
    } catch (error) {
        console.error('Ошибка авторизации:', error.response?.data?.message || error.message);
        return {error: 'Ошибка при авторизации'};
    }
};

export const getCurrentUser = async () => {
    try {
        const token = localStorage.getItem('token');
        if (!token) {
            return {error: 'Токен не найден'};
        }

        const response = await api.get('/auth/me', { //TODO переделать
            headers: {Authorization: `Bearer ${token}`}
        });
        return response.data;
    } catch (error) {
        console.error('Ошибка при получении данных пользователя:', error.response?.data?.message || error.message);
        return {error: 'Ошибка при получении данных пользователя'};
    }
};

// Логика выхода пользователя
export const logoutUser = () => {
    localStorage.removeItem('token');
};
