import React, { useState, useEffect } from "react";
import axios from "axios";
import { TextField, Button } from "@mui/material";
import AddBlogForm from "./AddBlog";
import EditBlogForm from "./EditBlog";

const BlogForm = () => {
  const [blogs, setBlogs] = useState([]);
  const [loading, setLoading] = useState(true);
  const [isAddMode, setAddMode] = useState(false);
  const [isEditMode, setEditMode] = useState(false);


  const handleSetAddMode = () => {
    setAddMode(true);
  }

  const handleSetEditMode = () => {
    setEditMode(true);
  }
  

  const deleteBlog = async (blogId) => {
    try {
      await axios.delete(`http://localhost:5263/api/Blog/${blogId}`);
      console.log("Blog deleted successfully");
    } catch (error) {
      console.error("Error deleting blog:", error);
    }
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get("http://localhost:5263/api/Blog");
        setBlogs(response.data);
        setLoading(false);
      } catch (error) {
        console.error("Error fetching blogs:", error);
      }
    };

    fetchData();
  }, []);

  return (
    <div>
      {loading ? (
        <p>Loading...</p>
      ) : (
        <div>
          {blogs.map((blog) => (
            <div key={blog.id}>
              <h3>{blog.title}</h3>
              <p>{blog.body}</p>
              <button onClick={() => deleteBlog(blog.id)}>Delete</button>
              {isEditMode ? (<EditBlogForm />) : (<button onClick={handleSetEditMode}>Edit blog</button>) }
            </div>
          ))}
        </div>
      )}
      <hr />
      {isAddMode ? (<AddBlogForm />) : (<button onClick={handleSetAddMode}>Add blog</button>) }
    </div>
  );
};

export default BlogForm;
