import React, { useState  } from "react";
import axios from "axios";
import {
  Box,
  Card,
  CardContent,
  Typography,
  CardActions,
  Button,
  CardMedia,
} from "@mui/material";

export default function ButtonActions() {
  const [isEditMode, setIsEditMode] = useState(false);

  const handleEditClick = () => {
    setIsEditMode(true);
  };

  const handleCancelClick = () => {
    setIsEditMode(false);
  };
  return (
    <CardActions>
      {isEditMode ? (
        <EditPost handleCancelClick={handleCancelClick} />
      ) : (
        <Button onClick={handleEditClick}>Edit</Button>
      )}
      <Button color="error" onClick={() => handleDelete(post.id)}>
        Delete
      </Button>
    </CardActions>
  );
}
