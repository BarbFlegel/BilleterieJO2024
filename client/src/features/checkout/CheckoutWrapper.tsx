import {Elements} from "@stripe/react-stripe-js";
import CheckoutPage from "./CheckoutPage";
import {loadStripe} from "@stripe/stripe-js";
import { useState, useEffect } from 'react';
import agent from '../../app/api/agent';
import LoadingComponent from '../../app/layout/LoadingComponent';
import { useAppDispatch } from '../../app/store/configureStore';
import { setBasket } from '../basket/basketSlice';

const stripePromise = loadStripe('pk_test_51PKfueKfTAK5M9Etcq92sDmbFpOJSIbBcRwHqB2CLuhXAl1FzqLFSlTLPYzJnGCH6DywpzuXnuFgTfAeHYJP8Wmp00XjUI8rhk');

export default function CheckoutWrapper() {
    const dispatch = useAppDispatch();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        agent.Payments.createPaymentIntent()
            .then(response => dispatch(setBasket(response)))
            .catch(error => console.log(error))
            .finally(() => setLoading(false))
    }, [dispatch]);

    if (loading) return <LoadingComponent message='Loading checkout' />

    return (
        <Elements stripe={stripePromise}>
            <CheckoutPage />
        </Elements>
    )
}