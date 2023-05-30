import { createSlice, createAsyncThunk } from "@reduxjs/toolkit"
import { getStations, getStationInfo, postStation, getNumberOfStations, getStationsNames } from "../services/StationsService"
import { strings } from "../utils/localization"

export const getStationsAsList = createAsyncThunk("stations/getStationsAsList", async (data) => {
  const response = await getStations(data)
  return response
})

export const getIndividualStationInfo = createAsyncThunk("stations/getIndividualStationInfo", async (data) => {
  const response = await getStationInfo(data)
  return response
})

export const createStation = createAsyncThunk("stations/createStation", async (data) => {
  const response = await postStation(data)
  return response
})

export const getStationsCount = createAsyncThunk("stations/getStationsCount", async (data) => {
  
  const response = await getNumberOfStations(data)
  return response
})
export const getStationsNamesAll = createAsyncThunk("stations/getStationsNamesAll", async () => {
  const response = await getStationsNames()
  return response
})

export const stationSlice = createSlice({
  name: "stations",
  initialState: {
    stationsCount: 0,
    station: {},
    stationList: [],
    stationNamesList: [],
    errorA: {
      target: null,
      message: {}
    },
    loadingStationsStations: false
  },
  reducers: {
    dismissStationError: (state) => {
      state.errorA.target = null
      state.errorA.message = {}
    }
  },
  extraReducers: (builder) => {
    builder.addCase(getStationsAsList.fulfilled, (state, action) => {
      if (state.loadingStations) state.loadingStations = false
      state.stationList = action.payload
    })
    builder.addCase(getIndividualStationInfo.fulfilled, (state, action) => {
      if (state.loadingStations) state.loadingStations = false
      state.station = action.payload
    })
    builder.addCase(createStation.fulfilled, (state, action) => {
      if (state.loadingStations) state.loadingStations = false
      state.stationNamesList = [...state.stationNamesList, action.payload]
      state.errorA.target = `success`
      state.errorA.message = strings.success
    })
    builder.addCase(getStationsCount.fulfilled, (state, action) => {
      state.stationsCount = action.payload
    })
    builder.addCase(getStationsNamesAll.fulfilled, (state, action) => {
      state.stationNamesList = action.payload
    })
    builder.addCase(getStationsAsList.pending, (state, action) => {
      if (!state.loadingStations) state.loadingStations = true
    })
    builder.addCase(getIndividualStationInfo.pending, (state, action) => {
      if (!state.loadingStations) state.loadingStations = true
    })
    builder.addCase(getStationsAsList.rejected, (state, action) => {
      if (state.loadingStations) state.loadingStations = false
      state.errorA.target = `getStationsAsList` 
      state.errorA.message = action.error
    })
    builder.addCase(getIndividualStationInfo.rejected, (state, action) => {
      if (state.loadingStations) state.loadingStations = false
      state.errorA.target = `getIndividualStationInfo`
      state.errorA.message = action.error
    })
    builder.addCase(createStation.rejected, (state, action) => {
      if (state.loadingStations) state.loadingStations = false
      state.errorA.target = `createStation`
      state.errorA.message = action.error
    })
    builder.addCase(getStationsCount.rejected, (state, action) => {
      state.errorA.target = `createStation`
      state.errorA.message = action.error
    })
    builder.addCase(getStationsNamesAll.rejected, (state, action) => {
      state.errorA.target = `getStationsNamesAll`
      state.errorA.message = action.error
    })
  }
})

export const {dismissStationError} = stationSlice.actions
export default stationSlice.reducer