import { Typography, Grid } from "@mui/material";
import { useFormContext } from 'react-hook-form';
import AppTextInput from '../../app/components/AppTextInput';
import AppCheckbox from '../../app/components/AppCheckBox';

export default function AddressForm() {
    const { control, formState } = useFormContext();
    return (
        <>
            <Typography variant="h6" gutterBottom>
                Shipping Email address
            </Typography>
            <Grid container spacing={3}>
                <Grid item xs={12} sm={12}>
                    <AppTextInput control={control} name='fullName' label='Full name' />
                </Grid>
                <Grid item xs={12}>
                    <AppTextInput control={control} name='emailAddress' label='Email address' />
                </Grid>
            
            </Grid>

            <Grid item xs={12}>
                <AppCheckbox 
                    disabled={!formState.isDirty}
                    name='saveAddress' 
                    label='Save this as default address' 
                    control={control} 
                />
            </Grid>
        </>
    );
}