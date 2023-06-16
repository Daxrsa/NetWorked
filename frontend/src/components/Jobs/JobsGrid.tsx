import { Box, CardMedia } from '@mui/material'
import { DataGrid } from '@mui/x-data-grid'
import React from 'react'
import moment from "moment";
import { IJob } from '../../Interfaces/global.typing'
import Button from "@mui/material/Button/Button";
// import './grid.css'

const column: GridColDef[] = [
    { field: "id", headerName: "Id", width: 100 },
    { field: "title", headerName: "Title", width: 200 },
    { field: "description", headerName: "Description", width: 150 },
    { field: "requirements", headerName: "Requirements", width: 200 },
    { field: "jobCategory", headerName: "JobCategory", width: 100 },
    { field: "jobLevel", headerName: "JobLevel", width: 100 },
    { field: "companyName", headerName: "CompanyName", width: 100 },
    {
        field: "createdAt",
        headerName: "Creation Time",
        width: 150,
        renderCell: (params) => moment(params.row.createdAt).fromNow(),
    },
    {
        field: "edit",
        headerName: "Edit",
        width: 100,
        renderCell: (params) => (
            <a href="#">
                <Button variant="contained" color="success">
                    Edit
                </Button>
            </a>
        )
    },
    {
        field: "delete",
        headerName: "Delete",
        width: 100,
        renderCell: (params) => (
            <a href="#">
                <Button variant="outlined" color="error">
                    Delete
                </Button>
            </a>
        )
    },
    {
        field: "img",
        headerName: "Logo",
        width: 100,
        renderCell: (params) => (
            <CardMedia
                component="img"
                height="200"
                image
                sx={{ objectFit: 'contain' }}
            />
        )
    },
];

interface IJobsGridProps {
    data: IJob[];
}

const JobsGrid = ({ data }: IJobsGridProps) => {
    return (
        <Box sx={{ width: "100%", height: 450 }} >
            <DataGrid rows={data} columns={column} getRowId={(row) => row.id} rowHeight={50} />
        </Box>
    );
};

export default JobsGrid;