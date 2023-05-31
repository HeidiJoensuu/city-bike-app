# City bike app

This project is based on solita's [pre-assigment exercise](https://github.com/solita/dev-academy-2023-exercise) 
The goal was to made website for displaying data of HSL's citybike journeys based on May-July 2021.

This project contains all following requirements:
- Validated data at database
- List of journeys
  - pagination
  - ordering per column
  - searching
  - Fltering
- List of stations 
  - pagination
  - searching
- Single station view
  - station name
  - station address
  - total number of journeys starting from the station
  - total number of journeys ending at the station
  - station location on the map
  - the average distance of a journey starting from the station
  - the average distance of a journey ending at the station
  - top 5 most popular return stations for journeys starting from the station
  - top 5 most popular departure stations for journeys ending at the station
  - ability to filter all the calculations per month
- Admin settings
  - availability to create new station
  - availability to create new journey
- Extra
  - localization for finnish, swedish and english
  - whole project is able to run in docker compose
  - self made "logo" for website

## Technologies used (Main ones)

* Python (for altering csv-files)
* Microsoft SQL Server Management Studio (development)
* Docker
* Postgesql (docker provides this)

Back end
* .NET 7
* ASP.NET Core Web Api
* C#
* xUnit-test
* Entity Framework

Front end
* npm
* React.js
* leaflet.js
* Redux
* Axios
* MUI

## Setting up the project

Clone this repository.

Load these files into databaseFiles-folder

<https://dev.hsl.fi/citybikes/od-trips-2021/2021-05.csv>

<https://dev.hsl.fi/citybikes/od-trips-2021/2021-06.csv>

<https://dev.hsl.fi/citybikes/od-trips-2021/2021-07.csv>

<https://opendata.arcgis.com/datasets/726277c507ef4914b0aec3cbcfcbfafc_0.csv>



License and information: <https://www.avoindata.fi/data/en/dataset/hsl-n-kaupunkipyoraasemat/resource/a23eef3a-cc40-4608-8aa2-c730d17e8902>

Then run alterCSVFiles.py -file. This file changes some headers and values in 2021-0X.csv files. For example, there is file header 'return' that does not go along with postgresql.

Then open your terminal and go to the root of this repository on run ```docker compose up --build```. This might take some time, but once it's ready you can should find website running in *http://localhost:3000*

## Front end structure
```
client
│   .eslintrc.js
│   .gitignore
│   LICENSE
│   package-lock.json
│   package.json
│
├───public
│
└───src
    │   App.js
    │   index.css
    │   index.js
    │   store.js                            # Redux storage
    │   styles.js                           # Fos customising MUI components
    │
    ├───assets                              # Images for website
    │
    ├───components
    │   │   FilterCard.js                   # Rendres filtering options
    │   │   LoadingScreen.js                # Renders loading sircle
    │   │   Navbar.js                       # Simple navigation bar
    │   │   NotificationHandler.js          # Renders messages from backend
    │   │   TableComponent.js               # Renders table component that JourneyList and StationList uses
    │   │
    │   └───pages
    │           JourneysList.js             # Renders Journey list page
    │           Settings.js                 # Contains admin settings aka. creating new journeys and stations
    │           StationDetails.js           # Renders information of individual station
    │           StationsList.js             # Renders Station list page
    │
    ├───reducers                            # Redux reducers
    │       journeyReducer.js
    │       stationReducer.js
    │
    ├───services                            # Services to fetch data from back end
    │       JourneysService.js
    │       StationsService.js
    │
    └───utils
            config.js                       # config
            localization.js                 # Localization library
            validation.js                   # Validation funcktions
``` 

## Back end structure
```
Api
│   .dockerignore
│   .gitignore
│   Api.csproj
│   Api.csproj.user
│   Api.sln
│   appsettings.Development.json
│   appsettings.json
│   Dockerfile
│   Program.cs
│
├───bin
│
├───Controllers                             # Calls services and mappers answer into right format
│       JourneysController.cs
│       StationController.cs
│
├───Exceptions                              # Custom made exeptions
│       DuplicateException.cs
│       InvalidInputException.cs
│       MissingInputsException.cs
│
├───Models                                  # Contains database tables and Dtos
│   │   CityBikesDBContext.cs
│   │
│   ├───DTOs                                # DTO´s of models
│   │       JourneyInfoDto.cs
│   │       ModifiedJourneyDto.cs
│   │       NewStationDto.cs
│   │       StationDto.cs
│   │       StationInfoDTO.cs
│   │       StationShortDto.cs
│   │
│   └───Models                              # Database tables
│           Journey.cs
│           JourneyAbstract.cs
│           July.cs
│           June.cs
│           May.cs
│           Station.cs
│
├───obj
│
├───Profiles                                # Profiles for Automapper
│       JourneysProfile.cs
│       StationProfile.cs
│
├───Properties
│
└───Services                                # Queries for database tables
        IJourneyService.cs
        IStationService.cs
        JourneyService.cs
        StationService.cs
``` 

## Other words / notes

In navbar there is default button *"Admin settings"*. normally I would hide it behind user type validation, but because this project did not include users I left it for everyone to see.

At first I started making raw SQL queries. When I started to do unit tests I noticed that Entity Framework with raw SQL was not compatible with xUnit tests. Therefore in commit [rawSQL replaced with LINQ](https://github.com/HeidiJoensuu/city-bike-app/commit/cb4e2a65de16281a4cfb9fb58ee05f16476b1831#diff-32fa6806557dad4e15090697adfe83b526fda31b880bfbf17a091df9c235a206) I had to change all sql queries to query expression.

I noticed that all data in 2021-0X.csv files was dublicated, so I took the liberty to remove all dublicants.
