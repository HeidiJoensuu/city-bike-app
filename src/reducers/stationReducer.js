import { createSlice, createAsyncThunk } from "@reduxjs/toolkit"
import { getStations, getStationInfo, postStation } from "../services/StationsService";

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

export const stationSlice = createSlice({
  name: "stations",
  initialState: {
    station: {},
    stationList: [],
    error: null,
    loading: false
  },
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(getStationsAsList.fulfilled, (state, action) => {
      if (state.loading) state.loading = false
      state.stationList = action.payload
    }),
    builder.addCase(getIndividualStationInfo.fulfilled, (state, action) => {
      if (state.loading) state.loading = false
      state.station = action.payload
    }),
    builder.addCase(createStation.fulfilled, (state, action) => {
      if (state.loading) state.loading = false
      state.stationList = [...state.stationList, action.payload]
    }),
    builder.addCase(getStationsAsList.pending, (state, action) => {
      if (!state.loading) state.loading = true
    }),
    builder.addCase(getIndividualStationInfo.pending, (state, action) => {
      if (!state.loading) state.loading = true
    }),
    builder.addCase(getStationsAsList.rejected, (state, action) => {
      if (state.loading) state.loading = false
      state.error = `getStationsAsList: ${action.error.message}` 
    }),
    builder.addCase(getIndividualStationInfo.rejected, (state, action) => {
      if (state.loading) state.loading = false
      state.error = `getIndividualStationInfo: ${action.error.message}`
    }),
    builder.addCase(createStation.rejected, (state, action) => {
      if (state.loading) state.loading = false
      state.error = `createStation: ${action.error.message}`
    })
  }
})

export const {} = stationSlice.actions
export default stationSlice.reducer