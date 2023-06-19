import { MDBBtn, MDBCardText, MDBCol, MDBRow } from "mdb-react-ui-kit";
import React, { useState } from "react";
import { logout } from "../AuthService";
import axios from "axios";

const handleDelete = () => {
  const token = localStorage.getItem("jwtToken");
  axios
    .delete("http://localhost:5116/api/User", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
    .then((response) => {
      console.log(response);
      logout();
    })
    .catch((error) => {
      console.error(error);
    });
};

export default function ConfirmationBox({ calcelConfirmation }) {
  return (
    <>
      <MDBRow>
        <MDBCol sm="3">
          <MDBCardText>Are you sure? This action cannot be undone.</MDBCardText>
        </MDBCol>
        <MDBCol sm="9">
          <MDBCardText className="text-muted">
            <MDBBtn className="me-1" color="danger" onClick={handleDelete}>
              Yes
            </MDBBtn>
            <MDBBtn
              className="me-1"
              color="success"
              onClick={calcelConfirmation}
            >
              No
            </MDBBtn>
          </MDBCardText>
        </MDBCol>
      </MDBRow>
    </>
  );
}
