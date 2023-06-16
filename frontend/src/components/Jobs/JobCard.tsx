import React from 'react'
import { Box, Grid, Typography, Button } from '@mui/material'
import { Column } from '../MiddleColumn/FeedPost/styles';
import { IJob } from '../../Interfaces/global.typing'
import './jobs.css';
import moment from 'moment';

const JobCard = (props) => {
    return (
        <Box className="box">
            <Grid container>
                <Grid item xs>
                    <Typography variant='subtitle1'>{props.title}</Typography>
                    <Typography className='companyName' variant='subtitle2'>{props.companyName}</Typography>
                </Grid>
                <Grid item container xs>
                    <b>Required skills:</b>  {props.requirements}
                </Grid>
                <Grid item container direction="column" alignItems="flex-end" xs>
                    <Typography variant=''>CREATED | CATEGORY | LEVEL</Typography>
                    <Typography variant='caption'>{moment(props.createdAt).fromNow()} | {props.jobCategory} | {props.jobLevel}</Typography>
                    <Box mt-2>
                        <Button variant='outlined'>Check</Button>
                    </Box>
                </Grid>
            </Grid>
        </Box>
    )
}

export default JobCard