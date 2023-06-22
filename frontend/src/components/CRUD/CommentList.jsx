import React, { useEffect, useState } from "react";
import axios from "axios";
import "./ArticleList.css";
import { Button } from "@mui/material";
import { useNavigate } from "react-router-dom";

export default function CommentList()
{
    const [semundjet, setSemundjet] = useState([]);
    const redirect = useNavigate("");

    useEffect(() =>
    {
        fetch();
    }, []);

    const fetch = async () =>
    {
        try
        {
            const response = await axios.get("http://localhost:5157/api/v1/Semundja");
            setSemundjet(response.data);
        } catch (error)
        {
            console.log(error);
        }
    };

    const handleDelete = async (id) =>
    {
        try
        {
            await axios.delete(`http://localhost:5157/api/v1/Semundja?id=${id}`);
            fetch();
        } catch (error)
        {
            console.log(error);
        }
    };

    return (
        <div className="article-list">
            <h1>Semundjet List</h1>
            <Button onClick={() => redirect("/comment/add")}>Add Semundjen</Button>
            {semundjet.length === 0 ? (
                <p>No semundje available</p>
            ) : (
                <table>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Specializimi id</th>
                        </tr>
                    </thead>
                    <tbody>
                        {semundjet.map((s) => (
                            <tr key={s.id}>
                                <td>{s.name}</td>
                                <td>{s.specializimiId}</td>
                                <Button onClick={() => redirect(`/comment/edit/${s.id}`)}>Edit</Button>
                                <Button color="error" onClick={() => handleDelete(s.id)}>Delete</Button>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}
        </div>
    );
}