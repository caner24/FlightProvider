import React, { useCallback, useEffect, useState } from 'react';
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { Search, Menu, Facebook, Twitter, Instagram } from 'lucide-react';
import Layout from '../src/Pages/Layout/Layout';
import Home from '../src/Pages/Home/Home';
import NotFound from '../src/Pages/NotFound/NotFound';
import Login from '../src/Pages/Login/Login';
import Ticket from '../src/Pages/Ticket/Ticket';
import { useDispatch, useSelector } from "react-redux";
import Register from "./Pages/Register/Register";
import TicketDetail from './Pages/TicketDetail/TicketDetail'
const App = () => {

    const dispatch = useDispatch();
    var user = JSON.parse(localStorage.getItem("userDetails"));
    var bearer = JSON.parse(localStorage.getItem("bearer"));
    var refreshTokenCode = JSON.parse(localStorage.getItem("refreshToken"));
    const refreshInterval = 3600 * 1000;
    const refreshToken = useCallback(async () => {
        const options = {
            refreshToken: refreshTokenCode,
        };
        await axios
            .post(
                "https://localhost:7242/api/identity/refresh",
                options
            )
            .then((response) => {
                localStorage.setItem(
                    "bearer",
                    JSON.stringify(response.data.accessToken)
                );
                localStorage.setItem(
                    "refreshToken",
                    JSON.stringify(response.data.refreshToken)
                );
                const bearer = { bearer: response.data.accessToken };
                const refreshToken = { refreshToken: response.data.refreshToken };
                dispatch({ type: "SET_BEARER", payload: { bearer } });
                dispatch({ type: "SET_REFRESH", payload: { refreshToken } });
            })
            .catch((err) => console.log(err));
    }, [refreshTokenCode, dispatch]);

    useEffect(() => {
        console.log(user);
        if (user !== null) {
            dispatch({ type: "LOGIN_USER", payload: { user } });
            dispatch({ type: "SET_BEARER", payload: { bearer } });
            setInterval(refreshToken, refreshInterval - 5 * 60 * 1000);
        }
    }, [
        bearer,
        dispatch,
        refreshToken,
        user,
        refreshInterval,
    ]);

    return (
        <Router>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route index element={<Home />} />
                    <Route path="/ticket" element={<Ticket />} />
                    <Route path="/flightDetail" element={<TicketDetail />} />
                </Route>
                <Route path="*" element={<NotFound />} />

                <Route path="/Login" element={<Login />} />
                <Route path="/register" element={<Register />} />

            </Routes>
        </Router>
    );
};

export default App;