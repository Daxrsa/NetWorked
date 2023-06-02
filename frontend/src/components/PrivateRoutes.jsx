import React from 'react';
import { isAuthenticated } from './AuthService';
import { Navigate, Outlet } from 'react-router-dom';

const PrivateRoutes = ({children, ...rest}) => {
    const authenticated = isAuthenticated();
    return (
        authenticated ? <Outlet/> : <Navigate to='/login'/>
      )
}
export default PrivateRoutes;
