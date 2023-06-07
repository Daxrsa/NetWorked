import React, { useState, useEffect } from 'react'
import { ICreateJobDto, ICompany, IJob } from '../../Interfaces/global.typing'
import { } from '@mui/material'
import TextField from '@mui/material/TextField/TextField'
import FormControl from "@mui/material/FormControl/FormControl";
import InputLabel from "@mui/material/InputLabel/InputLabel";
import Select from "@mui/material/Select/Select";
import MenuItem from "@mui/material/MenuItem/MenuItem";
import Button from "@mui/material/Button/Button";
import { useNavigate } from "react-router-dom";
import httpModule from "../../helpers/http.module";

const AddApplication = () => {
    const [application, setApplication] = useState<ICreateApplicationDto>({
        applicantId: "",
        jobId: 0
    });
    const [jobs, setJobs] = useState<IJob[]>([]);
    const [pdfFile, setPdfFile] = useState<File | null>();
    const redirect = useNavigate();

    useEffect(() => {
        httpModule
            .get<IJob[]>("/JobPosition")
            .then(response => {
                setJobs(response.data);
            })
            .catch((error) => {
                console.log(error);
            });
    }, []);

    const handleClickSaveBtn = () => {
        if (
            application.jobId === 0 ||
            application.applicantId === "" ||
            !pdfFile
        ) {
            alert("Fill all fields");
            return;
        }

        console.log(pdfFile)
        const newFormData = new FormData();
        newFormData.append("jobId", application.jobId);
        newFormData.append("applicantId", application.applicantId);
        newFormData.append("file", pdfFile);

        httpModule
            .post("/Application", newFormData)
            .then((response) => redirect("/jobApplications"))
            .catch((error) => console.log(error));
    };

    const handleClickBackBtn = () => {
        redirect("/jobApplications");
    };

    return (
        <div className='app companies'>
            <div className="add-application">
                <h2 className='h2c'>Add New Job</h2>
                <FormControl fullWidth>
                    <InputLabel>Job</InputLabel>
                    <Select
                        value={application.jobId}
                        label="Job"
                        onChange={(e) => setApplication({ ...application, jobId: e.target.value })}
                    >
                        {jobs.map((item) => (
                            <MenuItem key={item.id} value={item.id}>
                                {item.title}
                            </MenuItem>
                        ))}
                    </Select>
                </FormControl>

                <TextField
                    autoComplete='off'
                    label='Applicant Id'
                    variant='outlined'
                    value={application.applicantId}
                    onChange={e => setApplication({ ...application, applicantId: e.target.value })}
                />

                <input type="file" onChange={(event) => setPdfFile(event.target.files ? event.target.files[0] : null)} />

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
    )
}

export default AddApplication;