import {createContext, useContext, useState} from 'react';
import User from '../models/User';

// Создаём контекст
const UserContext = createContext();

// Провайдер контекста для управления состоянием пользователя
export const UserProvider = ({ children }) => {
    const [user, setUser] = useState(new User());

    // const login = (userData) => {
    //     const newUser = new User(userData.id, userData.username, userData.rating);
    //     setUser(newUser);
    // };

    const login = (user) => {
        setUser(user);
    }

    const logout = () => {
        localStorage.removeItem('token');
        setUser(new User());
    };

    const updateUserRating = (newRating) => {
        user.updateRating(newRating);
        setUser({ ...user });
    };

    return (
        <UserContext.Provider value={{ user, login, logout, updateUserRating }}>
            {children}
        </UserContext.Provider>
    );
};

// Хук для использования UserStore
export const useUserStore = () => useContext(UserContext);
