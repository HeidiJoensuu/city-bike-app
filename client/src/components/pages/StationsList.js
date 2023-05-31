import { getStationsAsList, getStationsCount } from "../../reducers/stationReducer"
import { useEffect, useState } from "react"
import { useDispatch, useSelector } from "react-redux"
import TableComponent from "../TableComponent"

/**
 * This component contains data of stations as a list. 
 * Calls TableComponent to render the data.
 * @returns {JSX.element} Rendered list of stations page
 */
const StationsList = () => {
  const dispatch = useDispatch()
  const { stationList, stationsCount } = useSelector(state => state.stations)
  const [orderData, setOrderData] = useState({
    offset:0,
    limit:20,
    order: "nimi",
    search: "",
    desc: false,
  })

  const orderNames = {
    Nimi: "Nimi",
    Namn: "Namn",
    Name: "Name",
    Osoite: "Osoite",
    Adress: "Adress",
  }

  useEffect(() => {
    dispatch(getStationsAsList(orderData))
    dispatch(getStationsCount(orderData))
  }, [dispatch, orderData])

  return (
    <>
      <TableComponent
        orderData={orderData}
        setOrderData={setOrderData}
        dataCount={stationsCount}
        data={stationList}
        orderNames={orderNames}
      />
    </>
  )
}


export default StationsList