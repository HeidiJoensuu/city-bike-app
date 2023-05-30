import { createSlice, createAsyncThunk } from "@reduxjs/toolkit"
import { getJourneys, getJourneysCounted, postJourney } from "../services/JourneysService"
import { strings } from "../utils/localization"

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
    errorJ: {
      target: null,
      message: {}
    },
    loadingJourneysJourneys: false
  },
  reducers: {
    dismissJourneyError: (state) => {
      state.errorJ.target = null
      state.errorJ.message = {}
    }
  },
  extraReducers: (builder) => {
    builder.addCase(getJourneysAsList.fulfilled, (state, action) => {
      if (state.loadingJourneys) state.loadingJourneys = false
      state.journeyList = action.payload
    }),
    builder.addCase(createJourney.fulfilled, (state, action) => {
      if (state.loadingJourneys) state.loadingJourneys = false
      state.errorJ.target = `success`
      state.errorJ.message = strings.success
    }),
    builder.addCase(getJourneysCount.fulfilled, (state, action) => {
      state.journeysCount = action.payload
    }),
    builder.addCase(getJourneysAsList.pending, (state, action) => {
      if (!state.loadingJourneys) state.loadingJourneys = true
    }),
    builder.addCase(getJourneysAsList.rejected, (state, action) => {
      if (state.loadingJourneys) state.loadingJourneys = false
      state.errorJ.target = `getJourneysAsList` 
      state.errorJ.message = action.error
    }),
    builder.addCase(createJourney.rejected, (state, action) => {
      if (state.loadingJourneys) state.loadingJourneys = false
      state.errorJ.target = `createJourney` 
      state.errorJ.message = action.error
    }),
    builder.addCase(getJourneysCount.rejected, (state, action) => {
      state.errorJ.target = `getJourneysCount` 
      state.errorJ.message = action.error
    })
  }
})

export const {dismissJourneyError} = journeysSlice.actions
export default journeysSlice.reducer