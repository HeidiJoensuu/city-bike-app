import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App'
import store from "./store"
import { Provider } from "react-redux"
import { PersistGate } from 'redux-persist/integration/react'
import { persistStore } from 'redux-persist'
import './index.css'
import { ThemeProvider } from '@mui/material'
import { theme } from './styles'

const root = ReactDOM.createRoot(document.getElementById('root'))
const persistor = persistStore(store)

root.render(
  <React.StrictMode>
    <Provider store={store}>
      <PersistGate loading={null} persistor={persistor}>
        <ThemeProvider theme={theme}>
          <App/>
        </ThemeProvider>
      </PersistGate>
    </Provider>
  </React.StrictMode>
)

