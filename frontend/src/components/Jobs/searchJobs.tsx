import React, { useState } from 'react';
import { TextField, Button } from '@mui/material';
import './jobs.css';
import { SearchOutlined } from '@mui/icons-material';

const SearchBar = ({ onSearch }) => {
    const [searchQuery, setSearchQuery] = useState('');

    const handleSearch = () => {
        onSearch(searchQuery);
    };

    const handleChange = event => {
        const { value } = event.target;
        setSearchQuery(value);
        onSearch(value);
    };

    return (
        <div className="search-bar-container">
            <TextField
                label="Search"
                value={searchQuery}
                className="search-input"
                onChange={handleChange}
            />
            <Button
                className="search-button"
                variant="contained"
                onClick={handleSearch}
                startIcon={<SearchOutlined />}
            >
                Search
            </Button>
        </div>
    );
};

export default SearchBar;
