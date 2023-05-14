import React from 'react';

import { Container, ProfileCircle, SearchInput, MessageIcon } from './styles';

const MobileHeader: React.FC = () => {
  return (
    <Container>
      <ProfileCircle src="https://avatars.githubusercontent.com/u/93683494?v=4" />
      <SearchInput placeholder="Search" />
      <MessageIcon />
    </Container>
  );
};

export default MobileHeader;
