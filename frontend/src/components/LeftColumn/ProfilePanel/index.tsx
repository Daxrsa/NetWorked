import React from 'react';

import Panel from '../../Panel';

import { Container } from './styles';

const ProfilePanel: React.FC = () => {
  return (
    <Panel>
      <Container>
        <div className="profile-cover"></div>
        <img
          src="https://avatars.githubusercontent.com/u/93683494?v=4"
          alt="Avatar"
          className="profile-picture"
        />
        <h1>Eros Mehmeti</h1>

        <h2>
          Fullstack Developer | Node.js | ReactJS | React Native | IT
          Infrastructure Analyst
        </h2>

        <div className="separator"></div>

      </Container>
    </Panel>
  );
};

export default ProfilePanel;
