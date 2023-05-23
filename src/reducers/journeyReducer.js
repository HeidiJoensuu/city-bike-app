import { createSlice, createAsyncThunk } from "@reduxjs/toolkit"
import { getJourneys, getJourneysCounted, postJourney } from "../services/JourneysService"

export const getJourneysAsList = createAsyncThunk("journeys/getJourneysAsList", async (data) =>{
  const response = await getJourneys(data)
  return response
})

export const createJourney = createAsyncThunk("journeys/createJourney", async (data) => {
  const response = await postJourney(data)
  return response
})

export const getJourneysCount = createAsyncThunk("journeys/getJourneysCount", async (data) =>{
  const response = await getJourneysCounted(data)
  return response
})

export const journeysSlice = createSlice({
  name: "journeys",
  initialState: {
    journeysCount: 0,
    journeyList: [],
    error: null,
    loadingJourneysJourneys: false
  },
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(getJourneysAsList.fulfilled, (state, action) => {
      if (state.loadingJourneys) state.loadingJourneys = false
      state.journeyList = action.payload
    }),
    builder.addCase(createJourney.fulfilled, (state, action) => {
      if (state.loadingJourneys) state.loadingJourneys = false
      state.journeyList = [...state.journeyList, action.payload]
    }),
    builder.addCase(getJourneysCount.fulfilled, (state, action) => {
      state.journeysCount = action.payload
    }),
    builder.addCase(getJourneysAsList.pending, (state, action) => {
      if (!state.loadingJourneys) state.loadingJourneys = true
    }),
    builder.addCase(getJourneysAsList.rejected, (state, action) => {
      if (state.loadingJourneys) state.loadingJourneys = false
      state.error = `getJourneysAsList: ${action.error.message}` 
    }),
    builder.addCase(createJourney.rejected, (state, action) => {
      if (state.loadingJourneys) state.loadingJourneys = false
      state.error = `createJourney: ${action.error.message}`
    }),
    builder.addCase(getJourneysCount.rejected, (state, action) => {
      state.error = `getJourneysCount: ${action.error.message}`
    })
  }
})

export const {} = journeysSlice.actions
export default journeysSlice.reducer