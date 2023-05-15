import { BrowserRouter, Routes, Route } from "react-router-dom"
import ErrorHandler from "./components/ErrorHandler"
import Navbar from "./components/Navbar"
import JourneysList from "./components/pages/JourneysList"
import StationsList from "./components/pages/StationsList"
import StationDetail from "./components/pages/StationDetails"
import { useEffect, useMemo, useState } from "react"
import { strings } from "./utils/localization"
import { useDispatch, useSelector } from "react-redux"
import LoadingScreen from "./components/LoadingScreen"
import { ThemeProvider, createTheme } from "@mui/material"
import { theme } from "./styles"
import * as locales from '@mui/material/locale'


/**
 * Combines different components and pages into one render
 * @returns {jsx.element}
 */
const App = () => {
  const dispatch = useDispatch()
  const [language, setLanguage] = useState("fi")
  const {loadingStations} = useSelector(state => state.stations)
  const {loadingJourneys} = useSelector(state => state.journeys)

  useEffect(() => {
    if (location.pathname === "/")
      location.replace("/stations")
  })

  useEffect(() => {
    const savedLanguage = localStorage.getItem("language")
    if (savedLanguage) changeLanguageHandler(savedLanguage)
  }, [language])

  const languageAlter = () => {
    switch (language) {
    case "fi":
      return "fiFI"
    case "gb":
      return "enUS"
    default:
      return "svSE"
    }
  }

  const localedTheme =  useMemo(() => createTheme(theme,locales[languageAlter()]), [theme,languageAlter()])

  /**
   * Sets language to localization and localstorage
   * @param {String} lang  current language
   * @returns {void}
   */
  const changeLanguageHandler = (lang) => {
    setLanguage(lang)
    strings.setLanguage(lang)
    localStorage.setItem("language", lang)
  }

  return (
    <ThemeProvider theme={localedTheme}>
      <BrowserRouter>
        <div className="App">
          {loadingStations || loadingJourneys ? <LoadingScreen /> : <></>}
          <ErrorHandler />
          <Navbar 
            language={language}
            changeLanguageHandler={changeLanguageHandler}/>
          <Routes>
            <Route path="/journeys" element={<JourneysList />} />
            <Route path="/stations/:id" element={<StationDetail />} />
            <Route path="/stations" element={<StationsList />} />
          </Routes>
        </div>
      </BrowserRouter>
    </ThemeProvider>
  )
}

export default App
