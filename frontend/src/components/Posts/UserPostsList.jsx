import React from "react";
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
import { Avatar, makeStyles } from "@material-ui/core";
import ThumbUpRoundedIcon from "@mui/icons-material/ThumbUpRounded";

const useStyles = makeStyles(() => ({
  container: {
    display: "flex",
    alignItems: "center",
  },
  avatar: {
    width: 50,
    height: 50,
    marginRight: 10,
  },
  card: {
    marginBottom: "10px",
  },
}));

export default function UserPostsList({ posts }) {
  const classes = useStyles();

  const formatDate = (dateString) => {
    const options = { year: "numeric", month: "long", day: "numeric" };
    return new Date(dateString).toLocaleDateString(undefined, options);
  };

  const handleDelete = (id) => {
    const token = localStorage.getItem("jwtToken");
    axios
      .delete(`http://localhost:5263/api/Post/${id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => {
        console.log(response);
        window.location.reload();
      })
      .catch((error) => {
        console.log(error);
      });
  };

  return (
    <Box width="300px">
      {posts.map((post) => (
        <Card key={post.id} className={classes.card}>
          <div className={classes.container}>
            <Avatar
              alt="User Profile"
              src="https://source.unsplash.com/random"
              style={{
                width: 40,
                height: 40,
              }}
            />
            <Typography
              gutterBottom
              variant="h9"
              component="div"
              marginLeft="5px"
            >
              {post.username}
            </Typography>
          </div>
          <CardMedia
            component="img"
            height="140"
            image={`http://localhost:5263/Resources/${post.filePath}`}
            alt="postImage"
            margin="10px"
          />
          <Typography margin='10px' variant="h9">{formatDate(post.dateCreated)}</Typography>
          <CardContent>
            <Typography gutterBottom variant="h5" component="div">
              {post.title}
            </Typography>
            <Typography
              variant="body2"
              color="text.secondary"
              marginBottom="10px"
            >
              {post.description}
            </Typography>
            <Typography variant="body2" color="text.secondary">
              <div className={classes.container}>
                <ThumbUpRoundedIcon marginLeft="8px" />
                <Typography marginLeft="8px">{post.likes}</Typography>
              </div>
            </Typography>
          </CardContent>
          <CardActions>
            <Button>Edit</Button>
            <Button
              color="error"
              onClick={() => handleDelete(post.id)} // Call the handleDelete function with the post's ID
            >
              Delete
            </Button>
          </CardActions>
        </Card>
      ))}
    </Box>
  );
}
