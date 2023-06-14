import React, { useEffect, useState } from "react";
import axios from 'axios';

export default function UserPostList() {
  const[posts, setPosts] = useState([]);
  
  useEffect(() => {
    axios.get('http://localhost:5263/api/Post/filter-posts');
  })

  return <h1>My Posts</h1>;
}
