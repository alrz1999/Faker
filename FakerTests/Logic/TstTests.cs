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
    public class TstTests
    {
        GeoDistance distance = new GeoDistance()
        {
            Distance = 1,
            DistanceUnit = GeoDistanceUnit.Kilometers
        };
        GeoPoint start = new GeoPoint()
        {
            Lat = 35.667579,
            Lon = 51.395738
        };
        GeoPoint end = new GeoPoint()
        {
            Lat = 35.667793,
            Lon = 51.398417
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