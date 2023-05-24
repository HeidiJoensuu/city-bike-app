using Api.Models;
using Moq;
using Api.Services;
using MockQueryable.Moq;
using Api.Models.DTOs;
using Api.Models.Models;
using Api.Exceptions;

namespace Unit_Tests
{
    public class StationServiceTests
    {
        [Fact]
        public async Task Get_AllStations_Offset0_Limit10_OrderName_SearchNone_Asc()
        {
            //Arrange
            var mock = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Stations).Returns(mock.Object);

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            var stations = await stationService.GetStations(0,10,"Name","",false);
            
            //Assert
            Assert.NotNull(stations);
            Assert.Equal(9, stations.Count());
            Assert.Equal("Eighth", stations.ElementAt(0).Name);
            Assert.Equal("Fifth", stations.ElementAt(1).Name);
            Assert.Equal("First", stations.ElementAt(2).Name);
            Assert.Equal("Fourth", stations.ElementAt(3).Name);
            Assert.Equal("Ninth", stations.ElementAt(4).Name);
            Assert.Equal("Second", stations.ElementAt(5).Name);
            Assert.Equal("Seventh", stations.ElementAt(6).Name);
            Assert.Equal("Sixth", stations.ElementAt(7).Name);
            Assert.Equal("Third", stations.ElementAt(8).Name);
        }

        [Fact]
        public async Task Get_AllStations_Offset5_Limit10_OrderName_SearchNone_Asc()
        {
            //Arrange
            var mock = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Stations).Returns(mock.Object);

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            var stations = await stationService.GetStations(5, 10, "Name", "", false);

            //Assert
            Assert.NotNull(stations);
            Assert.Equal(4, stations.Count());
            Assert.Equal("Second", stations.ElementAt(0).Name);
            Assert.Equal("Seventh", stations.ElementAt(1).Name);
            Assert.Equal("Sixth", stations.ElementAt(2).Name);
            Assert.Equal("Third", stations.ElementAt(3).Name);
        }

        [Fact]
        public async Task Get_AllStations_Offset2_Limit3_OrderName_SearchNone_Asc()
        {
            //Arrange
            var mock = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Stations).Returns(mock.Object);

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            var stations = await stationService.GetStations(2, 3, "Name", "", false);

            //Assert
            Assert.NotNull(stations);
            Assert.Equal(3, stations.Count());
            Assert.Equal("First", stations.ElementAt(0).Name);
            Assert.Equal("Fourth", stations.ElementAt(1).Name);
            Assert.Equal("Ninth", stations.ElementAt(2).Name);
        }

        [Fact]
        public async Task Get_AllStations_Offset0_Limit10_OrderNimi_SearchNone_Asc()
        {
            //Arrange
            var mock = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Stations).Returns(mock.Object);

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            var stations = await stationService.GetStations(0, 10, "Nimi", "", false);

            //Assert
            Assert.NotNull(stations);
            Assert.Equal(9, stations.Count());
            Assert.Equal("First", stations.ElementAt(0).Name);
            Assert.Equal("Eighth", stations.ElementAt(1).Name);
            Assert.Equal("Third", stations.ElementAt(2).Name);
            Assert.Equal("Sixth", stations.ElementAt(3).Name);
            Assert.Equal("Fourth", stations.ElementAt(4).Name);
            Assert.Equal("Seventh", stations.ElementAt(5).Name);
            Assert.Equal("Second", stations.ElementAt(6).Name);
            Assert.Equal("Fifth", stations.ElementAt(7).Name);
            Assert.Equal("Ninth", stations.ElementAt(8).Name);
        }

        [Fact]
        public async Task Get_AllStations_Offset0_Limit10_OrderName_SearchEk_Asc()
        {
            //Arrange
            var mock = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Stations).Returns(mock.Object);

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            var stations = await stationService.GetStations(0, 10, "Name", "Ek", false);

            //Assert
            Assert.NotNull(stations);
            Assert.Equal(2, stations.Count());
            Assert.Equal("Eighth", stations.ElementAt(0).Name);
            Assert.Equal("Ninth", stations.ElementAt(1).Name);
        }

        [Fact]
        public async Task Get_AllStations_Offset0_Limit10_OrderName_SearchXx_Asc()
        {
            //Arrange
            var mock = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Stations).Returns(mock.Object);

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            var stations = await stationService.GetStations(0, 10, "Name", "Xx", false);

            //Assert
            Assert.NotNull(stations);
            Assert.Equal(0, stations.Count());
        }

        [Fact]
        public async Task Get_AllStations_Offset0_Limit10_OrderName_SearchNone_Desc()
        {
            //Arrange
            var mock = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Stations).Returns(mock.Object);

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            var stations = await stationService.GetStations(0, 10, "Name", "", true);

            //Assert
            Assert.NotNull(stations);
            Assert.Equal(9, stations.Count());
            Assert.Equal("Eighth", stations.ElementAt(8).Name);
            Assert.Equal("Fifth", stations.ElementAt(7).Name);
            Assert.Equal("First", stations.ElementAt(6).Name);
            Assert.Equal("Fourth", stations.ElementAt(5).Name);
            Assert.Equal("Ninth", stations.ElementAt(4).Name);
            Assert.Equal("Second", stations.ElementAt(3).Name);
            Assert.Equal("Seventh", stations.ElementAt(2).Name);
            Assert.Equal("Sixth", stations.ElementAt(1).Name);
            Assert.Equal("Third", stations.ElementAt(0).Name);
        }

        [Fact]
        public async Task Get_AllStations_Offset1_Limit3_OrderName_SearchS_Desc()
        {
            //Arrange
            var mock = TestDataHelper.GetFakeStations().BuildMock().BuildMockDbSet();
            var cityBikeContextMock = new Mock<CityBikesDBContext>();
            cityBikeContextMock.Setup(table => table.Stations).Returns(mock.Object);

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            var stations = await stationService.GetStations(1, 3, "Name", "S", true);

            //Assert
            Assert.NotNull(stations);
            Assert.Equal(3, stations.Count());
            Assert.Equal("Sixth", stations.ElementAt(0).Name);
            Assert.Equal("Seventh", stations.ElementAt(1).Name);
            Assert.Equal("Second", stations.ElementAt(2).Name);
        }

        [Fact]
        public async Task Get_StationInfo_May()
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

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            var answer = await stationService.GetStationInfo(3, 5);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal("Kolmas", answer.Nimi);
            Assert.Equal("Tredje", answer.Namn);
            Assert.Equal("Third", answer.Name);
            Assert.Equal("Kolmas katu 3", answer.Osoite);
            Assert.Equal("Tredje gatan 3", answer.Adress);
            Assert.Equal(60.2504077926351, answer.y);
            Assert.Equal(24.2286486878388, answer.x);
            Assert.Equal((decimal)3.2, answer.journeyInfo.AverageDistanceDepartures);
            Assert.Equal((decimal)1.23, answer.journeyInfo.AverageDistanceReturns);
            Assert.Equal(1, answer.journeyInfo.CountOfDepartures);
            Assert.Equal(1, answer.journeyInfo.CountOfReturns);
            Assert.Equal("Kuudes", answer.journeyInfo.PopularDepartures[0]);
            Assert.Equal("Viides", answer.journeyInfo.PopularReturns[0]);
        }

        [Fact]
        public async Task Get_StationInfo_June()
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

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            var answer = await stationService.GetStationInfo(2, 6);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal("Toinen", answer.Nimi);
            Assert.Equal("Andra", answer.Namn);
            Assert.Equal("Second", answer.Name);
            Assert.Equal("Toinen katu 2", answer.Osoite);
            Assert.Equal("Andra gatan 2", answer.Adress);
            Assert.Equal(60.3504077926351, answer.y);
            Assert.Equal(24.2386486878388, answer.x);
            Assert.Equal((decimal)10, answer.journeyInfo.AverageDistanceDepartures);
            Assert.Equal((decimal)7, answer.journeyInfo.AverageDistanceReturns);
            Assert.Equal(1, answer.journeyInfo.CountOfDepartures);
            Assert.Equal(2, answer.journeyInfo.CountOfReturns);
            Assert.Equal("Toinen", answer.journeyInfo.PopularDepartures[0]);
            Assert.Equal("Viides", answer.journeyInfo.PopularDepartures[1]);
            Assert.Equal("Toinen", answer.journeyInfo.PopularReturns[0]);
        }

        [Fact]
        public async Task Get_StationInfo_July()
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

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            var answer = await stationService.GetStationInfo(1, 7);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal("Ensimmainen", answer.Nimi);
            Assert.Equal("Forsta", answer.Namn);
            Assert.Equal("First", answer.Name);
            Assert.Equal("Ensimmainen katu 1", answer.Osoite);
            Assert.Equal("Fosta gatan 1", answer.Adress);
            Assert.Equal(60.2504081726074, answer.y);
            Assert.Equal(24.2486486878388, answer.x);
            Assert.Equal((decimal)7.78, answer.journeyInfo.AverageDistanceDepartures);
            Assert.Equal((decimal)6.49, answer.journeyInfo.AverageDistanceReturns);
            Assert.Equal(3, answer.journeyInfo.CountOfDepartures);
            Assert.Equal(4, answer.journeyInfo.CountOfReturns);

            Assert.Equal("Viides", answer.journeyInfo.PopularDepartures[0]);
            Assert.Equal("Kolmas", answer.journeyInfo.PopularDepartures[1]);
            Assert.Equal("Ensimmainen", answer.journeyInfo.PopularDepartures[2]);
            Assert.Equal("Kuudes", answer.journeyInfo.PopularDepartures[3]);

            Assert.Equal("Kolmas", answer.journeyInfo.PopularReturns[0]);
            Assert.Equal("Seitsemas", answer.journeyInfo.PopularReturns[1]);
            Assert.Equal("Ensimmainen", answer.journeyInfo.PopularReturns[2]);
        }

        [Fact]
        public async Task Get_StationInfo_StationWithNoJourneys()
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

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            var answer = await stationService.GetStationInfo(9, 7);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal("Yhdeksas", answer.Nimi);
            Assert.Equal("Nionde", answer.Namn);
            Assert.Equal("Ninth", answer.Name);
            Assert.Equal("Yhdeksas katu 9", answer.Osoite);
            Assert.Equal("Nionde gatan 9", answer.Adress);
            Assert.Equal(60.7504077926351, answer.y);
            Assert.Equal(24.7986486878388, answer.x);
            Assert.Equal((decimal)0, answer.journeyInfo.AverageDistanceDepartures);
            Assert.Equal((decimal)0, answer.journeyInfo.AverageDistanceReturns);
            Assert.Equal(0, answer.journeyInfo.CountOfDepartures);
            Assert.Equal(0, answer.journeyInfo.CountOfReturns);
        }

        [Fact]
        public async Task Get_StationInfo_StationWithOnlyReturnJourneys()
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

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            var answer = await stationService.GetStationInfo(7, 7);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal("Seitsemas", answer.Nimi);
            Assert.Equal("Sjunde", answer.Namn);
            Assert.Equal("Seventh", answer.Name);
            Assert.Equal("Seitsemas katu 7", answer.Osoite);
            Assert.Equal("Sjunde gatan 7", answer.Adress);
            Assert.Equal(60.5504077926351, answer.y);
            Assert.Equal(24.7486486878388, answer.x);
            Assert.Equal((decimal)0, answer.journeyInfo.AverageDistanceDepartures);
            Assert.Equal((decimal)16, answer.journeyInfo.AverageDistanceReturns);
            Assert.Equal(0, answer.journeyInfo.CountOfDepartures);
            Assert.Equal(1, answer.journeyInfo.CountOfReturns);
            Assert.Equal("Ensimmainen", answer.journeyInfo.PopularDepartures[0]);
        }

        [Fact]
        public async Task Get_StationInfo_StationWithOnlyDepartureJourneys()
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

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            var answer = await stationService.GetStationInfo(4, 7);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal("Neljas", answer.Nimi);
            Assert.Equal("Fjarde", answer.Namn);
            Assert.Equal("Fourth", answer.Name);
            Assert.Equal("Neljas katu 4", answer.Osoite);
            Assert.Equal("Fjarde gatan 4", answer.Adress);
            Assert.Equal(60.2504077926351, answer.y);
            Assert.Equal(24.3486486878388, answer.x);
            Assert.Equal((decimal)3, answer.journeyInfo.AverageDistanceDepartures);
            Assert.Equal((decimal)0, answer.journeyInfo.AverageDistanceReturns);
            Assert.Equal(1, answer.journeyInfo.CountOfDepartures);
            Assert.Equal(0, answer.journeyInfo.CountOfReturns);
            Assert.Equal("Toinen", answer.journeyInfo.PopularReturns[0]);
        }

        [Fact]
        public async Task Get_StationInfo_StationDoesNotExists()
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

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            var answer = await stationService.GetStationInfo(11, 7);

            //Assert
            Assert.Null(answer);
        }


        [Fact]
        public async Task Get_StationInfo_AllMonths()
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

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            var answer = await stationService.GetStationInfo(2, 0);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal("Toinen", answer.Nimi);
            Assert.Equal("Andra", answer.Namn);
            Assert.Equal("Second", answer.Name);
            Assert.Equal("Toinen katu 2", answer.Osoite);
            Assert.Equal("Andra gatan 2", answer.Adress);
            Assert.Equal(60.350407792635103, answer.y);
            Assert.Equal(24.238648687838801, answer.x);
            Assert.Equal((decimal)10, answer.journeyInfo.AverageDistanceDepartures);
            Assert.Equal((decimal)5.24, answer.journeyInfo.AverageDistanceReturns);
            Assert.Equal(1, answer.journeyInfo.CountOfDepartures);
            Assert.Equal(7, answer.journeyInfo.CountOfReturns);

            Assert.Equal("Viides", answer.journeyInfo.PopularDepartures[0]);
            Assert.Equal("Kahdeksas", answer.journeyInfo.PopularDepartures[1]);
            Assert.Equal("Toinen", answer.journeyInfo.PopularDepartures[2]);
            Assert.Equal("Neljas", answer.journeyInfo.PopularDepartures[3]);
            Assert.Equal("Kuudes", answer.journeyInfo.PopularDepartures[4]);

            Assert.Equal("Toinen", answer.journeyInfo.PopularReturns[0]);
        }

        [Fact]
        public async Task Get_StationInfo_MonthOutOfIndex()
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

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            var answer = await stationService.GetStationInfo(2, 9);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal("Toinen", answer.Nimi);
            Assert.Equal("Andra", answer.Namn);
            Assert.Equal("Second", answer.Name);
            Assert.Equal("Toinen katu 2", answer.Osoite);
            Assert.Equal("Andra gatan 2", answer.Adress);
            Assert.Equal(60.3504077926351, answer.y);
            Assert.Equal(24.2386486878388, answer.x);
            Assert.Equal((decimal)10, answer.journeyInfo.AverageDistanceDepartures);
            Assert.Equal((decimal)5.24, answer.journeyInfo.AverageDistanceReturns);
            Assert.Equal(1, answer.journeyInfo.CountOfDepartures);
            Assert.Equal(7, answer.journeyInfo.CountOfReturns);

            Assert.Equal("Viides", answer.journeyInfo.PopularDepartures[0]);
            Assert.Equal("Kahdeksas", answer.journeyInfo.PopularDepartures[1]);
            Assert.Equal("Toinen", answer.journeyInfo.PopularDepartures[2]);
            Assert.Equal("Neljas", answer.journeyInfo.PopularDepartures[3]);
            Assert.Equal("Kuudes", answer.journeyInfo.PopularDepartures[4]);

            Assert.Equal("Toinen", answer.journeyInfo.PopularReturns[0]);
        }

        [Fact]
        public async Task Create_NewStation_EverythingRight()
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
            Station saveStation = new Station()
            {
                Adress = "Tionde katan 10",
                Kapasiteet = 15,
                Name = "Tenth",
                Namn = "Tionde",
                Nimi = "Kymmenes",
                Osoite = "Kymmenes katu 10",
                x = "24.555555",
                y = "60.244444",
            };

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            var answer = await stationService.CreateStation(saveStation);

            //Assert
            Assert.NotNull(answer);
            Assert.Equal(10, answer.Id);


        }
        
        [Fact]
        public async Task Create_NewStation_NameIsDublicate_ShouldThrowException()
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
            Station saveStation = new Station()
            {
                Adress = "Tionde katan 10",
                Kapasiteet = 15,
                Name = "Ninth",
                Namn = "Tionde",
                Nimi = "Kymmenes",
                Osoite = "Kymmenes katu 10",
                x = "24.555555",
                y = "60.244444",
            };

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);

            //Assert
            await Assert.ThrowsAsync<DuplicateException>(async () => await stationService.CreateStation(saveStation));
        }
        
        [Fact]
        public async Task Create_NewStation_NamnIsDublicate_ShouldThrowException()
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
            Station saveStation = new Station()
            {
                Adress = "Tionde katan 10",
                Kapasiteet = 15,
                Name = "Tenth",
                Namn = "Nionde",
                Nimi = "Kymmenes",
                Osoite = "Kymmenes katu 10",
                x = "24.555555",
                y = "60.244444",
            };

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            
            //Assert
            await Assert.ThrowsAsync<DuplicateException>(async () => await stationService.CreateStation(saveStation));
        }

        [Fact]
        public async Task Create_NewStation_NimiIsDublicate_ShouldThrowException()
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
            Station saveStation = new Station()
            {
                Adress = "Tionde katan 10",
                Kapasiteet = 15,
                Name = "Tenth",
                Namn = "Tionde",
                Nimi = "Yhdeksas",
                Osoite = "Kymmenes katu 10",
                x = "24.555555",
                y = "60.244444",
            };

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            
            //Assert
            await Assert.ThrowsAsync<DuplicateException>(async () => await stationService.CreateStation(saveStation));
        }
        
        [Fact]
        public async Task Create_NewStation_xIsZero_ShouldThrowException()
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
            Station saveStation = new Station()
            {
                Adress = "Tionde katan 10",
                Kapasiteet = 15,
                Name = "Tenth",
                Namn = "Tionde",
                Nimi = "Kymmenes",
                Osoite = "Kymmenes katu 10",
                x = "0",
                y = "60.244444",
            };

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);

            //Assert
            await Assert.ThrowsAsync<MissingInputsException>(async () => await stationService.CreateStation(saveStation));
        }

        [Fact]
        public async Task Create_NewStation_yIsZero_ShouldThrowException()
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
            Station saveStation = new Station()
            {
                Adress = "Tionde katan 10",
                Kapasiteet = 15,
                Name = "Tenth",
                Namn = "Tionde",
                Nimi = "Kymmenes",
                Osoite = "Kymmenes katu 10",
                x = "24.555555",
                y = "0",
            };

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);

            //Assert
            await Assert.ThrowsAsync<MissingInputsException>(async () => await stationService.CreateStation(saveStation));
        }

        [Fact]
        public async Task Create_NewStation_KapasiteetIsZero_ShouldThrowException()
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
            Station saveStation = new Station()
            {
                Adress = "Tionde katan 10",
                Kapasiteet = 0,
                Name = "Tenth",
                Namn = "Tionde",
                Nimi = "Kymmenes",
                Osoite = "Kymmenes katu 10",
                x = "24.555555",
                y = "60.244444",
            };

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);

            //Assert
            await Assert.ThrowsAsync<MissingInputsException>(async () => await stationService.CreateStation(saveStation));
        }

        [Fact]
        public async Task Create_NewStation_AddressIsEmpty_ShouldThrowException()
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
            Station saveStation = new Station()
            {
                Adress = "",
                Kapasiteet = 15,
                Name = "Tenth",
                Namn = "Tionde",
                Nimi = "Kymmenes",
                Osoite = "Kymmenes katu 10",
                x = "24.555555",
                y = "60.244444",
            };

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            
            //Assert
            await Assert.ThrowsAsync<MissingInputsException>(async () => await stationService.CreateStation(saveStation));
        }

        [Fact]
        public async Task Create_NewStation_NamnIsEmpty_ShouldThrowException()
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
            Station saveStation = new Station()
            {
                Adress = "Tionde katan 10",
                Kapasiteet = 15,
                Name = "Tenth",
                Namn = "",
                Nimi = "Kymmenes",
                Osoite = "Kymmenes katu 10",
                x = "24.555555",
                y = "60.244444",
            };

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);
            
            //Assert
            await Assert.ThrowsAsync<MissingInputsException>(async () => await stationService.CreateStation(saveStation));
        }

        [Fact]
        public async Task Create_NewStation_NimiIsEmpty_ShouldThrowException()
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
            Station saveStation = new Station()
            {
                Adress = "Tionde katan 10",
                Kapasiteet = 15,
                Name = "Tenth",
                Namn = "Tionde",
                Nimi = "",
                Osoite = "Kymmenes katu 10",
                x = "24.555555",
                y = "60.244444",
            };

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);

            //Assert
            await Assert.ThrowsAsync<MissingInputsException>(async () => await stationService.CreateStation(saveStation));
        }

        [Fact]
        public async Task Create_NewStation_OsoiteIsEmpty_ShouldThrowException()
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
            Station saveStation = new Station()
            {
                Adress = "Tionde katan 10",
                Kapasiteet = 15,
                Name = "Tenth",
                Namn = "Tionde",
                Nimi = "Kymmenes",
                Osoite = "",
                x = "24.555555",
                y = "60.244444",
            };

            //Act
            StationService stationService = new StationService(cityBikeContextMock.Object);

            //Assert
            await Assert.ThrowsAsync<MissingInputsException>(async () => await stationService.CreateStation(saveStation));
        }
    }
}
