using Api.Utils;

namespace Unit_Tests
{
    public class UtilitiesTests
    {
        private readonly Utilities _utilities = new Utilities();
        [Fact]
        public void WantedMonth_ReturnsCorrectMonthWithNumber_5()
        {
            string answer = _utilities.WantedMont(5);
            Assert.Equal("[2021-05] ", answer);
        }

        [Fact]
        public void WantedMonth_ReturnsCorrectMonthWithNumber_6()
        {
            string answer = _utilities.WantedMont(6);
            Assert.Equal("[2021-06] ", answer);
        }

        [Fact]
        public void WantedMonth_ReturnsCorrectMonthWithNumber_7()
        {
            string answer = _utilities.WantedMont(7);
            Assert.Equal("[2021-07] ", answer);
        }

        [Fact]
        public void WantedMonth_ReturnsCorrectMonthWithNumber_OutOfIndex()
        {
            string answer = _utilities.WantedMont(8);
            Assert.Equal("[2021-07] ", answer);
        }

        [Fact]
        public void AscOrDesc_ReturnsCorrectAnswerWith_True()
        {
            string answer = _utilities.AscOrDesc(true);
            Assert.Equal("DESC", answer);
        }

        [Fact]
        public void AscOrDesc_ReturnsCorrectAnswerWith_False()
        {
            string answer = _utilities.AscOrDesc(false);
            Assert.Equal("ASC", answer);
        }
    }
}