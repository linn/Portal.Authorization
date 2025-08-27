import React from 'react';
import Typography from '@mui/material/Typography';
import { Grid } from '@mui/material';
import Page from '../containers/Page';

function App() {
    return (
        <Page>
            <Grid container spacing={3}>
                <Grid size={12}>
                    <Typography variant="h4">Portal Authz</Typography>
                </Grid>
            </Grid>
        </Page>
    );
}

export default App;
