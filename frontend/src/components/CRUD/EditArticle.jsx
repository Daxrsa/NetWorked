import React, { useState } from "react";
import axios from "axios";
import './box.css'
import { useParams } from "react-router-dom";
import { useEffect } from "react";

export default function EditArticle()
{
    const { id } = useParams();
    const [name, setName] = useState('');
    const [statusCode, setStatusCode] = useState('');

    useEffect(() =>
    {
        fetch();
    }, []);

    const fetch = async () =>
    {
        try
        {
            const response = await axios.get(`http://localhost:5157/api/v1/Specializimi/${id}`);
            const { name } = response.data;
            setName(name);
        } catch (error)
        {
            console.log(error);
        }
    };


    const handleUpdate = async () =>
    {
        try
        {
            await axios.put(`http://localhost:5157/api/v1/Specializimi?id=${id}`, {
                name
            });
        } catch (error)
        {
            console.log(error);
        }
    };

    return (
        <div className="boxi">
            <form >
                <label>
                    Name:
                    <input
                        type="text"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                    />
                </label>
                <br />
                <button type="submit" onClick={handleUpdate}>Save</button>
            </form>
            {statusCode && <p>{statusCode}</p>}
        </div>
    );
}