import React, { useEffect, useState } from 'react';
import { useLocation, Link } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { Search } from 'lucide-react';
import axios from 'axios';
const Ticket = () => {
    const [departureFlights, setDepartureFlights] = useState(null);
    const [returnFlights, setReturnFlights] = useState(null);



    const departure = useSelector((state) => state.departure);
    const returnFlightsData = useSelector((state) => state.origin);
    const bearerToken = useSelector((state) => state.bearer);

    const navigate = useNavigate();
    useEffect(() => {

        if (!departure || !returnFlightsData) {
            navigate('/', { replace: true });
        } else {
            setDepartureFlights(departure);
            setReturnFlights(returnFlightsData);
        }
    }, [departure, returnFlightsData, navigate]);



    async function postData() {


        const date = new Date(departureFlights.departureDateTime);
        const timeStringArrival = date.toLocaleString('en-US', { hour: '2-digit', minute: '2-digit', second: '2-digit', hour12: false });

        const dateOrigin = new Date(returnFlights.departureDateTime);
        const timeStringOrigin = date.toLocaleString('en-US', { hour: '2-digit', minute: '2-digit', second: '2-digit', hour12: false });

        console.log(departureFlights.departureDateTime);
        const requestData = {
            "email": "string",
            "departureDate": departureFlights.departureDateTime,
            "departureTime": timeStringOrigin,
            "arrivalDate": departureFlights.arrivalDateTime,
            "arrivalTime": timeStringArrival,
            "departureCity": returnFlights.origin,
            "arrivalCity": returnFlights.departure,
            "flightNo": "string",
            "amount": 1,
            "totalPrice": departureFlights.price
        };

        // Send Axios request
        await axios.post('https://localhost:7242/api/v1/Stripe/createCheckoutSession', requestData, {
            headers: {
                Authorization: `Bearer ${bearerToken.bearer}`
            }
        })
            .then(response => {

                const stripeUrl = response.headers.location;
                if (stripeUrl)
                    window.location.href = stripeUrl;
            })
            .catch(error => {
                console.error('Error:', error);
            });
    }


    return (
        <div className="container mx-auto px-4 py-8">
            <h1 className="text-2xl font-bold mb-4">Ticket Details</h1>
            <div className="mb-4 flex flex-col">
                <div className="bg-white shadow rounded-lg p-4 mb-2">
                    <p>Origin: {returnFlights ? returnFlights.origin : 'Loading...'}</p>
                    <p>Destination: {returnFlights ? returnFlights.departure : 'Loading...'}</p>
                    <p>Departure: {departureFlights ? new Date(departureFlights.departureDateTime).toLocaleString() : 'Loading...'}</p>
                    <p>Arrival: {departureFlights ? new Date(departureFlights.arrivalDateTime).toLocaleString() : 'Loading...'}</p>
                    <p>Price: {departureFlights ? departureFlights.price : 'Loading...'} TL</p>
                </div>
                <button
                    onClick={postData}
                    type="submit"
                    className="bg-orange-500 text-white px-6 py-2 rounded flex items-center 'cursor-not-allowed opacity-50' : 'hover:bg-orange-600 transition-colors">
                    <Search className="mr-2" />
                    Satın Al
                </button>

            </div>

            <Link to="/" className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600 transition-colors">
                Back to Search
            </Link>
        </div>
    );
};

export default Ticket;
