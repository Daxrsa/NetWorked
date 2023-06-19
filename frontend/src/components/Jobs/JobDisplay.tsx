import React from 'react'
import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import Header from "../DesktopHeader";
import JobCard from './JobCard';
import httpModule from '../../Helpers/http.module'
import { IJob } from '../../Interfaces/global.typing'
import SearchBar from './searchJobs';
import FilterDropdown from './filterJobs';

const JobDisplay = () => {
    const [jobs, setJobs] = useState<IJob[]>([]);
    const [filteredJobs, setFilteredJobs] = useState<IJob[]>([]);
    const [loading, setLoading] = useState<boolean>(false);

    const token = localStorage.getItem("jwtToken");
    useEffect(() => {
        setLoading(true);
        httpModule
            .get('/JobPosition', {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            })
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

    const handleFilter = async (query: string) => {
        try {
            const response = await httpModule.get(`/Search/filter?result=${query}`);
            const filterData = response.data;
            setFilteredJobs(filterData);
            console.log(filterData)
        } catch (error) {
            console.log('Filter error: ', error);
        }
    }

    return (
        <div className="heading jobWrap">
            <Header />
            <SearchBar onSearch={handleSearch} />
            <FilterDropdown onSelect={handleFilter} />
            {filteredJobs.map(job => <JobCard key={job.id} {...job} />)}
        </div>
    );
};

export default JobDisplay