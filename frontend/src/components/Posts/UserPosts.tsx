import React, { useEffect, useState } from "react";
import axios from "axios";
import { Grid, Header, List } from "semantic-ui-react";
import UserPostsList from "./UserPostsList";
import { Post } from "../../models/Post";

interface Props {
  posts: Post[];
}

export default function UserPosts({ posts }: Props) {
  return (
    <Grid>
      <Grid.Column width="10">
        <Header as="h2" content="My Posts" />
        <UserPostsList posts={posts} />
      </Grid.Column>
    </Grid>
  );
}
