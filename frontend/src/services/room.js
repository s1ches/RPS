import {$gameApi} from "./index";

export const createRoom = async (maxAllowedGameRating) => {
    const response = await $gameApi.post('/rooms', {maxAllowedGameRating});
    if (response.status === 200) {
        return {roomId: response.data.roomId, error: null};
    } else {
        return {roomId: null, error: response.data.Message};
    }
};

export const joinRoom = async (roomId) => {
    const response = await $gameApi.post('/rooms/join-room', roomId);
    if (response.status === 200) {
        return {error: null};
    } else {
        console.log(response);
        return {error: response.data.Message};
    }
};

export const leaveRoom = async (roomId) => {
    const response = await $gameApi.post('/rooms/leave-room', {roomId});
    if (response.status === 200) {
        return {error: null};
    } else {
        return {error: response.data.Message};
    }
};

export const getRooms = async (limit, offset) => {
    const response = await $gameApi.get(`/rooms?limit=${limit}&offset=${offset}`);
    if (response.status === 200) {
        return {rooms: response.data.rooms, totalCount: response.data.totalCount, error: null};
    } else {
        return {rooms: null, totalCount: 0, error: response.data.Message};
    }
};

export const getRoom = async (roomId) => {
    const response = await $gameApi.get(`/rooms/${roomId}`);
    if (response.status === 200) {
        return {room: response.data, error: null};
    } else {
        return {room: null, error: response.data.Message};
    }
};