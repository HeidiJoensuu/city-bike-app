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
            var answer = await journeyService.GetJourneys(0, 10, "Departure", "", false, 5, "2021-01-01T00:00:00", "2021-12-31T23:59:59", 0, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal("2.5.2021 8.32.00", answer.ElementAt(0).departure.ToString());
            Assert.Equal("25.5.2021 10.15.00", answer.ElementAt(1).departure.ToString());
            Assert.Equal("25.5.2021 10.38.00", answer.ElementAt(2).departure.ToString());
            Assert.Equal("25.5.2021 12.43.00", answer.ElementAt(3).departure.ToString());
            Assert.Equal("25.5.2021 15.27.00", answer.ElementAt(4).departure.ToString());
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
            var answer = await journeyService.GetJourneys(0, 10, "Departure", "", false, 6, "2021-01-01T00:00:00", "2021-12-31T23:59:59", 0, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal("2.6.2021 7.15.00", answer.ElementAt(0).departure.ToString());
            Assert.Equal("25.6.2021 9.10.00", answer.ElementAt(1).departure.ToString());
            Assert.Equal("25.6.2021 14.25.30", answer.ElementAt(2).departure.ToString());
            Assert.Equal("25.6.2021 15.40.00", answer.ElementAt(3).departure.ToString());
            Assert.Equal("25.6.2021 17.45.00", answer.ElementAt(4).departure.ToString());
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
            var answer = await journeyService.GetJourneys(0, 6, "Departure", "", false, 7, "2021-01-01T00:00:00", "2021-12-31T23:59:59", 0, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(6, answer.Count());
            Assert.Equal("2.7.2021 8.30.00", answer.ElementAt(0).departure.ToString());
            Assert.Equal("3.7.2021 8.30.00", answer.ElementAt(1).departure.ToString());
            Assert.Equal("12.7.2021 15.02.00", answer.ElementAt(2).departure.ToString());
            Assert.Equal("24.7.2021 12.02.00", answer.ElementAt(3).departure.ToString());
            Assert.Equal("24.7.2021 19.32.00", answer.ElementAt(4).departure.ToString());
            Assert.Equal("25.7.2021 10.30.00", answer.ElementAt(5).departure.ToString());
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
            var answer = await journeyService.GetJourneys(0, 6, "Departure", "", true, 7, "2021-01-01T00:00:00", "2021-12-31T23:59:59", 0, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(6, answer.Count());
            Assert.Equal("31.7.2021 23.58.00", answer.ElementAt(0).departure.ToString());
            Assert.Equal("25.7.2021 15.00.00", answer.ElementAt(1).departure.ToString());
            Assert.Equal("25.7.2021 12.45.00", answer.ElementAt(2).departure.ToString());
            Assert.Equal("25.7.2021 10.30.45", answer.ElementAt(3).departure.ToString());
            Assert.Equal("25.7.2021 10.30.00", answer.ElementAt(4).departure.ToString());
            Assert.Equal("24.7.2021 19.32.00", answer.ElementAt(5).departure.ToString());
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
            var answer = await journeyService.GetJourneys(0, 6, "Return", "", false, 7, "2021-01-01T00:00:00", "2021-12-31T23:59:59", 0, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(6, answer.Count());
            Assert.Equal("2.7.2021 8.35.00", answer.ElementAt(0).returntime.ToString());
            Assert.Equal("3.7.2021 8.34.00", answer.ElementAt(1).returntime.ToString());
            Assert.Equal("12.7.2021 15.20.00", answer.ElementAt(2).returntime.ToString());
            Assert.Equal("24.7.2021 12.45.00", answer.ElementAt(3).returntime.ToString());
            Assert.Equal("24.7.2021 19.00.00", answer.ElementAt(4).returntime.ToString());
            Assert.Equal("25.7.2021 11.00.00", answer.ElementAt(5).returntime.ToString());
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
            var answer = await journeyService.GetJourneys(0, 6, "Return", "", true, 7, "2021-01-01T00:00:00", "2021-12-31T23:59:59", 0, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(6, answer.Count());
            Assert.Equal("1.8.2021 0.30.00", answer.ElementAt(0).returntime.ToString());
            Assert.Equal("25.7.2021 16.00.00", answer.ElementAt(1).returntime.ToString());
            Assert.Equal("25.7.2021 13.00.00", answer.ElementAt(2).returntime.ToString());
            Assert.Equal("25.7.2021 11.05.00", answer.ElementAt(3).returntime.ToString());
            Assert.Equal("25.7.2021 11.00.00", answer.ElementAt(4).returntime.ToString());
            Assert.Equal("24.7.2021 19.00.00", answer.ElementAt(5).returntime.ToString());
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
            var answer = await journeyService.GetJourneys(0, 5, "Departure_station_name", "", false, 7, "2021-01-01T00:00:00", "2021-12-31T23:59:59", 0, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal("Ensimmainen", answer.ElementAt(0).departure_station_name);
            Assert.Equal("Ensimmainen", answer.ElementAt(1).departure_station_name);
            Assert.Equal("Ensimmainen", answer.ElementAt(2).departure_station_name);
            Assert.Equal("Kolmas", answer.ElementAt(3).departure_station_name);
            Assert.Equal("Kolmas", answer.ElementAt(4).departure_station_name);
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
            var answer = await journeyService.GetJourneys(0, 5, "Departure_station_name", "", true, 7, "2021-01-01T00:00:00", "2021-12-31T23:59:59", 0, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal("Viides", answer.ElementAt(0).departure_station_name);
            Assert.Equal("Viides", answer.ElementAt(1).departure_station_name);
            Assert.Equal("Neljas", answer.ElementAt(2).departure_station_name);
            Assert.Equal("Kuudes", answer.ElementAt(3).departure_station_name);
            Assert.Equal("Kuudes", answer.ElementAt(4).departure_station_name);
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
            var answer = await journeyService.GetJourneys(0, 5, "Return_station_name", "", false, 7, "2021-01-01T00:00:00", "2021-12-31T23:59:59", 0, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal("Ensimmainen", answer.ElementAt(0).return_station_name);
            Assert.Equal("Ensimmainen", answer.ElementAt(1).return_station_name);
            Assert.Equal("Ensimmainen", answer.ElementAt(2).return_station_name);
            Assert.Equal("Ensimmainen", answer.ElementAt(3).return_station_name);
            Assert.Equal("Kolmas", answer.ElementAt(4).return_station_name);
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
            var answer = await journeyService.GetJourneys(0, 5, "Return_station_name", "", true, 7, "2021-01-01T00:00:00", "2021-12-31T23:59:59", 0, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal("Toinen", answer.ElementAt(0).return_station_name);
            Assert.Equal("Toinen", answer.ElementAt(1).return_station_name);
            Assert.Equal("Toinen", answer.ElementAt(2).return_station_name);
            Assert.Equal("Toinen", answer.ElementAt(3).return_station_name);
            Assert.Equal("Seitsemas", answer.ElementAt(4).return_station_name);
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
            var answer = await journeyService.GetJourneys(0, 5, "Covered_distance", "", false, 7, "2021-01-01T00:00:00", "2021-12-31T23:59:59", 0, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal(1350, answer.ElementAt(0).covered_distance_m);
            Assert.Equal(1650, answer.ElementAt(1).covered_distance_m);
            Assert.Equal(2780, answer.ElementAt(2).covered_distance_m);
            Assert.Equal(3000, answer.ElementAt(3).covered_distance_m);
            Assert.Equal(4230, answer.ElementAt(4).covered_distance_m);
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
            var answer = await journeyService.GetJourneys(0, 5, "Covered_distance", "", true, 7, "2021-01-01T00:00:00", "2021-12-31T23:59:59", 0, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal(16500, answer.ElementAt(0).covered_distance_m);
            Assert.Equal(16000, answer.ElementAt(1).covered_distance_m);
            Assert.Equal(6450, answer.ElementAt(2).covered_distance_m);
            Assert.Equal(6300, answer.ElementAt(3).covered_distance_m);
            Assert.Equal(6000, answer.ElementAt(4).covered_distance_m);
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
            var answer = await journeyService.GetJourneys(0, 5, "Duration", "", false, 7, "2021-01-01T00:00:00", "2021-12-31T23:59:59", 0, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal(240, answer.ElementAt(0).duration_sec);
            Assert.Equal(300, answer.ElementAt(1).duration_sec);
            Assert.Equal(900, answer.ElementAt(2).duration_sec);
            Assert.Equal(1080, answer.ElementAt(3).duration_sec);
            Assert.Equal(1580, answer.ElementAt(4).duration_sec);
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
            var answer = await journeyService.GetJourneys(0, 5, "Duration", "", true, 7, "2021-01-01T00:00:00", "2021-12-31T23:59:59", 0, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal(3600, answer.ElementAt(0).duration_sec);
            Assert.Equal(2220, answer.ElementAt(1).duration_sec);
            Assert.Equal(2055, answer.ElementAt(2).duration_sec);
            Assert.Equal(1800, answer.ElementAt(3).duration_sec);
            Assert.Equal(1680, answer.ElementAt(4).duration_sec);
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
            var answer = await journeyService.GetJourneys(0, 5, "Duration", "Toi", false, 7, "2021-01-01T00:00:00", "2021-12-31T23:59:59", 0, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(4, answer.Count());
            Assert.Equal(900, answer.ElementAt(0).duration_sec);
            Assert.Equal(1080, answer.ElementAt(1).duration_sec);
            Assert.Equal(1580, answer.ElementAt(2).duration_sec);
            Assert.Equal(3600, answer.ElementAt(3).duration_sec);
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
                departure = new DateTime(2021, 7, 15, 14, 55, 00),
                returntime = new DateTime(2021, 7, 15, 15, 13, 00),
                departure_station_id = 3,
                departure_station_name = "Kolmas",
                return_station_id = 5,
                return_station_name = "Viides",
                covered_distance_m = 3780,
                duration_sec = 1080
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.CreateJourney(journey);

            //Assert
            Assert.IsType<July>(answer);
            Assert.Equal(journey.departure, answer.departure);
            Assert.Equal(journey.returntime, answer.returntime);
            Assert.Equal(journey.departure_station_id, answer.departure_station_id);
            Assert.Equal(journey.departure_station_name, answer.departure_station_name);
            Assert.Equal(journey.return_station_id, answer.return_station_id);
            Assert.Equal(journey.return_station_name, answer.return_station_name);
            Assert.Equal(journey.covered_distance_m, answer.covered_distance_m);
            Assert.Equal(journey.duration_sec, answer.duration_sec);
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
                departure = new DateTime(2021, 6, 15, 14, 55, 00),
                returntime = new DateTime(2021, 6, 15, 15, 13, 00),
                departure_station_id = 3,
                departure_station_name = "Kolmas",
                return_station_id = 5,
                return_station_name = "Viides",
                covered_distance_m = 3780,
                duration_sec = 1080
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.CreateJourney(journey);

            //Assert
            Assert.IsType<June>(answer);
            Assert.Equal(journey.departure, answer.departure);
            Assert.Equal(journey.returntime, answer.returntime);
            Assert.Equal(journey.departure_station_id, answer.departure_station_id);
            Assert.Equal(journey.departure_station_name, answer.departure_station_name);
            Assert.Equal(journey.return_station_id, answer.return_station_id);
            Assert.Equal(journey.return_station_name, answer.return_station_name);
            Assert.Equal(journey.covered_distance_m, answer.covered_distance_m);
            Assert.Equal(journey.duration_sec, answer.duration_sec);

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
                departure = new DateTime(2021, 5, 15, 14, 55, 00),
                returntime = new DateTime(2021, 5, 15, 15, 13, 00),
                departure_station_id = 3,
                departure_station_name = "Kolmas",
                return_station_id = 5,
                return_station_name = "Viides",
                covered_distance_m = 3780,
                duration_sec = 1080
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.CreateJourney(journey);

            //Assert
            Assert.IsType<May>(answer);
            Assert.Equal(journey.departure, answer.departure);
            Assert.Equal(journey.returntime, answer.returntime);
            Assert.Equal(journey.departure_station_id, answer.departure_station_id);
            Assert.Equal(journey.departure_station_name, answer.departure_station_name);
            Assert.Equal(journey.return_station_id, answer.return_station_id);
            Assert.Equal(journey.return_station_name, answer.return_station_name);
            Assert.Equal(journey.covered_distance_m, answer.covered_distance_m);
            Assert.Equal(journey.duration_sec, answer.duration_sec);

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
                departure = new DateTime(2021, 9, 15, 14, 55, 00),
                returntime = new DateTime(2021, 9, 15, 15, 13, 00),
                departure_station_id = 3,
                departure_station_name = "Kolmas",
                return_station_id = 5,
                return_station_name = "Viides",
                covered_distance_m = 3780,
                duration_sec = 1080
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
                departure = new DateTime(2022, 6, 15, 14, 55, 00),
                returntime = new DateTime(2021, 6, 15, 15, 13, 00),
                departure_station_id = 3,
                departure_station_name = "Kolmas",
                return_station_id = 5,
                return_station_name = "Viides",
                covered_distance_m = 3780,
                duration_sec = 1080
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
                departure = new DateTime(2021, 5, 15, 14, 55, 00),
                returntime = new DateTime(2022, 5, 15, 15, 13, 00),
                departure_station_id = 3,
                departure_station_name = "Kolmas",
                return_station_id = 5,
                return_station_name = "Viides",
                covered_distance_m = 3780,
                duration_sec = 1080
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
                departure = new DateTime(2021, 6, 15, 14, 55, 00),
                returntime = new DateTime(2022, 6, 15, 13, 13, 00),
                departure_station_id = 3,
                departure_station_name = "Kolmas",
                return_station_id = 5,
                return_station_name = "Viides",
                covered_distance_m = 3780,
                duration_sec = 1080
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
                departure = new DateTime(2021, 6, 15, 14, 55, 00),
                returntime = new DateTime(2022, 6, 15, 14, 55, 00),
                departure_station_id = 3,
                departure_station_name = "Kolmas",
                return_station_id = 5,
                return_station_name = "Viides",
                covered_distance_m = 3780,
                duration_sec = 1080
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
                departure = new DateTime(2021, 5, 30, 23, 55, 00),
                returntime = new DateTime(2021, 6, 1, 02, 13, 00),
                departure_station_id = 3,
                departure_station_name = "Kolmas",
                return_station_id = 5,
                return_station_name = "Viides",
                covered_distance_m = 3780,
                duration_sec = 1080
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.CreateJourney(journey);


            //Assert
            Assert.IsType<May>(answer);
            Assert.Equal(journey.departure, answer.departure);
            Assert.Equal(journey.returntime, answer.returntime);
            Assert.Equal(journey.departure_station_id, answer.departure_station_id);
            Assert.Equal(journey.departure_station_name, answer.departure_station_name);
            Assert.Equal(journey.return_station_id, answer.return_station_id);
            Assert.Equal(journey.return_station_name, answer.return_station_name);
            Assert.Equal(journey.covered_distance_m, answer.covered_distance_m);
            Assert.Equal(journey.duration_sec, answer.duration_sec);
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
                departure = new DateTime(2021, 9, 15, 14, 55, 00),
                returntime = new DateTime(2021, 9, 15, 15, 13, 00),
                departure_station_id = 12,
                departure_station_name = "Kolmas",
                return_station_id = 5,
                return_station_name = "Viides",
                covered_distance_m = 3780,
                duration_sec = 1080
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
                departure = new DateTime(2021, 9, 15, 14, 55, 00),
                returntime = new DateTime(2021, 9, 15, 15, 13, 00),
                departure_station_id = 3,
                departure_station_name = "Kolmannes",
                return_station_id = 5,
                return_station_name = "Viides",
                covered_distance_m = 3780,
                duration_sec = 1080
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
                departure = new DateTime(2021, 9, 15, 14, 55, 00),
                returntime = new DateTime(2021, 9, 15, 15, 13, 00),
                departure_station_id = 5,
                departure_station_name = "Kolmas",
                return_station_id = 5,
                return_station_name = "Viides",
                covered_distance_m = 3780,
                duration_sec = 1080
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
                departure = new DateTime(2021, 9, 15, 14, 55, 00),
                returntime = new DateTime(2021, 9, 15, 15, 13, 00),
                departure_station_id = 3,
                departure_station_name = "Kolmas",
                return_station_id = 12,
                return_station_name = "Viides",
                covered_distance_m = 3780,
                duration_sec = 1080
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
                departure = new DateTime(2021, 9, 15, 14, 55, 00),
                returntime = new DateTime(2021, 9, 15, 15, 13, 00),
                departure_station_id = 3,
                departure_station_name = "Kolmas",
                return_station_id = 5,
                return_station_name = "Viidennes",
                covered_distance_m = 3780,
                duration_sec = 1080
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
                departure = new DateTime(2021, 9, 15, 14, 55, 00),
                returntime = new DateTime(2021, 9, 15, 15, 13, 00),
                departure_station_id = 3,
                departure_station_name = "Kolmas",
                return_station_id = 4,
                return_station_name = "Viides",
                covered_distance_m = 3780,
                duration_sec = 1080
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
                departure = new DateTime(2021, 9, 15, 14, 55, 00),
                returntime = new DateTime(2021, 9, 15, 15, 13, 00),
                departure_station_id = 3,
                departure_station_name = "Kolmas",
                return_station_id = 5,
                return_station_name = "Viides",
                covered_distance_m = 5,
                duration_sec = 1080
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
                departure = new DateTime(2021, 9, 15, 14, 55, 00),
                returntime = new DateTime(2021, 9, 15, 15, 13, 00),
                departure_station_id = 3,
                departure_station_name = "Kolmas",
                return_station_id = 5,
                return_station_name = "Viides",
                covered_distance_m = 3780,
                duration_sec = 5
            };

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);

            //Assert
            await Assert.ThrowsAsync<InvalidInputException>(async () => await journeyService.CreateJourney(journey));
        }

        [Fact]
        public async Task Get_JourneysCount_July_SearchNone()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneysCount("", 7, "2021-01-01T00:00:00+03:00", "2021-12-31T23:59:59+03:00", 0, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(10, answer);
        }

        [Fact]
        public async Task Get_JourneysCount_June_ParametersNone()
        {
            //Arrange
            var mockJune = TestDataHelper.GetFakeJuneJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Junes).Returns(mockJune.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneysCount("", 6, "2021-01-01T00:00:00+03:00", "2021-12-31T23:59:59+03:00", 0, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer);
        }

        [Fact]
        public async Task Get_JourneysCount_July_MultibleParameters()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneysCount("", 7, "2021-07-04T00:00:00+03:00", "2021-12-28T23:59:59+03:00", 0.3, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(8, answer);
        }

        [Fact]
        public async Task Get_JourneysCount_July_SearchSe()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneysCount("Se", 7, "2021-01-01T00:00:00+03:00", "2021-12-31T23:59:59+03:00", 0, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(1, answer);
        }

        [Fact]
        public async Task Get_JourneysJuly_Departure24Day_ReturnTimeDefaul()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 10, "Departure", "", false, 7, "2021-07-24T12:02:00+03:00", "2021-12-31T23:59:59+03:00", 0, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(7, answer.Count());
            Assert.Equal("24.7.2021 12.02.00", answer.ElementAt(0).departure.ToString());
            Assert.Equal("24.7.2021 19.32.00", answer.ElementAt(1).departure.ToString());
            Assert.Equal("25.7.2021 10.30.00", answer.ElementAt(2).departure.ToString());
            Assert.Equal("25.7.2021 10.30.45", answer.ElementAt(3).departure.ToString());
            Assert.Equal("25.7.2021 12.45.00", answer.ElementAt(4).departure.ToString());
            Assert.Equal("25.7.2021 15.00.00", answer.ElementAt(5).departure.ToString());
            Assert.Equal("31.7.2021 23.58.00", answer.ElementAt(6).departure.ToString());
        }
        [Fact]
        public async Task Get_JourneysJuly_DepartureDefault_ReturnTime24Day()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 10, "Return", "", false, 7, "2021-01-01T00:00:00+03:00", "2021-07-24T12:45:00+03:00", 0, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(4, answer.Count());
            Assert.Equal("2.7.2021 8.35.00", answer.ElementAt(0).returntime.ToString());
            Assert.Equal("3.7.2021 8.34.00", answer.ElementAt(1).returntime.ToString());
            Assert.Equal("12.7.2021 15.20.00", answer.ElementAt(2).returntime.ToString());
            Assert.Equal("24.7.2021 12.45.00", answer.ElementAt(3).returntime.ToString());
        }

        [Fact]
        public async Task Get_JourneysJuly_DistanceMinDefault_DistanceMax45()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 10, "Covered_distance", "", false, 7, "2021-01-01T00:00:00+03:00", "2021-12-31T23:59:59+03:00", 0, 4.5, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(4, answer.Count());
            Assert.Equal(1350, answer.ElementAt(0).covered_distance_m);
            Assert.Equal(1650, answer.ElementAt(1).covered_distance_m);
            Assert.Equal(2780, answer.ElementAt(2).covered_distance_m);
            Assert.Equal(3000, answer.ElementAt(3).covered_distance_m);
        }
        [Fact]
        public async Task Get_JourneysJuly_DistanceMin3_DistanceMaxDefaul()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 10, "Covered_distance", "", false, 7, "2021-01-01T00:00:00+03:00", "2021-12-31T23:59:59+03:00", 3, 214748.3647, 0, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(7, answer.Count());
            Assert.Equal(3000, answer.ElementAt(0).covered_distance_m);
            Assert.Equal(4230, answer.ElementAt(1).covered_distance_m);
            Assert.Equal(6000, answer.ElementAt(2).covered_distance_m);
            Assert.Equal(6300, answer.ElementAt(3).covered_distance_m);
            Assert.Equal(6450, answer.ElementAt(4).covered_distance_m);
            Assert.Equal(16000, answer.ElementAt(5).covered_distance_m);
            Assert.Equal(16500, answer.ElementAt(6).covered_distance_m);
        }

        [Fact]
        public async Task Get_JourneysJuly_DurationMin1600_DurationMaxDefaul()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 10, "Duration", "", false, 7, "2021-01-01T00:00:00+03:00", "2021-12-31T23:59:59+03:00", 0, 214748.3647, 1600, 2147483647);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal(1680, answer.ElementAt(0).duration_sec);
            Assert.Equal(1800, answer.ElementAt(1).duration_sec);
            Assert.Equal(2055, answer.ElementAt(2).duration_sec);
            Assert.Equal(2220, answer.ElementAt(3).duration_sec);
            Assert.Equal(3600, answer.ElementAt(4).duration_sec);
        }
        [Fact]
        public async Task Get_JourneysJuly_DurationMinDefault_DurationMax1580()
        {
            //Arrange
            var mockJuly = TestDataHelper.GetFakeJulyJourneys().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Julys).Returns(mockJuly.Object);

            //Act
            JourneyService journeyService = new JourneyService(cityBikeContextMock.Object);
            var answer = await journeyService.GetJourneys(0, 10, "Duration", "", false, 7, "2021-01-01T00:00:00+03:00", "2021-12-31T23:59:59+03:00", 0, 214748.3647, 0, 1580);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(5, answer.Count());
            Assert.Equal(240, answer.ElementAt(0).duration_sec);
            Assert.Equal(300, answer.ElementAt(1).duration_sec);
            Assert.Equal(900, answer.ElementAt(2).duration_sec);
            Assert.Equal(1080, answer.ElementAt(3).duration_sec);
            Assert.Equal(1580, answer.ElementAt(4).duration_sec);
        }
    }
}
