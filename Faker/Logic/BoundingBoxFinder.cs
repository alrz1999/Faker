using Faker.Classes;
using System;
using System.Collections.Generic;
using CoordinateSharp;
namespace Faker.Logic
{
    public class BoundingBoxFinder
    {
        private readonly GeoDistance distance;
        private readonly Coordinate start;
        private readonly Coordinate end;

        private double verticalDistance;
        private double horizontalDistance;

        public BoundingBoxFinder(GeoDistance distance, GeoPoint start, GeoPoint end)
        {
            this.distance = distance;
            var offEagerLoad = new EagerLoad(false);

            if (!Coordinate.TryParse(start.ToString(), offEagerLoad, out this.start))
                throw new ArgumentException("invalid start point!");

            if (!Coordinate.TryParse(end.ToString(), offEagerLoad, out this.end))
                throw new ArgumentException("invalid end point!");           
        }

        public IEnumerable<GeoPoint> GetBoundingBox()
        {
            var lonDistance = start.Longitude.ToDouble() - end.Longitude.ToDouble();
            var latDistance = start.Latitude.ToDouble() - end.Latitude.ToDouble();

            var distance = Math.Sqrt(Math.Pow(lonDistance, 2) + Math.Pow(latDistance, 2));

            verticalDistance = this.distance.Distance * lonDistance / distance;
            horizontalDistance = this.distance.Distance * latDistance / distance;

            return new List<GeoPoint>() { GetTopLeft(), GetTopRight(), GetBottomRight(), GetBottomLeft(), GetTopLeft() };
        }

        private GeoPoint GetBottomLeft()
        {
            return new GeoPoint
            {
                Lat = start.Longitude.ToDouble() + horizontalDistance,
                Lon = start.Latitude.ToDouble() - verticalDistance
            };
        }

        private GeoPoint GetBottomRight()
        {
            return new GeoPoint
            {
                Lat = end.Longitude.ToDouble() + horizontalDistance,
                Lon = end.Latitude.ToDouble() - verticalDistance
            };
        }

        private GeoPoint GetTopRight()
        {
            return new GeoPoint
            {
                Lat = end.Longitude.ToDouble() - horizontalDistance,
                Lon = end.Latitude.ToDouble() + verticalDistance
            };
        }

        private GeoPoint GetTopLeft()
        {
            return new GeoPoint
            {
                Lat = start.Longitude.ToDouble() - horizontalDistance,
                Lon = start.Latitude.ToDouble() + verticalDistance
            };
        }
    }
}