import { Box } from '@mui/material'
import { DataGrid } from '@mui/x-data-grid'
import React from 'react'
import moment from "moment";
import { IJob } from '../../Interfaces/global.typing'

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