import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import useFetch from "../../hooks/useFetch";
import Search from "../Search/Search";
import axios from "axios";
import {
  Container,
  Wrapper,
  HomeIcon,
  NetworkIcon,
  JobsIcon,
  MessagesIcon,
  NotificationsIcon,
  ProfileCircle,
  CaretDownIcon,
  DropdownMenu,
  NotificationsDropdownMenu,
  CartIcon,
} from "./styles";
import NetworkLogo from "./networklogo.png";
import { logout } from "../AuthService";
interface Notification {
  username: string;
  description: string;
}

interface ResponseData {
  mappedData: Notification[];
}

const Header: React.FC = () => {
  const navigate = useNavigate();
  const [activeButton, setActiveButton] = useState("home");
  const [menuOpen, setMenuOpen] = useState(false); // Track the state of the dropdown menu
  const [notificationOpen, setNotificationOpen] = useState(false); // Track the state of the notification dropdown
  const [loggedInUserName, setUsername] = useState(null);
  const [notifications, setNotifications] = useState<Notification[]>([]);

  const handleChatsClick = () => {
    navigate("/mainchat");
    setMenuOpen(false);
    setActiveButton("messages");
  };
  const fetchLoggedInUser = async () => {
    try {
      const token = localStorage.getItem("jwtToken"); // Retrieve the token from localStorage
      const response = await axios.get(
        "http://localhost:5116/api/Auth/GetloggedInUser",
        {
          headers: {
            Authorization: `Bearer ${token}`, // Add the token to the request headers
          },
        }
      );
      const udata = response.data; // No need to parse the JSON object
     
      setUsername(udata.username);
    } catch (error) {
      console.error("Error fetching logged-in user:", error);
    }
  };
  useEffect(() => {
    fetchLoggedInUser();
  }, []);
  const fetchNotifs = async () => {
    try {
      const response = await axios.get<ResponseData>(
        "http://localhost:8800/notifications"
      );
      const token = localStorage.getItem("jwtToken");
      const responsep = await axios.get(
        "http://localhost:5116/api/Auth/GetloggedInUser",
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      const udata = responsep.data;
      const profession = udata.profession.toLowerCase(); // Convert to lowercase
      const { mappedData } = response.data;

      // Filter notifications based on profession (case-insensitive)
      const filteredNotifications = mappedData.filter((notification) =>
        notification.description.toLowerCase().includes(profession)
      );

      // Map the username and description from filtered notifications
      const notificationsData = filteredNotifications.map((notification) => ({
        username: notification.username,
        description: notification.description,
      }));

      console.log(notificationsData);
      setNotifications(notificationsData);
    } catch (error) {
      console.error("Error fetching notifications:", error);
    }
  };

  useEffect(() => {
    fetchNotifs();
  }, []);

  const handleNetworkClick = () => {
    navigate("/");
    setActiveButton("home");
  };

  const handleHomeClick = () => {
    navigate("/");
    setMenuOpen(false);
    setActiveButton("home");
  };
  const handleCartClick = () => {
    navigate("/payment");
    setMenuOpen(false);
    setActiveButton("cart");
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

  // const handlePostClick = () => {
  //   navigate("/posts");
  // const handleApplicationsClick = () => {
  //   navigate('/jobApplications');
  //   setMenuOpen(false);
  //   setActiveButton('jobApplications');
  // };

  const handleCompaniesClick = () => {
    navigate("/companies");
    setMenuOpen(false);
    setActiveButton("companies");
  };
  const handleDashboardClick = () => {
    navigate("/jobDashboard");
    setMenuOpen(false);
    setActiveButton("companies");
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
            <Search />
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

            {/* <button
              className={activeButton === "posts" ? "active" : ""}
              onClick={handlePostClick}
            >
              <JobsIcon />
              <span>Posts</span>
            </button> */}

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
                    <div key={notification.username}>
                      <p>
                        <span>{notification.username}</span>{" "}
                        <span>{notification.description}</span>
                      </p>
                    </div>
                  ))}
                </NotificationsDropdownMenu>
              )}
            </button>
            <button
              className={activeButton === "cart" ? "active" : ""}
              onClick={handleCartClick}
            >
              <CartIcon />
              <span>Cart</span>
            </button>
            <button
              onClick={handleMeClick}
              className={activeButton === "me" ? "active" : ""}
            >
              <ProfileCircle src="https://avatars.githubusercontent.com/u/93683494?v=" />
              <span>
                {loggedInUserName} <CaretDownIcon />
                {menuOpen && (
                  <DropdownMenu>
                    {!loggedInUserName && (
                      <>
                        <button onClick={handleRegisterClick}>Register</button>
                        <button onClick={handleLoginClick}>Login</button>
                      </>
                    )}
                    <button onClick={handleProfilePageClick}>My Profile</button>
                    <button onClick={logout}>Logout</button>
                    <button onClick={handleJobsClick}>Jobs</button>
                    {/* <button onClick={handleApplicationsClick}>Applications</button> */}
                    <button onClick={handleCompaniesClick}>Companies</button>
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
