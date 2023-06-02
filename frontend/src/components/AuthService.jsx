import React from 'react';

export const logout = () => {
    localStorage.removeItem("jwtToken");
    window.location.reload();
  };

export const isAuthenticated = () => {
  return localStorage.getItem("jwtToken");
};

