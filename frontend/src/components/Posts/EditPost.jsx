import React, { useState } from "react";
import axios from "axios";
import {
  Grid,
  TextField,
  Button,
  Card,
  CardContent,
  Typography,
} from "@material-ui/core";

function EditPost({ handleCancelClick }) {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");

  const postData = async () => {
    const data = {
      title,
      description
    };

    const jwtToken = localStorage.getItem("jwtToken");
    try {
      const config = {
        headers: {
          Authorization: `Bearer ${jwtToken}`,
        },
      };
      const response = await axios.put(
        "http://localhost:5263/api/Post",
        data,
        config
      );

      console.log("Data sent successfully.", response.data);
    } catch (error) {
      console.error("Error sending data.", error);
    }
  };

  return (
    <div className="App">
      <Grid>
        <Card style={{ maxWidth: 450, padding: "20px 5px", margin: "0 auto" }}>
          <CardContent>
            <Typography
              variant="body2"
              color="textSecondary"
              component="p"
              gutterBottom
            >
              Edit Post
            </Typography>
            <form>
              <Grid container spacing={1}>
                <Grid xs={12} sm={12} item>
                  <TextField
                    placeholder="Enter Title"
                    label="Title"
                    variant="outlined"
                    fullWidth
                    required
                    value={title}
                    onChange={(event) => setTitle(event.target.value)}
                  />
                </Grid>
                <Grid item xs={12}>
                  <TextField
                    label="Description"
                    multiline
                    rows={4}
                    placeholder="Type your description here"
                    variant="outlined"
                    fullWidth
                    required
                    value={description}
                    onChange={(event) => setDescription(event.target.value)}
                  />
                </Grid>
                <Grid item xs={12}>
                  <Button
                    type="submit"
                    variant="contained"
                    color="primary"
                    fullWidth
                    onClick={postData}
                  >
                    Submit
                  </Button>
                </Grid>
                <Grid item xs={12}>
                  <Button
                    onClick={handleCancelClick}
                    variant="contained"
                    color="primary"
                    fullWidth
                  >
                    Cancel
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

export default EditPost;
