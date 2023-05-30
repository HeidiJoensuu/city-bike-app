import { useEffect, useState } from "react"
import { useDispatch, useSelector } from "react-redux"
import { getJourneysAsList, getJourneysCount } from "../../reducers/journeyReducer"
import TableComponent from "../TableComponent"

const JourneysList = () => {
  const dispatch = useDispatch()
  const {journeysCount, journeyList} = useSelector(state => state.journeys)
  const [orderData, setOrderData] = useState(JSON.parse(sessionStorage.getItem("order")) || {
    offset:0,
    limit:20,
    order: "Departure",
    search: "",
    desc: false,
    month: 5
  })
  const [filterData, setFilterData] = useState(JSON.parse(sessionStorage.getItem("filter")) || {
    departure: "",
    returntime: "",
    distanceMin: "",
    distanceMax: "",
    durationMin: "",
    durationMax: ""
  })

  const orderNames = {
    Departure: "Departure",
    returntime: "Returntime",
    Departure_station_name: "Departure_station_name",
    Return_station_name: "Return_station_name",
    Covered_distance: "Covered_distance",
    Duration: "Duration"
  }
  if (!sessionStorage.getItem("filter")) sessionStorage.setItem("filter", JSON.stringify(filterData))
  if (!sessionStorage.getItem("order")) sessionStorage.setItem("order", JSON.stringify(orderData))

  useEffect(() => {
    if (filterHasChanges() || orderHasChanges()) {
      sessionStorage.setItem("filter", JSON.stringify(filterData))
      sessionStorage.setItem("order", JSON.stringify(orderData))
      const getData =  Object.assign(orderData, filterData)
      dispatch(getJourneysAsList(getData))
      dispatch(getJourneysCount(getData))
    }
  }, [orderData, filterData])

  const filterHasChanges = () => {
    let isThereChange = false
    for (const element in filterData) {
      if (filterData[element] !== JSON.parse(sessionStorage.getItem("filter"))[element]) isThereChange =  true
      if (filterData[element] !== "") isThereChange =  true
    }
    return isThereChange
  }

  const orderHasChanges = () => {
    let isThereChange = false
    for (const element in orderData) {
      if (orderData[element] !== JSON.parse(sessionStorage.getItem("order"))[element]) isThereChange =  true
      if (orderData[element] !== "") isThereChange =  true
    }
    return isThereChange
  }


  return (
    <>
      <TableComponent
        orderData={orderData}
        setOrderData={setOrderData}
        dataCount={journeysCount}
        data={journeyList}
        orderNames={orderNames}
        filterData={filterData}
        setFilterData={setFilterData}
      />
    </>
  )
}

export default JourneysList