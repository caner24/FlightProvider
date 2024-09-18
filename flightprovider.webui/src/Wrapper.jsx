import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './Router.jsx'
import FlightProvider from '../src/context/FlightProvider.jsx';


export default function Wrapper() {

    return (
        <FlightProvider>
            <App />
        </FlightProvider>
    );
}