import React, { useEffect, useState } from "react";
import axios from "axios";
import "./ArticleList.css";
import { Button } from "@mui/material";
import { useNavigate } from "react-router-dom";

export default function ArticleList()
{
    const [specializimet, setSpecializimet] = useState([]);
    const redirect = useNavigate("");
    const [name, setName] = useState('');
    // const [content, setContent] = useState('');
    const [statusCode, setStatusCode] = useState('');

    useEffect(() =>
    {
        fetch();
    }, []);

    const fetch = async () =>
    {
        try
        {
            const response = await axios.get("http://localhost:5157/api/v1/Specializimi");
            setSpecializimet(response.data);
        } catch (error)
        {
            console.log(error);
        }
    };

    const handleDelete = async (id) =>
    {
        try
        {
            await axios.delete(`http://localhost:5157/api/v1/Specializimi?id=${id}`);
            fetch();
        } catch (error)
        {
            console.log(error);
        }
    };

    return (
        <div className="article-list">
            <h1>Specializimet List</h1>
            <Button onClick={() => redirect("/specializimi/add")}>Add Spec</Button>
            {specializimet.length === 0 ? (
                <p>No specializime available</p>
            ) : (
                <table>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Title</th>
                        </tr>
                    </thead>
                    <tbody>
                        {specializimet.map((s) => (
                            <tr key={s.id}>
                                <td>{s.name}</td>
                                <Button onClick={() => redirect(`/specializimi/edit/${s.id}`)}>Edit</Button>
                                <Button color="error" onClick={() => handleDelete(s.id)}>Delete</Button>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}
        </div>
    );
}