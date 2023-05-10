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




function App() {
  return (
    <>
      <Navbar />
      <Container style={{ marginTop: "7em" }}>
       
        

      
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/register" element={<Register />} />
          <Route path="/login" element={<Login />} />
          <Route path="/mainchat" element={<MainChat />} />
        </Routes>
      </Container>
      
    </>
  );
}

export default App;
