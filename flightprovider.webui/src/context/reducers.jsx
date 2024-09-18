export default function reducers(state = initialState, action) {
    switch (action.type) {
        case "LOGIN_USER":
            const { user } = action.payload;
            return { ...state, user };
        case "SET_BEARER":
            const { bearer } = action.payload;
            return { ...state, bearer };
        case "SET_REFRESH":
            const { refreshToken } = action.payload;
            return { ...state, refreshToken };
        case "SET_DEPARTURE":
            return { ...state, departure: action.payload };
        case "SET_ORIGIN":
            return { ...state, origin: action.payload };
        // other cases...
        default:
            return state;
    }
}