import React from "react"
import { useEffect, useState } from 'react'
import httpModule from '../../Helpers/http.module'
import { ICompany } from '../../Interfaces/global.typing'
import { Button, CircularProgress } from '@mui/material'
import { useNavigate } from 'react-router-dom'
import { Add } from '@mui/icons-material'
import CompaniesGrid from "./CompaniesGrid"
import NavBar from "../../RecruiterDashboard/Navbar"
import JobApplicationsTable from "../JobApplications/ApplicationTable"

function CompanyPage() {
  const [companies, setCompanies] = useState<ICompany[]>([]);
  const [loading, setLoading] = useState<boolean>(false);

  const redirect = useNavigate();
  useEffect(() => {
    setLoading(true);
    httpModule.get("/Company")
      .then(response => {
        setCompanies(response.data);
        setLoading(false);
      })
      .catch((error) => {
        //alert("Error");
        console.log(error);
        setLoading(false);
      });
  }, []);

  return (
    <div className='app'>
      <NavBar />
      <div className='companies'>
        <div className="heading">
          <Button variant='outlined' onClick={() => redirect("/companies/add")}>
            <Add />Create new company
          </Button>

        </div>
        {loading ? (
          <CircularProgress size={100} />
        ) : companies.length === 0 ? (
          <h1>No Company</h1>
        ) : (
          <CompaniesGrid data={companies} />
        )}
      </div>
    </div>
  );
};

export default CompanyPage;