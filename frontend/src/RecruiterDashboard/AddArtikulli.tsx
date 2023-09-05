import React, { useState, useEffect } from 'react';
import { ICreateJobDto, ICompany, ICategory, IArticle } from '../Interfaces/global.typing';
import TextField from '@mui/material/TextField/TextField';
import FormControl from "@mui/material/FormControl/FormControl";
import InputLabel from "@mui/material/InputLabel/InputLabel";
import Select from "@mui/material/Select/Select";
import MenuItem from "@mui/material/MenuItem/MenuItem";
import Button from "@mui/material/Button/Button";
import { useNavigate } from "react-router-dom";
import httpModule from "../helpers/http.module";
import './addToDb.css';
import NavBar from './Navbar';
import Header from '../components/DesktopHeader';
import axios from 'axios';

const AddArtikulli = () => {
    const [article, setArticle] = useState<IArticle>({
        title: "",
        name: "",
    });
    const redirect = useNavigate();

    const handleClickSaveBtn = () => {
        if (
            article.title === "" ||
            article.name === ""
        ) {
            alert("Fill all fields");
            return;
        }
        axios
            .post("http..", article)
            .then((response) => redirect("/"))
            .catch((error) => console.log(error));
    };

    const handleClickBackBtn = () => {
        redirect("/");
    };

    return (
        <div>
            <Header />
            <div className='app'>

                <div className='somethingelse'>
                    <div className="add-job">
                        <h2 className='h2c'>Add Article</h2>
                        <TextField
                            autoComplete='off'
                            label='Title'
                            variant='outlined'
                            value={article.title}
                            onChange={(e) => setArticle({ ...article, title: e.target.value })}
                        />

                        <TextField
                            autoComplete='off'
                            label='Name'
                            variant='outlined'
                            value={article.name}
                            onChange={(e) => setArticle({ ...article, name: e.target.value })}
                        />

                        <div className="btns">
                            <Button variant="outlined" color="secondary" onClick={handleClickBackBtn}>
                                Back
                            </Button>
                            <Button variant="outlined" color="primary" onClick={handleClickSaveBtn}>
                                Save
                            </Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default AddArtikulli;