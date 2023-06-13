import React from 'react';
import { Box, Button, Card, CardActionArea, CardContent, CardMedia, Grid, Typography } from '@mui/material';
import Header from "../DesktopHeader";

const JobCard: React.FC<IJob> = ({
    // title,
    // company,
    // location,
    // description,
    // requirements,
}) => {
    return (
        <div className='app companies'>
            <Header />
            <div className="heading">
                <Grid item xs={4}>
                    <Card>
                        {/* <Card key={jobPosition.id}> */}
                        <CardActionArea>
                            <CardMedia
                                component="img"
                                height="200"
                                image
                                sx={{ objectFit: 'contain' }}
                            />
                            <CardContent>
                                <Typography gutterBottom variant="h5" component="div">
                                    {/* {jobPosition.title} */}
                                    Title
                                </Typography>
                                <Typography variant="body2" color="text.secondary">
                                    {/* {jobPosition.company.name} */}
                                    Company
                                </Typography>
                                <Typography variant="body2" color="text.secondary">
                                    {/* Requirements: {jobPosition.skillSet} */}
                                    Requirements
                                </Typography>
                                <Typography variant="body2" color="text.secondary">
                                    {/* <>Skadon më: {jobPosition.expiryDate}</> */}
                                    Skadon
                                </Typography>
                                <Box sx={{ mt: 2 }}>
                                    <Button
                                        variant="contained"
                                    // onClick={() => {
                                    //     handleApply(jobPosition.id);
                                    // }}
                                    >
                                        Apply
                                    </Button>
                                </Box>
                            </CardContent>
                        </CardActionArea>
                    </Card>
                </Grid>
            </div>
        </div>
    );
}
export default JobCard;