import { useState } from 'react';
import { Form, Button } from 'react-bootstrap';

const Lobby = ({ joinRoom, username }) => {
    const [room, setRoom] = useState("");
  
    return (
      <Form
        className="lobby"
        onSubmit={(e) => {
          e.preventDefault();
          joinRoom(username, room);
        }}
      >
        <Form.Group>
          <Form.Control
            placeholder={username} // Set the placeholder value to the username obtained from the backend
            onChange={(e) => setRoom(e.target.value)}
          />
          <Form.Control
            placeholder="room"
            style={{ marginTop: "10px" }}
            onChange={(e) => setRoom(e.target.value)}
          />
        </Form.Group>
        <Button variant="primary" type="submit" style={{ marginTop: "10px" }} disabled={!username || !room}>
          Join
        </Button>
      </Form>
    );
  };

export default Lobby;