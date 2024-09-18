import React, { useEffect, useState } from 'react';
import { useLocation, Link } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';

const TicketDetails = () => {
    const [departureFlights, setDepartureFlights] = useState(null);
    const [returnFlights, setReturnFlights] = useState(null);

    const departure = useSelector((state) => state.departure);
    const returnFlightsData = useSelector((state) => state.origin);
    const navigate = useNavigate();

    useEffect(() => {
        console.log('Departure from Redux store:', departure);
        console.log('Return flights from Redux store:', returnFlightsData);

        if (!departure || !returnFlightsData) {
            navigate('/', { replace: true }); 
        } else {
            setDepartureFlights(departure); 
            setReturnFlights(returnFlightsData);
        }
    }, [departure, returnFlightsData, navigate]); 

    return (
        <div className="container mx-auto px-4 py-8">
            <h1 className="text-2xl font-bold mb-4">Ticket Details</h1>
            <div className="mb-4">
                <h2 className="text-xl font-semibold">Departure Flights</h2>
                <div className="bg-white shadow rounded-lg p-4 mb-2">
                    <p>Departure: {departureFlights ? new Date(departureFlights.departureDateTime).toLocaleString() : 'Loading...'}</p>
                    <p>Arrival: {departureFlights ? new Date(departureFlights.arrivalDateTime).toLocaleString() : 'Loading...'}</p>
                    <p>Price: {departureFlights ? departureFlights.price : 'Loading...'} TL</p>
                </div>
            </div>
            <div className="mb-4">
                <h2 className="text-xl font-semibold">Return Flights</h2>
                <div className="bg-white shadow rounded-lg p-4 mb-2">
                    <p>Origin: {returnFlights ? returnFlights.origin : 'Loading...'}</p>
                    <p>Destination: {returnFlights ? returnFlights.destination : 'Loading...'}</p>
                </div>
            </div>
            <Link to="/" className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600 transition-colors">
                Back to Search
            </Link>
        </div>
    );
};

export default TicketDetails;
