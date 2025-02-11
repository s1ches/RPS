import {$authApi} from './index';
import {getCurrentUser} from "./users";

export const registerUser = async (username, email, password, confirmPassword) => {
    const response = await $authApi.post('/auth/register', {username, email, password, confirmPassword});
    if (response.status === 200) {
        const token = response.data.accessToken;
        localStorage.setItem('token', token);
        return await getCurrentUser(token);
    } else {
        return {user: null, error: response.data.Message};
    }
};

export const loginUser = async (email, password) => {
    const response = await $authApi.post('/auth/login', {email, password});
    if (response.status === 200) {
        const token = response.data.accessToken;
        localStorage.setItem('token', token);
        return await getCurrentUser(token);
    } else {
        return {user: null, error: response.data.Message};
    }
};


