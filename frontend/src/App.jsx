import { useContext, lazy, Suspense } from "react";
import React from "react";
import { Route, Routes } from "react-router-dom";
import Login from "./components/Login";
import Register from "./components/Register";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import MainChat from "./components/MainChat";
import Layout from "./components/Layout";
import GlobalStyles from "./styles/GlobalStyles";
import ProfilePage from "./components/ProfilePage/ProfilePage";
import PostDashboard from "./components/Posts/PostDashboard";
import PrivateRoutes from "./components/PrivateRoutes";
import CompanyPage from "./components/Companies/CompanyPage";
import AddCompany from "./components/Companies/AddCompany";
import JobPage from "./components/Jobs/JobPage";
import AddJob from "./RecruiterDashboard/AddJob";
import JobApplicationPage from "./components/JobApplications/JobApplicationPage";
import AddApplication from "./components/JobApplications/AddApplication";
import Payment from "./components/Payment/Payment"
import JobDisplay from "./components/Jobs/JobDisplay";
import JobPositonDetails from "./components/Jobs/jobPositionDetails";
import AdminDashboard from './components/Admin/AdminDashboard';

function App()
{
  return (
    <>
      <GlobalStyles />
      <Routes>
        <Route path="/" element={<Layout />} />
        <Route path="/register" element={<Register />} />
        <Route path="/login" element={<Login />} />
        <Route path="/posts" element={<PostDashboard />} />
        <Route path="jobDetails/:id" element={<JobPositonDetails />} />
        <Route path="/jobs" element={<JobDisplay />} />
        <Route path="/companies">
          <Route index element={<CompanyPage />} />
          <Route path="add" element={<AddCompany />} />
        </Route>
        <Route path="/jobDashboard">
          <Route index element={<JobPage />} />
          <Route path="add" element={<AddJob />} />
        </Route>
        <Route path="/jobApplications">
          <Route index element={<JobApplicationPage />} />
          <Route path="add" element={<AddApplication />} />
        </Route>
        <Route element={<PrivateRoutes />}>
          <Route path="/mainchat" element={<MainChat />} />
          <Route path="/profilePage" element={<ProfilePage />} />
          <Route path="/payment" element={<Payment />} />
          <Route path="/admin" element={<AdminDashboard />} />
        </Route>
      </Routes>
    </>
  );
}

export default App;
