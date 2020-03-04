using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Classes
{
    public class GeoLine : GeoShape
    {
        public List<GeoPoint> Corners { get; private set; }

        public GeoDistance Distance { get; private set; }

        public GeoLine(GeoDistance distance, IEnumerable<GeoPoint> corners = null)
        {
            SearchType = GeoSearchType.Line;

            if (corners == null || !corners.Any())
            {
                Corners = new List<GeoPoint>();
                return;
            }

            if (corners.Count() < 2)
                throw new ArgumentException("error");

            Corners = corners.ToList();

            if (distance.Distance <= 0)
                throw new ArgumentOutOfRangeException("error");

            Distance = distance;
        }

    }
}
