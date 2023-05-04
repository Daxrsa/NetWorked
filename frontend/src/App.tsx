import { Container } from "@mui/material";
import React from "react";
import { Route, Routes } from "react-router-dom";
import HomePage from "./components/HomePage";
import Login from "./components/Login";
import Navbar from "./components/Navbar";
import Register from "./components/Register";

function App() {
  return (
    <>
      <Navbar />
      <Container style={{ marginTop: "7em" }}>
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