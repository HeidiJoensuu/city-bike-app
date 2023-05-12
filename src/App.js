import { BrowserRouter, Routes, Route } from "react-router-dom"
import ErrorHandler from "./components/ErrorHandler"
import Navbar from "./components/Navbar"
import JourneysList from "./components/pages/JourneysList"
import StationsList from "./components/pages/StationsList"
import StationDetail from "./components/pages/StationDetails"
import { useEffect, useState } from "react"
import { strings } from "./utils/localization"


/**
 * Combines different components and pages into one render
 * @returns {jsx.element}
 */
const App = () => {
  const [language, setLanguage] = useState("fi")

  useEffect(() => {
    if (location.pathname === "/")
      location.replace("/stations")
  })
  useEffect(() => {
    const savedLanguage = localStorage.getItem("language")
    if (savedLanguage) changeLanguageHandler(savedLanguage)
  }, [language])

  /**
   * Sets language to localization and localstorage
   * @param {String} lang  current language
   * @returns {void}
   */
  const changeLanguageHandler = (lang) => {
    console.log(lang);
    setLanguage(lang)
    strings.setLanguage(lang)
    localStorage.setItem("language", lang)
  }

  return (
    <BrowserRouter>
      <div className="App">
        <ErrorHandler />
        <Navbar 
          language={language}
          changeLanguageHandler={changeLanguageHandler}/>
        <Routes>
          <Route path="/journeys" element={<JourneysList />} />
          <Route path="/stations/:name" element={<StationDetail />} />
          <Route path="/stations" element={<StationsList />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
