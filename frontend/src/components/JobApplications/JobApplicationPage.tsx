import React from "react"
import { useEffect, useState } from 'react'
import httpModule from '../../Helpers/http.module'
import { IApplication, ICompany } from '../../Interfaces/global.typing'
import { Button, CircularProgress } from '@mui/material'
import { useNavigate } from 'react-router-dom'
import ApplicationsGrid from "./JobApplicationGrid.tsx"
import { Add } from '@mui/icons-material'
import NavBar from "../../RecruiterDashboard/Navbar"

function JobApplicationPage() {
    const [applications, setApplications] = useState<IApplication[]>([]);
    const redirect = useNavigate();
    const [loading, setLoading] = useState<boolean>(false);

    useEffect(() => {
        setLoading(true);
        httpModule
            .get<IApplication[]>("/Application/jobId/3015")
            .then(response => {
                setApplications(response.data);
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
                    <h2 className="h2c"></h2>
                </div>
                {loading ? (
                    <CircularProgress size={100} />
                ) : applications.length === 0 ? (
                    <h1>No Job Application Found</h1>
                ) : (
                    <ApplicationsGrid data={applications} />
                )}
            </div>
        </div>
    );
};

export default JobApplicationPage;