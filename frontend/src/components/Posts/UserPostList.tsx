import React, { useEffect, useState } from "react";
import axios from "axios";
import { Header, List } from "semantic-ui-react";

export default function UserPostList() {
  const [posts, setPosts] = useState([]);

  useEffect(() => {
    const token = localStorage.getItem("jwtToken");
    axios
      .get("http://localhost:5263/api/Post/filter-posts", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => {
        console.log(response);
        setPosts(response.data);
      });
  }, []);

  return (
    <>
    <Header as = 'h2' content = "My Posts"/>
      <List>
        {posts.map((post: any) => (
          <List.Item key={post.id}>{post.title}</List.Item>
        ))}
      </List>
    </>
  );
}
