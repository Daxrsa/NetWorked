import React, { useState, useEffect } from "react";

export const logout = () =>
{
  localStorage.removeItem("jwtToken");
  localStorage.removeItem("loggedInUser");
  localStorage.removeItem("role");
  window.location.reload();
};

export const isAuthenticated = () =>
{
  return localStorage.getItem("jwtToken");
};

export const isRoleAllowed = () =>
{
  return localStorage.getItem("role");
}

