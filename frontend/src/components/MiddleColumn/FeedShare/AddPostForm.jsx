import React, { useState, useEffect } from "react";
import {
  Grid,
  TextField,
  Button,
  Card,
  CardContent,
  Typography,
  Box,
} from "@material-ui/core";
import axios from "axios";

function AddPostForm() {
  const [selectedImage, setSelectedImage] = useState(null);
  const [imageUrl, setImageUrl] = useState(null);
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");

  const removeImage = () => {
    setSelectedImage(false);
  };

  useEffect(() => {
    if (selectedImage) {
      setImageUrl(URL.createObjectURL(selectedImage));
    }
  }, [selectedImage]);

  const handleSubmit = async (event) => {
    event.preventDefault();
  
    const token = localStorage.getItem("jwtToken");
  
    try {
      const formData = new FormData();
      formData.append("title", title);
      formData.append("description", description);
      formData.append("imageName", selectedImage.name);
      formData.append("imageFile", selectedImage);
  
      const response = await axios.post(
        "http://localhost:5263/api/Post/add",
        formData,
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "multipart/form-data",
          },
        }
      );
  
      console.log(response);
  
      if (response) {
        console.log("Post has been added.");
        window.location.reload();
      } else {
        console.log("Problem adding post.");
      }
  
      console.log(response.data);
    } catch (error) {
      console.error(error);
    }
  };
  

  return (
    <div className="App">
      <Grid>
        <Card style={{ maxWidth: 450, padding: "20px 5px", margin: "0 auto" }}>
          <CardContent>
            <Typography gutterBottom variant="h5">
              What's on your mind?
            </Typography>
            <Typography
              variant="body2"
              color="textSecondary"
              component="p"
              gutterBottom
            >
              Add a post
            </Typography>
            <form>
              <Grid container spacing={1}>
                <Grid xs={12} sm={12} item>
                  <TextField
                    placeholder="Enter title"
                    label="Title"
                    variant="outlined"
                    fullWidth
                    required
                    onChange={(e) => setTitle(e.target.value)}
                  />
                </Grid>
                <Grid item xs={12}>
                  <TextField
                    placeholder="Enter description"
                    label="Description"
                    variant="outlined"
                    fullWidth
                    required
                    onChange={(e) => setDescription(e.target.value)}
                  />
                </Grid>
                <Grid item xs={12}>
                  <input
                    accept="image/*"
                    type="file"
                    id="select-image"
                    style={{ display: "none" }}
                    onChange={(e) => setSelectedImage(e.target.files[0])}
                  />
                  <label htmlFor="select-image">
                    <Button
                      variant="contained"
                      color="primary"
                      component="span"
                    >
                      Upload Image
                    </Button>
                  </label>
                  {selectedImage && imageUrl ? (
                    <Button
                      variant="contained"
                      color="secondary"
                      component="span"
                      onClick={removeImage}
                    >
                      Remove Image
                    </Button>
                  ) : (
                    <Button
                      variant="contained"
                      color="secondary"
                      component="span"
                      style={{ display: "none" }}
                    >
                      Remove Image
                    </Button>
                  )}
                </Grid>
                {imageUrl && selectedImage && (
                  <Box mt={2} textAlign="center">
                    <div>Image Preview:</div>
                    <img
                      src={imageUrl}
                      alt={selectedImage.name}
                      height="100px"
                    />
                  </Box>
                )}
                <Grid item xs={12}>
                  <Button
                    type="submit"
                    variant="contained"
                    color="primary"
                    fullWidth
                    onClick={handleSubmit}
                  >
                    Submit
                  </Button>
                </Grid>
              </Grid>
            </form>
          </CardContent>
        </Card>
      </Grid>
    </div>
  );
}

export default AddPostForm;
