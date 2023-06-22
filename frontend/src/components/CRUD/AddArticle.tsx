import React, { useState } from "react";
import axios from "axios";
import './box.css'

export default function AddSpec() {
    const [name, setName] = useState('');
    const [id, setId] = useState('');
    const [statusCode, setStatusCode] = useState('');

    const handleSubmit = () => {
        axios
            .post("http://localhost:5157/api/v1/Specializimi", {
                name
            })
            .then((response) => {
                setStatusCode("Specializimi saved successfully");
            })
            .catch((error) => {
                if (error.response && error.response.status === 400) {
                    setStatusCode("Already applied!");
                } else {
                    console.log(error);
                }
            });
    };

    return (
        <div className="boxi">
            <form onSubmit={handleSubmit}>
                <label>
                    Name:
                    <input
                        type="text"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                    />
                </label>
                <button type="submit">Submit</button>
            </form>
            {statusCode && <p>{statusCode}</p>}
        </div>
    );
}
