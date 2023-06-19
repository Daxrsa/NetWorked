import React, { useEffect, useState } from 'react';
import { FormControl, InputLabel, MenuItem, Select } from '@mui/material';
import httpModule from '../../Helpers/http.module';
import { ICategory, IJob } from '../../Interfaces/global.typing';

const FilterDropdown = ({ onSelect, onSearch }) => {
    const [categories, setCategories] = useState<ICategory[]>([]);
    const [selectedOption, setSelectedOption] = useState('');

    const token = localStorage.getItem("jwtToken");
    useEffect(() => {
        httpModule
            .get('/Category', {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            })
            .then(response => {
                setCategories(response.data);
            })
            .catch(error => {
                console.log(error);
            });
    }, []);

    const handleChange = (event) => {
        const value = event.target.value;
        setSelectedOption(value);
        onSelect(value);
    };

    return (
        <div style={{ display: 'flex', alignItems: 'center' }}>
            <FormControl sx={{ m: 1, minWidth: 120 }}>
                <InputLabel id="filter-dropdown-label">Filter By</InputLabel>
                <Select
                    labelId="filter-dropdown-label"
                    id="filter-dropdown"
                    value={selectedOption}
                    onChange={handleChange}
                >
                    {categories.map((option) => (
                        <MenuItem key={option.id} value={option.name}>
                            {option.name}
                        </MenuItem>
                    ))}
                </Select>
            </FormControl>
        </div>
    );
};

export default FilterDropdown;

