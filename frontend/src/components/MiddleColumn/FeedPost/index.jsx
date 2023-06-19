import React, { useState, useEffect } from "react";
import Panel from "../../Panel";

import {
  Container,
  Row,
  PostImage,
  Separator,
  Avatar,
  Column,
  LikeIcon,
} from "./styles";

import axios from "axios";

const FeedPost = () => {
  const [posts, setPosts] = useState([]);

  useEffect(() => {
    const token = localStorage.getItem("jwtToken");
    axios
      .get("http://localhost:5263/api/Post", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => {
        console.log(response.data);
        setPosts(response.data);
      })
      .catch((error) => {
        console.log(error);
        alert("Error fetching posts.");
      });
  }, []);

  const formatDate = (dateString) => {
    const options = { year: "numeric", month: "long", day: "numeric" };
    return new Date(dateString).toLocaleDateString(undefined, options);
  };

  return (
    <>
      {posts.map((post) => (
        <Panel key={post.id}>
          <Container>
            <Row className="heading">
              <Avatar src="https://source.unsplash.com/random" alt="Avatar" />
              <Column>
                <h3>{post.username}</h3>
                <p>{post.title}</p>
                <p>{formatDate(post.dateCreated)}</p>
              </Column>
            </Row>

            <PostImage
              src={`http://localhost:5263/Resources/${post.filePath}`}
              alt="Post Image"
            />

            <Row>
              <Column>{post.description}</Column>
            </Row>

            <Row className="likes">
              <img
                className="circle blue"
                src="https://static-exp1.licdn.com/sc/h/d310t2g24pvdy4pt1jkedo4yb"
                alt="Thumb Reaction"
              />
              <p>{post.likes}</p>
            </Row>
            <Row>
              <Separator />
            </Row>

            <Row className="actions">
              <button>
                <LikeIcon />
                <span>Like</span>
              </button>
            </Row>
          </Container>
        </Panel>
      ))}
    </>
  );
};

export default FeedPost;
