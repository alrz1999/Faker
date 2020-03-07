using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Faker.Classes;
using CoordinateSharp;

namespace Faker.Logic.Tests
{
    [TestClass()]
    public class BoxFinderTestTests
    {
        [TestMethod()]
        public void GetBoundingBoxTest()
        {
            GeoDistance faultUnit = new GeoDistance()
            {
                Distance = 2,
                DistanceUnit = GeoDistanceUnit.Meters
            };
            GeoDistance distance = new GeoDistance()
            {
                Distance = 10,
                DistanceUnit = GeoDistanceUnit.Meters
            };
            GeoPoint start = new GeoPoint()
            {
                Lat = 35.70088,
                Lon = 51.39222
            };
            GeoPoint end = new GeoPoint()
            {
                Lat = 35.7011,
                Lon = 51.4049
            };
            var boundingBoxFinder = new BoxFinderTest(distance, start, end);
            var boundingBox = boundingBoxFinder.GetBoundingBox();
            foreach(var point in boundingBox)
            {
                Distance distanceFromStart = new Distance(new Coordinate(start.Lat, start.Lon), new Coordinate(point.Lat, point.Lon));
                Distance distanceFromEnd = new Distance(new Coordinate(end.Lat, end.Lon), new Coordinate(point.Lat, point.Lon));

                if (!HasEnoughAccuracy(faultUnit, distance, distanceFromStart) && !HasEnoughAccuracy(faultUnit,distance,distanceFromEnd))
                    Assert.Fail("Not enough Accuracy");
                if (!IsGisInRange(point))
                    Assert.Fail("Not in Range Lat or Lon");
            }
        }

        private static bool HasEnoughAccuracy(GeoDistance faultUnit, GeoDistance distance, Distance distanceFromStart)
        {
            return GetAbstractDistance(distance, distanceFromStart) > faultUnit.Distance;
        }

        private static double GetAbstractDistance(GeoDistance distance, Distance distanceFromStart)
        {
            return Math.Abs(distanceFromStart.Meters - distance.Distance);
        }

        private bool IsGisInRange(GeoPoint point)
        {
            return IsLatInRange(point.Lat) && IsLonInRange(point.Lon);
        }

        private bool IsLatInRange(double lat)
        {
            return lat <= 90 && lat >= -90;
        }

        private bool IsLonInRange(double lon)
        {
            return lon <= 180 && lon >= -180;
        }
    }
}