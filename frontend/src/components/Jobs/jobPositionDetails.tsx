import { Box, Button, Container, Skeleton, Typography, CircularProgress } from '@mui/material';
import FmdGoodOutlinedIcon from '@mui/icons-material/FmdGoodOutlined';
import AccessTimeOutlinedIcon from '@mui/icons-material/AccessTimeOutlined';
import CalendarTodayOutlinedIcon from '@mui/icons-material/CalendarTodayOutlined';
import CalendarMonthOutlinedIcon from '@mui/icons-material/CalendarMonthOutlined';
import Header from "../DesktopHeader";
import httpModule from '../../Helpers/http.module'
import React, { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom';
import { useParams } from 'react-router-dom';
import { IJob } from '../../Interfaces/global.typing';
import moment from 'moment';
import { green } from '@mui/material/colors';

export default function JobPositonDetails() {
    const [job, setJob] = useState<IJob>({
        companyName: "",
        createdAt: "",
        description: "",
        jobCategory: "",
        requirements: "",
        title: "",
        username: "",
        companyLogo: ""
    })
    const { id }: { id: int } = useParams()
    const [isFileUploaded, setIsFileUploaded] = useState(false);
    const [errorMessage, setErrorMessage] = useState('');
    const [statusCode, setStatusCode] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState(false);

    const handleFileUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files && event.target.files[0];
        if (file) {
            if (file.type === 'application/pdf') {
                setIsFileUploaded(true);
                setErrorMessage('');
            } else {
                setIsFileUploaded(false);
                setErrorMessage('Only PDF files are accepted.');
            }
        } else {
            setIsFileUploaded(false);
            setErrorMessage('');
        }
    };

    useEffect(() => {
        httpModule
            .get(`/JobPosition/${id}`)
            .then(response => {
                setJob(response.data);
                console.log(response.data)
            })
            .catch((error) => {
                console.log(error);
            });
    }, [id])

    const imageSrc = `http://localhost:33364/Resources/${job.companyLogo}`;

    const [application, setApplication] = useState<ICreateApplicationDto>({
        applicantId: "",
        jobId: 0
    });
    //const [jobs, setJobs] = useState<IJob[]>([]);
    const [pdfFile, setPdfFile] = useState<File | null>();
    const redirect = useNavigate();

    const token = localStorage.getItem("jwtToken");
    const handleClickSaveBtn = () => {
        setIsLoading(true);

        const newFormData = new FormData();
        newFormData.append("jobId", id);
        newFormData.append("applicantId", application.applicantId);
        newFormData.append("file", pdfFile);

        httpModule
            .post("/Application", newFormData, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            })
            .then((response) => {
                setIsLoading(false);
                setStatusCode("Applied successfully");
            })
            .catch((error) => {
                if (error.response && error.response.status === 400) {
                    setStatusCode("Already applied!");
                } else {
                    console.log(error);
                }
                setIsLoading(false);
            });
    };

    return (
        <div className="heading jobWrap">
            <Header />
            <Container>
                <Box sx={{ display: 'flex' }}>
                    <div>
                        <img src={imageSrc} alt="Image" width={100} height={100} />
                    </div>
                    <Box sx={{ ml: 4 }}>
                        <Typography variant="h3">{job.title}</Typography>
                        <span>{job.createdAt}</span>
                    </Box>
                </Box>
                <Box sx={{ width: '75%' }}>
                    <Typography variant="body1" sx={{ mt: 4, lineHeight: 2 }}>
                        {job.description}
                    </Typography>
                    <Box component="div" sx={{ mt: 4, mb: 4 }}>
                        <Box
                            component="div"
                            sx={{ display: 'flex', alignItems: 'center', mb: 2 }}
                        >
                            <FmdGoodOutlinedIcon sx={{ mr: 1 }} /> {job.jobCategory}
                        </Box>
                        <Box
                            component="div"
                            sx={{ display: 'flex', alignItems: 'center', mb: 2 }}
                        >
                            <AccessTimeOutlinedIcon sx={{ mr: 1 }} />{job.requirements}
                        </Box>
                        <Box
                            component="div"
                            sx={{ display: 'flex', alignItems: 'center', mb: 2 }}
                        >
                            <CalendarMonthOutlinedIcon sx={{ mr: 1 }} /> {moment(job.createdAt).fromNow()}
                        </Box>
                        <Box
                            component="div"
                            sx={{ display: 'flex', alignItems: 'center', mb: 2 }}
                        >
                            <CalendarTodayOutlinedIcon sx={{ mr: 1 }} /> 07/02/2023
                        </Box>
                    </Box>
                    <Box>
                        <div className="file-upload-container">
                            <input type="file" accept=".pdf" onChange={(event) => {
                                handleFileUpload(event);
                                setPdfFile(event.target.files ? event.target.files[0] : null);
                            }} />
                            {errorMessage && <p className="error-message">{errorMessage}</p>}
                            {statusCode !== null && (
                                <p className="status-message">Message: {statusCode}</p>
                            )}
                        </div>
                        <Button variant="contained" disabled={!isFileUploaded || isLoading} onClick={handleClickSaveBtn}>
                            {isLoading ? <CircularProgress size={24} /> : 'Apply'}
                        </Button>
                    </Box>
                </Box>
            </Container>
        </div>
    );
}