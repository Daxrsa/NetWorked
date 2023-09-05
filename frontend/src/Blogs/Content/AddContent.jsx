import React, { useState } from "react";
import axios from "axios";
import { TextField, Button } from "@mui/material";

const AddEditForm = () => {
  const [body, setBody] = useState("");

  const generateNewBlogId = () => {
    // Generate a new unique identifier for the blog ID
    return Math.floor(Math.random() * 1000000);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const newBlog = {
        id: generateNewBlogId(), // Generate a new blog ID
        body: body
      };

      await axios.post(`http://localhost:5263/api/Blog/addContentToBlog?blogId=b5b3c40d-560e-4913-7a41-08db71810b6c`, newBlog);
      console.log("Content added successfully");
    } catch (error) {
      console.error("Error adding content:", error);
    }

    setBody("");
  };

  return (
    <form onSubmit={handleSubmit}>
      <TextField
        label="Body"
        multiline
        rows={4}
        value={body}
        onChange={(e) => setBody(e.target.value)}
        required
      />
      <br />
      <Button type="submit" variant="contained" color="primary">
        Add Content
      </Button>
    </form>
  );
};

export default AddEditForm;
