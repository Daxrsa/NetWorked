import React, { useState, useEffect } from 'react'
import { ICreateJobDto, ICompany, ICategory } from '../Interfaces/global.typing'
import { } from '@mui/material'
import TextField from '@mui/material/TextField/TextField'
import FormControl from "@mui/material/FormControl/FormControl";
import InputLabel from "@mui/material/InputLabel/InputLabel";
import Select from "@mui/material/Select/Select";
import MenuItem from "@mui/material/MenuItem/MenuItem";
import Button from "@mui/material/Button/Button";
import { useNavigate } from "react-router-dom";
import httpModule from "../helpers/http.module";
import './addToDb.css';
import NavBar from './Navbar';

const AddJob = () => {
    const [job, setJob] = useState<ICreateJobDto>({
        title: "",
        description: "",
        requirements: "",
        categoryId: "",
        jobLevel: "",
        companyId: ""
    });
    const [companies, setCompanies] = useState<ICompany[]>([]);
    const [categories, setCategories] = useState<ICategory[]>([]);
    const redirect = useNavigate();

    useEffect(() => {
        httpModule.get("/Company")
            .then(response => {
                setCompanies(response.data);
            })
            .catch((error) => {
                console.log(error);
            });
    }, []);

    useEffect(() => {
        httpModule.get("/Category")
            .then(response => {
                setCategories(response.data)
            })
            .catch((error) => {
                console.log(error)
            });
    }, []);

    const token = localStorage.getItem("jwtToken");
    const handleClickSaveBtn = () => {
        if (job.title === "" || job.description === "" || job.requirements === "" || job.categoryId === 0 || job.jobLevel === "" || job.companyId === "") {
            alert("Fill all fields");
            return;
        }
        httpModule
            .post("/JobPosition", job, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            })
            .then((response) => redirect("/jobs"))
            .catch((error) => console.log(error));
    };

    const handleClickBackBtn = () => {
        redirect("/jobs");
    };

    return (
        <div className='app'>
            <NavBar />
            <div className=' somethingelse'>
                <div className="add-job">
                    <h2 className='h2c'>Add New Job</h2>
                    <TextField
                        autoComplete='off'
                        label='Job Title'
                        variant='outlined'
                        value={job.title}
                        onChange={e => setJob({ ...job, title: e.target.value })}
                    />

                    <TextField
                        autoComplete='off'
                        label='Job Description'
                        variant='outlined'
                        value={job.description}
                        onChange={e => setJob({ ...job, description: e.target.value })}
                    />

                    <TextField
                        autoComplete='off'
                        label='Job Requirements'
                        variant='outlined'
                        value={job.requirements}
                        onChange={e => setJob({ ...job, requirements: e.target.value })}
                    />

                    <FormControl fullWidth>
                        <InputLabel>Job Category</InputLabel>
                        <Select
                            value={job.categoryId}
                            label="Category"
                            onChange={(e) => setJob({ ...job, categoryId: e.target.value })}
                        >
                            {categories.map((item) => (
                                <MenuItem key={item.id} value={item.id}>
                                    {item.name}
                                </MenuItem>
                            ))}
                        </Select>
                    </FormControl>

                    <FormControl fullWidth>
                        <InputLabel>Job Level</InputLabel>
                        <Select
                            value={job.jobLevel}
                            label="Job Level"
                            onChange={(e) => setJob({ ...job, jobLevel: e.target.value })}
                        >
                            <MenuItem value="INTERN">INTERN</MenuItem>
                            <MenuItem value="JUNIOR">JUNIOR</MenuItem>
                            <MenuItem value="MIDLEVEL">MIDLEVEL</MenuItem>
                            <MenuItem value="SENIOR">SENIOR</MenuItem>
                            <MenuItem value="CTO">CTO</MenuItem>
                            <MenuItem value="CEO">CEO</MenuItem>
                        </Select>
                    </FormControl>

                    <FormControl fullWidth>
                        <InputLabel>Company</InputLabel>
                        <Select
                            value={job.companyId}
                            label="Company"
                            onChange={(e) => setJob({ ...job, companyId: e.target.value })}
                        >
                            {companies.map((item) => (
                                <MenuItem key={item.id} value={item.id}>
                                    {item.name}
                                </MenuItem>
                            ))}
                        </Select>
                    </FormControl>

                    <div className="btns">
                        <Button variant="outlined" color="secondary" onClick={handleClickBackBtn}>
                            Back
                        </Button>
                        <Button variant="outlined" color="primary" onClick={handleClickSaveBtn}>
                            Save
                        </Button>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default AddJob