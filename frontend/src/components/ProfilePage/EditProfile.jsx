import React, { useState } from "react";
import { MDBCol, MDBBtn, MDBInput, MDBTextArea } from "mdb-react-ui-kit";

export default function EditProfile({ cancel, submit }) {
  return (
    <>
      <MDBCol lg="5">
        <MDBInput placeholder="Full Name"></MDBInput>
        <hr />
        <MDBInput placeholder="Email"></MDBInput>
        <hr />
        <MDBInput placeholder="Phone"></MDBInput>
        <hr />
        <MDBInput placeholder="Address"></MDBInput>
        <hr />
        <MDBInput placeholder="Profession"></MDBInput>
        <hr />
        <MDBTextArea placeholder="Bio"></MDBTextArea>
        <hr />
      </MDBCol>
      <MDBBtn color="success" onClick={submit}>
        Submit
      </MDBBtn>
      <MDBBtn color="danger" onClick={cancel}>
        Cancel
      </MDBBtn>
    </>
  );
}
