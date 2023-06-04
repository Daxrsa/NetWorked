import { Box } from '@mui/material'
import { DataGrid } from '@mui/x-data-grid'
import React from 'react'
import moment from "moment";
import { IApplication } from '../../Interfaces/global.typing'
import { baseJobUrl } from '../../constants/urlConstants';
import { PictureAsPdf } from '@mui/icons-material'
import Button from "@mui/material/Button/Button";

const column: GridColDef[] = [
    { field: "id", headerName: "Id", width: 300 },
    { field: "applicantId", headerName: "ApplicantId", width: 300 },
    {
        field: "dateApplied",
        headerName: "Date Applied",
        width: 200,
        renderCell: (params) => moment(params.row.dateApplied).fromNow(),
    },
    { field: "jobTitle", headerName: "Applied At:", width: 200 },
    {
        field: "resumeUrl",
        headerName: "Download Resume",
        width: 250,
        renderCell: (params) => (
            <a href={`${baseJobUrl}/Application/download/${params.row.resumeUrl}`}>Download Resume
                <PictureAsPdf />
            </a>
        )
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
];

interface IApplicationsGridProps {
    data: IApplication[];
}

const ApplicationsGrid = ({ data }: IApplicationsGridProps) => {
    return (
        <Box sx={{ width: "100%", height: 450 }} >
            <DataGrid rows={data} columns={column} getRowId={(row) => row.id} rowHeight={50} />
        </Box>
    );
};

export default ApplicationsGrid;