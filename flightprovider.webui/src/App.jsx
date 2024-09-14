import React, { useState } from 'react';
import { Search, Calendar, Users } from 'lucide-react';

const FlightBookingTemplate = () => {
    const [tripType, setTripType] = useState('roundTrip');

    return (
        <div className="min-h-screen bg-gray-100 flex items-center justify-center">
            <div className="bg-white p-8 rounded-lg shadow-md w-full max-w-3xl">
                <h1 className="text-3xl font-bold text-blue-600 mb-6">Uçuş Ara</h1>

                <div className="flex space-x-4 mb-6">
                    <button
                        className={`px-4 py-2 rounded ${tripType === 'roundTrip' ? 'bg-blue-600 text-white' : 'bg-gray-200'
                            }`}
                        onClick={() => setTripType('roundTrip')}
                    >
                        Gidiş-Dönüş
                    </button>
                    <button
                        className={`px-4 py-2 rounded ${tripType === 'oneWay' ? 'bg-blue-600 text-white' : 'bg-gray-200'
                            }`}
                        onClick={() => setTripType('oneWay')}
                    >
                        Tek Yön
                    </button>
                </div>

                <div className="grid grid-cols-2 gap-4 mb-6">
                    <div className="col-span-2 sm:col-span-1">
                        <label className="block text-sm font-medium text-gray-700 mb-1">Nereden</label>
                        <div className="relative">
                            <input
                                type="text"
                                className="w-full p-2 border rounded-md pl-10"
                                placeholder="Kalkış havaalanı"
                            />
                            <Search className="absolute left-3 top-2.5 h-5 w-5 text-gray-400" />
                        </div>
                    </div>
                    <div className="col-span-2 sm:col-span-1">
                        <label className="block text-sm font-medium text-gray-700 mb-1">Nereye</label>
                        <div className="relative">
                            <input
                                type="text"
                                className="w-full p-2 border rounded-md pl-10"
                                placeholder="Varış havaalanı"
                            />
                            <Search className="absolute left-3 top-2.5 h-5 w-5 text-gray-400" />
                        </div>
                    </div>
                    <div className="col-span-2 sm:col-span-1">
                        <label className="block text-sm font-medium text-gray-700 mb-1">Gidiş Tarihi</label>
                        <div className="relative">
                            <input
                                type="date"
                                className="w-full p-2 border rounded-md pl-10"
                            />
                            <Calendar className="absolute left-3 top-2.5 h-5 w-5 text-gray-400" />
                        </div>
                    </div>
                    {tripType === 'roundTrip' && (
                        <div className="col-span-2 sm:col-span-1">
                            <label className="block text-sm font-medium text-gray-700 mb-1">Dönüş Tarihi</label>
                            <div className="relative">
                                <input
                                    type="date"
                                    className="w-full p-2 border rounded-md pl-10"
                                />
                                <Calendar className="absolute left-3 top-2.5 h-5 w-5 text-gray-400" />
                            </div>
                        </div>
                    )}
                    <div className="col-span-2 sm:col-span-1">
                        <label className="block text-sm font-medium text-gray-700 mb-1">Yolcu Sayısı</label>
                        <div className="relative">
                            <input
                                type="number"
                                className="w-full p-2 border rounded-md pl-10"
                                placeholder="1"
                                min="1"
                            />
                            <Users className="absolute left-3 top-2.5 h-5 w-5 text-gray-400" />
                        </div>
                    </div>
                </div>

                <button className="w-full bg-orange-500 text-white py-2 px-4 rounded-md hover:bg-orange-600 transition duration-300">
                    Uçuş Ara
                </button>
            </div>
        </div>
    );
};

export default FlightBookingTemplate;