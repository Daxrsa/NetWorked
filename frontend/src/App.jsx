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
import Payment from "./components/Payment/Payment"
import JobDisplay from "./components/Jobs/JobDisplay";
import JobPositonDetails from "./components/Jobs/jobPositionDetails";
import AdminDashboard from './components/Admin/AdminDashboard';
import AddArtikulli from "./RecruiterDashboard/AddArtikulli";
import AddArticle from "./components/CRUD/AddArticle";
import ArticleList from "./components/CRUD/ArticleList";
import EditArticle from "./components/CRUD/EditArticle";
import AddComment from "./components/CRUD/AddComment";
import CommentList from "./components/CRUD/CommentList";
import EditComment from "./components/CRUD/EditComment";
import AddSpec from "./components/CRUD/AddArticle";

function App()
{
  return (
    <>
      <GlobalStyles />
      <Routes>
        <Route path="/specializimi/add" element={<AddSpec />} />
        <Route path="/specializimi/page" element={<ArticleList />} />
        <Route path="/specializimi/edit/:id" element={<EditArticle />} />


        <Route path="/comment/add" element={<AddComment />} />
        <Route path="/comment" element={<CommentList />} />
        <Route path="/comment/edit/:id" element={<EditComment />} />



        <Route path="/" element={<Layout />} />
        <Route path="/register" element={<Register />} />
        <Route path="/login" element={<Login />} />
        <Route path="job/details/:id" element={<JobPositonDetails />} />
        <Route path="/jobs" element={<JobDisplay />} />

        <Route element={<PrivateRoutes allowedRoles={["Recruiter", "Applicant", "Admin"]} />}>
          <Route path="/mainchat" element={<MainChat />} />
          <Route path="/profilePage" element={<ProfilePage />} />
          <Route path="/posts" element={<PostDashboard />} />

        </Route>

        <Route element={<PrivateRoutes allowedRoles={["Applicant"]} />}>
          <Route path="/payment" element={<Payment />} />
        </Route>

        <Route element={<PrivateRoutes allowedRoles={["Admin", "Recruiter"]} />}>
          <Route path="/admin" element={<AdminDashboard />} />
          <Route path="/companies">
            <Route index element={<CompanyPage />} />
            <Route path="add" element={<AddCompany />} />
          </Route>
        </Route>

        <Route element={<PrivateRoutes allowedRoles={["Recruiter"]} />}>
          <Route path="/jobDashboard">
            <Route index element={<JobPage />} />
            <Route path="add" element={<AddJob />} />
          </Route>
          <Route path="/jobDashboard/applications/:id">
            <Route index element={<JobApplicationPage />} />
          </Route>
        </Route>
      </Routes>
    </>
  );
}

export default App;
