import React, { useState, useEffect } from "react";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import Lobby from "./Lobby";
import Chat from "./Chat";
import "../App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import Header from '../components/DesktopHeader/index'
import axios from 'axios'

const MainChat = () => {
  const [connection, setConnection] = useState();
  const [messages, setMessages] = useState([]);
  const [users, setUsers] = useState([]);
  const [username, setUsername] = useState("");
  useEffect(() => {
    const fetchUsername = async () => {
      try {
        const token = localStorage.getItem('jwtToken');
        const response = await axios.get("http://localhost:5116/api/Auth/GetloggedInUser", {
          headers: {
            Authorization: `Bearer ${token}` // Add the token to the request headers
          },
        });

        const user = await response.data.username;
        console.log(user);
        setUsername(user);
      } catch (error) {
        console.log(error);
      }
    };

    fetchUsername();
  }, []);
  const joinRoom = async (user, room) => {
    try {
      const connection = new HubConnectionBuilder()
        .withUrl("https://localhost:44315/chat")
        .configureLogging(LogLevel.Information)
        .build();

      connection.on("UsersInRoom", (users) => {
        setUsers(users);
      });

      connection.on("ReceiveMessage", (user, message) => {
        const newMessage = { id: generateUniqueId(), user, message };
        setMessages((messages) => [...messages, newMessage]);
      });

      connection.onclose((e) => {
        setConnection();
        setMessages([]);
        setUsers([]);
      });

      await connection.start();
      await connection.invoke("JoinRoom", { user, room });
      setConnection(connection);
    } catch (e) {
      console.log(e);
    }
  };

  const closeConnection = async () => {
    try {
      await connection.stop();
    } catch (e) {
      console.log(e);
    }
  };

  const sendMessage = async (message) => {
    try {
      await connection.invoke("SendMessage", message);
    } catch (e) {
      console.log(e);
    }
  };

  const deleteMessage = async (messageId) => {
    try {
      const updatedMessages = messages.filter((m) => m.id !== messageId);
      setMessages(updatedMessages);
    } catch (e) {
      console.log(e);
    }
  };

  const handleDeleteMessage = async (messageId) => {
    try {
      await deleteMessage(messageId);
    } catch (e) {
      console.log(e);
    }
  };

  const generateUniqueId = () => {
    return Math.random().toString(36).substr(2, 9);
  };

  return (
    <div className="app">
      <Header/>
      <h2 className="h2c">NetWorked</h2>
      <hr className="line" />
      {!connection ? (
        <Lobby joinRoom={joinRoom} username={username} />
      ) : (
        <Chat
          sendMessage={sendMessage}
          messages={messages}
          closeConnection={closeConnection}
          users={users}
          deleteMessage={deleteMessage}
          handleDeleteMessage={handleDeleteMessage}
        />
      )}
    </div>
  );
};

export default MainChat;
