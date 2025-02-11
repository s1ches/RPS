import {api} from './api';
import User from "../models/User";

export const registerUser = async (username, email, password, confirmPassword) => {
    const response = await api.post('/auth/register', {username, email, password, confirmPassword});
    if (response.status === 200) {
        const token = response.data;
        localStorage.setItem('token', token);
        //TODO запрос на юзера
        return {user: new User('1', 'fuzikort', 0, true), error: null};
    } else {
        return {user: null, error: response.data.Message};
    }
};

export const loginUser = async (email, password) => {
    const response = await api.post('/auth/login', {email, password});
    if (response.status === 200) {
        const token = response.data;
        localStorage.setItem('token', token);
        //TODO запрос на юзера
        return {user: new User('1', 'fuzikort', 0, true), error: null};
    } else {
        return {user: null, error: response.data.Message};
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
