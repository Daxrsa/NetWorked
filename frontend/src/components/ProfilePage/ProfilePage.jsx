import React, { useState, useEffect } from "react";
import {
  MDBCol,
  MDBContainer,
  MDBRow,
  MDBCard,
  MDBCardText,
  MDBCardBody,
  MDBBtn,
  MDBBreadcrumb,
  MDBBreadcrumbItem,
} from "mdb-react-ui-kit";
import Header from "../DesktopHeader";
import EditProfile from "./EditProfile";
import axios from "axios";
import UserPostList from '../Posts/UserPostList'

export default function ProfilePage() {

  //axios states:-----------------------------------------------------------------------

  const [isEditMode, setIsEditMode] = useState(false);
  const [loggedInUserName, setLoggedInUsername] = useState(null);
  const [loggedInFullname, setLoggedInFullname] = useState(null);
  const [loggedInEmail, setLoggedInEmail] = useState(null);
  const [loggedInPhone, setLoggedInPhone] = useState(null);
  const [loggedInAddress, setLoggedInAddress] = useState(null);
  const [loggedInProfession, setLoggedInProfession] = useState(null);
  const [loggedInSkills, setLoggedInSkills] = useState(null);
  const [loggedInBio, setLoggedInBio] = useState(null);

  //-------------------------------------------------------------------------------------

  //functions----------------------------------------------------------------------------

  const handleEditClick = () => {
    setIsEditMode(true);
  };

  const handleCancelClick = () => {
    setIsEditMode(false);
  };

  const handleSubmitClick = () => {
    setIsEditMode(false);
  };

  //--------------------------------------------------------------------

  useEffect(() => {
    fetchLoggedInUser();
  }, []);

  const fetchLoggedInUser = async () => {
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
      console.log(data);
      setLoggedInUsername(data.username);
      setLoggedInFullname(data.fullname);
      setLoggedInEmail(data.email);
      setLoggedInPhone(data.phone);
      setLoggedInAddress(data.address);
      setLoggedInProfession(data.profession);
      setLoggedInSkills(data.skills);
      setLoggedInBio(data.bio);
    } catch (error) {
      alert("Error fetching logged-in user:", error);
    }
  };

  return (
    <section style={{ backgroundColor: "#eee" }}>
      <Header />
      <MDBContainer className="py-5">
        <MDBRow>
          <MDBCol>
            <MDBBreadcrumb className="bg-light rounded-3 p-3 mb-4">
              <MDBBreadcrumbItem active>
                Welcome back, {loggedInUserName}!
              </MDBBreadcrumbItem>
            </MDBBreadcrumb>
          </MDBCol>
        </MDBRow>

        <MDBRow>
          <MDBCol lg="4">
            <MDBCard className="mb-4">
              <MDBCardBody className="text-center">
                <img
                  src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava3.webp"
                  alt="avatar"
                  className="rounded-circle"
                  style={{ width: "150px" }}
                />
                <p>Bio</p>
                <div className="d-flex justify-content-center mb-2">
                  {loggedInBio}.{" "}
                </div>
              </MDBCardBody>
            </MDBCard>
          </MDBCol>
          <MDBCol lg="8">
            <MDBCard className="mb-4">
              <MDBCardBody>
                <MDBRow>
                  <MDBCol sm="3">
                    <MDBCardText>Full Name</MDBCardText>
                  </MDBCol>
                  <MDBCol sm="9">
                    <MDBCardText className="text-muted">
                      {loggedInFullname}
                    </MDBCardText>
                  </MDBCol>
                </MDBRow>
                <hr />
                <MDBRow>
                  <MDBCol sm="3">
                    <MDBCardText>Email</MDBCardText>
                  </MDBCol>
                  <MDBCol sm="9">
                    <MDBCardText className="text-muted">
                      {loggedInEmail}
                    </MDBCardText>
                  </MDBCol>
                </MDBRow>
                <hr />
                <MDBRow>
                  <MDBCol sm="3">
                    <MDBCardText>Phone</MDBCardText>
                  </MDBCol>
                  <MDBCol sm="9">
                    <MDBCardText className="text-muted">
                      {loggedInPhone}
                    </MDBCardText>
                  </MDBCol>
                </MDBRow>
                <hr />
                <MDBRow>
                  <MDBCol sm="3">
                    <MDBCardText>Address</MDBCardText>
                  </MDBCol>
                  <MDBCol sm="9">
                    <MDBCardText className="text-muted">
                      {loggedInAddress}
                    </MDBCardText>
                  </MDBCol>
                </MDBRow>
                <hr />
                <MDBRow>
                  <MDBCol sm="3">
                    <MDBCardText>Profession</MDBCardText>
                  </MDBCol>
                  <MDBCol sm="9">
                    <MDBCardText className="text-muted">
                      {loggedInProfession}
                    </MDBCardText>
                  </MDBCol>
                </MDBRow>
                <hr />
                <MDBRow>
                  <MDBCol sm="3">
                    <MDBCardText>Skills</MDBCardText>
                  </MDBCol>
                  <MDBCol sm="9">
                    <MDBCardText className="text-muted">
                      {loggedInSkills}
                    </MDBCardText>
                  </MDBCol>
                </MDBRow>
              </MDBCardBody>
            </MDBCard>
          </MDBCol>
        </MDBRow>
        {isEditMode ? (
          <EditProfile
            cancel={handleCancelClick}
            submit={handleSubmitClick}
          />
        ) : (
          <MDBBtn onClick={handleEditClick}>Edit</MDBBtn>
        )}
        <hr />
        <UserPostList />
      </MDBContainer>
    </section>
  );
}
