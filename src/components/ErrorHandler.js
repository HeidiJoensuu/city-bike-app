import { Alert, Snackbar } from "@mui/material"
import { useState } from "react"
import { useDispatch, useSelector } from "react-redux"
import { dismissError } from "../reducers/stationReducer"

/**
 * Renders error notifications
 * @returns Rendered error notification
 */
const ErrorHandler = () => {
  const dispatch = useDispatch()
  const { error } = useSelector(state => state.stations)
  const [openSnackbar, setOpenSnackbar] = useState(false)

  if (error.target && !openSnackbar) {
    setOpenSnackbar(true)
    console.log(error);
  }

  /**
   * Closes the snackbar
   */
  const handleSnackbarClose = () => {
    dispatch(dismissError())
    setOpenSnackbar(false)
  }


  return (
    <>
      <Snackbar open={openSnackbar} onClose={() => handleSnackbarClose}>
        <Alert onClose={handleSnackbarClose} severity="warning">
          {error.target} {error.message.message}
        </Alert>
      </Snackbar>
    </>
  )
}

export default ErrorHandler