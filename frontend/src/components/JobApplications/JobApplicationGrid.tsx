import { Box } from '@mui/material'
import { DataGrid } from '@mui/x-data-grid'
import React, { useEffect, useState } from 'react'
import moment from "moment";
import { IApplication, IResult } from '../../Interfaces/global.typing'
import { baseJobUrl } from '../../constants/urlConstants';
import { PictureAsPdf } from '@mui/icons-material'
import Button from "@mui/material/Button/Button";
import httpModule from '../../Helpers/http.module';
import axios from 'axios';

const column: GridColDef[] = [
    { field: "id", headerName: "Id", width: 100 },
    { field: "applicantId", headerName: "ApplicantId", width: 300 },
    {
        field: "dateApplied",
        headerName: "Date Applied",
        width: 150,
        renderCell: (params) => moment(params.row.dateApplied).fromNow(),
    },
    { field: "jobTitle", headerName: "Applied At:", width: 150 },
    // { field: "Matching result", headerName: "Matching result", width: 200 },
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
];
const token = localStorage.getItem("jwtToken");
const handleDelete = async (itemId) => {
    try {
        await httpModule.delete(`/Application/${itemId}`, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        });
        useEffect()
    } catch (error) {
        console.error('Error deleting item:', error);
    }
};

interface IApplicationsGridProps {
    data: IApplication[];
}

const ApplicationsGrid = ({ data }: IApplicationsGridProps) => {
    return (
        <Box sx={{ width: "100%", height: 600 }} >
            <DataGrid rows={data} columns={[
                ...column,
                {
                    field: 'delete',
                    headerName: 'Delete',
                    width: 100,
                    renderCell: (params) => (
                        <Button
                            variant='outlined'
                            color='error'
                            onClick={() => handleDelete(params.row.id)}
                        >
                            Delete
                        </Button>
                    ),
                },]}
                getRowId={(row) => row.id}
                rowHeight={50} />
        </Box>
    );
};

export default ApplicationsGrid;