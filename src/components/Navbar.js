import { Button, ButtonGroup, Grid, useMediaQuery } from "@mui/material"
import imageCycle from "../assets/imageCycle.png"
import imageTitle from "../assets/imageTitle.png"
import { theme } from "../styles"
import { useState } from "react"
import { NavLink, redirect } from "react-router-dom"
import ReactFlagsSelect from "react-flags-select"
import { strings } from "../utils/localization"
import { Height } from "@mui/icons-material"

/**
 * 
 * @returns 
 */
const Navbar = ({ language, changeLanguageHandler }) => {
  const small = useMediaQuery(theme.breakpoints.down("laptop"))

  const checkCurrentPage = () => {
    return location.pathname.split('/')[1]
  }

  return (
    <>
      <ReactFlagsSelect
        selected={language.toUpperCase()}
        onSelect={(code) => changeLanguageHandler(code.toLowerCase())}
        showSelectedLabel={false}
        countries={["FI", "GB", "SE"]}
        selectedSize={25}
        showOptionLabel={false}
        optionsSize={25}
        fullWidth={false}
      />
      <Grid
        container
        sx = {{
          direction:"row",
          justifyContent: small ? "center" : "space-between",
          alignItems:"center",
          
        }}
      >
        <Grid item style={{position: small ? '' : 'relative', marginLeft: small ? '' : '-100px'}}>
          <img src={imageCycle} style={{maxWidth:"100%", maxHeight: small ? "200px" : "300px"}}/>
        </Grid>
        
        <Grid item>
          <img src={imageTitle} style={{maxWidth:"100%", maxHeight: small ? "160px" : "300px"}}/>
        </Grid>
        
        <Grid item></Grid>
      </Grid>
      <Grid sx={{marginBottom: "25px"}}>
        <ButtonGroup variant="text" size="large">
          <Button
            color={checkCurrentPage() === "stations" ? 'secondary' : 'primary'}
            
          >
            <NavLink to={"/stations"} style={{marginRight: "30px"}}className="navButton">
              {strings.stations}
            </NavLink>
          </Button>
          <Button
            color={checkCurrentPage() === "journeys" ? 'secondary' : 'primary'}
          >
            <NavLink to={"/journeys"} style={{marginLeft: "30px"}} className="navButton">
              {strings.journeys}
            </NavLink>
          </Button>
        </ButtonGroup>
      </Grid>
    </>
  )
}

export default Navbar