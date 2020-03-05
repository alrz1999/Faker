using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faker.Classes;

namespace Faker.Logic.Tests
{
    [TestClass()]
    public class BoxFinderTestTests
    {
        GeoDistance distance = new GeoDistance()
        {
            Distance = 1900,
            DistanceUnit = GeoDistanceUnit.Meters
        };
        GeoPoint start = new GeoPoint()
        {
            Lat = 35.6996773010464,
            Lon = 51.3377235245839
        };
        GeoPoint end = new GeoPoint()
        {
            Lat = 35.72497,
            Lon = 51.38082
        };
        [TestMethod()]
        public void TstTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetBoundingBoxTest()
        {
            var boundingBoxFinder = new BoxFinderTest(distance, start, end);
            var boundingBox = boundingBoxFinder.GetBoundingBox();
            Console.WriteLine(boundingBox.ToList());
            Assert.Fail();
        }
    }
}