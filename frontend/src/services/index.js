import axios from 'axios';

const $authApi = axios.create({
    baseURL: process.env.REACT_APP_RPS_AUTH_API,
    headers: {
        'Content-Type': 'application/json',
    },
    validateStatus: status => true
});

const $usersApi = axios.create({
    baseURL: process.env.REACT_APP_RPS_USERS_API,
    headers: {
        'Content-Type': 'application/json',
    },
    validateStatus: status => true
});

const addAuthToken = (config) => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
};

$authApi.interceptors.request.use(addAuthToken);
$usersApi.interceptors.request.use(addAuthToken);

export {
    $authApi,
    $usersApi
}