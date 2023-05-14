import { Container } from "@mui/material";
import React, { useState } from "react";
import { Route, Routes } from "react-router-dom";
import HomePage from "./components/HomePage";
import Login from "./components/Login";
import Navbar from "./components/Navbar";
import Register from "./components/Register";
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import MainChat from "./components/MainChat";
import Layout from './components/Layout'
import GlobalStyles from './styles/GlobalStyles'



function App() {
  return (
    <>
       <Layout />
       <GlobalStyles />
       
       
        

      
       <Routes>
         <Route path="/" element={<HomePage />} />
         <Route path="/register" element={<Register />} />
         <Route path="/login" element={<Login />} />
         <Route path="/mainchat" element={<MainChat />} />
       </Routes>
     
    </>
  );
}

export default App;
