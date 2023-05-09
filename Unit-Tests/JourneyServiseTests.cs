using Api.Exceptions;
using Api.Models;
using Api.Models.Models;
using Api.Services;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Tests
{
    public class JourneyServiseTests
    {
        
        [Fact]
        public async Task Get_JourneysMay_Offset0_Limit10_OrderDeparture_SearchNone_Asc()
        {
            //Arrange
            var mockMay = TestDataHelper.GetFakeMayJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Mays).Returns(mockMay.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 10, "Departure", "", false, 5);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal("2.5.2021 8.32.00", answer.ElementAt(0).Departure.ToString());
            Assert.Equal("25.5.2021 10.15.00", answer.ElementAt(1).Departure.ToString());
            Assert.Equal("25.5.2021 10.38.00", answer.ElementAt(2).Departure.ToString());
            Assert.Equal("25.5.2021 12.43.00", answer.ElementAt(3).Departure.ToString());
            Assert.Equal("25.5.2021 15.27.00", answer.ElementAt(4).Departure.ToString());
        }

        [Fact]
        public async Task Get_JourneysJune_Offset0_Limit10_OrderDeparture_SearchNone_Asc()
        {
            //Arrange
            var mockJune = TestDataHelper.GetFakeJuneJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Junes).Returns(mockJune.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 10, "Departure", "", false, 6);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal("2.6.2021 7.15.00", answer.ElementAt(0).Departure.ToString());
            Assert.Equal("25.6.2021 9.10.00", answer.ElementAt(1).Departure.ToString());
            Assert.Equal("25.6.2021 14.25.30", answer.ElementAt(2).Departure.ToString());
            Assert.Equal("25.6.2021 15.40.00", answer.ElementAt(3).Departure.ToString());
            Assert.Equal("25.6.2021 17.45.00", answer.ElementAt(4).Departure.ToString());
        }

        [Fact]
        public async Task Get_JourneysJuly_Offset0_Limit6_OrderDeparture_SearchNone_Asc()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 6, "Departure", "", false, 7);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(6, answer.Count());
            Assert.Equal("2.7.2021 8.30.00", answer.ElementAt(0).Departure.ToString());
            Assert.Equal("3.7.2021 8.30.00", answer.ElementAt(1).Departure.ToString());
            Assert.Equal("12.7.2021 15.02.00", answer.ElementAt(2).Departure.ToString());
            Assert.Equal("24.7.2021 12.02.00", answer.ElementAt(3).Departure.ToString());
            Assert.Equal("24.7.2021 19.32.00", answer.ElementAt(4).Departure.ToString());
            Assert.Equal("25.7.2021 10.30.00", answer.ElementAt(5).Departure.ToString());
        }

        [Fact]
        public async Task Get_JourneysJuly_Offset0_Limit6_OrderDeparture_SearchNone_desc()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 6, "Departure", "", true, 7);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(6, answer.Count());
            Assert.Equal("31.7.2021 23.58.00", answer.ElementAt(0).Departure.ToString());
            Assert.Equal("25.7.2021 15.00.00", answer.ElementAt(1).Departure.ToString());
            Assert.Equal("25.7.2021 12.45.00", answer.ElementAt(2).Departure.ToString());
            Assert.Equal("25.7.2021 10.30.45", answer.ElementAt(3).Departure.ToString());
            Assert.Equal("25.7.2021 10.30.00", answer.ElementAt(4).Departure.ToString());
            Assert.Equal("24.7.2021 19.32.00", answer.ElementAt(5).Departure.ToString());
        }
        [Fact]
        public async Task Get_JourneysJuly_Offset0_Limit6_OrderReturn_SearchNone_Asc()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 6, "Return", "", false, 7);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(6, answer.Count());
            Assert.Equal("2.7.2021 8.35.00", answer.ElementAt(0).Return.ToString());
            Assert.Equal("3.7.2021 8.34.00", answer.ElementAt(1).Return.ToString());
            Assert.Equal("12.7.2021 15.20.00", answer.ElementAt(2).Return.ToString());
            Assert.Equal("24.7.2021 12.45.00", answer.ElementAt(3).Return.ToString());
            Assert.Equal("24.7.2021 19.00.00", answer.ElementAt(4).Return.ToString());
            Assert.Equal("25.7.2021 11.00.00", answer.ElementAt(5).Return.ToString());
        }

        [Fact]
        public async Task Get_JourneysJuly_Offset0_Limit6_OrderReturn_SearchNone_desc()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 6, "Return", "", true, 7);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(6, answer.Count());
            Assert.Equal("1.8.2021 0.30.00", answer.ElementAt(0).Return.ToString());
            Assert.Equal("25.7.2021 16.00.00", answer.ElementAt(1).Return.ToString());
            Assert.Equal("25.7.2021 13.00.00", answer.ElementAt(2).Return.ToString());
            Assert.Equal("25.7.2021 11.05.00", answer.ElementAt(3).Return.ToString());
            Assert.Equal("25.7.2021 11.00.00", answer.ElementAt(4).Return.ToString());
            Assert.Equal("24.7.2021 19.00.00", answer.ElementAt(5).Return.ToString());
        }

        [Fact]
        public async Task Get_JourneysJuly_Offset0_Limit5_OrderDepartureStationName_SearchNone_Asc()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 5, "Departure_station_name", "", false, 7);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal("Ensimmainen", answer.ElementAt(0).Departure_station_name);
            Assert.Equal("Ensimmainen", answer.ElementAt(1).Departure_station_name);
            Assert.Equal("Ensimmainen", answer.ElementAt(2).Departure_station_name);
            Assert.Equal("Kolmas", answer.ElementAt(3).Departure_station_name);
            Assert.Equal("Kolmas", answer.ElementAt(4).Departure_station_name);
        }

        [Fact]
        public async Task Get_JourneysJuly_Offset0_Limit5_OrderDepartureStationName_SearchNone_Desc()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 5, "Departure_station_name", "", true, 7);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal("Viides", answer.ElementAt(0).Departure_station_name);
            Assert.Equal("Viides", answer.ElementAt(1).Departure_station_name);
            Assert.Equal("Neljas", answer.ElementAt(2).Departure_station_name);
            Assert.Equal("Kuudes", answer.ElementAt(3).Departure_station_name);
            Assert.Equal("Kuudes", answer.ElementAt(4).Departure_station_name);
        }

        [Fact]
        public async Task Get_JourneysJuly_Offset0_Limit5_OrderReturnStationName_SearchNone_Asc()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 5, "Return_station_name", "", false, 7);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal("Ensimmainen", answer.ElementAt(0).Return_station_name);
            Assert.Equal("Ensimmainen", answer.ElementAt(1).Return_station_name);
            Assert.Equal("Ensimmainen", answer.ElementAt(2).Return_station_name);
            Assert.Equal("Ensimmainen", answer.ElementAt(3).Return_station_name);
            Assert.Equal("Kolmas", answer.ElementAt(4).Return_station_name);
        }

        [Fact]
        public async Task Get_JourneysJuly_Offset0_Limit5_OrderReturnStationName_SearchNone_Desc()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 5, "Return_station_name", "", true, 7);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal("Toinen", answer.ElementAt(0).Return_station_name);
            Assert.Equal("Toinen", answer.ElementAt(1).Return_station_name);
            Assert.Equal("Toinen", answer.ElementAt(2).Return_station_name);
            Assert.Equal("Toinen", answer.ElementAt(3).Return_station_name);
            Assert.Equal("Seitsemas", answer.ElementAt(4).Return_station_name);
        }

        [Fact]
        public async Task Get_JourneysJuly_Offset0_Limit5_OrderCoveredDistance_SearchNone_Asc()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 5, "Covered_distance", "", false, 7);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal(1350, answer.ElementAt(0).Covered_distance_m);
            Assert.Equal(1650, answer.ElementAt(1).Covered_distance_m);
            Assert.Equal(2780, answer.ElementAt(2).Covered_distance_m);
            Assert.Equal(3000, answer.ElementAt(3).Covered_distance_m);
            Assert.Equal(4230, answer.ElementAt(4).Covered_distance_m);
        }
        [Fact]
        public async Task Get_JourneysJuly_Offset0_Limit5_OrderCoveredDistance_SearchNone_Desc()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 5, "Covered_distance", "", true, 7);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal(16500, answer.ElementAt(0).Covered_distance_m);
            Assert.Equal(16000, answer.ElementAt(1).Covered_distance_m);
            Assert.Equal(6450, answer.ElementAt(2).Covered_distance_m);
            Assert.Equal(6300, answer.ElementAt(3).Covered_distance_m);
            Assert.Equal(6000, answer.ElementAt(4).Covered_distance_m);
        }


        [Fact]
        public async Task Get_JourneysJuly_Offset0_Limit5_OrderDuration_SearchNone_Asc()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 5, "Duration", "", false, 7);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal(240, answer.ElementAt(0).Duration_sec);
            Assert.Equal(300, answer.ElementAt(1).Duration_sec);
            Assert.Equal(900, answer.ElementAt(2).Duration_sec);
            Assert.Equal(1080, answer.ElementAt(3).Duration_sec);
            Assert.Equal(1580, answer.ElementAt(4).Duration_sec);
        }
        [Fact]
        public async Task Get_JourneysJuly_Offset0_Limit5_OrderDuration_SearchNone_Desc()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 5, "Duration", "", true, 7);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal(3600, answer.ElementAt(0).Duration_sec);
            Assert.Equal(2220, answer.ElementAt(1).Duration_sec);
            Assert.Equal(2055, answer.ElementAt(2).Duration_sec);
            Assert.Equal(1800, answer.ElementAt(3).Duration_sec);
            Assert.Equal(1680, answer.ElementAt(4).Duration_sec);
        }
        [Fact]
        public async Task Get_JourneysJuly_Offset0_Limit5_OrderDuration_SearchToi_Asc()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 5, "Duration", "Toi", false, 7);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(4, answer.Count());
            Assert.Equal(900, answer.ElementAt(0).Duration_sec);
            Assert.Equal(1080, answer.ElementAt(1).Duration_sec);
            Assert.Equal(1580, answer.ElementAt(2).Duration_sec);
            Assert.Equal(3600, answer.ElementAt(3).Duration_sec);
        }

        [Fact]
        public async Task Create_NewJourneyJuly_EverythingRight()
        {
            //Arrange
            var mockStation = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var mockMay = TestDataHelper.GetFakeMayJourneys().BuildMock().BuildMockDbSet();
            var mockJune = TestDataHelper.GetFakeJuneJourneys().BuildMock().BuildMockDbSet();
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();

            cityBikeContextMock.Setup(table => table.Stations).Returns(mockStation.Object);
            cityBikeContextMock.Setup(table => table.Mays).Returns(mockMay.Object);
            cityBikeContextMock.Setup(table => table.Junes).Returns(mockJune.Object);
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);
            Journey journey = new Journey()
            {
                Departure = new DateTime(2021, 7, 15, 14, 55, 00),
                Return = new DateTime(2021, 7, 15, 15, 13, 00),
                Departure_station_id = 3,
                Departure_station_name = "Kolmas",
                Return_station_id = 5,
                Return_station_name = "Viides",
                Covered_distance_m = 3780,
                Duration_sec = 1080
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.CreateJourney(journey);

            //Assert
            Assert.IsType<July>(answer);
            Assert.Equal(journey.Departure, answer.Departure);
            Assert.Equal(journey.Return, answer.Return);
            Assert.Equal(journey.Departure_station_id, answer.Departure_station_id);
            Assert.Equal(journey.Departure_station_name, answer.Departure_station_name);
            Assert.Equal(journey.Return_station_id, answer.Return_station_id);
            Assert.Equal(journey.Return_station_name, answer.Return_station_name);
            Assert.Equal(journey.Covered_distance_m, answer.Covered_distance_m);
            Assert.Equal(journey.Duration_sec, answer.Duration_sec);
        }
        [Fact]
        public async Task Create_NewJourneyJune_EverythingRight()
        {
            //Arrange
            var mockStation = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var mockMay = TestDataHelper.GetFakeMayJourneys().BuildMock().BuildMockDbSet();
            var mockJune = TestDataHelper.GetFakeJuneJourneys().BuildMock().BuildMockDbSet();
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();

            cityBikeContextMock.Setup(table => table.Stations).Returns(mockStation.Object);
            cityBikeContextMock.Setup(table => table.Mays).Returns(mockMay.Object);
            cityBikeContextMock.Setup(table => table.Junes).Returns(mockJune.Object);
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);
            Journey journey = new Journey()
            {
                Departure = new DateTime(2021, 6, 15, 14, 55, 00),
                Return = new DateTime(2021, 6, 15, 15, 13, 00),
                Departure_station_id = 3,
                Departure_station_name = "Kolmas",
                Return_station_id = 5,
                Return_station_name = "Viides",
                Covered_distance_m = 3780,
                Duration_sec = 1080
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.CreateJourney(journey);

            //Assert
            Assert.IsType<June>(answer);
            Assert.Equal(journey.Departure, answer.Departure);
            Assert.Equal(journey.Return, answer.Return);
            Assert.Equal(journey.Departure_station_id, answer.Departure_station_id);
            Assert.Equal(journey.Departure_station_name, answer.Departure_station_name);
            Assert.Equal(journey.Return_station_id, answer.Return_station_id);
            Assert.Equal(journey.Return_station_name, answer.Return_station_name);
            Assert.Equal(journey.Covered_distance_m, answer.Covered_distance_m);
            Assert.Equal(journey.Duration_sec, answer.Duration_sec);

        }
        
        [Fact]
        public async Task Create_NewJourneyMay_EverythingRight()
        {
            //Arrange
            var mockStation = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var mockMay = TestDataHelper.GetFakeMayJourneys().BuildMock().BuildMockDbSet();
            var mockJune = TestDataHelper.GetFakeJuneJourneys().BuildMock().BuildMockDbSet();
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();

            cityBikeContextMock.Setup(table => table.Stations).Returns(mockStation.Object);
            cityBikeContextMock.Setup(table => table.Mays).Returns(mockMay.Object);
            cityBikeContextMock.Setup(table => table.Junes).Returns(mockJune.Object);
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);
            Journey journey = new Journey()
            {
                Departure = new DateTime(2021, 5, 15, 14, 55, 00),
                Return = new DateTime(2021, 5, 15, 15, 13, 00),
                Departure_station_id = 3,
                Departure_station_name = "Kolmas",
                Return_station_id = 5,
                Return_station_name = "Viides",
                Covered_distance_m = 3780,
                Duration_sec = 1080
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.CreateJourney(journey);

            //Assert
            Assert.IsType<May>(answer);
            Assert.Equal(journey.Departure, answer.Departure);
            Assert.Equal(journey.Return, answer.Return);
            Assert.Equal(journey.Departure_station_id, answer.Departure_station_id);
            Assert.Equal(journey.Departure_station_name, answer.Departure_station_name);
            Assert.Equal(journey.Return_station_id, answer.Return_station_id);
            Assert.Equal(journey.Return_station_name, answer.Return_station_name);
            Assert.Equal(journey.Covered_distance_m, answer.Covered_distance_m);
            Assert.Equal(journey.Duration_sec, answer.Duration_sec);

        }
        [Fact]
        public async Task Create_NewJourneyOtherMonth_InvalidInput()
        {
            //Arrange
            var mockStation = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var mockMay = TestDataHelper.GetFakeMayJourneys().BuildMock().BuildMockDbSet();
            var mockJune = TestDataHelper.GetFakeJuneJourneys().BuildMock().BuildMockDbSet();
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();

            cityBikeContextMock.Setup(table => table.Stations).Returns(mockStation.Object);
            cityBikeContextMock.Setup(table => table.Mays).Returns(mockMay.Object);
            cityBikeContextMock.Setup(table => table.Junes).Returns(mockJune.Object);
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);
            Journey journey = new Journey()
            {
                Departure = new DateTime(2021, 9, 15, 14, 55, 00),
                Return = new DateTime(2021, 9, 15, 15, 13, 00),
                Departure_station_id = 3,
                Departure_station_name = "Kolmas",
                Return_station_id = 5,
                Return_station_name = "Viides",
                Covered_distance_m = 3780,
                Duration_sec = 1080
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            await Assert.ThrowsAsync<InvalidInputException>(async () => await journeyService.CreateJourney(journey));
        }

        [Fact]
        public async Task Create_NewJourney_Departure_InvalidYear()
        {
            //Arrange
            var mockStation = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var mockMay = TestDataHelper.GetFakeMayJourneys().BuildMock().BuildMockDbSet();
            var mockJune = TestDataHelper.GetFakeJuneJourneys().BuildMock().BuildMockDbSet();
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();

            cityBikeContextMock.Setup(table => table.Stations).Returns(mockStation.Object);
            cityBikeContextMock.Setup(table => table.Mays).Returns(mockMay.Object);
            cityBikeContextMock.Setup(table => table.Junes).Returns(mockJune.Object);
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);
            Journey journey = new Journey()
            {
                Departure = new DateTime(2022, 6, 15, 14, 55, 00),
                Return = new DateTime(2021, 6, 15, 15, 13, 00),
                Departure_station_id = 3,
                Departure_station_name = "Kolmas",
                Return_station_id = 5,
                Return_station_name = "Viides",
                Covered_distance_m = 3780,
                Duration_sec = 1080
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            await Assert.ThrowsAsync<InvalidInputException>(async () => await journeyService.CreateJourney(journey));
        }

        [Fact]
        public async Task Create_NewJourney_Return_InvalidYear()
        {
            //Arrange
            var mockStation = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var mockMay = TestDataHelper.GetFakeMayJourneys().BuildMock().BuildMockDbSet();
            var mockJune = TestDataHelper.GetFakeJuneJourneys().BuildMock().BuildMockDbSet();
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();

            cityBikeContextMock.Setup(table => table.Stations).Returns(mockStation.Object);
            cityBikeContextMock.Setup(table => table.Mays).Returns(mockMay.Object);
            cityBikeContextMock.Setup(table => table.Junes).Returns(mockJune.Object);
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);
            Journey journey = new Journey()
            {
                Departure = new DateTime(2021, 5, 15, 14, 55, 00),
                Return = new DateTime(2022, 5, 15, 15, 13, 00),
                Departure_station_id = 3,
                Departure_station_name = "Kolmas",
                Return_station_id = 5,
                Return_station_name = "Viides",
                Covered_distance_m = 3780,
                Duration_sec = 1080
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            await Assert.ThrowsAsync<InvalidInputException>(async () => await journeyService.CreateJourney(journey));
        }

        [Fact]
        public async Task Create_NewJourney_ReturnBeforeDeparture()
        {
            //Arrange
            var mockStation = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var mockMay = TestDataHelper.GetFakeMayJourneys().BuildMock().BuildMockDbSet();
            var mockJune = TestDataHelper.GetFakeJuneJourneys().BuildMock().BuildMockDbSet();
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();

            cityBikeContextMock.Setup(table => table.Stations).Returns(mockStation.Object);
            cityBikeContextMock.Setup(table => table.Mays).Returns(mockMay.Object);
            cityBikeContextMock.Setup(table => table.Junes).Returns(mockJune.Object);
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);
            Journey journey = new Journey()
            {
                Departure = new DateTime(2021, 6, 15, 14, 55, 00),
                Return = new DateTime(2022, 6, 15, 13, 13, 00),
                Departure_station_id = 3,
                Departure_station_name = "Kolmas",
                Return_station_id = 5,
                Return_station_name = "Viides",
                Covered_distance_m = 3780,
                Duration_sec = 1080
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            await Assert.ThrowsAsync<InvalidInputException>(async () => await journeyService.CreateJourney(journey));
        }

        [Fact]
        public async Task Create_NewJourney_ReturnAndDepartureSameTime()
        {
            //Arrange
            var mockStation = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var mockMay = TestDataHelper.GetFakeMayJourneys().BuildMock().BuildMockDbSet();
            var mockJune = TestDataHelper.GetFakeJuneJourneys().BuildMock().BuildMockDbSet();
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();

            cityBikeContextMock.Setup(table => table.Stations).Returns(mockStation.Object);
            cityBikeContextMock.Setup(table => table.Mays).Returns(mockMay.Object);
            cityBikeContextMock.Setup(table => table.Junes).Returns(mockJune.Object);
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);
            Journey journey = new Journey()
            {
                Departure = new DateTime(2021, 6, 15, 14, 55, 00),
                Return = new DateTime(2022, 6, 15, 14, 55, 00),
                Departure_station_id = 3,
                Departure_station_name = "Kolmas",
                Return_station_id = 5,
                Return_station_name = "Viides",
                Covered_distance_m = 3780,
                Duration_sec = 1080
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            await Assert.ThrowsAsync<InvalidInputException>(async () => await journeyService.CreateJourney(journey));
        }

        [Fact]
        public async Task Create_NewJourneyMonthChangesBetweenDepatureAndReturn_EverythingRight()
        {
            //Arrange
            var mockStation = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var mockMay = TestDataHelper.GetFakeMayJourneys().BuildMock().BuildMockDbSet();
            var mockJune = TestDataHelper.GetFakeJuneJourneys().BuildMock().BuildMockDbSet();
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();

            cityBikeContextMock.Setup(table => table.Stations).Returns(mockStation.Object);
            cityBikeContextMock.Setup(table => table.Mays).Returns(mockMay.Object);
            cityBikeContextMock.Setup(table => table.Junes).Returns(mockJune.Object);
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);
            Journey journey = new Journey()
            {
                Departure = new DateTime(2021, 5, 30, 23, 55, 00),
                Return = new DateTime(2021, 6, 1, 02, 13, 00),
                Departure_station_id = 3,
                Departure_station_name = "Kolmas",
                Return_station_id = 5,
                Return_station_name = "Viides",
                Covered_distance_m = 3780,
                Duration_sec = 1080
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.CreateJourney(journey);


            //Assert
            Assert.IsType<May>(answer);
            Assert.Equal(journey.Departure, answer.Departure);
            Assert.Equal(journey.Return, answer.Return);
            Assert.Equal(journey.Departure_station_id, answer.Departure_station_id);
            Assert.Equal(journey.Departure_station_name, answer.Departure_station_name);
            Assert.Equal(journey.Return_station_id, answer.Return_station_id);
            Assert.Equal(journey.Return_station_name, answer.Return_station_name);
            Assert.Equal(journey.Covered_distance_m, answer.Covered_distance_m);
            Assert.Equal(journey.Duration_sec, answer.Duration_sec);
        }


        [Fact]
        public async Task Create_NewJourney_DepartureStationId_DoesnotExist()
        {
            //Arrange
            var mockStation = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var mockMay = TestDataHelper.GetFakeMayJourneys().BuildMock().BuildMockDbSet();
            var mockJune = TestDataHelper.GetFakeJuneJourneys().BuildMock().BuildMockDbSet();
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();

            cityBikeContextMock.Setup(table => table.Stations).Returns(mockStation.Object);
            cityBikeContextMock.Setup(table => table.Mays).Returns(mockMay.Object);
            cityBikeContextMock.Setup(table => table.Junes).Returns(mockJune.Object);
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);
            Journey journey = new Journey()
            {
                Departure = new DateTime(2021, 9, 15, 14, 55, 00),
                Return = new DateTime(2021, 9, 15, 15, 13, 00),
                Departure_station_id = 12,
                Departure_station_name = "Kolmas",
                Return_station_id = 5,
                Return_station_name = "Viides",
                Covered_distance_m = 3780,
                Duration_sec = 1080
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);

            //Assert
            await Assert.ThrowsAsync<InvalidInputException>(async () => await journeyService.CreateJourney(journey));
        }
        [Fact]
        public async Task Create_NewJourney_DepartureStationName_DoesnotExist()
        {
            //Arrange
            var mockStation = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var mockMay = TestDataHelper.GetFakeMayJourneys().BuildMock().BuildMockDbSet();
            var mockJune = TestDataHelper.GetFakeJuneJourneys().BuildMock().BuildMockDbSet();
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();

            cityBikeContextMock.Setup(table => table.Stations).Returns(mockStation.Object);
            cityBikeContextMock.Setup(table => table.Mays).Returns(mockMay.Object);
            cityBikeContextMock.Setup(table => table.Junes).Returns(mockJune.Object);
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);
            Journey journey = new Journey()
            {
                Departure = new DateTime(2021, 9, 15, 14, 55, 00),
                Return = new DateTime(2021, 9, 15, 15, 13, 00),
                Departure_station_id = 3,
                Departure_station_name = "Kolmannes",
                Return_station_id = 5,
                Return_station_name = "Viides",
                Covered_distance_m = 3780,
                Duration_sec = 1080
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);

            //Assert
            await Assert.ThrowsAsync<InvalidInputException>(async () => await journeyService.CreateJourney(journey));
        }

        [Fact]
        public async Task Create_NewJourney_DepartureStationsInput_MixMatch()
        {
            //Arrange
            var mockStation = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var mockMay = TestDataHelper.GetFakeMayJourneys().BuildMock().BuildMockDbSet();
            var mockJune = TestDataHelper.GetFakeJuneJourneys().BuildMock().BuildMockDbSet();
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();

            cityBikeContextMock.Setup(table => table.Stations).Returns(mockStation.Object);
            cityBikeContextMock.Setup(table => table.Mays).Returns(mockMay.Object);
            cityBikeContextMock.Setup(table => table.Junes).Returns(mockJune.Object);
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);
            Journey journey = new Journey()
            {
                Departure = new DateTime(2021, 9, 15, 14, 55, 00),
                Return = new DateTime(2021, 9, 15, 15, 13, 00),
                Departure_station_id = 5,
                Departure_station_name = "Kolmas",
                Return_station_id = 5,
                Return_station_name = "Viides",
                Covered_distance_m = 3780,
                Duration_sec = 1080
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);

            //Assert
            await Assert.ThrowsAsync<InvalidInputException>(async () => await journeyService.CreateJourney(journey));
        }

        [Fact]
        public async Task Create_NewJourney_ReturnStationId_DoesnotExist()
        {
            //Arrange
            var mockStation = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var mockMay = TestDataHelper.GetFakeMayJourneys().BuildMock().BuildMockDbSet();
            var mockJune = TestDataHelper.GetFakeJuneJourneys().BuildMock().BuildMockDbSet();
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();

            cityBikeContextMock.Setup(table => table.Stations).Returns(mockStation.Object);
            cityBikeContextMock.Setup(table => table.Mays).Returns(mockMay.Object);
            cityBikeContextMock.Setup(table => table.Junes).Returns(mockJune.Object);
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);
            Journey journey = new Journey()
            {
                Departure = new DateTime(2021, 9, 15, 14, 55, 00),
                Return = new DateTime(2021, 9, 15, 15, 13, 00),
                Departure_station_id = 3,
                Departure_station_name = "Kolmas",
                Return_station_id = 12,
                Return_station_name = "Viides",
                Covered_distance_m = 3780,
                Duration_sec = 1080
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);

            //Assert
            await Assert.ThrowsAsync<InvalidInputException>(async () => await journeyService.CreateJourney(journey));
        }
        [Fact]
        public async Task Create_NewJourney_ReturnStationName_DoesnotExist()
        {
            //Arrange
            var mockStation = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var mockMay = TestDataHelper.GetFakeMayJourneys().BuildMock().BuildMockDbSet();
            var mockJune = TestDataHelper.GetFakeJuneJourneys().BuildMock().BuildMockDbSet();
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();

            cityBikeContextMock.Setup(table => table.Stations).Returns(mockStation.Object);
            cityBikeContextMock.Setup(table => table.Mays).Returns(mockMay.Object);
            cityBikeContextMock.Setup(table => table.Junes).Returns(mockJune.Object);
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);
            Journey journey = new Journey()
            {
                Departure = new DateTime(2021, 9, 15, 14, 55, 00),
                Return = new DateTime(2021, 9, 15, 15, 13, 00),
                Departure_station_id = 3,
                Departure_station_name = "Kolmas",
                Return_station_id = 5,
                Return_station_name = "Viidennes",
                Covered_distance_m = 3780,
                Duration_sec = 1080
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);

            //Assert
            await Assert.ThrowsAsync<InvalidInputException>(async () => await journeyService.CreateJourney(journey));
        }
        [Fact]
        public async Task Create_NewJourney_ReturnStationsInput_MixmMatch()
        {
            //Arrange
            var mockStation = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var mockMay = TestDataHelper.GetFakeMayJourneys().BuildMock().BuildMockDbSet();
            var mockJune = TestDataHelper.GetFakeJuneJourneys().BuildMock().BuildMockDbSet();
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();

            cityBikeContextMock.Setup(table => table.Stations).Returns(mockStation.Object);
            cityBikeContextMock.Setup(table => table.Mays).Returns(mockMay.Object);
            cityBikeContextMock.Setup(table => table.Junes).Returns(mockJune.Object);
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);
            Journey journey = new Journey()
            {
                Departure = new DateTime(2021, 9, 15, 14, 55, 00),
                Return = new DateTime(2021, 9, 15, 15, 13, 00),
                Departure_station_id = 3,
                Departure_station_name = "Kolmas",
                Return_station_id = 4,
                Return_station_name = "Viides",
                Covered_distance_m = 3780,
                Duration_sec = 1080
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);

            //Assert
            await Assert.ThrowsAsync<InvalidInputException>(async () => await journeyService.CreateJourney(journey));
        }

        [Fact]
        public async Task Create_NewJourney_CoveredDistance_TooSmallNumber()
        {
            //Arrange
            var mockStation = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var mockMay = TestDataHelper.GetFakeMayJourneys().BuildMock().BuildMockDbSet();
            var mockJune = TestDataHelper.GetFakeJuneJourneys().BuildMock().BuildMockDbSet();
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();

            cityBikeContextMock.Setup(table => table.Stations).Returns(mockStation.Object);
            cityBikeContextMock.Setup(table => table.Mays).Returns(mockMay.Object);
            cityBikeContextMock.Setup(table => table.Junes).Returns(mockJune.Object);
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);
            Journey journey = new Journey()
            {
                Departure = new DateTime(2021, 9, 15, 14, 55, 00),
                Return = new DateTime(2021, 9, 15, 15, 13, 00),
                Departure_station_id = 3,
                Departure_station_name = "Kolmas",
                Return_station_id = 5,
                Return_station_name = "Viides",
                Covered_distance_m = 5,
                Duration_sec = 1080
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);

            //Assert
            await Assert.ThrowsAsync<InvalidInputException>(async () => await journeyService.CreateJourney(journey));
        }
        [Fact]
        public async Task Create_NewJourney_Duration_TooSmallNumber()
        {
            //Arrange
            var mockStation = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var mockMay = TestDataHelper.GetFakeMayJourneys().BuildMock().BuildMockDbSet();
            var mockJune = TestDataHelper.GetFakeJuneJourneys().BuildMock().BuildMockDbSet();
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();

            cityBikeContextMock.Setup(table => table.Stations).Returns(mockStation.Object);
            cityBikeContextMock.Setup(table => table.Mays).Returns(mockMay.Object);
            cityBikeContextMock.Setup(table => table.Junes).Returns(mockJune.Object);
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);
            Journey journey = new Journey()
            {
                Departure = new DateTime(2021, 9, 15, 14, 55, 00),
                Return = new DateTime(2021, 9, 15, 15, 13, 00),
                Departure_station_id = 3,
                Departure_station_name = "Kolmas",
                Return_station_id = 5,
                Return_station_name = "Viides",
                Covered_distance_m = 3780,
                Duration_sec = 5
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);

            //Assert
            await Assert.ThrowsAsync<InvalidInputException>(async () => await journeyService.CreateJourney(journey));
        }
    }
}
