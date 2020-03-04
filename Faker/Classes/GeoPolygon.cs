using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Classes
{
    public class GeoPolygon : GeoShape
    {
        public List<GeoPoint> Corners { get; private set; }

        public GeoPolygon(IEnumerable<GeoPoint> corners = null)
        {
            SearchType = GeoSearchType.Polygon;

            if (corners == null || !corners.Any())
            {
                Corners = new List<GeoPoint>();
                return;
            }

            if (corners.Count() < 4)
                throw new ArgumentException("error");

            if (!corners.First().Equals(corners.Last()))
                throw new ArgumentException("first point and last point should be equal.");

            Corners = corners.ToList();
        }
    }
}
