// models/User.js
class User {
    constructor(id = '', username = '', rating = 0, isAuth = false) {
        this._id = id;
        this._username = username;
        this._rating = rating;
        this._isAuth = isAuth;
    }

    get id() {
        return this._id;
    }

    get username() {
        return this._username;
    }

    get isAuth(){
        return this._isAuth;
    }

    get rating() {
        return this._rating;
    }

    updateRating(newRating) {
        this._rating = newRating;
    }
}

export default User;
