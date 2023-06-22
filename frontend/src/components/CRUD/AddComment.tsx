import React, { useState } from "react";
import { FormControl, InputLabel, Select, MenuItem } from "@mui/material";
import axios from "axios";
import './box.css'
import { useParams } from "react-router-dom";
import { useEffect } from "react";

export default function AddComment() {
    const [name, setName] = useState('');
    const [statusCode, setStatusCode] = useState('');
    const [specializimet, setSpecializimet] = useState([]);
    const [specializimiId, setSpecializimiId] = useState('');

    useEffect(() => {
        fetch();
    }, []);

    const fetch = async () => {
        try {
            const response = await axios.get("http://localhost:5157/api/v1/Specializimi");
            setSpecializimet(response.data);
        } catch (error) {
            console.log(error);
        }
    };

    const handleSubmit = () => {
        axios
            .post("http://localhost:5157/api/v1/Semundja", {
                name,
                specializimiId
            })
            .then((response) => {
                setStatusCode("Semundja saved successfully");
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
                <br />
                <FormControl fullWidth>
                    <InputLabel>Specializimet</InputLabel>
                    <Select
                        value={specializimiId}
                        label="Specializimi"
                        onChange={(e) => setSpecializimiId(e.target.value)}
                    >
                        {specializimet.map((item) => (
                            <MenuItem key={item.id} value={item.id}>
                                {item.name}
                            </MenuItem>
                        ))}
                    </Select>
                </FormControl>
                <button type="submit">Submit</button>
            </form>
            {statusCode && <p>{statusCode}</p>}
        </div>
    );
}
