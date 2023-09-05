import React, { useState, useEffect } from "react";
import axios from "axios";
import { TextField, Button } from "@mui/material";
import AddEditForm from './AddContent';

const BlogForm = () => {
  const [contents, setContents] = useState([]);
  const [loading, setLoading] = useState(true);
  const [isAddMode, setAddMode] = useState(false);
  const [isEditMode, setEditMode] = useState(false);

  const handleSetAddMode = () => {
    setAddMode(true);
  }

  const deleteContent = async (id) => {
    try {
      await axios.delete(`http://localhost:5263/api/Blog/${id}/deletecontent`);
      console.log("Content deleted successfully");
    } catch (error) {
      console.error("Error deleting content:", error);
    }
  };
  

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get("http://localhost:5263/api/Blog/GetAllContent");
        setContents(response.data);
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
          {contents.map((content) => (
            <div key={content.id}>
              <p>Content Id</p>
              <h3>{content.id}</h3>
              <p>Blog Id</p>
              <h3>{content.blogId}</h3>
              <p>Content Body</p>
              <p>{content.body}</p>
              <button onClick={() => deleteContent(content.id)}>Delete</button>
              { /* idEditMode */ }
            </div>
          ))}
        </div>
      )}
      <hr />
      {isAddMode ? (<AddEditForm />) : (<button onClick={handleSetAddMode}>Add Content</button>) }
    </div>
  );
};

export default BlogForm;
// {isEditMode ? (<EditBlogForm />) : (<button onClick={handleSetEditMode}>Edit Content</button>) }