import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

import {
  Container,
  Wrapper,
  LinkedInIcon,
  SearchInput,
  HomeIcon,
  NetworkIcon,
  JobsIcon,
  MessagesIcon,
  NotificationsIcon,
  ProfileCircle,
  CaretDownIcon,
} from './styles';
import NetworkLogo from './networklogo.png';

const Header: React.FC = () => {
  const navigate = useNavigate();
  const [activeButton, setActiveButton] = useState('home');

  const handleChatsClick = () => {
    navigate('/mainchat');
    setActiveButton('messages');
  };

  const handleHomeClick = () => {
    navigate('/');
    setActiveButton('home');
  };

  const handleJobsClick = () => {
   navigate('/jobs');
    setActiveButton('jobs');
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
            <button>
              <ProfileCircle src="https://avatars.githubusercontent.com/u/93683494?v=" />
              <span>
                Me <CaretDownIcon />
              </span>
            </button>
          </nav>
        </div>
      </Wrapper>
    </Container>
  );
};

export default Header;
