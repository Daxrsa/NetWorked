import { Box, CardMedia } from '@mui/material'
import { DataGrid } from '@mui/x-data-grid'
import React from 'react'
import moment from "moment";
import { IJob } from '../../Interfaces/global.typing'
import Button from "@mui/material/Button/Button";
import httpModule from '../../Helpers/http.module';
import { useNavigate } from 'react-router-dom';

const column: GridColDef[] = [
    { field: "id", headerName: "Id", width: 85 },
    { field: "title", headerName: "Title", width: 100 },
    { field: "description", headerName: "Description", width: 150 },
    { field: "requirements", headerName: "Requirements", width: 200 },
    { field: "jobCategory", headerName: "JobCategory", width: 200 },
    { field: "jobLevel", headerName: "JobLevel", width: 100 },
    { field: "companyName", headerName: "CompanyName", width: 100 },
    {
        field: "createdAt",
        headerName: "Creation Time",
        width: 95,
        renderCell: (params) => moment(params.row.createdAt).fromNow(),
    },
    {
        field: "expireDate",
        headerName: "Expiration",
        width: 95,
        renderCell: (params) => {
            const expireDate = moment(params.row.expireDate);
            const currentDate = moment();
            const daysLeft = expireDate.diff(currentDate, 'days');
            return `${daysLeft} days`;
        },
    },
];

const token = localStorage.getItem("jwtToken");
const handleDelete = async (itemId) => {
    try {
        await httpModule.delete(`/JobPosition/${itemId}`, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        });
        //useEffect()
    } catch (error) {
        console.error('Error deleting item:', error);
    }
};

interface IJobsGridProps {
    data: IJob[];
}

const JobsGrid = ({ data }: IJobsGridProps) => {
    const redirect = useNavigate();
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
                },
                {
                    field: 'applications',
                    headerName: 'Applications',
                    width: 200,
                    renderCell: (params) => (
                        <Button
                            variant='outlined'
                            color='info'
                            onClick={() => redirect(`applications/${params.row.id}`)}
                        >
                            Check Applications
                        </Button>
                    ),
                },
            ]}
                getRowId={(row) => row.id}
                rowHeight={50} />
        </Box>
    );
};

export default JobsGrid;