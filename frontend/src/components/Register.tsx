import React, { useState } from "react";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import FormControlLabel from "@mui/material/FormControlLabel";
import Checkbox from "@mui/material/Checkbox";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { NavLink } from "react-router-dom";
import "../App.css";
import axios from "axios";
import { TextArea } from "semantic-ui-react";
import { Card } from "@mui/material";
import { useNavigate } from "react-router-dom";

function Copyright(props: any) {
  return (
    <Typography
      variant="body2"
      color="grey"
      align="center"
      style={{ color: "grey" }}
      {...props}
    >
      {"Copyright © "}
      <Link color="inherit" href="https://mui.com/">
        NetWorked
      </Link>{" "}
      {new Date().getFullYear()}
      {"."}
    </Typography>
  );
}

const theme = createTheme();

export default function SignIn() {
  const [username, setUsername] = useState("");
  const [fullname, setFullname] = useState("");
  const [email, setEmail] = useState("");
  const [phone, setPhone] = useState("");
  const [address, setAddress] = useState("");
  const [profession, setProfession] = useState("");
  const [skills, setSkills] = useState("");
  const [password, setPassword] = useState("");
  const [bio, setBio] = useState("");
  const redirect = useNavigate();

  const handleSubmit = async (event: any) => {
    event.preventDefault();

    try {
      const response = await axios.post(
        "http://localhost:5116/api/Auth/Register",
        {
          username,
          password,
          fullname,
          email,
          phone,
          address,
          profession,
          skills,
          bio,
        }
      );
      console.log("Register success.", response.data);
      alert("Check your email")
      // Handle successful login or any other logic
    } catch (error) {
      console.error("Register failure", error);
      redirect("/login")
      // Handle error
    }
  };

  return (
    <div>
      <ThemeProvider theme={theme}>
        <Container component="main" maxWidth="xs">
          <CssBaseline />
          <Box
            sx={{
              marginTop: 8,
              display: "flex",
              flexDirection: "column",
              alignItems: "center",
            }}
          >
            <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
              <LockOutlinedIcon />
            </Avatar>
            <Typography component="h1" variant="h5">
              Register
            </Typography>
            <Box
              component="form"
              onSubmit={handleSubmit}
              noValidate
              sx={{ mt: 1 }}
            >
              <TextField
                margin="normal"
                required
                fullWidth
                id="username"
                value={username}
                label="Username"
                name="username"
                onChange={(e) => setUsername(e.target.value)}
                autoComplete="username"
                autoFocus
                InputLabelProps={{
                  style: { color: "black" },
                }}
                InputProps={{
                  sx: {
                    "& .MuiOutlinedInput-notchedOutline": {
                      borderColor: "black !important",
                    },
                  },
                }}
              />

              <TextField
                margin="normal"
                required
                fullWidth
                id="fullname"
                value={fullname}
                label="Full Name"
                name="fullname"
                onChange={(e) => setFullname(e.target.value)}
                autoComplete="fullname"
                autoFocus
                InputLabelProps={{
                  style: { color: "black" },
                }}
                InputProps={{
                  sx: {
                    "& .MuiOutlinedInput-notchedOutline": {
                      borderColor: "black !important",
                    },
                  },
                }}
              />

              <TextField
                margin="normal"
                required
                fullWidth
                id="email"
                value={email}
                label="Email"
                name="email"
                onChange={(e) => setEmail(e.target.value)}
                autoComplete="email"
                autoFocus
                InputLabelProps={{
                  style: { color: "black" },
                }}
                InputProps={{
                  sx: {
                    "& .MuiOutlinedInput-notchedOutline": {
                      borderColor: "black !important",
                    },
                  },
                }}
              />

              <TextField
                margin="normal"
                required
                fullWidth
                id="phone"
                value={phone}
                label="Phone"
                name="phone"
                onChange={(e) => setPhone(e.target.value)}
                autoComplete="phone"
                autoFocus
                InputLabelProps={{
                  style: { color: "black" },
                }}
                InputProps={{
                  sx: {
                    "& .MuiOutlinedInput-notchedOutline": {
                      borderColor: "black !important",
                    },
                  },
                }}
              />

              <TextField
                margin="normal"
                required
                fullWidth
                id="address"
                value={address}
                label="Address"
                name="address"
                onChange={(e) => setAddress(e.target.value)}
                autoComplete="address"
                autoFocus
                InputLabelProps={{
                  style: { color: "black" },
                }}
                InputProps={{
                  sx: {
                    "& .MuiOutlinedInput-notchedOutline": {
                      borderColor: "black !important",
                    },
                  },
                }}
              />

              <TextField
                margin="normal"
                required
                fullWidth
                id="profession"
                value={profession}
                label="Profession"
                name="profession"
                onChange={(e) => setProfession(e.target.value)}
                autoComplete="profession"
                autoFocus
                InputLabelProps={{
                  style: { color: "black" },
                }}
                InputProps={{
                  sx: {
                    "& .MuiOutlinedInput-notchedOutline": {
                      borderColor: "black !important",
                    },
                  },
                }}
              />

              <TextField
                margin="normal"
                required
                fullWidth
                id="skills"
                value={skills}
                label="Skills"
                name="skills"
                onChange={(e) => setSkills(e.target.value)}
                autoComplete="skills"
                autoFocus
                InputLabelProps={{
                  style: { color: "black" },
                }}
                InputProps={{
                  sx: {
                    "& .MuiOutlinedInput-notchedOutline": {
                      borderColor: "black !important",
                    },
                  },
                }}
              />

              <TextField
                margin="normal"
                required
                fullWidth
                name="password"
                label="Password"
                type="password"
                value={password}
                id="password"
                onChange={(e) => setPassword(e.target.value)}
                autoComplete="current-password"
                InputLabelProps={{
                  style: { color: "black" },
                }}
                InputProps={{
                  sx: {
                    "& .MuiOutlinedInput-notchedOutline": {
                      borderColor: "black !important",
                    },
                  },
                }}
              />

              <TextField
                margin="normal"
                required
                fullWidth
                name="bio"
                label="Tell us about yourself"
                type="bio"
                value={bio}
                id="bio"
                onChange={(e) => setBio(e.target.value)}
                autoComplete="current-bio"
                InputLabelProps={{
                  style: { color: "black" },
                }}
                InputProps={{
                  sx: {
                    "& .MuiOutlinedInput-notchedOutline": {
                      borderColor: "black !important",
                    },
                  },
                }}
              />

              <FormControlLabel
                control={<Checkbox sx={{ color: "black" }} value="remember" />}
                label="Remember me"
              />

              <Button
                type="submit"
                fullWidth
                variant="contained"
                sx={{ mt: 3, mb: 2 }}
                onClick={() => SignIn()}
              >
                Sign In
              </Button>
              <Grid container>
                <Grid item xs>
                  <Link href="#" variant="body2">
                    Forgot password?
                  </Link>
                </Grid>
                <Grid item>
                  <Link component={NavLink} to="/login" variant="body2">
                    {"Already have an account? Login"}
                  </Link>
                </Grid>
              </Grid>
            </Box>
          </Box>
          <Copyright sx={{ mt: 8, mb: 4 }} />
        </Container>
      </ThemeProvider>
    </div>
  );
}
