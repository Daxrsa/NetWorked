import React from 'react';

export const logout = () => {
    localStorage.removeItem("jwtToken");
  };

export const isAuthenticated = () => {
  return localStorage.getItem("jwtToken");
};

