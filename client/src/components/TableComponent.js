import { Box, Grid, IconButton, InputAdornment, Paper, Table, TableBody, TableContainer, TableFooter, TableHead, TablePagination, TableRow, TableSortLabel, TextField } from "@mui/material"
import { StyledTableCell } from "../styles"
import LastPage from '@mui/icons-material/LastPage'
import FirstPage from '@mui/icons-material/FirstPage'
import KeyboardArrowRight from '@mui/icons-material/KeyboardArrowRight'
import KeyboardArrowLeft from '@mui/icons-material/KeyboardArrowLeft'
import SearchIcon from '@mui/icons-material/Search'
import { getIndividualStationInfo } from "../reducers/stationReducer"

import { useState } from "react"
import { strings } from "../utils/localization"
import { useNavigate } from "react-router-dom"

import 'dayjs/locale/fi'
import 'dayjs/locale/se'
import 'dayjs/locale/en-gb'
import FilterCard from "./FilterCard"
import { useDispatch } from "react-redux"

/**
 * Gets array of data and renders it into table list.
 * @param {Object} orderData Contains data of saved order values
 * @param {ReferenceState} setOrderData Sets new value to orderData
 * @param {Number} dataCount Tells how many items there are in current filtered data
 * @param {Array<Object>} data Showing data
 * @param {Object} orderNames Column headers
 * @param {Object} filterData Contains data of saved filter values
 * @param {ReferenceState} setFilterData Sets new values to filterData
 * @returns {JSX.Element} Rendered table component
 */
const TableComponent = ({orderData, setOrderData, dataCount, data, orderNames, filterData, setFilterData}) => {
  const dispatch = useDispatch()
  const navigate = useNavigate()
  const [page, setPage] = (useState(Number(sessionStorage.getItem("page")))||0)
  const [selectedMonth, setSelectedMonth] = useState(orderData.month || 5)
  const [currentSearch, setCurrentSearch] = useState("")
  const [openFilterCard, setOpenFilterCard] = useState(filterData ? true : false)

  /**
   * Sorts data based on selected header
   * @param {String} orderName selected header
   * @returns {void}
   */
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

  /**
   * Sets rows width based on how many headers there are.
   * @returns {String} new width as prosentage
   */
  const rowWidth = () => {
    return `${100 / Object.keys(orderNames).length}%`
  }

  /**
   * Sets page to be 0
   * @returns {void}
   */
  const handleFirstPageButtonClick = () => {
    setPage(0)
    sessionStorage.setItem("page", 0)
    setOrderData(orderData => ({
      ...orderData,
      offset: 0
    }))
  }

  /**
  * Sets page to be 1 smaller than current one
  * @returns {void}
  */
  const handleBackButtonClick = () => {
    setPage(page - 1)
    sessionStorage.setItem("page", page - 1)
    setOrderData(orderData => ({
      ...orderData,
      offset: Math.ceil(orderData.limit * (page-1))
    }))
  }

  /**
  * Sets page to be 1 bigger than current one
  * @returns {void}
  */
  const handleNextButtonClick = () => {
    setPage(page + 1)
    sessionStorage.setItem("page", page + 1)
    setOrderData(orderData => ({
      ...orderData,
      offset: Math.ceil(orderData.limit * (page+1))
    }))
  }

  /**
  * Sets page to be last possible number
  * @returns {void}
  */
  const handleLastPageButtonClick = () => {
    setPage(Math.max(0, Math.ceil(dataCount / orderData.limit) - 1))
    sessionStorage.setItem("page", Math.max(0, Math.ceil(dataCount / orderData.limit) - 1))
    setOrderData(orderData => ({
      ...orderData,
      offset: Math.ceil(orderData.limit*Math.ceil(dataCount / orderData.limit-1))
    }))
  }

  /**
   * Checks if selected target is already the previous header to be sorted.
   * Then returns value depenging on the previous one.
   * @param {String} target selected header
   * @returns  {String} desc or asc
   */
  const direction = (target) => {
    if (target === orderData.order) {
      if (!orderData.desc) return 'desc'
    }
    return 'asc'
  }

  /**
   * Handle page change function for TablePagination
   * @param {Event} event default event 
   * @param {Number} newPage new pageNumber
   * @returns {void}
   */
  const handleChangePage = (event, newPage) => {
    setPage(newPage)
  }

  /**
   * Sets new limit of showing rows in table
   * @param {Event} event default event 
   * @returns {void}
   */
  const handleChangeRowsPerPage = (event) => {
    setOrderData(orderData => ({
      ...orderData,
      limit: parseInt(event.target.value, 10)
    }))
    setPage(0)
  }

  /**
   * Sets new seach value to orderData
   * @returns {void}
   */
  const handleSearch = () => {
    setOrderData(orderData => ({
      ...orderData,
      search: currentSearch
    }))
  }

  /**
   * Navigates to the selected station's information
   * @param {Number} id Selected station's id
   * @returns {void}
   */
  const handleNavigation = (id) => {
    dispatch(getIndividualStationInfo({id: id, month: ""}))
    navigate(`/stations/${id}`)
  }

  /**
   * Handles and renders options of table pagination
   * @returns {JSX.Element} Rendered pagination options
   */
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

  /**
   * Renders table headers
   * @returns {JSX.Element} Table headers
   */
  const headers = () => {
    if (filterData) {
      console.log(orderNames);
      return Object.entries(orderNames).map(([key, value]) => {
        const name = value.toLowerCase().replace(/([-_][a-z])/g, group => group.toUpperCase().replace('-', '').replace('_', ''))

        return (<StyledTableCell key={value}>
          <TableSortLabel 
            onClick={() => handleSorting(value)}
            direction={direction(value)}
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
        >
          {value}
        </TableSortLabel>
      </StyledTableCell>
      )
    })
  }

  /**
   * Renders current row into table
   * @param {Object} row Current rendering row
   * @returns {JSX.Element} Table Row
   */
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

  return (
    <Paper elevation={0} style={{width: "100%", display:"grid", justifyItems:"end", marginBottom: "25px"}}>
      <Grid container direction="row" justifyContent="flex-end" alignItems="baseline">
        
        <TextField
          variant="outlined"
          id="filled-basic"
          label={strings.search}
          defaultValue={orderData.search}
          InputProps={{
            endAdornment: (
              <InputAdornment position="end">
                <IconButton type="button" sx={{ p: '10px', backgroundColor:currentSearch !== orderData.search ? "rgb(182, 217, 220)" : "" }} aria-label="search" onClick={() => handleSearch()}>
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
        {openFilterCard && 
        <FilterCard 
          filterData={filterData} 
          setFilterData={setFilterData} 
          selectedMonth={selectedMonth}
          setOrderData={setOrderData}
          setSelectedMonth={setSelectedMonth}
          orderData={orderData}
        />}
      </Grid>
      
      <TableContainer className="tableContainer">
        <Table stickyHeader aria-label="sticky table">
          <TableHead>
            <TableRow>
              {headers()}
            </TableRow>
          </TableHead>
          <TableBody>
            
            {data && data.map((row) => (
              <TableRow
                key={row.id}
                onClick={() => !filterData && handleNavigation(row.id)}
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