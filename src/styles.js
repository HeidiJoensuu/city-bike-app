import styled from "@emotion/styled";
import { Button, Grid, TableCell, TableRow, ToggleButtonGroup, createTheme, tableCellClasses } from "@mui/material";
import {enUS, fiFI, svSE} from '@mui/material/locale'


export const theme = createTheme(
  {
    typography: {
      fontFamily: 'Georgia',
      body1: {
        fontStyle: 'italic',
        //color: '#007c86',
      },
      body2: {
        fontStyle: 'italic',
        color: '#007c86',
        fontSize: 16,
      },
      subtitle1: {
        fontStyle: 'italic',
        fontWeight: 'bold',
        fontSize: 16,
        color: '#007c86',
      }
    },
    palette: {
      primary: {
        main: '#009fab',
        dark: '#007c86',
      },
      secondary: {
        main: '#19ce76'
      },
    },
    breakpoints: {
      values: {
        xsPhone: 350,
        tablet: 640,
        laptop: 1004,
        desktop: 1200,
      }
    },
  },
)


export const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    backgroundColor: theme.palette.primary.main,
    color: theme.palette.common.white,
    fontSize: 16,
    
  },
  [`&.${tableCellClasses.body}`]: {
    fontSize: 14,
    color: theme.palette.primary.dark,
  },
  [`&.${tableCellClasses.footer}`]: {
    fontSize: 14,
    color: theme.palette.primary.dark,
  },
}))

export const ReturnButton = styled(Button)({
  textTransform: "none"
})

export const ListGrid = styled(Grid)({
  marginBottom:"20px"
})
