import { Alert, Snackbar } from "@mui/material"
import { useState } from "react"
import { useSelector } from "react-redux"

/**
 * Renders error notifications
 * @returns Rendered error notification
 */
const ErrorHandler = () => {
  const { error } = useSelector(state => state.stations)
  const [openSnackbar, setOpenSnackbar] = useState(false)

  if (error && !openSnackbar) {
    setOpenSnackbar(true)
  }

  /**
   * Closes the snackbar
   */
  const handleSnackbarClose = () => {
    setOpenSnackbar(false)
  }

  return (
    <>
      <Snackbar open={openSnackbar} onClose={() => handleSnackbarClose}>
        <Alert onClose={handleSnackbarClose} severity="warning">
          {error}
        </Alert>
      </Snackbar>
    </>
  )
}

export default ErrorHandler