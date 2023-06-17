import { Card } from "@mui/material";
import { MDBBtn, MDBCardText, MDBCol, MDBRow } from "mdb-react-ui-kit";
import React, { useState } from "react";

export default function ConfirmationBox() {
  return (
    <>
      <MDBRow>
        <MDBCol sm="3">
          <MDBCardText color="yellow">
            Are you sure? This action cannot be undone.
          </MDBCardText>
        </MDBCol>
        <MDBCol sm="9">
          <MDBCardText className="text-muted">
            <MDBBtn className="me-1" color="danger">
              Yes
            </MDBBtn>
            <MDBBtn className="me-1" color="success">
              No
            </MDBBtn>
          </MDBCardText>
        </MDBCol>
      </MDBRow>
    </>
  );
}
