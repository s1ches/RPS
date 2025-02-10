import { api } from './api';  // axios-экземпляр для запросов

// Регистрация пользователя
export const registerUser = async (username, password) => {
    try {
        const response = await api.post('/auth/register', { username, password });
        return response.data;  // Возвращаем данные пользователя или токен
    } catch (error) {
        console.error('Ошибка регистрации:', error.response?.data?.message || error.message);
        return { error: 'Ошибка при регистрации' };  // Возвращаем ошибку, а не выбрасываем исключение
    }
};

// Логин пользователя
export const loginUser = async (username, password) => {
    try {
        const response = await api.post('/auth/login', { username, password });
        const { token, user } = response.data;  // Получаем токен и данные пользователя
        localStorage.setItem('token', token);  // Сохраняем токен в localStorage
        return { user };  // Возвращаем данные пользователя
    } catch (error) {
        console.error('Ошибка авторизации:', error.response?.data?.message || error.message);
        return { error: 'Ошибка при авторизации' };  // Возвращаем ошибку
    }
};

// Получение информации о текущем пользователе (можно использовать для верификации токена)
export const getCurrentUser = async () => {
    try {
        const token = localStorage.getItem('token');
        if (!token) {
            return { error: 'Токен не найден' };  // Возвращаем ошибку, если токен отсутствует
        }

        const response = await api.get('/auth/me', {
            headers: { Authorization: `Bearer ${token}` }
        });
        return response.data;  // Возвращаем информацию о текущем пользователе
    } catch (error) {
        console.error('Ошибка при получении данных пользователя:', error.response?.data?.message || error.message);
        return { error: 'Ошибка при получении данных пользователя' };  // Возвращаем ошибку
    }
};

// Логика выхода пользователя
export const logoutUser = () => {
    localStorage.removeItem('token');  // Удаляем токен из localStorage
};
