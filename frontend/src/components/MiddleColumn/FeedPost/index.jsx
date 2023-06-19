import React, { useState, useEffect } from 'react';
import Panel from '../../Panel';

import {
  Container,
  Row,
  PostImage,
  Separator,
  Avatar,
  Column,
  LikeIcon,
} from './styles';

import axios from 'axios';

const FeedPost = () => {
  const [posts, setPosts] = useState([]);
  
  useEffect(() => {
    const token = localStorage.getItem("jwtToken");
    axios
      .get('http://localhost:5263/api/Post', {
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
  
  return (
    <>
      {posts.map((post) => (
        <Panel key={post.id}>
          <Container>
            <Row className="heading">
              <Avatar src={post.avatar_url} alt="Avatar" />
              <Column>
                <h3>{post.username}</h3>
                <h4>{post.title}</h4>
                <time>{post.dateCreated}</time>
              </Column>
            </Row>

            <PostImage image={`http://localhost:5263/Resources/${post.filePath}`} alt="Post Image" />

            <Row className="likes">
              <img
                className="circle blue"
                src="https://static-exp1.licdn.com/sc/h/d310t2g24pvdy4pt1jkedo4yb"
                alt="Thumb Reaction"
              />

              <img
                className="circle red"
                src="https://static-exp1.licdn.com/sc/h/7fx9nkd7mx8avdpqm5hqcbi97"
                alt="Heart Reaction"
              />

              <img
                className="circle green"
                src="https://static-exp1.licdn.com/sc/h/5thsbmikm6a8uov24ygwd914f"
                alt="Clap Reaction"
              />
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
