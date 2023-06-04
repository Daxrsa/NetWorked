import { useContext, lazy, Suspense } from "react";
import React from "react";
import { Route, Routes } from "react-router-dom";
import HomePage from "./components/HomePage";
import Login from "./components/Login";
import Register from "./components/Register";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import MainChat from "./components/MainChat";
import Layout from "./components/Layout";
import GlobalStyles from "./styles/GlobalStyles";
import CompanyPage from "./components/Companies/CompanyPage";
import AddCompany from "./components/Companies/AddCompany";
import JobPage from "./components/Jobs/JobPage";
import AddJob from "./RecruiterDashboard/AddJob";
import JobApplicationPage from "./components/JobApplications/JobApplicationPage";
import AddApplication from "./components/JobApplications/AddApplication";

//const HomePage = lazy(() => import("./components/HomePage"))

function App()
{
  return (
    <>
      <Layout />
      <GlobalStyles />
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/register" element={<Register />} />
        <Route path="/login" element={<Login />} />
        <Route path="/mainchat" element={<MainChat />} />
        <Route path="/companies">
          <Route index element={<CompanyPage />} />
          <Route path="add" element={<AddCompany />} />
        </Route>
        <Route path="/jobs">
          <Route index element={<JobPage />} />
          <Route path="add" element={<AddJob />} />
        </Route>
        <Route path="/jobApplications">
          <Route index element={<JobApplicationPage />} />
          <Route path="add" element={<AddApplication />} />
        </Route>
      </Routes>
    </>
  );
}

export default App;
