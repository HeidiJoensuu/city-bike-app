import { createTheme } from "@mui/material";
import {enUS, fiFI, svSE} from '@mui/material/locale'


export const theme = createTheme({
  typography: {
    fontFamily: 'Georgia Italic'
  },
  palette: {
    primary: {
      main: '#009fab',
    },
    secondary: {
      main: '#55eb98'
    },
  },
  breakpoints: {
    values: {
      tablet: 640,
      laptop: 1004,
      desktop: 1200,
    }
  },
  /*
  enUS,
  fiFI,
  svSE
  */
})

export const styles = (theme) => ({
  root: {
    backgroundColor: 'blue',
    // Match [md, ∞)
    //       [900px, ∞)
    [theme.breakpoints.up('md')]: {
      backgroundColor: 'red',
    },
  },
})
