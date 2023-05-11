import { useState } from 'react';
import { Form, Button } from 'react-bootstrap';

const Lobby = ({ joinRoom }) => {
    const [user, setUser] = useState();
    const [room, setRoom] = useState();

    return <Form className='lobby'
        onSubmit={e => {
            e.preventDefault();
            joinRoom(user, room);
        }} >
        <Form.Group>
            <Form.Control placeholder="name" onChange={e => setUser(e.target.value)} />
            <Form.Control placeholder="room" style={{ marginTop: '10px' }} onChange={e => setRoom(e.target.value)} />
        </Form.Group>
        <Button variant="primary" type="submit" style={{ marginTop: '10px' }} disabled={!user || !room}>Join</Button>
    </Form>
}

export default Lobby;