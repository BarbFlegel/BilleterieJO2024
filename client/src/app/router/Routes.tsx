import { Navigate, createBrowserRouter } from 'react-router-dom';
import App from '../layout/App';
import Catalog from '../../features/catalog/Catalog';
import ProductDetails from '../../features/catalog/ProductDetails';
import ErrorsPage from '../../features/errors/ErrorsPage';
import ContactPage from '../../features/contact/ContactPage';
import ServerError from '../errors/ServerError';
import NotFound from '../errors/NotFound';
import BasketPage from '../../features/basket/BasketPage';
import Login from '../../features/account/Login';
import Register from '../../features/account/Register';
import RequireAuth from './RequireAuth';
import Orders from '../../features/orders/Orders';
import CheckoutWrapper from '../../features/checkout/CheckoutWrapper';
import Inventory from '../../features/admin/Inventory';
import Promotion from '../../features/admin/Promotion';

export const router = createBrowserRouter(([
    {
        path: '/',
        element: <App />,
        children: [
            {
                // authenticated routes
                element: <RequireAuth />, children: [
                    { path: '/checkout', element: <CheckoutWrapper /> },
                    { path: '/orders', element: <Orders /> },
                ]
            },
            {
                // admin routes
                element: <RequireAuth roles={['Admin']} />, children: [
                    { path: '/', element: <Inventory /> },
                ]
            },
            {
                element: <RequireAuth roles={['Admin']} />, children: [
                    { path: '/promotion', element: <Promotion /> },
                ]
            },
            { path: 'catalog', element: <Catalog /> },
            { path: 'catalog/:id', element: <ProductDetails /> },
            { path: 'errors', element: <ErrorsPage /> },
            { path: 'contact', element: <ContactPage /> },
            { path: '/server-error', element: <ServerError /> },
            { path: '/not-found', element: <NotFound /> },
            { path: '/basket', element: <BasketPage /> },
            { path: '/login', element: <Login /> },
            { path: '/register', element: <Register /> },
            { path: '*', element: <Navigate replace to='/not-found' /> },
        ]
    }
]))