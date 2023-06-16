import React from 'react'
import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import Header from "../DesktopHeader";
import JobCard from './JobCard';
import httpModule from '../../Helpers/http.module'
import { IJob } from '../../Interfaces/global.typing'

const JobDisplay = () => {
    const [jobs, setJobs] = useState<IJob[]>([]);
    const redirect = useNavigate();
    const [loading, setLoading] = useState<boolean>(false);

    useEffect(() => {
        setLoading(true);
        httpModule
            .get("/JobPosition")
            .then(response => {
                setJobs(response.data);
                setLoading(false);
            })
            .catch((error) => {
                //alert("Error");
                console.log(error);
                setLoading(false);
            });
    }, []);
    return (
        <div className="heading jobWrap">
            <Header />
            {jobs.map(job => <JobCard key={job.id} {...job} />)}
        </div>
    )
}

export default JobDisplay