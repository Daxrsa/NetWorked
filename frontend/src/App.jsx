import React from "react";
import { Route, Routes } from "react-router-dom";
//import HomePage from "./components/HomePage";
import Login from "./components/Login";
import Register from "./components/Register";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import MainChat from "./components/MainChat";
import Layout from "./components/Layout";
import GlobalStyles from "./styles/GlobalStyles";


function App() {
  return (
    <>
      <GlobalStyles />
      <Routes>
        <Route path="/" element={<Layout />}/>
        <Route path="/register" element={<Register />} />
        <Route path="/login" element={<Login />} />
        <Route path="/mainchat" element={<MainChat />} />
      </Routes>
    </>
  );
}

export default App;
