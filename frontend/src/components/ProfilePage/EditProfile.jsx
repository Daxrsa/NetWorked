import React, { useState } from "react";
import { MDBCol, MDBBtn, MDBInput, MDBTextArea } from "mdb-react-ui-kit";
import axios from "axios";
import { logout } from '../AuthService';

export default function EditProfile({ cancel, submit }) {
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

  return (
    <>
      <MDBCol lg="5">
        <MDBInput
          placeholder="Full Name"
          onChange={(e) => setFullName(e.target.value)}
        ></MDBInput>
        <hr />
        <MDBInput
          placeholder="Email"
          onChange={(e) => setEmail(e.target.value)}
        ></MDBInput>
        <hr />
        <MDBInput
          placeholder="Phone"
          onChange={(e) => setPhone(e.target.value)}
        ></MDBInput>
        <hr />
        <MDBInput
          placeholder="Address"
          onChange={(e) => setAddress(e.target.value)}
        ></MDBInput>
        <hr />
        <MDBInput
          placeholder="Profession"
          onChange={(e) => setProfession(e.target.value)}
        ></MDBInput>
        <hr />
        <MDBInput
          placeholder="Skills"
          onChange={(e) => setSkills(e.target.value)}
        ></MDBInput>
        <hr />
        <MDBTextArea
          placeholder="Bio"
          onChange={(e) => setBio(e.target.value)}
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
