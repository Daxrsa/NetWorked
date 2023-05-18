import React, { useEffect, useRef } from 'react';

const MessageContainer = ({ messages, handleDeleteMessage }) => {
  const messageRef = useRef();

  useEffect(() => {
    if (messageRef && messageRef.current) {
      const { scrollHeight, clientHeight } = messageRef.current;
      messageRef.current.scrollTo({ left: 0, top: scrollHeight - clientHeight, behavior: 'smooth' });
    }
  }, [messages]);

  return (
    <div ref={messageRef} className="message-container">
      {messages.map((m) => (
        <div key={m.id} className="user-message">
          <div className="message bg-primary">{m.message}</div>
          <div className="from-user">{m.user}</div>
          <button className="delete-button" onClick={() => handleDeleteMessage(m.id)}>
            <span role="img" aria-label="delete" className="delete-icon">
              🗑️
            </span>
          </button>
        </div>
      ))}
    </div>
  );
};

export default MessageContainer;
