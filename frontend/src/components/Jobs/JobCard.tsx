import React from 'react'
import { Box, Grid, Typography, Button } from '@mui/material'
import { Column } from '../MiddleColumn/FeedPost/styles';
import { IJob } from '../../Interfaces/global.typing'
import './jobs.css';
import moment from 'moment';
import httpModule from '../../Helpers/http.module'
import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'

const JobCard = (props) => {
    const [id, setId] = useState();
    const redirect = useNavigate();

    const handleClickBtn = (id: int) => {
        redirect(`/job/details/${id}`)
    }
    const imageSrc = `http://localhost:33364/Resources/${props.companyLogo}`;

    return (
        <Box className="box">
            <Grid container>
                <Grid item xs>
                    {/* <Typography variant='subtitle1'>{props.title}</Typography> */}
                    <div>
                        <img src={imageSrc} alt="Image" width={80} height={80} />
                    </div>
                    <Typography variant='subtitle1'>Position: {props.title}</Typography>
                </Grid>
                <Grid item container xs>
                    <b>Required skills:</b>  {props.requirements}
                </Grid>
                <Grid item container direction="column" alignItems="flex-end" xs>
                    <Typography variant=''>CREATED | CATEGORY | LEVEL</Typography>
                    <Typography variant='caption'>{moment(props.createdAt).fromNow()} | {props.jobCategory} | {props.jobLevel}</Typography>
                    <Box mt-2>
                        <Button onClick={() => handleClickBtn(props.id)} variant='outlined'>Check</Button>
                    </Box>
                </Grid>
            </Grid>
        </Box>
    )
}

export default JobCard