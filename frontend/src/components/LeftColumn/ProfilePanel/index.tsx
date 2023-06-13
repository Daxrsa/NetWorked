import React, { useState,useEffect } from "react";
import axios from 'axios'

import Panel from '../../Panel';

import { Container } from './styles';

const ProfilePanel: React.FC = () => {
  const [loggedInUserName, setUsername] = useState(null);
  const [loggedInSkills, setSkills] = useState(null);
  const fetchLoggedInUser = async () => {
    try {
      const token = localStorage.getItem('jwtToken'); // Retrieve the token from localStorage
      const response = await axios.get("http://localhost:5116/api/Auth/GetloggedInUser", {
        headers: {
          Authorization: `Bearer ${token}` // Add the token to the request headers
        }
      });
      const udata = response.data; // No need to parse the JSON object
      console.log(udata);
      setUsername(udata.username);
      setSkills(udata.skills);
    } catch (error) {
      console.error("Error fetching logged-in user:", error);
    }
  };
  useEffect(() => {
    fetchLoggedInUser();
  }, []);
  return (
    <Panel>
      <Container>
        <div className="profile-cover"></div>
        <img
          src="https://avatars.githubusercontent.com/u/93683494?v=4"
          alt="Avatar"
          className="profile-picture"
        />
        <h1> {loggedInUserName}</h1>

        <h2>
        {loggedInSkills}
        </h2>

        <div className="separator"></div>

      </Container>
    </Panel>
  );
};

export default ProfilePanel;
