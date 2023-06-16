import React from 'react'
import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import Header from "../DesktopHeader";
import JobCard from './JobCard';
import httpModule from '../../Helpers/http.module'
import { IJob } from '../../Interfaces/global.typing'
import SearchBar from './searchJobs';

const JobDisplay = () => {
    const [jobs, setJobs] = useState<IJob[]>([]);
    const [filteredJobs, setFilteredJobs] = useState<IJob[]>([]);
    const [loading, setLoading] = useState<boolean>(false);

    useEffect(() => {
        setLoading(true);
        httpModule
            .get('/JobPosition')
            .then(response => {
                setJobs(response.data);
                setFilteredJobs(response.data)
                setLoading(false);
            })
            .catch(error => {
                console.log(error);
                setLoading(false);
            });
    }, []);

    const handleSearch = async (searchQuery: string) => {
        try {
            const response = await httpModule.get(`/Search?result=${searchQuery}`);
            const searchData = response.data;
            setFilteredJobs(searchData);
            //console.log(searchData)
        } catch (error) {
            console.error('Search error:', error);
        }
    };
    return (
        <div className="heading jobWrap">
            <Header />
            <SearchBar onSearch={handleSearch} />
            {filteredJobs.map(job => <JobCard key={job.id} {...job} />)}
        </div>
    );
};

export default JobDisplay