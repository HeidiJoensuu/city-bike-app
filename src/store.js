import { combineReducers, configureStore } from "@reduxjs/toolkit"
import storage from 'redux-persist/lib/storage'
import stationReducer from "./reducers/stationReducer"
import persistReducer from "redux-persist/es/persistReducer";

const reducers = combineReducers({
  stations: stationReducer
})
const persistConfig = {
  key: 'root',
  storage
}

const persistedReducer = persistReducer(persistConfig, reducers)

export default configureStore({
  reducer: persistedReducer
})