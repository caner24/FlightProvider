import React, { useCallback, useEffect, useState } from 'react';
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Layout from '../src/Pages/Layout/Layout';
import Home from '../src/Pages/Home/Home';
import NotFound from '../src/Pages/NotFound/NotFound';
import Login from '../src/Pages/Login/Login';
import Ticket from '../src/Pages/Ticket/Ticket';
import Register from "./Pages/Register/Register";
import TicketDetail from './Pages/TicketDetail/TicketDetail';
import { useDispatch, useSelector } from "react-redux";
import axios from 'axios';

const App = () => {
    const dispatch = useDispatch();
    const user = JSON.parse(localStorage.getItem("userDetails"));
    const bearer = JSON.parse(localStorage.getItem("bearer"));
    const refreshTokenCode = JSON.parse(localStorage.getItem("refreshToken"));
    const refreshInterval = 3600 * 1000;

    const refreshToken = useCallback(async () => {
        const options = {
            refreshToken: refreshTokenCode,
        };
        await axios
            .post("https://localhost:7242/api/identity/refresh", options)
            .then((response) => {
                localStorage.setItem("bearer", JSON.stringify(response.data.accessToken));
                localStorage.setItem("refreshToken", JSON.stringify(response.data.refreshToken));
                dispatch({ type: "SET_BEARER", payload: { bearer: response.data.accessToken } });
                dispatch({ type: "SET_REFRESH", payload: { refreshToken: response.data.refreshToken } });
            })
            .catch((err) => console.log(err));
    }, [refreshTokenCode, dispatch]);

    useEffect(() => {
        if (user !== null) {
            dispatch({ type: "LOGIN_USER", payload: { user } });
            dispatch({ type: "SET_BEARER", payload: { bearer } });
            setInterval(refreshToken, refreshInterval - 5 * 60 * 1000);
        }
    }, [bearer, dispatch, refreshToken, user, refreshInterval]);

    return (
        <Router>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route index element={<Home />} />
                    <Route path="/ticket" element={<Ticket />} />
                    <Route path="/flightDetail" element={<TicketDetail />} />
                </Route>
                <Route path="*" element={<NotFound />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
            </Routes>
        </Router>
    );
};

export default App;
