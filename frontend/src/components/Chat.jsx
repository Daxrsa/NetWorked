import React from 'react';
import SendMessageForm from './SendMessageForm';
import MessageContainer from './MessageContainer';
import { Button } from 'react-bootstrap';
import ConnectedUsers from './ConnectedUsers';

const Chat = ({ sendMessage, messages, closeConnection, users, deleteMessage,handleDeleteMessage }) => {
  

  return (
    <div>
      <div className="leave-room">
        <Button variant="danger" onClick={closeConnection}>
          Leave Room
        </Button>
      </div>
      <ConnectedUsers users={users} />
      <div className="chat">
        <MessageContainer messages={messages} handleDeleteMessage={handleDeleteMessage} deleteMessage={deleteMessage} />
        <SendMessageForm sendMessage={sendMessage} />
      </div>
    </div>
  );
};

export default Chat;
