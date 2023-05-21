import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

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
  DropdownMenu
} from './styles';
import NetworkLogo from './networklogo.png';

const Header: React.FC = () => {
  const navigate = useNavigate();
  const [activeButton, setActiveButton] = useState('home');
  const [menuOpen, setMenuOpen] = useState(false); // Track the state of the dropdown menu

  const handleChatsClick = () => {
    navigate('/mainchat');
    setMenuOpen(false);
    setActiveButton('messages');
  };

  const handleHomeClick = () => {
    navigate('/');
    setMenuOpen(false);
    setActiveButton('home');
  };

  const handleJobsClick = () => {
    navigate('/jobs');
    setMenuOpen(false);
    setActiveButton('jobs');
  };

  const handleMeClick = () => {
    setMenuOpen(!menuOpen);
    setActiveButton('me') // Toggle the dropdown menu
  };

  const handleRegisterClick = () => {
    navigate('/register');
  };

  const handleLoginClick = () => {
    navigate('/login');
  };

  return (
    <Container>
      <Wrapper>
        <div className="left">
          <img
            src={NetworkLogo}
            alt="My Image"
            style={{ width: '120px', height: '90px', marginTop: '10px' }}
          />
          <SearchInput placeholder="Search" />
        </div>

        <div className="right">
          <nav>
            <button className={activeButton === 'home' ? 'active' : ''} onClick={handleHomeClick}>
              <HomeIcon />
              <span>Home</span>
            </button>
            <button className={activeButton === 'jobs' ? 'active' : ''} onClick={handleJobsClick}>
              <JobsIcon />
              <span>Jobs</span>
            </button>
            <button className={activeButton === 'messages' ? 'active' : ''} onClick={handleChatsClick}>
              <MessagesIcon />
              <span>Messaging</span>
            </button>
            <button className="notification">
              <NotificationsIcon />
              <span>Notifications</span>
            </button>
            <button onClick={handleMeClick} className={activeButton === 'me' ? 'active' : ''}>
              <ProfileCircle src="https://avatars.githubusercontent.com/u/93683494?v=" />
              <span>
                Me <CaretDownIcon />
                {menuOpen && ( // Render the dropdown menu if the menuOpen state is true
              <DropdownMenu>
                <button onClick={handleRegisterClick}>Register</button>
                <button onClick={handleLoginClick}>Login</button>
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
