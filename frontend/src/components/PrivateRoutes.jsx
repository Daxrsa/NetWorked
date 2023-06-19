import { isAuthenticated } from './AuthService';
import { Navigate, Outlet } from 'react-router-dom';
import React, { useState, useEffect } from "react";

const PrivateRoutes = ({ allowedRoles }) =>
{
  const role = localStorage.getItem("role");
  const authenticated = isAuthenticated();

  const isRoleAllowed = allowedRoles.includes(role);

  return (
    authenticated && isRoleAllowed ? <Outlet /> : <Navigate to='/' />
  );
};

export default PrivateRoutes;

