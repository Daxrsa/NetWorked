import { Container } from "@mui/material";
import React, { useState } from "react";
import { Route, Routes } from "react-router-dom";
import HomePage from "./components/HomePage";
import Login from "./components/Login";
import Navbar from "./components/Navbar";
import Register from "./components/Register";
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import Lobby from './components/Lobby';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Chat from './components/Chat';


function App() {
  const [connection, setConnection] = useState();
  const [messages, setMessages] = useState([]);
  

  const joinRoom = async (user, room) => {
    try {
      const connection = new HubConnectionBuilder()
        .withUrl("https://localhost:44315/chat")
        .configureLogging(LogLevel.Information)
        .build();

        connection.on("ReceiveMessage", (user, message) => {
          setMessages(messages => [...messages, { user, message }]);
        });
  
  

      

      await connection.start();
      await connection.invoke("JoinRoom", { user, room });
      setConnection(connection);
    } catch (e) {
      console.log(e);
    }
  }

 

  

  return (
    <>
      <Navbar />
      <Container style={{ marginTop: "7em" }}>
       
        <div className='app'>
    <h2>MyChat</h2>
    <hr className='line' />
    {!connection
      ? <Lobby joinRoom={joinRoom} />
      : <Chat messages={messages}  />}
  </div>

      
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/register" element={<Register />} />
          <Route path="/login" element={<Login />} />
        </Routes>
      </Container>
      
    </>
  );
}

export default App;
