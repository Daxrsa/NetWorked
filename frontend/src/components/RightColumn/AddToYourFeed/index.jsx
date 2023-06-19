import React, { useEffect, useState } from 'react';
import axios from 'axios';
import Panel from '../../Panel';
import { Container } from './styles';

const AddToYourFeed = () => {
  const [users, setUsers] = useState([]);

  useEffect(() => {
    const token = localStorage.getItem("jwtToken");
    axios.get("http://localhost:5116/api/User", {
      headers: {
        Authorization: `Bearer ${token}`
      }
    })
    .then((response) => {
      console.log(response.data.data);
      setUsers(response.data.data);
    })
    .catch((error) => {
      console.log(error);
      alert("Error fetching users in the AddToFeed panel");
    })
  }, [])

  return (
    <Container>
      <Panel>
        <span className="title">Add To Your Feed</span>
        <ul>
        {Array.isArray(users) &&
          users.map((user) => (
            <li key={user.id}>
              <div>
                <img className="avatar-img" src={`http://localhost:5116/Resources/${user.profilePictureUrl}`} alt="Avatar" />
                <span className="description">
                  <span className="username">{user.fullname}</span>
                  <span className="role">{user.role}</span>
                </span>
              </div>

              <button>View</button>
            </li>
          ))}
        </ul>
      </Panel>
    </Container>
  );
};

export default AddToYourFeed;