import React, { useState, useEffect } from "react";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import Lobby from "./Lobby";
import Chat from "./Chat";
import "../App.css";
import "bootstrap/dist/css/bootstrap.min.css";

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
            Authorization: "bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjQ2YzU2NTM3LWU5M2YtNDhkMy0zZWExLTA4ZGI1OWY5ODhkZiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJlcm9zZW1yaSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNjg0Nzc2NDkwfQ.aWi5gSw8XUlllXc4DpW6AzaxtTs2x7dmroU6PRZYyPFL6Szqu4qcvSU_bTQ1N4Aoa0C60ppMak6M7cc-Gyco_A", // Replace YOUR_TOKEN_HERE with your actual token
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
