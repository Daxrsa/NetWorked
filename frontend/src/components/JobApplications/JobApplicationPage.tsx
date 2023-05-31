import React from "react"
import { useEffect, useState } from 'react'
import httpModule from '../../Helpers/http.module'
import { IApplication, ICompany } from '../../Interfaces/global.typing'
import { Button, CircularProgress } from '@mui/material'
import { useNavigate } from 'react-router-dom'
import ApplicationsGrid from "./JobApplicationGrid.tsx"
import { Add } from '@mui/icons-material'

function JobApplicationPage() {
    const [applications, setApplications] = useState<IApplication[]>([]);
    const redirect = useNavigate();
    const [loading, setLoading] = useState<boolean>(false);

    useEffect(() => {
        setLoading(true);
        httpModule
            .get<IApplication[]>("/Application")
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

    //console.log(companies);

    return (
        <div className='app companies'>
            <div className="heading">
                <h2 className="h2c">Job Applications</h2>
                <Button variant='outlined' onClick={() => redirect("/jobApplications/add")}>
                    <Add />
                </Button>
            </div>
            {loading ? (
                <CircularProgress size={100} />
            ) : applications.length === 0 ? (
                <h1>No Job Application</h1>
            ) : (
                <ApplicationsGrid data={applications} />
            )}
        </div>
    );
};

export default JobApplicationPage;