import React, { useState, useEffect } from "react";
import Card from "./Card";
import ListUsers from "./ListUsers";
import ListPosts from "./ListPosts";
import JobList from "./JobList";
import axios from 'axios';

export default function AdminDashboard() {
  const [loggedInUserName, setLoggedInUsername] = useState(null);
  useEffect(() => {
    fetchLoggedInAdmin();
  }, []);

  const fetchLoggedInAdmin = async () => {
    try {
      const token = localStorage.getItem("jwtToken");
      const response = await axios.get(
        "http://localhost:5116/api/Auth/GetloggedInUser",
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      const data = response.data;
      console.log(data.username);
      setLoggedInUsername(data.username);
    } catch (error) {
      alert("Error fetching logged-in admin.");
    }
  };

  return (
    <>
      <h1>Welcome back, {loggedInUserName}!</h1>
      <Card />
      <ListUsers />
      <ListPosts />
      <JobList />
    </>
  );
}
