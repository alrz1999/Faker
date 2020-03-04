using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Classes
{
    public class GeoCircle : GeoShape
    {
        public GeoCircle(GeoPoint center, GeoDistance distance)
        {
            SearchType = GeoSearchType.Circle;
            Center = center ?? throw new ArgumentNullException(nameof(center));

            if (distance.Distance <= 0)
                throw new ArgumentOutOfRangeException("error");

            Distance = distance;
        }

        public GeoPoint Center { get; private set; }

        public GeoDistance Distance { get; private set; }
    }
}
