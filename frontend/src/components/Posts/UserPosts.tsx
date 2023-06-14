import React, { useEffect, useState } from "react";
import UserPostsList from "./UserPostsList";
import { Post } from "../../models/Post";
import { Grid } from "@mui/material";

interface Props {
  posts: Post[];
}

export default function UserPosts({ posts }: Props) {
  return (
      <Grid classes='10'>
        <header content="My Posts" />
        <UserPostsList posts={posts} />
      </Grid>
  );
}
