import { Box, Button, Card, CardContent, Checkbox, FormControl, FormControlLabel, Grid, IconButton, InputAdornment, Paper, Table, TableBody, TableContainer, TableFooter, TableHead, TablePagination, TableRow, TableSortLabel, TextField, ToggleButton, ToggleButtonGroup, Typography } from "@mui/material"
import { StyledTableCell } from "../styles"
import LastPage from '@mui/icons-material/LastPage'
import FirstPage from '@mui/icons-material/FirstPage'
import KeyboardArrowRight from '@mui/icons-material/KeyboardArrowRight'
import KeyboardArrowLeft from '@mui/icons-material/KeyboardArrowLeft'
import SearchIcon from '@mui/icons-material/Search'
import HorizontalRuleIcon from '@mui/icons-material/HorizontalRule'
import { useState } from "react"
import { strings } from "../utils/localization"
import { useNavigate } from "react-router-dom"
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs'
import { DateTimePicker, LocalizationProvider } from "@mui/x-date-pickers"
import dayjs from 'dayjs'
import 'dayjs/locale/fi'
import 'dayjs/locale/se'
import 'dayjs/locale/en-gb'
import { validateAll, validateIsItIntegerNumber, validateIsItNumber, validateNumberIsBiggerThanMinumum, validateTimeNumberIsBiggerThanMinumum } from "../utils/validation"

const TableComponent = ({orderData, setOrderData, dataCount, data, orderNames, filterData, setFilterData}) => {
  const navigate = useNavigate()
  const [page, setPage] = (useState(Number(sessionStorage.getItem("page")))||0)
  const [selectedMonth, setSelectedMonth] = useState(orderData.month)
  const [currentSearch, setCurrentSearch] = useState("")

  const [openFilterCard, setOpenFilterCard] = useState(filterData ? true : false)
  const [enableHours, setEnableHours] = useState(filterData?.durationMin >= 60)
  const [departureFilter, setDepartureFilter] = useState(filterData?.departure ? dayjs(filterData?.departure) : "")
  const [returnFilter, setReturnFilter] = useState(filterData?.return ? dayjs(filterData?.return) : "")
  const [distanceMinFilter, setDistanceMinFilter] = useState(filterData?.distanceMin || "")
  const [distanceMaxFilter, setDistanceMaxFilter] = useState(filterData?.distanceMax || "")
  const [durationHourMinFilter, setDurationHourMinFilter] = useState(Math.trunc(Number(filterData?.durationMin)/60) || "")
  const [durationHourMaxFilter, setDurationHourMaxFilter] = useState(Math.trunc(Number(filterData?.durationMax)/60) || "")
  const [durationMinFilter, setDurationMinFilter] = useState(Number(filterData?.durationMin)%60 || "")
  const [durationMaxFilter, setDurationMaxFilter] = useState(Number(filterData?.durationMax)%60 || "")
  const [minStartTime, setMinStartTime] = useState(dayjs(`2021-0${selectedMonth}-01T00:00`))
  const [maxStartTime, setMaxStartTime] = useState(dayjs(`2021-0${selectedMonth}-${new Date(2021, selectedMonth, 0).getDate()}T23:59:59`))
  const maxEndTime = dayjs(`2021-12-31T23:59:59`)

  const language = () => {
    if (localStorage.getItem("language") === "gb") return "en-gb"
    if (localStorage.getItem("language") === "se") return "sv"
    return "fi"
  }

  const handleSorting = (orderName) => {
    if (orderData.order !== orderName) {
      setOrderData(orderData => ({
        ...orderData,
        order: orderName,
        desc: false
      }))
    }else {
      setOrderData(orderData => ({
        ...orderData,
        order: orderName,
        desc: !orderData.desc
      }))
    }
  }

  const rowWidth = () => {
    return `${100 / Object.keys(orderNames).length}%`
  }

  const handleFirstPageButtonClick = () => {
    setPage(0)
    sessionStorage.setItem("page", 0)
    setOrderData(orderData => ({
      ...orderData,
      offset: 0
    }))
  }

  const handleBackButtonClick = () => {
    setPage(page - 1)
    sessionStorage.setItem("page", page - 1)
    setOrderData(orderData => ({
      ...orderData,
      offset: Math.ceil(orderData.limit * (page-1))
    }))
  }

  const handleNextButtonClick = () => {
    setPage(page + 1)
    sessionStorage.setItem("page", page + 1)
    setOrderData(orderData => ({
      ...orderData,
      offset: Math.ceil(orderData.limit * (page+1))
    }))
  }

  const handleLastPageButtonClick = () => {
    setPage(Math.max(0, Math.ceil(dataCount / orderData.limit) - 1))
    sessionStorage.setItem("page", Math.max(0, Math.ceil(dataCount / orderData.limit) - 1))
    setOrderData(orderData => ({
      ...orderData,
      offset: Math.ceil(orderData.limit*Math.ceil(dataCount / orderData.limit-1))
    }))
  }

  const direction = (target) => {
    if (target === orderData.order) {
      if (!orderData.desc) return 'desc'
    }
    return 'asc'
  }
  
  const handleChangePage = (event, newPage) => {
    setPage(newPage)
  }

  const handleChangeRowsPerPage = (event) => {
    setOrderData(orderData => ({
      ...orderData,
      limit: parseInt(event.target.value, 10)
    }))
    setPage(0)
  }

  const handleSearch = () => {
    setOrderData(orderData => ({
      ...orderData,
      search: currentSearch
    }))
  }

  const handleAlignment = (event, newAlignment) => {
    if (newAlignment !== null) {
      setSelectedMonth(newAlignment)
      setOrderData(orderData => ({
        ...orderData,
        month: newAlignment
      }))
      setMinStartTime(dayjs(`2021-0${newAlignment}-01T00:00`))
      setMaxStartTime(dayjs(`2021-0${newAlignment}-${new Date(2021, newAlignment, 0).getDate()}T23:59:59`))
    }
  }

  const TablePaginationActions = () => {
    return (
      <Box sx={{ flexShrink: 0, ml: 2.5 }}>
        <IconButton
          onClick={handleFirstPageButtonClick}
          disabled={page === 0}
          aria-label="first page"
        >
          <FirstPage />
        </IconButton>
        <IconButton
          onClick={handleBackButtonClick}
          disabled={page === 0}
          aria-label="previous page"
        >
          <KeyboardArrowLeft />
        </IconButton>
        <IconButton
          onClick={handleNextButtonClick}
          disabled={page >= Math.ceil(dataCount / orderData.limit) - 1}
          aria-label="next page"
        >
          <KeyboardArrowRight />
        </IconButton>
        <IconButton
          onClick={handleLastPageButtonClick}
          disabled={page >= Math.ceil(dataCount / orderData.limit) - 1}
          aria-label="last page"
        >
          <LastPage />
        </IconButton>
      </Box>
    )
  }

  const headers = () => {
    if (filterData) {
      return Object.entries(orderNames).map(([key, value]) => {
        const name = value.toLowerCase().replace(/([-_][a-z])/g, group => group.toUpperCase().replace('-', '').replace('_', ''))
        return (<StyledTableCell key={value}>
          <TableSortLabel 
            onClick={() => handleSorting(value)}
            direction={direction(value)}
            key={`id_${value}`}
          >
            {strings[name]}
          </TableSortLabel>
        </StyledTableCell>
        )
      })
    }
    return Object.entries(orderNames).map(([key, value]) => {
      return (<StyledTableCell key={value}>
        <TableSortLabel 
          onClick={() => handleSorting(value)}
          direction={direction(value)}
          key={`id${value}`}
        >
          {value}
        </TableSortLabel>
      </StyledTableCell>
      )
    })
  }

  const rowData = (row) => {
    return Object.entries(orderNames).map(([key]) => {
      if (typeof row[key.toLocaleLowerCase()] === "string") {
        if (row[key.toLocaleLowerCase()].split(":").length > 3) {
          const durationWithDays = row[key.toLocaleLowerCase()].split(/:(.*)/)
          return <StyledTableCell key={`${row.id}${key}`} style={{width: rowWidth(), cursor:"pointer"}}>{durationWithDays[0]} {strings.days} {durationWithDays[1]}</StyledTableCell>
        }
      }
      return <StyledTableCell key={`${row.id}${key}`} style={{width: rowWidth(), cursor:"pointer"}}>{row[key.toLocaleLowerCase()]}</StyledTableCell>
    } )
  }

  const filterCard = () => {
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

    const handleSubmit = () => {
      let durationMin = Number(durationMinFilter)
      let durationMax = Number(durationMaxFilter)
      if (durationHourMinFilter) durationMin += Number(durationHourMinFilter)*60
      if (durationHourMaxFilter) durationMax += Number(durationHourMaxFilter)*60
      
      setFilterData(filterData => ({
        departure: departureFilter ? departureFilter.format() : "",
        return: returnFilter ? returnFilter.format() : "",
        distanceMin: Number(distanceMinFilter),
        distanceMax: Number(distanceMaxFilter),
        durationMin: Number(durationMin),
        durationMax: Number(durationMax)
      }))
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
                <Typography variant="body2">{strings.return}</Typography>
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
                  error={validateIsItNumber(distanceMinFilter)}
                  sx={{width: "100px"}}
                  onChange={(e) => setDistanceMinFilter(e.target.value)}
                  value={distanceMinFilter}
                  helperText={validateIsItNumber(distanceMinFilter)}
                />
                <HorizontalRuleIcon sx={{position: "relative", top:"15px"}}/>
                <TextField
                  error={validateIsItNumber(distanceMaxFilter) || validateNumberIsBiggerThanMinumum(distanceMinFilter,distanceMaxFilter)}
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
                    error={validateIsItIntegerNumber(durationHourMinFilter)}
                    helperText={validateIsItIntegerNumber(durationHourMinFilter)}
                  />
                }
                <TextField
                  label={strings.min}
                  sx={{width: "100px"}}
                  onChange={(e) => setDurationMinFilter(e.target.value)}
                  value={durationMinFilter}
                  error={validateIsItIntegerNumber(durationMinFilter)}
                  helperText={validateIsItIntegerNumber(durationMinFilter)}
                />
                <HorizontalRuleIcon sx={{position: "relative", top:"15px"}}/>
                {enableHours && 
                  <TextField
                    label={"H"}
                    sx={{width: "60px"}}
                    onChange={(e) => setDurationHourMaxFilter(e.target.value)}
                    value={durationHourMaxFilter}
                    error={validateIsItIntegerNumber(durationHourMaxFilter)
                      || validateTimeNumberIsBiggerThanMinumum(durationHourMinFilter, durationMinFilter, durationHourMaxFilter, durationMaxFilter)}
                    helperText={validateIsItIntegerNumber(durationHourMaxFilter)
                      || validateTimeNumberIsBiggerThanMinumum(durationHourMinFilter, durationMinFilter, durationHourMaxFilter, durationMaxFilter)}
                  />
                }
                <TextField
                  label={strings.min}
                  sx={{width: "100px"}}
                  onChange={(e) => setDurationMaxFilter(e.target.value)}
                  value={durationMaxFilter}
                  error={validateIsItIntegerNumber(durationMaxFilter)
                    || validateTimeNumberIsBiggerThanMinumum(durationHourMinFilter, durationMinFilter, durationHourMaxFilter, durationMaxFilter)}
                  helperText={validateIsItIntegerNumber(durationMaxFilter) 
                    || validateTimeNumberIsBiggerThanMinumum(durationHourMinFilter, durationMinFilter, durationHourMaxFilter, durationMaxFilter)}
                />
              </div>
              
            </Grid>
            <div style={{display:"flex", justifyContent:"end"}}>
              <Button onClick={() => clearFrom()}>{strings.empty}</Button>
              <Button disabled={ableToSubmit()} onClick={() => handleSubmit()} color='secondary'>{strings.filter}</Button>
            </div>
          </FormControl>
        </CardContent>
      </Card>
    )
  }

  return (
    <Paper elevation={0} style={{width: "100%", display:"grid", justifyItems:"end", marginBottom: "25px"}}>
      <Grid container direction="row" justifyContent="flex-end" alignItems="baseline">
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
        <TextField
          variant="outlined"
          id="filled-basic"
          label={strings.search}
          defaultValue={orderData.search}
          InputProps={{
            endAdornment: (
              <InputAdornment position="end">
                <IconButton type="button" sx={{ p: '10px', backgroundColor:"rgb(182, 217, 220)" }} aria-label="search" onClick={() => handleSearch()}>
                  <SearchIcon />
                </IconButton>
              </InputAdornment>
            ),
          }}
          className="searchInput"
          onChange={event => setCurrentSearch(event.target.value)}
        />
      </Grid>
      <Grid container justifyContent="center">
        {openFilterCard && filterCard()}
      </Grid>
      
      <TableContainer className="tableContainer">
        <Table stickyHeader aria-label="sticky table">
          <TableHead>
            <TableRow>
              {headers()}
            </TableRow>
          </TableHead>
          <TableBody>
            {data.map((row) => (
              <TableRow
                key={row.id}
                onClick={() => !filterData && navigate(`/stations/${row.id}`)}
              >
                {rowData(row)}
              </TableRow>
            ))}
            
          </TableBody>
          <TableFooter>
            <TableRow>
              <TablePagination
                rowsPerPageOptions={[20, 50, 100]}
                count={dataCount}
                rowsPerPage={orderData.limit}
                page={page}
                onPageChange={handleChangePage}
                onRowsPerPageChange={handleChangeRowsPerPage}
                ActionsComponent={TablePaginationActions}
              />
            </TableRow>
          </TableFooter>
        </Table>
      </TableContainer>
    </Paper>
  )
}

export default TableComponent