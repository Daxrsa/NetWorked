import React, { useState } from 'react'
import { ICreateCompanyDto } from '../../Interfaces/global.typing'
import { } from '@mui/material'
import TextField from '@mui/material/TextField/TextField'
import FormControl from "@mui/material/FormControl/FormControl";
import InputLabel from "@mui/material/InputLabel/InputLabel";
import Select from "@mui/material/Select/Select";
import MenuItem from "@mui/material/MenuItem/MenuItem";
import Button from "@mui/material/Button/Button";
import { useNavigate } from "react-router-dom";
import httpModule from "../../helpers/http.module";

const AddCompany = () => {
    const [company, setCompany] = useState<ICreateCompanyDto>({ name: "", size: "", cityLocation: "", file: null });
    const [image, setImage] = useState<File | null>();
    const redirect = useNavigate();

    const handleClickSaveBtn = () => {
        if (company.name === "" || company.size === "" || company.cityLocation === "") {
            alert("Fill all fields");
            return;
        }
        console.log(company.name)
        console.log(company.file)
        httpModule
            .post("/Company", company)
            .then((responst) => redirect("/companies"))
            .catch((error) => console.log(error));
    };

    const handleClickBackBtn = () => {
        redirect("/companies");
    };

    return (
        <div className='app companies'>
            <div className="add-company">
                <h2 className='h2c'>Add new Company</h2>
                <TextField
                    autoComplete='off'
                    label='Company Name'
                    variant='outlined'
                    value={company.name}
                    onChange={e => setCompany({ ...company, name: e.target.value })}
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

                <input type="file" onChange={(e) => setCompany({ ...company, file: e.target.value })} />

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
    )
}

export default AddCompany