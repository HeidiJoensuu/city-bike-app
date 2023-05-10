import { createSlice, createAsyncThunk } from "@reduxjs/toolkit"
import { getJourneys, postJourney } from "../services/JourneysService"

export const getJourneysAsList = createAsyncThunk("journeys/getJourneysAsList", async (data) =>{
  const response = await getJourneys(data)
  return response
})

export const createJourney = createAsyncThunk("journeys/createJourney", async (data) => {
  const response = await postJourney(data)
  return response
})

export const journeysSlice = createSlice({
  name: "journeys",
  initialState: {
    journeyList: [],
    error: null,
    loading: false
  },
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(getJourneysAsList.fulfilled, (state, action) => {
      if (state.loading) state.loading = false
      state.journeyList = action.payload
    }),
    builder.addCase(createJourney.fulfilled, (state, action) => {
      if (state.loading) state.loading = false
      state.journeyList = [...state.journeyList, action.payload]
    }),
    builder.addCase(getJourneysAsList.pending, (state, action) => {
      if (!state.loading) state.loading = true
    })
    builder.addCase(getJourneysAsList.rejected, (state, action) => {
      if (state.loading) state.loading = false
      state.error = `getJourneysAsList: ${action.error.message}` 
    }),
    builder.addCase(createJourney.rejected, (state, action) => {
      if (state.loading) state.loading = false
      state.error = `createJourney: ${action.error.message}`
    })
  }
})