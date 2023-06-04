import { Box } from '@mui/material'
import { DataGrid } from '@mui/x-data-grid'
import React from 'react'
import { ICompany } from '../../Interfaces/global.typing'
import Button from "@mui/material/Button/Button";

const column: GridColDef[] = [
    { field: "id", headerName: "Id", width: 100 },
    { field: "name", headerName: "Name", width: 200 },
    { field: "size", headerName: "Size", width: 150 },
    { field: "cityLocation", headerName: "CityLocation", width: 200 },
    { field: "logo", headerName: "Logo", width: 100 },
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

interface ICompaniesGridProps {
    data: ICompany[];
}

const CompaniesGrid = ({ data }: ICompaniesGridProps) => {
    return (
        <Box sx={{ width: "100%", height: 450 }} >
            <DataGrid rows={data} columns={column} getRowId={(row) => row.id} rowHeight={50} />
        </Box>
    );
};

export default CompaniesGrid;