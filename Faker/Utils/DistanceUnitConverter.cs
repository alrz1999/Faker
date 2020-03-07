using Faker.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Utils
{
    public class DistanceUnitConverter
    {
        private static readonly Dictionary<GeoDistanceUnit, double> distanceUnits = new Dictionary<GeoDistanceUnit, double>()
        {
            {GeoDistanceUnit.Inch, 0.0254 },
            {GeoDistanceUnit.Feet, 0.3048 },
            {GeoDistanceUnit.Yards, 0.9144 },
            {GeoDistanceUnit.Miles, 1609.34},
            {GeoDistanceUnit.NauticalMiles, 1852 },
            {GeoDistanceUnit.Kilometers, 1000 },
            {GeoDistanceUnit.Meters, 1 },
            {GeoDistanceUnit.Centimeters, 0.01 },
            {GeoDistanceUnit.Millimeters, 0.001 },
        };   



        public static double Convert(GeoDistanceUnit startUnit,GeoDistanceUnit endUnit, double value)
        {
            return value * distanceUnits[startUnit] / distanceUnits[endUnit];
        }
    }
}
