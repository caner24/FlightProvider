import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import { Menu } from 'lucide-react';

const Header = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const user = useSelector((state) => state.user);

    const handleLogout = () => {
        localStorage.removeItem("userDetails");
        localStorage.removeItem("bearer");
        localStorage.removeItem("refreshToken");
        window.location.reload();
    };

    return (
        <div className="flex flex-col">
            {/* Header */}
            <header className="bg-white shadow-md">
                <div className="container mx-auto px-4 py-2 flex justify-between items-center">
                    <div className="flex items-center space-x-4">
                        <img
                            src="https://files.sikayetvar.com/lg/cmp/19/19424.png?1522650125"
                            alt="Pegasus Logo"
                            className="h-10"
                        />
                        <nav className="hidden md:flex space-x-4">
                            <Link to="/" className="text-gray-600 hover:text-blue-500">
                                Anasayfa
                            </Link>
                            <Link to="/flightDetail" className="text-gray-600 hover:text-blue-500">
                                Bilet Sorgu
                            </Link>
                        </nav>
                    </div>
                    <div className="flex items-center space-x-4">
                        {user ? (
                            <>
                                <span className="text-gray-600">Hoşgeldin, {user.userName}</span>
                                <button
                                    onClick={handleLogout}
                                    className="bg-red-500 text-white px-4 py-2 rounded"
                                >
                                    Çıkış Yap
                                </button>
                            </>
                        ) : (
                            <Link to="/Login" className="bg-orange-500 text-white px-4 py-2 rounded">
                                Giriş Yap
                            </Link>
                        )}
                        <Menu className="md:hidden" />
                    </div>
                </div>
            </header>
        </div>
    );
};

export default Header;
