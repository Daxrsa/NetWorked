import React, { useState } from "react";
import { MDBCol, MDBBtn, MDBInput, MDBTextArea } from "mdb-react-ui-kit";

export default function EditProfile({ cancel, submit }) {
  //capture react states:-------------------------------------------------------------------------

  const [fullname, setFullName] = useState("");
  const [email, setEmail] = useState("");
  const [phone, setPhone] = useState("");
  const [address, setAddress] = useState("");
  const [profession, setProfession] = useState("");
  const [skills, setSkills] = useState("");
  const [bio, setBio] = useState("");

  //functions:-------------------------------------------------------------------------------------

  const postData = () => {
    console.log(fullname);
    console.log(email);
    console.log(phone);
    console.log(address);
    console.log(profession);
    console.log(skills);
    console.log(bio);
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
