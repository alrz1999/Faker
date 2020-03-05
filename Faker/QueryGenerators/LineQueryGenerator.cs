using Faker.Classes;
using Faker.Logic;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.QueryGenerators
{
    public class LineQueryGenerator : QueryGenerator
    {
        private readonly GeoLine line;
        public LineQueryGenerator(GeoLine line, string field) : base(field)
        {
            this.line = line;
        }

        public BoolQuery GetQuery()
        {
            var orQueries = new List<QueryContainer>();

            var firstCorner = line.Corners.First();
            var firstCornerQuery = GetDistanceQuery(firstCorner);
            orQueries.Add(firstCornerQuery);

            for (int i = 1; i < line.Corners.Count; i++)
            {
                var start = line.Corners[i - 1];
                var end = line.Corners[i];

                var polygonQuery = GetPolygonQuery(start, end);
                var distanceQuery = GetDistanceQuery(end);

                orQueries.Add(polygonQuery);
                orQueries.Add(distanceQuery);
            }

            return new BoolQuery { Should = orQueries };
        }

        private GeoPolygonQuery GetPolygonQuery(GeoPoint start, GeoPoint end)
        {
            var boundingBoxFinder = new BoxFinderTest(line.Distance, start, end);
            var corners = boundingBoxFinder.GetBoundingBox();

            return new GeoPolygonQuery
            {
                Field = field,
                ValidationMethod = GeoValidationMethod.IgnoreMalformed,
                Points = corners.Select(x => new GeoLocation(x.Lat, x.Lon)),
                
            };
        }

        private GeoDistanceQuery GetDistanceQuery(GeoPoint center)
        {
            return new GeoDistanceQuery
            {
                Field = field,
                DistanceType = GeoDistanceType.Arc,
                Location = new GeoLocation(center.Lat, center.Lon),
                Distance = new Distance(line.Distance.Distance, ConvertGeoDistanceUnitToNestDistanceUnit(line.Distance.DistanceUnit)),
                ValidationMethod = GeoValidationMethod.IgnoreMalformed
            };
        }

        private DistanceUnit ConvertGeoDistanceUnitToNestDistanceUnit(GeoDistanceUnit geoDistanceUnit)
        {
            return (DistanceUnit)Enum.Parse(typeof(GeoDistanceUnit), line.Distance.DistanceUnit.ToString(), true);
        }
    }
}
