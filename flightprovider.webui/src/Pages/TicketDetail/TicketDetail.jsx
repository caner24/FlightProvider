import React, { useState } from 'react';
import { Search } from 'lucide-react';

const TicketDetail = () => {
    const [searchId, setSearchId] = useState('');
    const [flightData, setFlightData] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);

    const handleSearch = async () => {
        setIsLoading(true);
        setError(null);
        try {
            const response = await fetch(`https://localhost:7242/api/v1/Flight/GetFlightWithNumber?flightNumber=${searchId}`);
            if (!response.ok) {
                throw new Error('Uçuş bulunamadi !.');
            }
            const data = await response.json();
            console.log(data);
            setFlightData(data);
        } catch (error) {
            console.error('Error fetching flight data:', error);
            setError(error.message);
            setFlightData(null);
        } finally {
            setIsLoading(false);
        }
    };

    const handlePurchase = () => {
        // Implement purchase logic here
        console.log('Purchase initiated for flight:', flightData);
    };

    return (
        <div className="container mx-auto px-4 py-8">
            <h1 className="text-2xl font-bold mb-4">Ticket Details</h1>
            <div className="mb-4 flex items-center space-x-2">
                <input
                    type="text"
                    value={searchId}
                    onChange={(e) => setSearchId(e.target.value)}
                    placeholder="Enter flight ID"
                    className="border rounded px-2 py-1 flex-grow"
                />
                <button
                    onClick={handleSearch}
                    disabled={isLoading}
                    className="bg-blue-500 text-white px-4 py-1 rounded hover:bg-blue-600 transition-colors disabled:opacity-50"
                >
                    {isLoading ? 'Searching...' : 'Search'}
                </button>
            </div>
            {error && (
                <div className="text-red-500 mb-4">{error}</div>
            )}
            {flightData && (
                <div className="bg-white shadow rounded-lg p-4 mb-4">
                    <p>Origin: {flightData.flightDetail[0].arrivalCity}</p>
                    <p>Destination: {flightData.flightDetail[0].departureCity}</p>
                    <p>Departure: {flightData.flightDetail[0].arrivalDate}, {flightData.flightDetail[0].departureTime}</p>
                    <p>Arrival: {flightData.flightDetail[0].departureDate}, {flightData.flightDetail[0].arrivalTime}</p>
                    <p>Price: {flightData.totalPrice} TL</p>
                </div>
            )}

        </div>
    );
};

export default TicketDetail;