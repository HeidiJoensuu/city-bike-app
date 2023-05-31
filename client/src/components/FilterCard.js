import { Button, Card, CardContent, Checkbox, FormControl, FormControlLabel, Grid, TextField, ToggleButton, ToggleButtonGroup, Typography } from "@mui/material"
import { strings } from "../utils/localization"
import { validateAll, validateIsItIntegerNumber, validateIsItNumber, validateNumberIsBiggerThanMinumum, validateTimeNumberIsBiggerThanMinumum } from "../utils/validation"
import HorizontalRuleIcon from '@mui/icons-material/HorizontalRule'
import { useState } from "react"
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs'
import { DateTimePicker, LocalizationProvider } from "@mui/x-date-pickers"
import dayjs from "dayjs"

/**
 * Renders card of filters for journeys
 * @param {Object} filterData Contains data of saved filter values
 * @param {ReferenceState} setFilterData Sets new values to filterData
 * @param {Number} selectedMonth Current selected month
 * @param {Object} orderData Contains data of saved order values
 * @param {ReferenceState} setOrderData Sets new value to orderData
 * @param {ReferenceState} setSelectedMonth Sets new value to selectedMonth
 * @returns {JSX.Element} rendered filter card
 */
const FilterCard = ({filterData, setFilterData, selectedMonth, orderData, setOrderData, setSelectedMonth}) => {
  const [enableHours, setEnableHours] = useState(filterData?.durationMin >= 60)
  const [departureFilter, setDepartureFilter] = useState(filterData?.departure ? dayjs(filterData?.departure) : "")
  const [returnFilter, setReturnFilter] = useState(filterData?.returntime ? dayjs(filterData?.returntime) : "")
  const [distanceMinFilter, setDistanceMinFilter] = useState(filterData?.distanceMin || "")
  const [distanceMaxFilter, setDistanceMaxFilter] = useState(filterData?.distanceMax || "")
  const [durationHourMinFilter, setDurationHourMinFilter] = useState(Math.trunc(Number(filterData?.durationMin)/60) || "")
  const [durationHourMaxFilter, setDurationHourMaxFilter] = useState(Math.trunc(Number(filterData?.durationMax)/60) || "")
  const [durationMinFilter, setDurationMinFilter] = useState(Number(filterData?.durationMin)%60 || "")
  const [durationMaxFilter, setDurationMaxFilter] = useState(Number(filterData?.durationMax)%60 || "")
  const [minStartTime, setMinStartTime] = useState(dayjs(`2021-0${selectedMonth}-01T00:00`))
  const [maxStartTime, setMaxStartTime] = useState(dayjs(`2021-0${selectedMonth}-${new Date(2021, selectedMonth, 0).getDate()}T23:59:59`))
  const maxEndTime = dayjs(`2021-12-31T23:59:59`)

  /**
   * Checks and gives correct language option for LocalizationProvider
   * @returns {String} current selected language
   */
  const language = () => {
    if (localStorage.getItem("language") === "gb") return "en-gb"
    if (localStorage.getItem("language") === "se") return "sv"
    return "fi"
  }

  /**
   * Checks if there are any error in filter inputs.
   * If there are errors this function disables the submit-button.
   * @returns {Boolean} Disables or not the submit button
   */
  const ableToSubmit = () => validateAll(
    selectedMonth,
    new Date(2021, selectedMonth, 0).getDate(),
    departureFilter,
    returnFilter,
    distanceMinFilter,
    distanceMaxFilter,
    durationHourMinFilter,
    durationMinFilter,
    durationHourMaxFilter,
    durationMaxFilter
  )

  /**
   * Clears all filtering data form states.
   * @returns {void}
   */
  const clearFrom = () => {
    setDepartureFilter("")
    setReturnFilter("")
    setDistanceMinFilter("")
    setDistanceMaxFilter("")
    setDurationMinFilter("")
    setDurationMaxFilter("")
    setDurationHourMinFilter("")
    setDurationHourMaxFilter("")

    setFilterData(filterData => ({
      departure: "",
      return: "",
      distanceMin: "",
      distanceMax: "",
      durationMin: "",
      durationMax: ""
    }))
  }

  /**
   * Submits/saves new filterdata
   * @returns {void}
   */
  const handleSubmit = () => {
    let durationMin = Number(durationMinFilter)
    let durationMax = Number(durationMaxFilter)
    if (durationHourMinFilter) durationMin += Number(durationHourMinFilter)*60
    if (durationHourMaxFilter) durationMax += Number(durationHourMaxFilter)*60
    
    setFilterData(filterData => ({
      departure: departureFilter ? departureFilter.format() : "",
      returntime: returnFilter ? returnFilter.format() : "",
      distanceMin: typeof distanceMinFilter === 'string' ? Number(distanceMinFilter.replace(",", ".")) : distanceMinFilter,
      distanceMax: typeof distanceMaxFilter === 'string' ? Number(distanceMaxFilter.replace(",", ".")) : distanceMaxFilter,
      durationMin: Number(durationMin)*60,
      durationMax: Number(durationMax)*60
    }))
  }

  /**
   * Changes all information that is related to selecedMonth
   * @param {Event} event default event 
   * @param {strign} newAlignment Current selected month
   * @returns {void}
   */
  const handleAlignment = (event, newAlignment) => {
    if (newAlignment !== null) {
      setSelectedMonth(newAlignment)
      setOrderData(orderData => ({
        ...orderData,
        month: newAlignment,
        departure: `2021-0${newAlignment}-01T00:00`,
        returntime: `2021-0${newAlignment}-${new Date(2021, newAlignment, 0).getDate()}T23:59:59`
      }))
      if (orderData.departure !== "") {
        setOrderData(orderData => ({
          ...orderData,
          departure: `2021-0${newAlignment}-01T00:00`,
        }))
      }
      if (orderData.returntime !== "") {
        setOrderData(orderData => ({
          ...orderData,
          returntime: `2021-0${newAlignment}-${new Date(2021, newAlignment, 0).getDate()}T23:59:59`,
        }))
      }
      setMinStartTime(dayjs(`2021-0${newAlignment}-01T00:00`))
      setMaxStartTime(dayjs(`2021-0${newAlignment}-${new Date(2021, newAlignment, 0).getDate()}T23:59:59`))
    }
    
  }

  return (
    <Card sx={{width: "100%"}}>
      <CardContent sx={{width: "99%"}}>
        <FormControl sx={{width: "99%"}}>
          <Grid container
            direction="row"
            justifyContent="space-between"
          >
            <div>
              <Typography variant="body2">{strings.departure}</Typography>
              <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale={language()} >
                <DateTimePicker
                  value={departureFilter !== "" ? departureFilter : minStartTime}
                  ampm={false}
                  views={['day', 'hours']}
                  minDateTime={minStartTime}
                  maxDateTime={returnFilter || maxStartTime}
                  onChange={(newTime) => setDepartureFilter(newTime)}
                />
              </LocalizationProvider>
            </div>
            <div>
              <Typography variant="body2">{strings.returntime}</Typography>
              <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale={language()} >
                <DateTimePicker
                  value={returnFilter !== "" ? returnFilter : maxStartTime}
                  ampm={false}
                  views={['day', 'hours']}
                  minDateTime={departureFilter || dayjs(`2021-0${selectedMonth}-01T00:01`)}
                  maxDateTime={maxEndTime}
                  onChange={(newTime) => setReturnFilter(newTime)}
                />
              </LocalizationProvider>
            </div>

            <div >
              <Typography variant="body2">{strings.coveredDistance}</Typography>
              <TextField 
                error={validateIsItNumber(distanceMinFilter) !== ""}
                sx={{width: "100px"}}
                onChange={(e) => setDistanceMinFilter(e.target.value)}
                value={distanceMinFilter}
                helperText={validateIsItNumber(distanceMinFilter)}
              />
              <HorizontalRuleIcon sx={{position: "relative", top:"15px"}}/>
              <TextField
                error={validateIsItNumber(distanceMaxFilter) !== "" || validateNumberIsBiggerThanMinumum(distanceMinFilter,distanceMaxFilter) !== ""}
                sx={{width: "100px"}}
                onChange={(e) => setDistanceMaxFilter(e.target.value)}
                value={distanceMaxFilter}
                helperText={validateIsItNumber(distanceMaxFilter) ||
                  validateNumberIsBiggerThanMinumum(distanceMinFilter,distanceMaxFilter)}
              />
            </div>
            <div>
              <div style={{display:"flex", alignItems:"center", justifyContent:"space-between"}}>
                <Typography variant="body2">
                  {strings.duration}
                </Typography>
                <FormControlLabel
                  value="start"
                  control={
                    <Checkbox
                      checked={enableHours}
                      onChange={() => setEnableHours(!enableHours)}
                      inputProps={{ 'aria-label': 'controlled' }}
                    />
                  }
                  label={strings.hours}
                  labelPlacement="start"
                />
              </div>
              {enableHours && 
                <TextField
                  label={"H"}
                  sx={{width: "60px"}}
                  onChange={(e) => setDurationHourMinFilter(e.target.value)}
                  value={durationHourMinFilter}
                  error={validateIsItIntegerNumber(durationHourMinFilter) !== ""}
                  helperText={validateIsItIntegerNumber(durationHourMinFilter)}
                />
              }
              <TextField
                label={strings.min}
                sx={{width: "100px"}}
                onChange={(e) => setDurationMinFilter(e.target.value)}
                value={durationMinFilter}
                error={validateIsItIntegerNumber(durationMinFilter) !== ""}
                helperText={validateIsItIntegerNumber(durationMinFilter)}
              />
              <HorizontalRuleIcon sx={{position: "relative", top:"15px"}}/>
              {enableHours && 
                <TextField
                  label={"H"}
                  sx={{width: "60px"}}
                  onChange={(e) => setDurationHourMaxFilter(e.target.value)}
                  value={durationHourMaxFilter}
                  error={validateIsItIntegerNumber(durationHourMaxFilter)  !== ""
                    || validateTimeNumberIsBiggerThanMinumum(durationHourMinFilter, durationMinFilter, durationHourMaxFilter, durationMaxFilter) !== ""}
                  helperText={validateIsItIntegerNumber(durationHourMaxFilter)
                    || validateTimeNumberIsBiggerThanMinumum(durationHourMinFilter, durationMinFilter, durationHourMaxFilter, durationMaxFilter)}
                />
              }
              <TextField
                label={strings.min}
                sx={{width: "100px"}}
                onChange={(e) => setDurationMaxFilter(e.target.value)}
                value={durationMaxFilter}
                error={validateIsItIntegerNumber(durationMaxFilter)  !== ""
                  || validateTimeNumberIsBiggerThanMinumum(durationHourMinFilter, durationMinFilter, durationHourMaxFilter, durationMaxFilter) !== ""}
                helperText={validateIsItIntegerNumber(durationMaxFilter) 
                  || validateTimeNumberIsBiggerThanMinumum(durationHourMinFilter, durationMinFilter, durationHourMaxFilter, durationMaxFilter)}
              />
            </div>
            
          </Grid>
          <div style={{display:"flex", justifyContent:"end", alignItems:"baseline"}}>
            {filterData && <>
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
                <ToggleButton value="5">{strings.may}</ToggleButton >
                <ToggleButton value="6">{strings.june}</ToggleButton >
                <ToggleButton value="7">{strings.july}</ToggleButton >
              </ToggleButtonGroup>
            </>
            }
            <Button onClick={() => clearFrom()}>{strings.empty}</Button>
            <Button disabled={ableToSubmit()} onClick={() => handleSubmit()} color='secondary'>{strings.filter}</Button>
          </div>
        </FormControl>
      </CardContent>
    </Card>
  )
}

export default FilterCard