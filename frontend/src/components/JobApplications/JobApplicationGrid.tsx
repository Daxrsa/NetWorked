import { Box } from '@mui/material'
import { DataGrid } from '@mui/x-data-grid'
import React from 'react'
import moment from "moment";
import { IApplication } from '../../Interfaces/global.typing'
import { baseJobUrl } from '../../constants/urlConstants';
import { PictureAsPdf } from '@mui/icons-material'

const column: GridColDef[] = [
    { field: "id", headerName: "Id", width: 300 },
    { field: "applicantId", headerName: "ApplicantId", width: 300 },
    {
        field: "dateApplied",
        headerName: "Date Applied",
        width: 200,
        renderCell: (params) => moment(params.row.dateApplied).fromNow(),
    },
    { field: "jobTitle", headerName: "jobTitle", width: 200 },
    {
        field: "resumeUrl",
        headerName: "Download",
        width: 250,
        renderCell: (params) => (
            <a href={`${baseJobUrl}/Application/download/${params.row.resumeUrl}`}>Download Resume
                <PictureAsPdf />
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