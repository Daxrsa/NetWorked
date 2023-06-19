import React from "react"
import { useEffect, useState } from 'react'
import httpModule from '../../Helpers/http.module'
import { ICompany } from '../../Interfaces/global.typing'
import { Button, CircularProgress } from '@mui/material'
import { useNavigate } from 'react-router-dom'
import JobsGrid from "./JobsGrid.tsx"
import { Add } from '@mui/icons-material'
import Header from "../DesktopHeader";
import Sidebar from "../sidebar/Sidebar"
import NavBar from "../../RecruiterDashboard/Navbar"

function JobPage() {
  const [jobs, setJobs] = useState<IJob[]>([]);
  const redirect = useNavigate();
  const [loading, setLoading] = useState<boolean>(false);

  useEffect(() => {
    setLoading(true);
    httpModule.get("/JobPosition")
      .then(response => {
        setJobs(response.data);
        setLoading(false);
      })
      .catch((error) => {
        alert("Error");
        console.log(error);
        setLoading(false);
      });
  }, []);

  return (
    <div className='app'>
      <NavBar />
      <div className='companies'>
        <div className="heading">
          <h2 className="h2c"></h2>
          <Button variant='outlined' onClick={() => redirect("/jobDashboard/add")}>
            <Add />
          </Button>
        </div>
        {loading ? (
          <CircularProgress size={100} />
        ) : jobs.length === 0 ? (
          <h1>No Job Position</h1>
        ) : (
          <JobsGrid className="jobs-grid-container" data={jobs} />
        )}
      </div>
    </div>
  );
};

export default JobPage;