import { Button, ButtonGroup, Grid, useMediaQuery } from "@mui/material"
import imageCycle from "../assets/imageCycle.png"
import imageTitle from "../assets/imageTitle.png"
import { theme } from "../styles"
import { useState } from "react"
import { NavLink, redirect } from "react-router-dom"
import ReactFlagsSelect from "react-flags-select"
import { strings } from "../utils/localization"

/**
 * 
 * @returns 
 */
const Navbar = ({ language, changeLanguageHandler }) => {
  const small = useMediaQuery(theme.breakpoints.down("laptop"))
  const [selected, setSelected] = useState(language.toUpperCase())


  console.log(location.pathname.split('/')[1])
  const checkCurrentPage = (page) => {
    if (location.pathname.split('/')[1] === page) return true
    return false
  }
  const handleLanguageSelection = (code) => {

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
          alignItems:"center"
        }}
        
      >
        <Grid item style={{position: small ? '' : 'relative', marginLeft: small ? '' : '-100px'}}><img src={imageCycle} style={{maxWidth:"100%", maxHeight: "405px"}}/></Grid>
        
        <Grid item><img src={imageTitle} style={{maxWidth:"100%"}}/></Grid>
        
        <Grid item></Grid>
      </Grid>
      <Grid>
        <ButtonGroup variant="text" size="large">
          <Button
            color={checkCurrentPage("stations") ? 'secondary' : 'primary'}
            style={{fontSize:"30px"}}
          >
            <NavLink to={"/stations"} style={{all:"unset"}}>
              {strings.stations}
            </NavLink>
          </Button>
          <Button
            color={checkCurrentPage("journeys") ? 'secondary' : 'primary'}
            style={{fontSize:"30px"}}
          >
            <NavLink to={"/journeys"} style={{all:"unset"}}>
              {strings.journeys}
            </NavLink>
          </Button>
        </ButtonGroup>
      </Grid>
    </>
  )
}

export default Navbar