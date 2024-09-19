import React, { useState, useEffect } from 'react';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import { Search } from 'lucide-react';
import { addDays, format } from 'date-fns';
import axios from 'axios';
import { Formik, Field, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { Link, useNavigate } from "react-router-dom";
import { useDispatch } from 'react-redux';

const Home = () => {
    const [departureDate, setDepartureDate] = useState(null);
    const [returnDate, setReturnDate] = useState(null);
    const [returnMinDate, setReturnMinDate] = useState(null);
    const [origin, setOrigin] = useState("");
    const [destination, setDestination] = useState("");
    const [ticketPrices, setTicketPrices] = useState({});
    const [maxDate, setMaxDate] = useState(null);
    const [flights, setFlights] = useState([]);
    const [cities, setCities] = useState([]);
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const CancelToken = axios.CancelToken;
    const source = CancelToken.source();

    useEffect(() => {
        fetchCities();
    }, []);

    const fetchCities = async () => {
        try {
            const response = await axios.get("https://localhost:7242/api/v1/Flight/GetFlightData");
            if (response.data.isSuccess) {
                setCities(response.data.cities);
            }
        } catch (error) {
            console.error("Error fetching cities:", error);
        }
    };

    const getFlightData = async (date) => {
        console.log(date.toISOString());
        try {
            const response = await axios.post("https://localhost:7242/api/v1/Flight/AvailabilitySearchRequest",
                {
                    origin: origin,
                    destination: destination,
                    departureDate: date.toISOString(),
                },
                {
                    cancelToken: source.token,
                }
            );

            const prices = response.data.reduce((acc, flight) => {
                const flightDate = new Date(flight.departureDateTime).toISOString().split('T')[0];
                acc[flightDate] = flight.price;
                return acc;
            }, {});
            setFlights(response.data);
            setTicketPrices(prices);
            const maxAvailableDate = new Date(Math.max(...Object.keys(prices).map(date => new Date(date))));
            setMaxDate(maxAvailableDate);
        } catch (err) {
            console.log(err);
        }
    };

    const handleDepartureChange = async (date) => {
        if (returnDate != null)
            setReturnDate(null);
        setDepartureDate(date);
        setReturnMinDate(date);
        await getFlightData(addDays(date, 1));
    };

    const renderDayContents = (day, date) => {
        const formattedDate = format(date, 'yyyy-MM-dd');
        const price = ticketPrices[formattedDate];

        return (
            <div className="flex flex-col items-center justify-center h-full">
                <span className="text-sm font-semibold">{day}</span>
                {price && (
                    <span className="text-xs font-medium text-green-600 mt-1">
                        {`${price} TL`}
                    </span>
                )}
            </div>
        );
    };

    const returnDateDispatch = (e) => {
        const selectedFlight = flights.find(flight => new Date(flight.departureDateTime).toISOString().split('T')[0] === format(e, 'yyyy-MM-dd'));
        dispatch({ type: 'SET_DEPARTURE', payload: selectedFlight });
        setReturnDate(e);
    }

    const handleFormSubmit = (values) => {
        setOrigin(values.origin);
        setDestination(values.destination);
        var originResponse = { origin: values.origin, departure: values.destination };
        dispatch({ type: 'SET_ORIGIN', payload: originResponse });
        if (departureDate) {
            getFlightData(addDays(departureDate, 1));
        }
    };

    return (
        <main className="flex-grow bg-gray-100 min-h-screen">
            <div className="container mx-auto px-4 py-8">
                <div className="bg-white rounded-lg shadow-md p-6">
                    <h2 className="text-2xl font-bold mb-4">Book a Flight</h2>
                    <Formik
                        initialValues={{ origin: '', destination: '' }}
                        validationSchema={Yup.object({
                            origin: Yup.string().required('Origin is required'),
                            destination: Yup.string().required('Destination is required'),
                        })}
                        onSubmit={(values) => {
                            handleFormSubmit(values);
                            navigate('/Ticket'); // Navigate to the Ticket page after form submission
                        }}
                    >
                        {({ isValid, dirty }) => (
                            <Form className="space-y-4">
                                <div className="flex flex-wrap -mx-2">
                                    <div className="w-full md:w-1/2 px-2 mb-4">
                                        <label className="block text-gray-700 mb-2">From</label>
                                        <Field
                                            name="origin"
                                            as="select"
                                            className="w-full border rounded px-3 py-2"
                                        >
                                            <option value="">Select Departure Airport</option>
                                            {cities.map((city) => (
                                                <option key={city.code} value={city.code}>
                                                    {city.title}
                                                </option>
                                            ))}
                                        </Field>
                                        <ErrorMessage name="origin" component="div" className="text-red-500 text-sm" />
                                    </div>
                                    <div className="w-full md:w-1/2 px-2 mb-4">
                                        <label className="block text-gray-700 mb-2">To</label>
                                        <Field
                                            name="destination"
                                            as="select"
                                            className="w-full border rounded px-3 py-2"
                                        >
                                            <option value="">Select Arrival Airport</option>
                                            {cities.map((city) => (
                                                <option key={city.code} value={city.code}>
                                                    {city.title}
                                                </option>
                                            ))}
                                        </Field>
                                        <ErrorMessage name="destination" component="div" className="text-red-500 text-sm" />
                                    </div>
                                </div>

                                <div className="flex flex-wrap -mx-2">
                                    <div className="w-full md:w-1/2 px-2 mb-4">
                                        <label className="block text-gray-700 mb-2">Departure</label>
                                        <DatePicker
                                            selected={departureDate}
                                            onChange={handleDepartureChange}
                                            className="w-full border rounded px-3 py-2"
                                            minDate={new Date()}
                                            calendarClassName="custom-datepicker"
                                        />
                                    </div>
                                    <div className="w-full md:w-1/2 px-2 mb-4">
                                        <label className="block text-gray-700 mb-2">Return</label>
                                        <DatePicker
                                            selected={returnDate}
                                            onChange={returnDateDispatch}
                                            renderDayContents={renderDayContents}
                                            className="w-full border rounded px-3 py-2"
                                            minDate={returnMinDate}
                                            maxDate={maxDate}
                                            disabled={!departureDate}
                                            calendarClassName="custom-datepicker"
                                        />
                                    </div>
                                </div>

                                <button
                                    type="submit"
                                    className={`bg-orange-500 text-white px-6 py-2 rounded flex items-center ${!isValid || !dirty ? 'cursor-not-allowed opacity-50' : 'hover:bg-orange-600 transition-colors'}`}
                                    disabled={!isValid || !dirty}
                                >
                                    <Search className="mr-2" />
                                    Search Flights
                                </button>
                            </Form>
                        )}
                    </Formik>
                </div>
            </div>
        </main>
    );
};

export default Home;
