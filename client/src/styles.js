import styled from "@emotion/styled"
import { Button, Grid, TableCell, createTheme, tableCellClasses } from "@mui/material"

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
      },
      h2: {
        fontStyle: 'italic',
        fontWeight: 'bold',
        color: '#007c86',
        fontSize:26,
        margin: "10px"
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

export const AdminButton = styled(Button)({
  position: "absolute",
  right: "26px",
  top: "75px",
  textTransform: "capitalize",
  fontStyle: 'italic',
})
