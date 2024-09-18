import React from "react";
import { Outlet } from "react-router-dom";
import Header from "../Header/Header";

const Layout = () => {
    return (
        <div
            style={{
                width: "100vw",
                height: "100vh",
                overflow: 'hidden',
            }}
        >
            <Header />
            <Outlet />
        </div>
    );
};

export default Layout;