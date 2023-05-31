import React, {useState} from "react";
import { MDBCol, MDBBtn, MDBInput } from "mdb-react-ui-kit";

export default function EditProfile() {
  const [isEditMode, setIsEditMode] = useState(false);

  const handleSaveClick = () => {
    // Perform save logic
    setIsEditMode(false);
  };

  const handleCancelClick = () => {
    setIsEditMode(false);
  };

  return (
    <>
      <MDBCol lg="4">
        <MDBInput placeholder="Full Name" width="20px"></MDBInput>
        <MDBInput placeholder="Email"></MDBInput>
        <MDBInput placeholder="Phone"></MDBInput>
        <MDBInput placeholder="Mobile"></MDBInput>
        <MDBInput placeholder="Address"></MDBInput>
        <MDBInput placeholder="Profession"></MDBInput>
      </MDBCol>
      <MDBBtn onClick={handleSaveClick} className="me-1" color="success">
        Submit
      </MDBBtn>
      <MDBBtn onClick={handleCancelClick} className="me-1" color="danger">
        Cancel
      </MDBBtn>
    </>
  );
}
