import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import useFetch from "../../hooks/useFetch";
import Search from '../Search/Search'

import {
  Container,
  Wrapper,
  SearchInput,
  HomeIcon,
  NetworkIcon,
  JobsIcon,
  MessagesIcon,
  NotificationsIcon,
  ProfileCircle,
  CaretDownIcon,
  DropdownMenu,
  NotificationsDropdownMenu,
} from "./styles";
import NetworkLogo from "./networklogo.png";
import { logout } from "../AuthService";
interface ResponseData {
  descriptions: string[];
}
const Header: React.FC = () => {
  const { data, loading, error } = useFetch<ResponseData>(
    "http://localhost:8800/notifications"
  );

  let notifications: { id: number; message: string }[] = [];

  if (data && data.descriptions) {
    notifications = data.descriptions.map((description, index) => ({
      id: index + 1,
      message: description,
    }));
  }

  console.log(notifications);

  const navigate = useNavigate();
  const [activeButton, setActiveButton] = useState("home");
  const [menuOpen, setMenuOpen] = useState(false); // Track the state of the dropdown menu
  const [notificationOpen, setNotificationOpen] = useState(false); // Track the state of the notification dropdown

  const handleChatsClick = () => {
    navigate("/mainchat");
    setMenuOpen(false);
    setActiveButton("messages");
  };

  const handleNetworkClick = () => {
    navigate("/");
    setActiveButton("home");
  };

  const handleHomeClick = () => {
    navigate("/");
    setMenuOpen(false);
    setActiveButton("home");
  };

  const handleJobsClick = () => {
    navigate("/jobs");
    setMenuOpen(false);
    setActiveButton("jobs");
  };

  const handleMeClick = () => {
    setMenuOpen(!menuOpen);
    setActiveButton("me"); // Toggle the dropdown menu
  };

  const handleNotificationClick = () => {
    setNotificationOpen(!notificationOpen);
    setActiveButton("notifications");
  };

  const handleRegisterClick = () => {
    navigate("/register");
  };

  const handleLoginClick = () => {
    navigate("/login");
  };

  const handleProfilePageClick = () => {
    navigate("/profilePage");
  };

  const handleNotificationsClick = () => {
    navigate("/notifications");
  };

  const handlePostClick = () => {
    navigate("/posts");
  };

  return (
    <Container>
      <Wrapper>
        <div className="left">
          <div onClick={handleNetworkClick}>
            <img
              src={NetworkLogo}
              alt="My Image"
              style={{ width: "60px", height: "50px", marginTop: "0.1px" }}
            />
             <Search/>
          </div>
        </div>

        <div className="right">
          <nav>
            <button
              className={activeButton === "home" ? "active" : ""}
              onClick={handleHomeClick}
            >
              <HomeIcon />
              <span>Home</span>
            </button>
            <button
              className={activeButton === "jobs" ? "active" : ""}
              onClick={handleJobsClick}
            >
              <JobsIcon />
              <span>Jobs</span>
            </button>

            <button
              className={activeButton === "posts" ? "active" : ""}
              onClick={handlePostClick}
            >
              <JobsIcon />
              <span>Posts</span>
            </button>

            <button
              className={activeButton === "messages" ? "active" : ""}
              onClick={handleChatsClick}
            >
              <MessagesIcon />
              <span>Messaging</span>
            </button>
            <button
              onClick={handleNotificationClick}
              className={notificationOpen ? "active" : ""}
            >
              <NotificationsIcon />
              <span>Notifications</span>
              {notificationOpen && (
                <NotificationsDropdownMenu>
                  {notifications.map((notification) => (
                    <p key={notification.id}>{notification.message}</p>
                  ))}
                </NotificationsDropdownMenu>
              )}
            </button>
            <button
              onClick={handleMeClick}
              className={activeButton === "me" ? "active" : ""}
            >
              <ProfileCircle src="https://avatars.githubusercontent.com/u/93683494?v=" />
              <span>
                Me <CaretDownIcon />
                {menuOpen && (
                  <DropdownMenu>
                    <button onClick={handleRegisterClick}>Register</button>
                    <button onClick={handleLoginClick}>Login</button>
                    <button onClick={handleProfilePageClick}>My Profile</button>
                    <button onClick={logout}>Logout</button>
                  </DropdownMenu>
                )}
              </span>
            </button>
          </nav>
        </div>
      </Wrapper>
    </Container>
  );
};

export default Header;
