import { Box, IconButton, InputAdornment, Paper, Table, TableBody, TableContainer, TableFooter, TableHead, TablePagination, TableRow, TableSortLabel, TextField, useTheme } from "@mui/material"
import LastPage from '@mui/icons-material/LastPage'
import FirstPage from '@mui/icons-material/FirstPage'
import KeyboardArrowRight from '@mui/icons-material/KeyboardArrowRight'
import KeyboardArrowLeft from '@mui/icons-material/KeyboardArrowLeft'
import SearchIcon from '@mui/icons-material/Search'
import { getStationsAsList, getStationsCount } from "../../reducers/stationReducer"
import { useEffect, useState } from "react"
import { useDispatch, useSelector } from "react-redux"
import { strings } from "../../utils/localization"
import { StyledTableCell } from "../../styles"
import { useNavigate } from "react-router-dom"

const OrderNames = {
  Nimi: "Nimi",
  Namn: "Namn",
  Name: "Name",
  Osoite: "Osoite",
  Adress: "Adress"
}

const StationsList = () => {
  const dispatch = useDispatch()
  const navigate = useNavigate()
  const [page, setPage] = useState(0)
  const { stationList, stationsCount } = useSelector(state => state.stations)
  const [wantedData, setWantedData] = useState({
    offset:0,
    limit:20,
    order: OrderNames.Nimi,
    search: "",
    desc: false
  })
  useEffect(() => {
    dispatch(getStationsAsList(wantedData))
    dispatch(getStationsCount(wantedData))
  }, [dispatch, wantedData])

  const handleSorting = (orderName) => {
    if (wantedData.order !== orderName) {
      setWantedData(wantedData => ({
        ...wantedData,
        order: orderName,
        desc: false
      }))
    }else {
      setWantedData(wantedData => ({
        ...wantedData,
        order: orderName,
        desc: !wantedData.desc
      }))
    }
  }

  const handleFirstPageButtonClick = () => {
    setPage(0)
    setWantedData(wantedData => ({
      ...wantedData,
      offset: 0
    }))
  }

  const handleBackButtonClick = () => {
    setPage(page - 1)
    setWantedData(wantedData => ({
      ...wantedData,
      offset: Math.ceil(wantedData.limit * (page-1))
    }))
  }

  const handleNextButtonClick = () => {
    setPage(page + 1)
    setWantedData(wantedData => ({
      ...wantedData,
      offset: Math.ceil(wantedData.limit * (page+1))
    }))
  }

  const handleLastPageButtonClick = () => {
    setPage(Math.max(0, Math.ceil(stationsCount / wantedData.limit) - 1))
    setWantedData(wantedData => ({
      ...wantedData,
      offset: Math.ceil(wantedData.limit*Math.ceil(stationsCount / wantedData.limit-1))
    }))
  }

  const direction = (target) => {
    if (target === wantedData.order) {
      if (!wantedData.desc) return 'desc'
    }
    return 'asc'
  }
  
  const handleChangePage = (event, newPage) => {
    setPage(newPage)
  }
  const handleChangeRowsPerPage = (event) => {
    setWantedData(wantedData => ({
      ...wantedData,
      limit: parseInt(event.target.value, 10)
    }))
    setPage(0)
  }
  const handleSearch = (event) => {

    setWantedData(wantedData => ({
      ...wantedData,
      search: event.target.value
    }))
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
          disabled={page >= Math.ceil(stationsCount / wantedData.limit) - 1}
          aria-label="next page"
        >
          <KeyboardArrowRight />
        </IconButton>
        <IconButton
          onClick={handleLastPageButtonClick}
          disabled={page >= Math.ceil(stationsCount / wantedData.limit) - 1}
          aria-label="last page"
        >
          <LastPage />
        </IconButton>
      </Box>
    )
  }

  return (
    <Paper elevation={0} style={{width: "100%", display:"grid", justifyItems:"end", marginBottom: "25px"}}>
      <TextField
        variant="outlined"
        id="filled-basic"
        label={strings.search}
        defaultValue=""
        InputProps={{
          endAdornment: (
            <InputAdornment position="end">
              <SearchIcon />
            </InputAdornment>
          ),
        }}
        className="searchInput"
        onChange={event => handleSearch(event)}
      />
      <TableContainer className="tableContainer">
        <Table stickyHeader aria-label="sticky table">
          <TableHead>
            <TableRow>
              <StyledTableCell>
                <TableSortLabel 
                  onClick={() => handleSorting(OrderNames.Nimi)}
                  direction={direction(OrderNames.Nimi)}
                >
                  Nimi
                </TableSortLabel>
              </StyledTableCell>
              <StyledTableCell>
                <TableSortLabel 
                  onClick={() => handleSorting(OrderNames.Namn)}
                  direction={direction(OrderNames.Namn)}
                >
                  Namn
                </TableSortLabel>
              </StyledTableCell>
              <StyledTableCell>
                <TableSortLabel 
                  onClick={() => handleSorting(OrderNames.Name)}
                  direction={direction(OrderNames.Name)}
                >
                  Name
                </TableSortLabel>
              </StyledTableCell>
              <StyledTableCell>
                <TableSortLabel 
                  onClick={() => handleSorting(OrderNames.Osoite)}
                  direction={direction(OrderNames.Osoite)}
                >
                  Osoite
                </TableSortLabel>
              </StyledTableCell>
              <StyledTableCell>
                <TableSortLabel 
                  onClick={() => handleSorting(OrderNames.Adress)}
                  direction={direction(OrderNames.Adress)}
                >
                  Adress
                </TableSortLabel>
              </StyledTableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {stationList.map((row) => (
              <TableRow
                key={row.id}
                onClick={() => navigate(`/stations/${row.id}`)}
              >
                <StyledTableCell>{row.nimi}</StyledTableCell>
                <StyledTableCell >{row.namn}</StyledTableCell>
                <StyledTableCell >{row.name}</StyledTableCell>
                <StyledTableCell >{row.osoite}</StyledTableCell>
                <StyledTableCell >{row.adress}</StyledTableCell>
              </TableRow>
            ))}
            
          </TableBody>
          <TableFooter>
            <TableRow>
              <TablePagination
                rowsPerPageOptions={[20, 50, 100]}
                count={stationsCount}
                rowsPerPage={wantedData.limit}
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


export default StationsList