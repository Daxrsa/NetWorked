import React from 'react';
import { Grid, TextField, Button, Card, CardContent, Typography } from '@material-ui/core';


function EditPost({handleCancelClick}) {
  return (
    <div className="App"> 
      <Grid>
        <Card style={{ maxWidth: 450, padding: "20px 5px", margin: "0 auto" }}>
          <CardContent>
            <Typography variant="body2" color="textSecondary" component="p" gutterBottom>
              Edit Post
          </Typography> 
            <form>
              <Grid container spacing={1}>
                <Grid xs={12} sm={12} item>
                  <TextField placeholder="Enter Title" label="Title" variant="outlined" fullWidth required />
                </Grid>
                <Grid item xs={12}>
                  <TextField label="Description" multiline rows={4} placeholder="Type your description here" variant="outlined" fullWidth required />
                </Grid>
                <Grid item xs={12}>
                  <Button type="submit" variant="contained" color="primary" fullWidth>Submit</Button>
                </Grid>
                <Grid item xs={12}>
                  <Button onClick={handleCancelClick} variant="contained" color="primary" fullWidth>Cancel</Button>
                </Grid>
              </Grid>
            </form>
          </CardContent>
        </Card>
      </Grid>
    </div>
  );
}

export default EditPost;