import * as React from "react";
import { useState, useEffect } from "react";
import Box from "@mui/material/Box";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import PersonIcon from "@mui/icons-material/Person";
import DynamicFeedIcon from "@mui/icons-material/DynamicFeed";
import WorkIcon from "@mui/icons-material/Work";
import axios from "axios";
import ApprovalIcon from '@mui/icons-material/Approval';
import ApartmentIcon from '@mui/icons-material/Apartment';

//http://localhost:5194/api/v1/Application/countApplications
//http://localhost:5194/api/v1/Company/countCompanies
//http://localhost:5194/api/v1/JobPosition/countJobPositions

const bull = (
  <Box
    component="span"
    sx={{ display: "inline-block", mx: "2px", transform: "scale(0.8)" }}
  >
    •
  </Box>
);

export default function OutlinedCard() {
  const [userCount, setUserCount] = useState("");
  const [postCount, setPostCount] = useState("");
  const [applicationCount, setApplicationCount] = useState("");
  const [companyCount, setCompanyCount] = useState("");
  const [jobpositionCount, setJobpositionCount] = useState("");

  useEffect(() => {
    axios
      .get("http://localhost:5116/api/User/countUsers", {})
      .then((response) => {
        console.log(response.data);
        setUserCount(response.data);
      });
  }, []);

  useEffect(() => {
    axios
      .get("http://localhost:5263/api/Post/countPosts", {})
      .then((response) => {
        console.log(response.data);
        setPostCount(response.data);
      });
  }, []);

  useEffect(() => {
    axios
      .get("http://localhost:5194/api/v1/Application/countApplications", {})
      .then((response) => {
        console.log(response.data);
        setApplicationCount(response.data);
      });
  }, []);

  useEffect(() => {
    axios
      .get("http://localhost:5194/api/v1/Company/countCompanies", {})
      .then((response) => {
        console.log(response.data);
        setCompanyCount(response.data);
      });
  }, []);

  useEffect(() => {
    axios
      .get("http://localhost:5194/api/v1/JobPosition/countJobPositions", {})
      .then((response) => {
        console.log(response.data);
        setJobpositionCount(response.data);
      });
  }, []);

  const card = (
    <React.Fragment>
      <CardContent>
        <PersonIcon />
        <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
          Registered Users:
        </Typography>
        <Typography variant="h5" component="div">
          {userCount}
        </Typography>
      </CardContent>
      <hr />
      <CardContent>
        <DynamicFeedIcon />
        <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
          Posts Made:
        </Typography>
        <Typography variant="h5" component="div">
          {postCount}
        </Typography>
      </CardContent>
      <hr />
      <CardContent>
        <WorkIcon />
        <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
          Jobs Made:
        </Typography>
        <Typography variant="h5" component="div">
          {jobpositionCount}
        </Typography>
      </CardContent>
      <hr />
      <CardContent>
        <ApprovalIcon />
        <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
          Applications Made:
        </Typography>
        <Typography variant="h5" component="div">
          {applicationCount}
        </Typography>
      </CardContent>
      <hr />
      <CardContent>
        <ApartmentIcon />
        <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
          Companies Registered:
        </Typography>
        <Typography variant="h5" component="div">
          {companyCount}
        </Typography>
      </CardContent>
    </React.Fragment>
  );

  return (
    <Box sx={{ minWidth: 275 }}>
      <Card variant="outlined">{card}</Card>
    </Box>
  );
}
