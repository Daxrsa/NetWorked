import React from "react";
import Card from './Card';
import ListUsers from './ListUsers';
import ListPosts from "./ListPosts";
import JobList from "./JobList";

export default function AdminDashboard() {
  return (
    <>
      <h1>Welcome, {/* loggedInUser */} !</h1>
      <Card />
      <ListUsers />
      <ListPosts />
      <JobList />
    </>
  );
}
