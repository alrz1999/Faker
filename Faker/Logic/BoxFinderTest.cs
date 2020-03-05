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
        private readonly GeoDistance distance;
        private readonly Coordinate start;
        private readonly Coordinate end;
        private readonly Distance routeDistance;

        public BoxFinderTest(GeoDistance distance, GeoPoint start, GeoPoint end)
        {
            this.distance = distance;
            var offEagerLoad = new EagerLoad(false);

            if (!Coordinate.TryParse(start.ToString(), offEagerLoad, out this.start))
                throw new ArgumentException("invalid start point!");

            if (!Coordinate.TryParse(end.ToString(), offEagerLoad, out this.end))
                throw new ArgumentException("invalid end point!");

            routeDistance = new Distance(this.start,this.end);
        }

        public IEnumerable<GeoPoint> GetBoundingBox()
        {
            return new List<GeoPoint>() { GetTopLeft(), GetTopRight(), GetBottomRight(), GetBottomLeft(), GetTopLeft() };
        }

        private GeoPoint GetBottomLeft()
        {
            Coordinate newPoint;
            if (this.routeDistance.Bearing == 0)
                newPoint = GetNewPoint(270, this.start);
            else if (this.routeDistance.Bearing > 180)
                newPoint = GetNewPoint(routeDistance.Bearing - 90, this.end);
            else if (this.routeDistance.Bearing < 180)
                newPoint = GetNewPoint(routeDistance.Bearing + 90, this.start);
            else
                newPoint = GetNewPoint(270, this.end);
            return new GeoPoint
            {
                Lat = newPoint.Latitude.ToDouble(),
                Lon = newPoint.Longitude.ToDouble()
            };
        }

        private GeoPoint GetBottomRight()
        {
            Coordinate newPoint;
            if (this.routeDistance.Bearing == 0)
                newPoint = GetNewPoint(90, this.start);
            else if (this.routeDistance.Bearing > 180)
                newPoint = GetNewPoint(routeDistance.Bearing - 90, this.start);
            else if (this.routeDistance.Bearing < 180)
                newPoint = GetNewPoint(routeDistance.Bearing + 90, this.end);
            else
                newPoint = GetNewPoint(90, this.end);
            return new GeoPoint
            {
                Lat = newPoint.Latitude.ToDouble(),
                Lon = newPoint.Longitude.ToDouble()
            };
        }

        private GeoPoint GetTopRight()
        {
            Coordinate newPoint;
            if (this.routeDistance.Bearing == 0)
                newPoint = GetNewPoint(90, this.end);
            else if (this.routeDistance.Bearing > 180)
                newPoint = GetNewPoint((routeDistance.Bearing + 90) % 360, this.start);
            else if (this.routeDistance.Bearing < 180)
                newPoint = GetNewPoint((routeDistance.Bearing - 90) % 360, this.end);
            else
                newPoint = GetNewPoint(90, this.start);
            return new GeoPoint
            {
                Lat = newPoint.Latitude.ToDouble(),
                Lon = newPoint.Longitude.ToDouble()
            };
        }

        private GeoPoint GetTopLeft()
        {
            Coordinate newPoint;
            if (this.routeDistance.Bearing == 0)
                newPoint = GetNewPoint(270, this.end);
            else if (this.routeDistance.Bearing > 180)
                newPoint = GetNewPoint((routeDistance.Bearing + 90) % 360, this.end);
            else if (this.routeDistance.Bearing < 180)
                newPoint = GetNewPoint((routeDistance.Bearing - 90) % 360, this.start);
            else
                newPoint = GetNewPoint(270, this.start);
            return new GeoPoint
            {
                Lat = newPoint.Latitude.ToDouble(),
                Lon = newPoint.Longitude.ToDouble()
            };
        }

        private Coordinate GetNewPoint(double bearing, Coordinate startPoint)
        {
            Coordinate newPoint = new Coordinate(startPoint.Latitude.ToDouble(),startPoint.Longitude.ToDouble());
            newPoint.Move(distance.Distance, bearing, Shape.Ellipsoid);
            return newPoint;
        }
    }
}
