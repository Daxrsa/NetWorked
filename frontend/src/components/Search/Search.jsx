import React, { useState } from 'react';
import styled from 'styled-components';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const SearchContainer = styled.div`
  display: flex;
  align-items: center;
  margin-top: -42px;
  margin-left:100px;
`;

export const SearchInput = styled.input`
  margin-left: 12px;

  background: var(--color-white);
  color: var(--color-black);
  font-size: 14px;
  padding: 7.5px 8px;
  border: none;
  outline: none;
  border-radius: 2px;
  width: 300px;

  &:focus {
    background: var(--color-white);
  }
`;

const SearchButton = styled.button`
  background: var(--color-white);
  color: #050535;
  font-size: 14px;
  padding: 7.5px 16px;
  border: none;
  outline: none;
  border-radius: 2px;
  margin-left: 5px;
`;

const SearchComponent = () => {
  const navigate = useNavigate();
  const [searchQuery, setSearchQuery] = useState('');
  const [searchResults, setSearchResults] = useState([]);

  const handleSearch = async () => {
    try {
      const response = await axios.post('http://localhost:8800/notifications/search', { username: searchQuery });
      console.log(response.data)
      setSearchResults(response.data);
      // Navigate to a new page passing the searchQuery as a URL parameter
      navigate(`/user/${searchQuery}`);
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div>
      <SearchContainer>
        <SearchInput
          type="text"
          placeholder="Search..."
          value={searchQuery}
          onChange={(e) => setSearchQuery(e.target.value)}
        />
        <SearchButton onClick={handleSearch}>Search</SearchButton>
      </SearchContainer>
      {/* Render search results */}
      {searchResults.map((result) => (
        <div key={result.id}>{result.message}</div>
      ))}
    </div>
  );
};

export default SearchComponent;
