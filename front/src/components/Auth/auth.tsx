import React, { useState } from 'react';
import FormControl from '@material-ui/core/FormControl';
import InputLabel from '@material-ui/core/InputLabel';
import Input from '@material-ui/core/Input';
import InputAdornment from '@material-ui/core/InputAdornment';
import IconButton from '@material-ui/core/IconButton';
import Visibility from '@material-ui/icons/Visibility';
import VisibilityOff from '@material-ui/icons/VisibilityOff';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import { Button, Grid, withStyles, createStyles, WithStyles } from '@material-ui/core';
import {Link} from 'react-router-dom';

const styles = createStyles({
  root: {
    flexGrow: 1
  },
  paper: {
    margin: 'auto',
    maxWidth: 600
  },
  submitButton: {
    marginTop: 10
  }
});

interface Props extends WithStyles<typeof styles>{}

const Auth = withStyles(styles)(({classes}: Props) => {
  const [showPassword, setShowingPassword] = useState(false);
  const [login, setLogin] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  return (
    <Grid container className={classes.root}>
      <Paper className={classes.paper}>
        <Grid container direction="column">
          <Typography variant={"h4"}>
            Sign in
          </Typography>
          <FormControl>
            <InputLabel htmlFor="user-login">Login</InputLabel>
            <Input
              id="user-login"
              type={'text'}
              value={login}
              onChange={e => setLogin(e.target.value)}
            />
          </FormControl>
          <FormControl>
            <InputLabel htmlFor="user-password">Password</InputLabel>
            <Input
              id="user-password"
              type={showPassword ? 'text' : 'password'}
              value={password}
              onChange={e => setPassword(e.target.value)}
              endAdornment={
                <InputAdornment position="end">
                  <IconButton
                    aria-label="Toggle password visibility"
                    onClick={() => setShowingPassword(!showPassword)}
                  >
                    {showPassword ? <Visibility /> : <VisibilityOff />}
                  </IconButton>
                </InputAdornment>
              }
            />
          </FormControl>
          <Link to="/profile">
            <Button variant="contained" color="primary" className={classes.submitButton}>
                Continue
            </Button>
          </Link>
        </Grid>
      </Paper>
    </Grid>
  );
});

export default Auth;