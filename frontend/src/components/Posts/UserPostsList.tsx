import React, { useEffect, useState } from "react";
import { Post } from "../../models/Post";
import { Button, Card, CardHeader, Container } from "@mui/material";

interface Props {
  posts: Post[];
}

export default function UserPostsList({ posts }: Props) {
  return (
    <>
      <Container>
        {posts.map((post) => (
          <Card key={post.id}>
              <CardHeader>{post.title}</CardHeader>
              <div>
                <p>{post.filePath}</p>
                <p>{post.description}</p>
              </div>
              <p>{post.dateCreated}</p>
              <p>{post.likes}</p>
              <p>
                <button content="View" color="blue" />
              </p>
          </Card>
        ))}
      </Container>
    </>
  );
}
