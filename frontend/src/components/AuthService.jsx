import React from 'react';

export const logout = () => {
    localStorage.removeItem("jwtToken");
    localStorage.removeItem("loggedInUser");
    window.location.reload();
  };

export const isAuthenticated = () => {
  return localStorage.getItem("jwtToken");
};

