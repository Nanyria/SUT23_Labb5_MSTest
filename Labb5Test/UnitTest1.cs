using DotNetDynamosV2;
using System;

namespace Labb5Test
{
    [TestClass]
    public class ConverterTests
    {
        //Given_When_Then
        [TestMethod]

        public void FromSekToYen_100_SEK_Return_14000_Yen()
        {
            //Arrange (input)
            Converter systemUnderTesting = new Converter()
            {
                Yen = 14.0M
            };

            //Act
            var actual = systemUnderTesting.FromSekToYen(100);
            var expected = 1400;

            //Assert
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void FromYenToSek_14000_Yen_Return_100_SEK()
        {
            Converter systemUnderTesting = new Converter()
            {
                Yen = 14.0M
            };
            var actual = systemUnderTesting.FromYenToSek(1400);
            var expected = 100;

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void FromSekToEURO_100_SEK_Return_8_9_EURO()
        {
            Converter systemÚnderTesting = new Converter()
            {
                Euro = 0.089M
            };
            var actual = systemÚnderTesting.FromSekToEur(100);
        }
        [TestMethod]
        public void FromEuroToSek_8_9_Euro_return_100_Sek()
        {
            Converter systemÚnderTesting = new Converter()
            {
                Yen = 14.0M
            };

        }

    }
}