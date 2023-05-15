import styled from "@emotion/styled";
import { TableCell, TableRow, createTheme, tableCellClasses } from "@mui/material";
import {enUS, fiFI, svSE} from '@mui/material/locale'


export const theme = createTheme(
  {
    typography: {
      fontFamily: 'Georgia',
      body1: {
        fontStyle: 'italic'
      }
      
    },
    palette: {
      primary: {
        main: '#009fab',
        dark: '#007c86',
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
