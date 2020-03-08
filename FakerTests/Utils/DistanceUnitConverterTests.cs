using Faker.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Faker.Utils.Tests
{
    [TestClass()]
    public class DistanceUnitConverterTests
    {
        [TestMethod()]
        public void KmToCentimeter()
        {
            var actual = DistanceUnitConverter.Convert(GeoDistanceUnit.Kilometers, GeoDistanceUnit.Centimeters, 5);
            var expected = 500000;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MileToMeter()
        {
            var actual = DistanceUnitConverter.Convert(GeoDistanceUnit.Miles, GeoDistanceUnit.Meters, 15);
            var expected = 24140.16;
            Assert.AreEqual(expected, actual);
        }

    }
}