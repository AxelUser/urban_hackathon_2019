import React from 'react';
import { Paper, Typography, TextField, Button } from '@material-ui/core';

const Profile = () => {
  return (
    <Paper>
      <Typography variant="h4">
        Your profile
      </Typography>
      <TextField multiline fullWidth variant='outlined' label="Description"/>
      <Button variant='contained' color='primary'>Save</Button>
    </Paper>
  )
}

export default Profile;