import React, { useEffect, useState } from "react";
import { Post } from "../../models/Post";
import { Grid } from "@mui/material";
import UserPostsList from "./UserPostsList";

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
