import React, { useState } from "react";
import { MDBCol, MDBBtn, MDBInput, MDBTextArea } from "mdb-react-ui-kit";
import axios from "axios";
import { logout } from "../AuthService";

export default function EditProfile({ cancel, submit, 
  loggedInFullname, 
  loggedInEmail, loggedInPhone, 
  loggedInAddress, loggedInProfession,
  loggedInSkills,
  loggedInBio }) {
  const [fullname, setFullName] = useState("");
  const [email, setEmail] = useState("");
  const [phone, setPhone] = useState("");
  const [address, setAddress] = useState("");
  const [profession, setProfession] = useState("");
  const [skills, setSkills] = useState("");
  const [bio, setBio] = useState("");

  const postData = async () => {
    const data = {
      fullname,
      email,
      phone,
      address,
      profession,
      skills,
      bio,
    };

    // Check if JWT token exists in local storage
    const jwtToken = localStorage.getItem("jwtToken");

    try {
      const config = {
        headers: {
          Authorization: `Bearer ${jwtToken}`,
        },
      };

      const response = await axios.put(
        "http://localhost:5116/api/User",
        data,
        config
      );

      console.log("Data sent successfully.", response.data);

      // Call logout() function after successful data send
      logout();
    } catch (error) {
      console.error("Error sending data.", error);
    }
  };

  const handleFullNameChange = (value) => {
    setFullName(value);
  };

  const handleEmailChange = (value) => {
    setEmail(value);
  };

  const handlePhoneChange = (value) => {
    setPhone(value);
  };

  const handleAddressChange = (value) => {
    setAddress(value);
  };

  const handleProfessionChange = (value) => {
    setProfession(value);
  };

  const handleSkillsChange = (value) => {
    setSkills(value);
  };

  const handleBioChange = (value) => {
    setBio(value);
  };

  return (
    <>
      <MDBCol lg="5">
        <MDBInput
          placeholder="Full Name"
          defaultValue={loggedInFullname}
          onChange={(e) => handleFullNameChange(e.target.value)}
        ></MDBInput>
        <hr />
        <MDBInput
          placeholder="Email"
          defaultValue={loggedInEmail}
          onChange={(e) => handleEmailChange(e.target.value)}
        ></MDBInput>
        <hr />
        <MDBInput
          placeholder="Phone"
          defaultValue={loggedInPhone}
          onChange={(e) => handlePhoneChange(e.target.value)}
        ></MDBInput>
        <hr />
        <MDBInput
          placeholder="Address"
          defaultValue={loggedInAddress}
          onChange={(e) => handleAddressChange(e.target.value)}
        ></MDBInput>
        <hr />
        <MDBInput
          placeholder="Profession"
          defaultValue={loggedInProfession}
          onChange={(e) => handleProfessionChange(e.target.value)}
        ></MDBInput>
        <hr />
        <MDBInput
          placeholder="Skills"
          defaultValue={loggedInSkills}
          onChange={(e) => handleSkillsChange(e.target.value)}
        ></MDBInput>
        <hr />
        <MDBTextArea
          placeholder="Bio"
          defaultValue={loggedInBio}
          onChange={(e) => handleBioChange(e.target.value)}
        ></MDBTextArea>
        <hr />
      </MDBCol>
      <MDBBtn color="success" onClick={postData} type="submit">
        Submit
      </MDBBtn>
      <MDBBtn color="danger" onClick={cancel}>
        Cancel
      </MDBBtn>
    </>
  );
}
