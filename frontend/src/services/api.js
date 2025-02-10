import axios from 'axios';

export const api = axios.create({
    baseURL: process.env.VITE_RPS_API_URL,
    headers: {
        'Content-Type': 'application/json',
    },
});

// Добавляем токен в каждый запрос
api.interceptors.request.use((config) => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

// Обработка ошибок
api.interceptors.response.use(
    (response) => response,
    (error) => {
        if (error.response?.status === 401) {
            localStorage.removeItem('token');
            window.location.href = '/login'; // Перенаправление на страницу входа
        }
        return Promise.reject(error);
    }
);