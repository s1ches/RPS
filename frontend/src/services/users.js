import {$usersApi} from "./index";
import User from "../models/User";
import { jwtDecode } from 'jwt-decode';

export const getCurrentUser = async () => {
    const token = localStorage.getItem('token');
    if (!token) {
        return { user: null, error: 'Токен не найден. Пожалуйста, войдите.' };
    }

    const decodedToken = jwtDecode(token);
    const userId = decodedToken.id;

    const response = await $usersApi.get('/users');
    if (response.status === 200) {
        return {user: new User(userId, response.data.userName, response.data.rating, true), error: null};
    } else {
        return {user: null, error: response.data.Message};
    }
};

export const getUsers = async (userIds) => {
    const response = await $usersApi.get('/users/users-infos', {userIds: userIds});
    if (response.status === 200) {
        return {
            users: response.data.users.map(user => ({
                id: user.userId,
                username: user.userName,
                rating: user.rating
            })),
            error: null
        };
    } else {
        return {users: null, error: response.data.Message};
    }
};