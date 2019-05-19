import React, { useState } from "react";
import FormControl from "@material-ui/core/FormControl";
import InputLabel from "@material-ui/core/InputLabel";
import Input from "@material-ui/core/Input";
import InputAdornment from "@material-ui/core/InputAdornment";
import IconButton from "@material-ui/core/IconButton";
import Visibility from "@material-ui/icons/Visibility";
import VisibilityOff from "@material-ui/icons/VisibilityOff";
import styles from "./auth.module.css";
import Paper from "@material-ui/core/Paper";
import Typography from "@material-ui/core/Typography";
import { Button } from "@material-ui/core";

const Auth: React.FC = () => {
    const [showPassword, setShowingPassword] = useState(false);
    const [login, setLogin] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    return (
        <Paper className={styles.block}>
            <Typography variant={"h4"}>
                Sign in
            </Typography>
            <form className={styles.form}>
                <FormControl>
                    <InputLabel htmlFor="user-login">Login</InputLabel>
                    <Input
                        id="user-login"
                        type={"text"}
                        value={login}
                        onChange={e => setLogin(e.target.value)}/>
                </FormControl>
                <FormControl>
                    <InputLabel htmlFor="user-password">Password</InputLabel>
                    <Input
                        id="user-password"
                        type={showPassword ? "text" : "password"}
                        value={password}
                        onChange={e => setPassword(e.target.value)}
                        endAdornment={
              <InputAdornment position="end">
                  <IconButton
                      aria-label="Toggle password visibility"
                      onClick={() => setShowingPassword(!showPassword)}>
                      {showPassword ? <Visibility/> : <VisibilityOff/>}
                  </IconButton>
              </InputAdornment>
            }/>
                </FormControl>
                <Button variant="contained" color="primary">
                    Continue
                </Button>
            </form>
        </Paper>
    );
};

export default Auth;