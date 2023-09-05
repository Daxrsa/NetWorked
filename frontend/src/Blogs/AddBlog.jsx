import React, { useState } from "react";
import axios from "axios";
import { TextField, Button } from "@mui/material";

const AddBlogForm = () => {
  const [title, setTitle] = useState("");
  const [body, setBody] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const newBlog = {
        title: title,
        body: body,
      };

      await axios.post("http://localhost:5263/api/Blog/add", newBlog);
      console.log("Blog added successfully");
    } catch (error) {
      console.error("Error adding blog:", error);
    }

    // Clear the form after submission
    setTitle("");
    setBody("");
  };

  return (
    <form onSubmit={handleSubmit}>
      <TextField
        label="Title"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        required
      />
      <br />
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
        Add Blog
      </Button>
    </form>
  );
};

export default AddBlogForm;
