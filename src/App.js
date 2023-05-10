import ErrorHandler from "./components/ErrorHandler"

/**
 * Combines different components and pages into one render
 * @returns rendered
 */
const App = () => {

  return (
    <div className="App">
      <ErrorHandler />
    </div>
  );
}

export default App;
