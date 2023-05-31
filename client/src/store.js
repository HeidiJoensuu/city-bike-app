import { combineReducers, configureStore } from "@reduxjs/toolkit"
import storageSession from 'redux-persist/lib/storage/session'
import persistReducer from "redux-persist/es/persistReducer"
import stationReducer from "./reducers/stationReducer"
import journeyReducer from "./reducers/journeyReducer"

const reducers = combineReducers({
  stations: stationReducer,
  journeys: journeyReducer
})

const persistConfig = {
  key: 'root',
  storage: storageSession
}

const persistedReducer = persistReducer(persistConfig, reducers)

export default configureStore({
  reducer: persistedReducer
})