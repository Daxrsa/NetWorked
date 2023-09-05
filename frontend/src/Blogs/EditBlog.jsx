import React, { useState, useEffect } from "react";
import axios from "axios";
import { TextField, Button } from "@mui/material";

const EditBlogForm = ({ blogId }) => {
  const [title, setTitle] = useState("");
  const [body, setBody] = useState("");

  useEffect(() => {
    const fetchBlog = async () => {
      try {
        const response = await axios.put(
          `http://localhost:5263/api/Blog/${blogId}`
        );
        const { title, body } = response.data;
        setTitle(title);
        setBody(body);
      } catch (error) {
        console.error("Error fetching blog:", error);
      }
    };

    fetchBlog();
  }, [blogId]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const updatedBlog = {
        id: blogId, // Include the blogId in the updatedBlog object
        title: title,
        body: body,
      };

      await axios.put(`http://localhost:5263/api/Blog/${blogId}`, updatedBlog);
      console.log("Blog updated successfully");
    } catch (error) {
      console.error("Error updating blog:", error);
    }
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
        Update Blog
      </Button>
    </form>
  );
};

export default EditBlogForm;
