import { Autocomplete, Backdrop, Button, Grid, Paper, Popover, TextField, Typography, useMediaQuery } from "@mui/material"
import { useEffect, useState } from "react"
import AddLocationAltSharpIcon from '@mui/icons-material/AddLocationAltSharp'
import CloseRoundedIcon from '@mui/icons-material/CloseRounded'
import CheckRoundedIcon from '@mui/icons-material/CheckRounded'
import { useDispatch, useSelector } from "react-redux"
import { createStation, getStationsNamesAll } from "../../reducers/stationReducer"
import { DateTimeField, LocalizationProvider } from "@mui/x-date-pickers"
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs'
import { strings } from "../../utils/localization"
import dayjs from "dayjs"
import L from "leaflet"
import { validateIsItIntegerNumber, validateIsItNumber } from "../../utils/validation"
import { theme } from "../../styles"
import { MapContainer, Marker, Popup, TileLayer, useMapEvent } from "react-leaflet"
import icon from '../../assets/StationMark.png'
import { createJourney } from "../../reducers/journeyReducer"

/**
 * This component returns page of admin settings for creating new journeys and stations
 * @returns {JSX.element} Rendered settings page
 */
const Settings = () => {
  const dispatch = useDispatch()
  const small = useMediaQuery(theme.breakpoints.down("674"))
  const [inputValueDeparture, setInputValueDeparture] = useState('')
  const [inputValueReturn, setInputValueReturn] = useState('')
  const [anchorJourney, setAnchorJourney] = useState(null)
  const [anchorStation, setAnchorStation] = useState(null)
  const [openMap, setOpenMap] = useState(false)
  
  const [marker, setMarker] = useState(null)
  const [errorMessage, setErrorMessage] = useState("")
  const {stationNamesList} = useSelector(state => state.stations)
  let stationNames = []

  const [newStation, setNewStation] = useState({
    nimi: "",
    namn: "",
    name: "",
    osoite: "",
    adress: "",
    x: 0,
    y: 0,
    kapasiteet: 0
  })
  const [newJourney, setNewJourney] = useState({
    departure: dayjs(`2021-05-01T00:00:00`),
    returntime: dayjs(`2021-05-01T00:00:00`),
    departure_station_id: 0,
    departure_station_name: "",
    return_station_id: 0,
    return_station_name: "",
    covered_distance_m: 0,
    duration_sec: 0
  })

  const DefaultIcon = L.icon({
    iconUrl: icon,
    iconSize: [16, 40],
    iconAnchor: [8,40],
  })

  L.Marker.prototype.options.icon = DefaultIcon
  
  /**
   * Checks and gives correct language option for LocalizationProvider
   * @returns {String} current selected language
   */
  const language = () => {
    if (localStorage.getItem("language") === "gb") return "en-gb"
    if (localStorage.getItem("language") === "se") return "sv"
    return "fi"
  }

  useEffect(() => {
    dispatch(getStationsNamesAll())
  }, [dispatch])

  useEffect(() => {
    sortStations()
  }, [stationNamesList])


  /**
   * Sorts stationNameList into alphabetical order.
   * @returns {void}
   */
  const sortStations = () => {
    if (stationNamesList.length !== 0) {
      stationNames = stationNamesList.map(currentStation => {
        const dublicate = stationNamesList.find(station  => station.nimi === currentStation.nimi && station.id !== currentStation.id)
        if (dublicate !== undefined && currentStation.kaupunki !== null) return (`${currentStation.nimi} (${currentStation.kaupunki})`)
        return currentStation.nimi
      }).sort()
    }
  }
  sortStations()

  /**
   * Capitalizes the given string
   * @param {String} string String to be capitalized
   * @returns {String} Capitalized string
   */
  const capitalize =(string) => {
    return string.charAt(0).toUpperCase() + string.slice(1)
  }

  /**
   * Validates given inputs for new journey. Either passes new data to reducer or sets error message to open.
   * @param {Event} event default event
   * @returns {void}
   */
  const handleJourneyClick = (event) => {
    /**
     * Sets error messade sets popover to open.
     * @param {Event} event default event
     * @param {String} target error message
     * @returns {void}
     */
    const handlePopoverOpen = (event, target) => {
      setErrorMessage(target)
      setAnchorJourney(event.currentTarget)
    }
    const data = newJourney

    //Checnking if time based inputs are valid
    if (newJourney.departure.isBefore(dayjs(`2021-05-01T00:00`))) return handlePopoverOpen(event, strings.departure)
    if (newJourney.departure.isAfter(dayjs(`2021-12-31T23:59:59`))) return handlePopoverOpen(event, strings.departure)
    if (newJourney.returntime.isBefore(dayjs(`2021-05-01T00:00`))) return handlePopoverOpen(event, strings.returntime)
    if (newJourney.returntime.isAfter(dayjs(`2021-12-31T23:59:59`))) return handlePopoverOpen(event, strings.returntime)

    //Checking if selected departure and return stations are valid
    const departureStationId = stationNamesList.find(station  => newJourney.departure_station_name === station.nimi)
    if (departureStationId) newJourney.departure_station_id = departureStationId.id
    else return handlePopoverOpen(event, strings.departureStationName)
    const returnStationId = stationNamesList.find(station  => newJourney.return_station_name === station.nimi)
    if (returnStationId) newJourney.return_station_id = returnStationId.id
    else return handlePopoverOpen(event, strings.returnStationName)

    //Checnking if number input are valid
    newJourney.duration_sec = newJourney.returntime.diff(newJourney.departure, 'second')
    if (validateIsItIntegerNumber(newJourney.covered_distance_m) !== "" || Number(newJourney.covered_distance_m) < 10) return handlePopoverOpen(event, strings.coveredDistanceM)
    if (validateIsItIntegerNumber(newJourney.duration_sec) !== "" || Number(newJourney.duration_sec) < 10) return handlePopoverOpen(event, `${strings.departure}/${strings.returntime}`)
    
    //Saving inputs into correct format
    const currentDeparture = newJourney.departure.format()
    const currentReturn = newJourney.returntime.format()
    data.departure = currentDeparture.slice(0, currentDeparture.indexOf('+'))
    data.returntime = currentReturn.slice(0, currentReturn.indexOf('+'))
    if (typeof newJourney.covered_distance_m === "string") data.covered_distance_m = Number(newJourney.covered_distance_m)
    
    //Calling reducer
    dispatch(createJourney(data))
  }

  /**
   * Validates given inputs for new station. Either passes new data to reducer or sets error message to open.
   * @param {Event} event default event
   * @returns {void}
   */
  const handleStationClick = (event) => {
    /**
     * Sets error messade sets popover to open.
     * @param {Event} event default event
     * @param {String} target error message
     * @returns {void}
     */
    const handlePopoverOpen = (event, target) => {
      setErrorMessage(target)
      setAnchorStation(event.currentTarget)
    }
    const data = {}

    //Checking if given names are there and not dublicates
    if (newStation.nimi === "") return handlePopoverOpen(event, `Nimi`)
    if (newStation.namn === "") return handlePopoverOpen(event, `Namn`)
    if (newStation.name === "") return handlePopoverOpen(event, `Name`)
    if (stationNamesList.find(station  => newStation.nimi === station.nimi)) return handlePopoverOpen(event, `Nimi (${strings.dublicate})`)
    if (stationNamesList.find(station  => newStation.namn === station.namn)) return handlePopoverOpen(event, `Namn (${strings.dublicate})`)
    if (stationNamesList.find(station  => newStation.name === station.name)) return handlePopoverOpen(event, `Name (${strings.dublicate})`)
    if (newStation.osoite === "") return handlePopoverOpen(event, `Osoite`)
    if (newStation.adress === "") return handlePopoverOpen(event, `Adress`)

    //Checking given numbers
    if (validateIsItNumber(newStation.x) !== "" || newStation.x === 0) return handlePopoverOpen(event, 'x')
    if (validateIsItNumber(newStation.y) !== "" || newStation.y === 0) return handlePopoverOpen(event, 'y')
    if (validateIsItIntegerNumber(newStation.kapasiteet) !== "" || newStation.kapasiteet=== 0) return handlePopoverOpen(event, strings.capacity)

    //Saving inputs into correct format
    data.nimi = capitalize(newStation.nimi)
    data.namn = capitalize(newStation.namn)
    data.name = capitalize(newStation.name)
    data.osoite = capitalize(newStation.osoite)
    data.adress = capitalize(newStation.adress)
    if(typeof newStation.x === 'string') data.x = Number(newStation.x.replace(",", "."))
    else data.x = newStation.x
    if(typeof newStation.y === 'string') data.y = Number(newStation.y.replace(",", "."))
    else data.y = newStation.y
    if (typeof newStation.kapasiteet === "string") data.kapasiteet = Number(newStation.kapasiteet)

    //Calling reducer
    dispatch(createStation(data))
  }

  /**
   * Closes error message
   * @returns {void}
   */
  const handlePopoverClose = () => {
    setAnchorJourney(null)
    setAnchorStation(null)
    setErrorMessage("")
  }

  /**
   * Listens users click inputs in map.
   * @returns {void}
   */
  const ClickListener = () => {
    const map = useMapEvent('click', (e) => {
      setMarker([e.latlng.lat, e.latlng.lng])
    })
    return null
  }

  /**
   * Saves selected coordinates and removes marker
   * @param {Boolean} save Tells if there are coordinates to be saved
   * @return {void}
   */
  const handleMapClose = (save) => {
    setOpenMap(false)
    if (save)
      setNewStation(newStation => ({
        ...newStation,
        x: marker[0],
        y: marker[1]
      }))
    setMarker(null)
  }

  return (
    <>
      <Paper elevation={4} style={{width: "95%", display:"flex", justifyItems:"end", marginTop: "20px",marginBottom: "25px"}}>
        <Backdrop
          sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
          open={openMap}
        >
          <Paper elevation={0} style={{width:"60%", height:"60%",display:"flex", alignItems: "flex-end", flexDirection:"column"}}>
            <div style={{display:"flex"}}>
              <Button disabled={!marker} onClick={() => handleMapClose(true)}>{strings.save}</Button>
              <CloseRoundedIcon onClick={() => handleMapClose(false)} style={{cursor:"pointer"}} />
            </div>
            <MapContainer center={[60.168916, 24.936007]} zoom={15} scrollWheelZoom={false} className="map">
              <TileLayer
                attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
              />
              <ClickListener />
              {marker && <Marker position={marker}>
                <Popup className="leaflet-popup">
                  <CheckRoundedIcon fontSize="large" style={{cursor:"pointer", color: "#19ce76"}} onClick={() => handleMapClose(true)}/>
                </Popup>
              </Marker>
              }
            </MapContainer>
          </Paper>
        </Backdrop>
        <Grid container direction="row" justifyContent={small ? "center" : "space-between"}>
          <div style={{width:"45%", display: "flex", flexDirection: "column", marginLeft: "20px", minWidth:"300px"}}>
            <Typography variant={"h2"}>{strings.newJourney}</Typography>
            <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale={language()} >
              <DateTimeField
                label={strings.departure}
                format={strings.format}
                value={newJourney.departure}
                onChange={(newValue) => setNewJourney(newJourney => ({...newJourney, departure: newValue}))}
                sx={{marginTop:"10px"}}
              />
              <DateTimeField
                label={strings.return}
                format={strings.format}
                value={newJourney.returntime}
                onChange={(newValue) => setNewJourney(newJourney => ({...newJourney, returntime: newValue}))}
                sx={{marginTop:"10px"}}
              />
            </LocalizationProvider>

            <Autocomplete
              value={newJourney.departure_station_name || null}
              sx={{ width: "100%", marginTop:"10px" }}
              options={stationNames}
              onChange={(event, newValue) => {
                setNewJourney(newJourney => ({...newJourney, departure_station_name: newValue}))
              }}
              inputValue={inputValueDeparture}
              onInputChange={(event, newInputValue) => {
                setInputValueDeparture(newInputValue)
              }}
              renderInput={(params) => <TextField {...params} label={strings.departureStationName} />}
              
            />
            <Autocomplete
              value={newJourney.return_station_name || null}
              sx={{ width: "100%", marginTop:"10px" }}
              options={stationNames}
              onChange={(event, newValue) => {
                setNewJourney(newJourney => ({...newJourney, return_station_name: newValue}))
              }}
              inputValue={inputValueReturn}
              onInputChange={(event, newInputValue) => {
                setInputValueReturn(newInputValue)
              }}
              renderInput={(params) => <TextField {...params} label={strings.returnStationName} />}
            />

            <TextField
              label={strings.coveredDistanceM}
              onChange={(event) => setNewJourney(newJourney => ({...newJourney, covered_distance_m: event.target.value}))}
              sx={{marginTop:"10px"}}
            />

            <Button onClick={(event) => handleJourneyClick(event)} sx={{marginTop:"10px"}}>{strings.createNewJourney}</Button>
            <Popover
              id={"journeyPopover"}
              open={Boolean(anchorJourney)}
              anchorEl={anchorJourney}
              onClose={() => handlePopoverClose()}
              anchorOrigin={{
                vertical: 'bottom',
                horizontal: 'center',
              }}
              transformOrigin={{
                vertical: 'top',
                horizontal: 'center',
              }}
            >
              <Typography sx={{ p: 2 }}>{strings.errors.invalidInput}: {errorMessage}</Typography>
            </Popover>
          </div>

          <div style={{width:"45%", display: "flex", flexDirection: "column", marginRight: "20px", minWidth:"300px"}}>
            <Typography variant={"h2"}>{strings.newStation}</Typography>
            <TextField
              label="Nimi"
              value={newStation.nimi}
              onChange={(event) => setNewStation(newStation => ({...newStation, nimi: (event.target.value)}))}
              sx={{marginTop:"10px"}}
            />
            <TextField
              label="Namn"
              value={newStation.namn}
              onChange={(event) => setNewStation(newStation => ({...newStation, namn: (event.target.value)}))}
              sx={{marginTop:"10px"}}
            />
            <TextField
              label="Name"
              value={newStation.name}
              onChange={(event) => setNewStation(newStation => ({...newStation, name: (event.target.value)}))}
              sx={{marginTop:"10px"}}
            />
            <TextField
              label="Osoite"
              value={newStation.osoite}
              onChange={(event) => setNewStation(newStation => ({...newStation, osoite: (event.target.value)}))}
              sx={{marginTop:"10px"}}
            />
            <TextField
              label="Adress"
              value={newStation.adress}
              onChange={(event) => setNewStation(newStation => ({...newStation, adress: (event.target.value)}))}
              sx={{marginTop:"10px"}}
            />
            <div style={{marginTop:"10px", display:"flex"}}>
              <div style={{width:"80%"}}>
                <TextField
                  label="x"
                  value={newStation.x}
                  onChange={(event) => setNewStation(newStation => ({...newStation, x: (event.target.value)}))}
                  sx={{width: "100%"}}
                />
                <TextField
                  label="y"
                  value={newStation.y}
                  onChange={(event) => setNewStation(newStation => ({...newStation, y: (event.target.value)}))}
                  sx={{width: "100%", marginTop:"10px"}}
                />
              </div>
              <Button
                sx={{width: "20%", height:"100%"}}
                variant="outlined"
                onClick={() => setOpenMap(true)}
              >
                <AddLocationAltSharpIcon />
              </Button>
            </div>
            <TextField
              label={strings.capacity}
              value={newStation.kapasiteet}
              onChange={(event) => setNewStation(newStation => ({...newStation, kapasiteet: (event.target.value)}))}
              sx={{marginTop:"10px"}}
            />
            <Button onClick={(event) => handleStationClick(event)} sx={{marginTop:"10px", marginBottom:"10px"}}>{strings.createNewStation}</Button>
            <Popover
              id={"stationPopover"}
              open={Boolean(anchorStation)}
              anchorEl={anchorStation}
              onClose={() => handlePopoverClose()}
              anchorOrigin={{
                vertical: 'bottom',
                horizontal: 'center',
              }}
              transformOrigin={{
                vertical: 'top',
                horizontal: 'center',
              }}
            >
              <Typography sx={{ p: 2 }}>{strings.errors.invalidInput}: {errorMessage}</Typography>
            </Popover>
          </div>
        </Grid>
      </Paper>
    </>
  )
}

export default Settings