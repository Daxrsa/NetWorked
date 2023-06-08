import React, { useState } from "react";
import { MDBCol, MDBBtn, MDBInput, MDBTextArea } from "mdb-react-ui-kit";

export default function EditProfile({
  cancel,
  submit,
  editFullname,
  editEmail,
  editPhone,
  editAddress,
  editProfession,
  editSkills,
  editBio,
  setData
}) {
  return (
    <>
      <MDBCol lg="5">
        <MDBInput placeholder="Full Name" onChange={editFullname}></MDBInput>
        <hr />
        <MDBInput placeholder="Email" onChange={editEmail}></MDBInput>
        <hr />
        <MDBInput placeholder="Phone" onChange={editPhone}></MDBInput>
        <hr />
        <MDBInput placeholder="Address" onChange={editAddress}></MDBInput>
        <hr />
        <MDBInput placeholder="Profession" onChange={editProfession}></MDBInput>
        <hr />
        <MDBInput placeholder="Skills" onChange={editSkills}></MDBInput>
        <hr />
        <MDBTextArea placeholder="Bio" onChange={editBio}></MDBTextArea>
        <hr />
      </MDBCol>
      <MDBBtn color="success" onClick={submit} type="submit">
        Submit
      </MDBBtn>
      <MDBBtn color="danger" onClick={cancel}>
        Cancel
      </MDBBtn>
    </>
  );
}
