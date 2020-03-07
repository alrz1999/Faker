using CoordinateSharp;
using Faker.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Logic
{
    public class BoxFinderTest
    {
        private readonly GeoDistance searchDistance;
        private readonly Coordinate start;
        private readonly Coordinate end;
        private readonly Distance startToEndDistance;

        public BoxFinderTest(GeoDistance distance, GeoPoint start, GeoPoint end)
        {
            this.searchDistance = distance;
            var offEagerLoad = new EagerLoad(false);

            if (!Coordinate.TryParse(start.ToString(), offEagerLoad, out this.start))
                throw new ArgumentException("invalid start point!");

            if (!Coordinate.TryParse(end.ToString(), offEagerLoad, out this.end))
                throw new ArgumentException("invalid end point!");

            startToEndDistance = new Distance(this.start,this.end);
        }

        public IEnumerable<GeoPoint> GetBoundingBox()
        {
            return new List<GeoPoint>() { GetTopLeft(), GetTopRight(), GetBottomRight(), GetBottomLeft(), GetTopLeft() };
        }

        private GeoPoint GetBottomLeft()
        {
            Coordinate newPoint;
            if (this.startToEndDistance.Bearing == 0)
                newPoint = GetNewPoint(270, this.start);
            else if (this.startToEndDistance.Bearing > 180)
                newPoint = GetNewPoint(startToEndDistance.Bearing - 90, this.end);
            else if (this.startToEndDistance.Bearing < 180)
                newPoint = GetNewPoint(startToEndDistance.Bearing + 90, this.start);
            else
                newPoint = GetNewPoint(270, this.end);
            return ConvertCoordinateToGeoPoint(newPoint);
        }

        private GeoPoint GetBottomRight()
        {
            Coordinate newPoint;
            if (this.startToEndDistance.Bearing == 0)
                newPoint = GetNewPoint(90, this.start);
            else if (this.startToEndDistance.Bearing > 180)
                newPoint = GetNewPoint(startToEndDistance.Bearing - 90, this.start);
            else if (this.startToEndDistance.Bearing < 180)
                newPoint = GetNewPoint(startToEndDistance.Bearing + 90, this.end);
            else
                newPoint = GetNewPoint(90, this.end);
            return ConvertCoordinateToGeoPoint(newPoint);
        }

        private GeoPoint GetTopRight()
        {
            Coordinate newPoint;
            if (this.startToEndDistance.Bearing == 0)
                newPoint = GetNewPoint(90, this.end);
            else if (this.startToEndDistance.Bearing > 180)
                newPoint = GetNewPoint((startToEndDistance.Bearing + 90) % 360, this.start);
            else if (this.startToEndDistance.Bearing < 180)
                newPoint = GetNewPoint((startToEndDistance.Bearing - 90) % 360, this.end);
            else
                newPoint = GetNewPoint(90, this.start);
            return ConvertCoordinateToGeoPoint(newPoint);
        }

        private GeoPoint GetTopLeft()
        {
            Coordinate newPoint;
            if (this.startToEndDistance.Bearing == 0)
                newPoint = GetNewPoint(270, this.end);
            else if (this.startToEndDistance.Bearing > 180)
                newPoint = GetNewPoint((startToEndDistance.Bearing + 90) % 360, this.end);
            else if (this.startToEndDistance.Bearing < 180)
                newPoint = GetNewPoint((startToEndDistance.Bearing - 90) % 360, this.start);
            else
                newPoint = GetNewPoint(270, this.start);
            return ConvertCoordinateToGeoPoint(newPoint);
        }

        private Coordinate GetNewPoint(double bearing, Coordinate startPoint)
        {
            var searchDistanceInMeter = DistanceUnitConverter.Convert(searchDistance.DistanceUnit, GeoDistanceUnit.Meters, searchDistance.Distance);
            Coordinate newPoint = new Coordinate(startPoint.Latitude.ToDouble(),startPoint.Longitude.ToDouble());
            newPoint.Move(searchDistanceInMeter, bearing, Shape.Ellipsoid);
            return newPoint;
        }

        private static GeoPoint ConvertCoordinateToGeoPoint(Coordinate newPoint)
        {
            return new GeoPoint
            {
                Lat = newPoint.Latitude.ToDouble(),
                Lon = newPoint.Longitude.ToDouble()
            };
        }
    }
}
