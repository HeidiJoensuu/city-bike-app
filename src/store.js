import { combineReducers, configureStore } from "@reduxjs/toolkit"
import storage from 'redux-persist/lib/storage'
import persistReducer from "redux-persist/es/persistReducer"
import stationReducer from "./reducers/stationReducer"
import journeyReducer from "./reducers/journeyReducer"

const reducers = combineReducers({
  stations: stationReducer,
  journeys: journeyReducer
})

const persistConfig = {
  key: 'root',
  storage
}

const persistedReducer = persistReducer(persistConfig, reducers)


export default configureStore({
  reducer: persistedReducer
})