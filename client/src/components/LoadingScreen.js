import CircularProgress from '@mui/material/CircularProgress'

const LoadingScreen = () => {
  return (
    <div className="processPaper">
      <CircularProgress size={100}/>
    </div>
  )
}

export default LoadingScreen