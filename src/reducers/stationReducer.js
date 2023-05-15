import { createSlice, createAsyncThunk } from "@reduxjs/toolkit"
import { getStations, getStationInfo, postStation, getNumberOfStations } from "../services/StationsService";

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

export const stationSlice = createSlice({
  name: "stations",
  initialState: {
    stationsCount: 0,
    station: {},
    stationList: [],
    error: {
      target: null,
      message: {}
    },
    loadingStationsStations: false
  },
  reducers: {
    dismissError: (state) => {
      state.error.target = null
      state.error.message = {}
    }
  },
  extraReducers: (builder) => {
    builder.addCase(getStationsAsList.fulfilled, (state, action) => {
      if (state.loadingStations) state.loadingStations = false
      state.stationList = action.payload
    }),
    builder.addCase(getIndividualStationInfo.fulfilled, (state, action) => {
      if (state.loadingStations) state.loadingStations = false
      state.station = action.payload
    }),
    builder.addCase(createStation.fulfilled, (state, action) => {
      if (state.loadingStations) state.loadingStations = false
      state.stationList = [...state.stationList, action.payload]
    }),
    builder.addCase(getStationsCount.fulfilled, (state, action) => {
      state.stationsCount = action.payload
    }),
    builder.addCase(getStationsAsList.pending, (state, action) => {
      if (!state.loadingStations) state.loadingStations = true
    }),
    builder.addCase(getIndividualStationInfo.pending, (state, action) => {
      if (!state.loadingStations) state.loadingStations = true
    }),
    builder.addCase(getStationsAsList.rejected, (state, action) => {
      if (state.loadingStations) state.loadingStations = false
      state.error.target = `getStationsAsList` 
      state.error.message = action.error
    }),
    builder.addCase(getIndividualStationInfo.rejected, (state, action) => {
      if (state.loadingStations) state.loadingStations = false
      state.error.target = `getIndividualStationInfo`
      state.error.message = action.error
    }),
    builder.addCase(createStation.rejected, (state, action) => {
      if (state.loadingStations) state.loadingStations = false
      state.error.target = `createStation`
      state.error.message = action.error
    }),
    builder.addCase(getStationsCount.rejected, (state, action) => {
      state.error.target = `createStation`
      state.error.message = action.error
    })
  }
})

export const {dismissError} = stationSlice.actions
export default stationSlice.reducer