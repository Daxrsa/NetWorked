import React, { useState } from 'react';
import { ICreateCompanyDto } from '../../Interfaces/global.typing';
import TextField from '@mui/material/TextField/TextField';
import FormControl from "@mui/material/FormControl/FormControl";
import InputLabel from "@mui/material/InputLabel/InputLabel";
import Select from "@mui/material/Select/Select";
import MenuItem from "@mui/material/MenuItem/MenuItem";
import Button from "@mui/material/Button/Button";
import { useNavigate } from "react-router-dom";
import httpModule from "../../helpers/http.module";
import NavBar from '../../RecruiterDashboard/Navbar';

const AddCompany = () => {
    const [company, setCompany] = useState<ICreateCompanyDto>({ name: "", size: "", cityLocation: "", file: null });
    const redirect = useNavigate();
    const [statusCode, setStatusCode] = useState("");

    const handleClickSaveBtn = () => {
        const newFormData = new FormData();
        newFormData.append("name", company.name);
        newFormData.append("size", company.size);
        newFormData.append("file", company.file);

        httpModule
            .post("/Company", newFormData)
            .then((response) => {
                setStatusCode("Company saved successfully");
            })
            .catch((error) => {
                if (error.response && error.response.status === 400) {
                    setStatusCode("Already applied!");
                } else {
                    console.log(error);
                }
            });
    };

    const handleClickBackBtn = () => {
        redirect("/companies");
    };

    const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const files = event.target.files;
        if (files && files.length > 0) {
            setCompany({ ...company, file: files[0] });
        }
    };

    return (
        <div className='app'>
            <NavBar />
            <div className='somethingelse'>
                <div className="add-job">
                    <h2 className='h2c'>Create Company</h2>
                    <TextField
                        autoComplete='off'
                        label='Company Name'
                        variant='outlined'
                        value={company.name}
                        onChange={(e) => setCompany({ ...company, name: e.target.value })}
                    />
                    <FormControl fullWidth>
                        <InputLabel>Company Size</InputLabel>
                        <Select
                            value={company.size}
                            label="Company Size"
                            onChange={(e) => setCompany({ ...company, size: e.target.value })}
                        >
                            <MenuItem value="Small">SMALL</MenuItem>
                            <MenuItem value="Medium">MEDIUM</MenuItem>
                            <MenuItem value="Large">LARGE</MenuItem>
                        </Select>
                    </FormControl>

                    <FormControl fullWidth>
                        <InputLabel>Company Location</InputLabel>
                        <Select
                            value={company.cityLocation}
                            label="Company Size"
                            onChange={(e) => setCompany({ ...company, cityLocation: e.target.value })}
                        >
                            <MenuItem value="Small">PRISHTINA</MenuItem>
                            <MenuItem value="Medium">MITROVICA</MenuItem>
                            <MenuItem value="Large">PEJA</MenuItem>
                        </Select>
                    </FormControl>

                    <input type="file" onChange={handleFileChange} />

                    <div className="btns">
                        <Button variant="outlined" color="primary" onClick={handleClickSaveBtn}>
                            Save
                        </Button>
                        <Button variant="outlined" color="secondary" onClick={handleClickBackBtn}>
                            Back
                        </Button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default AddCompany;
