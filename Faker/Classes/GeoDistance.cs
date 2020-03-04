using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Classes
{
    public class GeoDistance
    {
        public GeoDistanceUnit DistanceUnit { get; set; }

        public double Distance { get; set; }

        public GeoDistance Clone()
        {
            var res = (GeoDistance)MemberwiseClone();
            return res;
        }
    }
}
