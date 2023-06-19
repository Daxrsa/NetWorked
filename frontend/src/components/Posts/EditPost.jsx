import React from "react";
import axios from "axios";
import { MDBCol, MDBBtn, MDBInput, MDBTextArea } from "mdb-react-ui-kit";


export default function EditPost({ handleCancelClick}) {
  return (
    <>
        <MDBCol lg="15">
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
        <MDBBtn color="success" type="submit">
          Submit
        </MDBBtn>
        <MDBBtn color="danger" onClick={handleCancelClick}>Cancel</MDBBtn>
    </>
  );
}
