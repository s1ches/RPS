import {$gameApi} from "./index";

export const createRoom = async (maxAllowedGameRating) => {
    const response = await $gameApi.post('/rooms', {maxAllowedGameRating});
    if (response.status === 200) {
        return {roomId: response.data.RoomId, error: null};
    } else {
        return {roomId: null, error: response.data.Message};
    }
};

export const joinRoom = async (roomId) => {
    const response = await $gameApi.post('/rooms/join-room', {roomId});
    if (response.status === 200) {
        return {error: null};
    } else {
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
    const response = await $gameApi.get('/rooms/leave-room', {limit, offset});
    if (response.status === 200) {
        return {rooms: response.data.Rooms, totalCount: response.data.TotalCount, error: null};
    } else {
        return {roomId: null, totalCount: 0, error: response.data.Message};
    }
};

export const getRoom = async (roomId) => {
    const response = await $gameApi.get(`/rooms/leave-room/${roomId}`);
    if (response.status === 200) {
        return {room: response.data, error: null};
    } else {
        return {room: null, error: response.data.Message};
    }
};