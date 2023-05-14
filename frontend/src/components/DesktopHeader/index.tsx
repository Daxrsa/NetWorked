import React from 'react';

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
import NetworkLogo from './networklogo.png'
const handleChatsClick = () => {
  window.location.href = "/MainChat"
}
const handleHomeClick = () => {
  window.location.href = "/"
}
const Header: React.FC = () => {
  return (
    <Container>
      <Wrapper>
      <div className="left">
  <img src={NetworkLogo} alt="My Image" style={{ width: '120px', height: '90px', marginTop: '10px' }} />
  <SearchInput placeholder="Search" />
</div>



        <div className="right">
          <nav>
            <button className="active" onClick={handleHomeClick}>
              <HomeIcon />
              <span>Home</span>
            </button>
            <button>
              <JobsIcon />
              <span>Jobs</span>
            </button>
            <button onClick={handleChatsClick}>
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
