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

export default function ProfilePage() {
  const [isEditMode, setIsEditMode] = useState(false);
  const [loggedInUserName, setUsername] = useState(null);
  const [loggedInFullname, setFullname] = useState(null);
  const [loggedInEmail, setEmail] = useState(null);
  const [loggedInPhone, setPhone] = useState(null);
  const [loggedInAddress, setAddress] = useState(null);
  const [loggedInProfession, setProfession] = useState(null);
  const [loggedInSkills, setSkills] = useState(null);
  const [loggedInBio, setBio] = useState(null);
  const [fullname, setEditedFullName] = useState("");
  const [email, setEditedEmail] = useState("");
  const [skills, setEditedSkills] = useState("");
  const [phone, setEditedPhone] = useState("");
  const [address, setEditedAddress] = useState("");
  const [profession, setEditedProfession] = useState("");
  const [bio, setEditedBio] = useState("");

  //for editing
  const handleFullNameChange = (event) => {
    setEditedFullName(event.target.value);
  };

  const handleEmailChange = (event) => {
    setEditedEmail(event.target.value);
  };

  const handlePhoneChange = (event) => {
    setEditedPhone(event.target.value);
  };

  const handleAddressChange = (event) => {
    setEditedAddress(event.target.value);
  };

  const handleProfessionChange = (event) => {
    setEditedProfession(event.target.value);
  };

  const handleSkillsChange = (event) => {
    setEditedSkills(event.target.value);
  };

  const handleBioChange = (event) => {
    setEditedBio(event.target.value);
  };

  //----------------------------------------------------------------------------

  //for buttons
  const handleEditClick = () => {
    setIsEditMode(true);
  };

  const handleCancelClick = () => {
    setIsEditMode(false);
  };

  const handleSubmitClick = () => {
    setIsEditMode(false);
    console.log("Full Name:", fullname);
    console.log("Email:", email);
    console.log("Phone", phone);
    console.log("Address", address);
    console.log("Profession", profession);
    console.log("Skills", skills);
    console.log("Bio", bio);
    setEditedFullName(fullname);
    setEditedEmail(email);
    setEditedPhone(phone);
    setEditedAddress(address);
    setEditedProfession(profession);
    setEditedSkills(skills);
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
      setUsername(data.username);
      setFullname(data.fullname);
      setEmail(data.email);
      setPhone(data.phone);
      setAddress(data.address);
      setProfession(data.profession);
      setSkills(data.skills);
      setBio(data.bio);
    } catch (error) {
      console.error("Error fetching logged-in user:", error);
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
        <hr />
        {isEditMode ? (
          <EditProfile
            cancel={handleCancelClick}
            submit={handleSubmitClick}
            editFullname={handleFullNameChange}
            editEmail={handleEmailChange}
            editPhone={handlePhoneChange}
            editAddress={handleAddressChange}
            editProfession={handleProfessionChange}
            editSkills={handleSkillsChange}
            editBio={handleBioChange}
          />
        ) : (
          <MDBBtn onClick={handleEditClick}>Edit</MDBBtn>
        )}
      </MDBContainer>
    </section>
  );
}
