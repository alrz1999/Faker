using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Classes
{
    public class GeoPoint
    {
        public double Lat { get; set; }
        public double Lon { get; set; }

        public override bool Equals(object obj)
        {
            return obj is GeoPoint point &&
                   Lat == point.Lat &&
                   Lon == point.Lon;
        }

        public override int GetHashCode()
        {
            var hashCode = -53261388;
            hashCode = hashCode * -1521134295 + Lat.GetHashCode();
            hashCode = hashCode * -1521134295 + Lon.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"{Lat},{Lon}";
        }
    }
}
