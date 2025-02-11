import axios from 'axios';

export const api = axios.create({
    baseURL: process.env.REACT_APP_RPS_API,
    headers: {
        'Content-Type': 'application/json',
    },
    validateStatus: status => true
});

api.interceptors.request.use((config) => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});