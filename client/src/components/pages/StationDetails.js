import { useEffect, useMemo, useState } from "react"
import { useDispatch,useSelector } from "react-redux"
import { getIndividualStationInfo } from "../../reducers/stationReducer"
import {  Grid, Paper, ToggleButton, ToggleButtonGroup, Typography, useMediaQuery } from "@mui/material"
import { strings } from "../../utils/localization"
import ArrowBackIcon from '@mui/icons-material/ArrowBack'
import { ListGrid, ReturnButton, theme } from "../../styles"
import { useNavigate } from "react-router-dom"
import { MapContainer, Marker, TileLayer } from 'react-leaflet'
import L from "leaflet"
import 'leaflet/dist/leaflet.css'
import icon from '../../assets/StationMark.png'

/**
 * Renders the information page of selected station.
 * @returns {JSX.element} Rendred station detail page
 */
const StationDetail = () => {
  const dispatch = useDispatch()
  const navigate = useNavigate()
  const small = useMediaQuery(theme.breakpoints.down("laptop"))
  const { station } = useSelector(state => state.stations)
  const [map, setMap] = useState(null)
  const [marker, setMarker] = useState(null)
  const id = location.pathname.split('/')[2]
  const [selectedMonth, setSelectedMonth] = useState("")
  const [center, setCenter] = useState(station.y ?[station.y, station.x] : [60.168916, 24.936007])

  const DefaultIcon = L.icon({
    iconUrl: icon,
    iconSize: [16, 40],
    iconAnchor: [8,40],
  })

  L.Marker.prototype.options.icon = DefaultIcon

  useEffect(() =>{
    mapCenter()
  }, [station])

  /**
   * Changes selecedMonth
   * @param {Event} event default event 
   * @param {strign} newAlignment Current selected month
   * @returns {void}
   */
  const handleAlignment = (event, newAlignment) => {
    if (newAlignment === '') {
      setSelectedMonth("")
      dispatch(getIndividualStationInfo({id: id, month: ""}))
    }
    else if (newAlignment !== null) {
      setSelectedMonth(newAlignment)
      dispatch(getIndividualStationInfo({id: id, month: newAlignment}))
    }
  }

  /**
   * Renders simple list of stations
   * @param {Array<String>} listOfStations List of station
   * @returns {JSX.Element} list of stations
   */
  const stationList = (listOfStations) => {
    if(listOfStations)
      return listOfStations.map(list => <Typography key={list}>{list}</Typography>)
  }

  /**
   * Sets map center to it's new coordinates
   * @returns {void}
   */
  const mapCenter = () => {
    if (station.x && map!== null) {
      map.flyTo([station.y, station.x], 15)
      setCenter([station.y, station.x])
      marker.setLatLng([station.y, station.x])
    }
  }

  const markerLocation = useMemo(() => (
    <Marker position={center} ref={setMarker}>
    </Marker>
  ),[])

  const displayMap = useMemo(() => (
    <MapContainer center={center} zoom={15} scrollWheelZoom={false} className="map" ref={setMap}>
      <TileLayer
        attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
      />
      {markerLocation}
    </MapContainer>
  ),[])

  return (
    <Paper elevation={3} style={{width: "90%", marginBottom: "25px"}}>
      <ReturnButton
        startIcon={<ArrowBackIcon />}
        onClick={() => navigate(`/stations`)}
      >
        {strings.returnToList}
      </ReturnButton>
      <ToggleButtonGroup 
        variant="outlined"
        value={selectedMonth}
        exclusive
        onChange={handleAlignment}
        size="small"
        color="secondary"
        sx={{
          '& .MuiToggleButton-root': { borderColor: 'rgb(111, 201, 207)'},
          marginTop: '15px',
        }}
      >
        <ToggleButton value="">{strings.all}</ToggleButton >
        <ToggleButton value="5">{strings.may}</ToggleButton >
        <ToggleButton value="6">{strings.june}</ToggleButton >
        <ToggleButton value="7">{strings.july}</ToggleButton >
      </ToggleButtonGroup>
      <Grid container direction={small ? "column-reverse" : "row"} justifyContent="space-between" alignItems={small ? "center" : ""}>
        <Grid
          container
          direction="column"
          style={{marginLeft:"10px", marginTop:"20px", width: small ? "90%" : "50%"}}

        >
          <ListGrid container direction="row" rowSpacing={1} spacing={4}>
            <Grid item xs={6}>
              <Typography variant="subtitle1">Nimi</Typography>
              <Typography>{station.nimi}</Typography>
            </Grid>
            <Grid item xs={6}>
              <Typography variant="subtitle1">Namn</Typography>
              <Typography>{station.namn}</Typography>
            </Grid>
            <Grid item xs={6}>
              <Typography variant="subtitle1">Name</Typography>
              <Typography>{station.name}</Typography>
            </Grid>
          </ListGrid>

          <ListGrid container direction="row" rowSpacing={1} spacing={4}>
            <Grid item xs={6}>
              <Typography variant="subtitle1">Osoite</Typography>
              <Typography>{station.osoite}</Typography>
            </Grid>
            <Grid item xs={6}>
              <Typography variant="subtitle1">Adress/Address</Typography>
              <Typography>{station.adress}</Typography>
            </Grid>
          </ListGrid>

          <ListGrid container direction="row" rowSpacing={1} spacing={4}>
            <Grid item xs={6}>
              <Typography variant="subtitle1">{strings.countOfDepartures}</Typography>
              <Typography>{station.journeyInfo?.countOfDepartures}</Typography>
            </Grid>
            <Grid item xs={6}>
              <Typography variant="subtitle1">{strings.countOfReturns}</Typography>
              <Typography>{station.journeyInfo?.countOfReturns}</Typography>
            </Grid>
          </ListGrid>

          <ListGrid>
            <Typography variant="subtitle1">{strings.avDistanceDepartures}</Typography>
            <Typography>{station.journeyInfo?.averageDistanceDepartures}</Typography>

            <Typography variant="subtitle1">{strings.avDistanceReturns}</Typography>
            <Typography>{station.journeyInfo?.averageDistanceReturns}</Typography>
          </ListGrid>

          <ListGrid container direction="row" rowSpacing={1} spacing={4}>
            <Grid item xs={6}>
              <Typography variant="subtitle1">{strings.popularReturns}</Typography>
              {stationList(station.journeyInfo?.popularReturns)}
            </Grid>
            <Grid item xs={6}>
              <Typography variant="subtitle1">{strings.popularDepartures}</Typography>
              {stationList(station.journeyInfo?.popularDepartures)}
            </Grid>
          </ListGrid>
        </Grid>
        <Grid container style={{marginRight:"10px", marginTop:"20px", width: small ? "90%": "45%", height: small ? "400px" : ""}}>
          {displayMap}
        </Grid>
      </Grid>
    </Paper>
  )
}

export default StationDetail