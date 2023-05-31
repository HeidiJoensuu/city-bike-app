import CircularProgress from '@mui/material/CircularProgress'

/**
 * Renders spinning progress element
 * @returns {JSX.Element} CisculatProgress
 */
const LoadingScreen = () => {
  return (
    <div className="processPaper">
      <CircularProgress size={100}/>
    </div>
  )
}

export default LoadingScreen