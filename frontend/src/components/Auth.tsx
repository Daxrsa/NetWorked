import React from "react";
//import jwt from "jsonwebtoken";

export const isAuthenticated = () => {
  const token = localStorage.getItem("jwtToken");

  if (token) {
    try {
      const decodedToken = jwt.verify(token, "your-secret-key");
      return true;
    } catch (error) {
      return false;
    }
  } else {
    return false;
  }
};
