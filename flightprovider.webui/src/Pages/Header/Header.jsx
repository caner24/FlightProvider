import React from 'react';
import { Search, Menu, ChevronDown, Facebook, Twitter, Instagram } from 'lucide-react';
import { Link } from 'react-router-dom';
const Header = () => {
    return (
        <div className="flex flex-col">
            {/* Header */}
            <header className="bg-white shadow-md">
                <div className="container mx-auto px-4 py-2 flex justify-between items-center">
                    <div className="flex items-center space-x-4">
                        <img src="https://files.sikayetvar.com/lg/cmp/19/19424.png?1522650125" alt="Pegasus Logo" className="h-10" />
                        <nav className="hidden md:flex space-x-4">
                            <Link to={"/"} className="text-gray-600 hover:text-blue-500">Anasayfa</Link>
                            <a href="#" className="text-gray-600 hover:text-blue-500">Manage</a>
                            <a href="#" className="text-gray-600 hover:text-blue-500">Where We Fly</a>
                            <a href="#" className="text-gray-600 hover:text-blue-500">Information</a>
                        </nav>
                    </div>
                    <div className="flex items-center space-x-4">
                        <Link to={"/Login"} className="bg-orange-500 text-white px-4 py-2 rounded">Giriş Yap</Link>
                        <Menu className="md:hidden" />
                    </div>
                </div>
            </header>
        </div>
    );
};

export default Header;