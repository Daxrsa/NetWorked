import React, { useState, useEffect } from "react";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import Lobby from "./Lobby";
import Chat from "./Chat";
import "../App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import Header from '../components/DesktopHeader/index'

const MainChat = () => {
  const [connection, setConnection] = useState();
  const [messages, setMessages] = useState([]);
  const [users, setUsers] = useState([]);
  const [username, setUsername] = useState("");
  useEffect(() => {
    const fetchUsername = async () => {
      try {
        const response = await fetch("https://localhost:7212/api/Auth/Getloggedinuser", {
          headers: {
            Authorization: "bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjI3NTk5NTYwLWI2MjYtNGY0OC1kM2U5LTA4ZGI1YWZlNGVlNyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJlcm9zaTEyM2EiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTY4NDg3MTY3NH0.IxqR3z7rLJ5foeeJ8Mzpr-9P50zv--a0JKjzHRq_Yx19TUQ8Qi3cE7FBbt6dWcWPCkv4Hy_EpcM2Gc4dhoIpTQ", // Replace YOUR_TOKEN_HERE with your actual token
          },
        });

        const user = await response.text();
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
