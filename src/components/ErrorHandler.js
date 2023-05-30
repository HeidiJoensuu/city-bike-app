import { Alert, Snackbar } from "@mui/material"
import { useState } from "react"
import { useDispatch, useSelector } from "react-redux"
import { dismissStationError } from "../reducers/stationReducer"
import { dismissJourneyError } from "../reducers/journeyReducer"

/**
 * Renders error notifications
 * @returns Rendered error notification
 */
const ErrorHandler = () => {
  const dispatch = useDispatch()
  const { errorA } = useSelector(state => state.stations)
  const { errorJ } = useSelector(state => state.journeys)
  const [openSnackbar, setOpenSnackbar] = useState(false)

  if ((errorA?.target || errorJ?.target) && !openSnackbar) {
    setOpenSnackbar(true)
    console.log(errorA, errorJ)
  }
  

  /**
   * Closes the snackbar
   */
  const handleSnackbarClose = () => {
    dispatch(dismissStationError())
    dispatch(dismissJourneyError())
    setOpenSnackbar(false)
  }

  const succesMessage = () => {
    if (errorA?.target === "success") return errorA.message
    else return errorJ.message
  }

  const errorMessage = () => {
    if (errorA?.target !== null) return `${errorA?.target} ${errorA?.message.message}`
    else return `${errorJ?.target} ${errorJ?.message.message}`
  }

  return (
    <>
      <Snackbar open={openSnackbar} onClose={handleSnackbarClose}>
        {errorA?.target === "success" || errorJ?.target === "success" 
          ? <Alert onClose={handleSnackbarClose} severity="success">
            {succesMessage()}
          </Alert>
          : <Alert onClose={handleSnackbarClose} severity="warning">
            {errorMessage()}
          </Alert>
        }
      </Snackbar>
    </>
  )
}

export default ErrorHandler