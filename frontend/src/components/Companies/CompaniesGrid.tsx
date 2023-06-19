import { Box } from '@mui/material'
import { DataGrid } from '@mui/x-data-grid'
import { React, useEffect } from 'react'
import { ICompany } from '../../Interfaces/global.typing'
import Button from "@mui/material/Button/Button";
import httpModule from '../../Helpers/http.module';

const column: GridColDef[] = [
    { field: "id", headerName: "Id", width: 100 },
    { field: "name", headerName: "Name", width: 200 },
    { field: "size", headerName: "Size", width: 150 },
    { field: "cityLocation", headerName: "CityLocation", width: 200 },
    { field: "logo", headerName: "Logo", width: 100 },
];

interface ICompaniesGridProps {
    data: ICompany[];
}

const handleDelete = async (itemId) => {
    try {
        await httpModule.delete(`/Company/${itemId}`);
        useEffect()
    } catch (error) {
        console.error('Error deleting item:', error);
    }
};

const CompaniesGrid = ({ data }: ICompaniesGridProps) => {
    return (
        <Box sx={{ width: "90%", height: 500, margin: "4%" }} >
            <DataGrid
                rows={data}
                columns={[
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
                ]}
                getRowId={(row) => row.id}
                rowHeight={50} />
        </Box>
    );
};

export default CompaniesGrid;