using Faker.Classes;
using Faker.Factory;
using Faker.Models;
using Faker.QueryGenerators;
using Nest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Faker.Repository
{
    class DocumentRepository : IDocumentRepository<Building>
    {
        private readonly Client client;

        public DocumentRepository(Client client)
        {
            this.client = client;
        }

        public void Delete(string id)
        {
            client.Delete<Building>(id);
        }

        public Building Get(string id)
        {
            return client.Get<Building>(id).Source;
        }

        public void Index(Building document)
        {
            client.IndexDocument<Building>(document);
        }

        public void IndexMany(IEnumerable<Building> documents)
        {
            client.IndexMany<Building>(documents);
        }

        public IEnumerable<Building> GeoDistance(GeoCircle circle)
        {
            DistanceUnit distanceUnit = ConvertToDistanceUnit(circle.Distance);
            var results = client.Search<Building>(s => s
                .Query(q => q
                    .GeoDistance(g => g
                        .Field(f => f.Location)
                        .DistanceType(GeoDistanceType.Arc)
                        .Location(circle.Center.Lat, circle.Center.Lon)
                        .Distance(circle.Distance.Distance, distanceUnit)
                        .ValidationMethod(GeoValidationMethod.IgnoreMalformed)
                        )
                    )
                );
            return results.Documents;
        }

        private DistanceUnit ConvertToDistanceUnit(GeoDistance geoDistance)
        {
            return (DistanceUnit)Enum.Parse(typeof(DistanceUnit), geoDistance.DistanceUnit.ToString());
        }

        public IEnumerable<Building> GeoPolygon(GeoPolygon polygon)
        {
            var geoLocations = ConvertToGeoLocation(polygon.Corners);
            var results = client.Search<Building>(s => s
                .Query(q => q
                    .GeoPolygon(g => g
                        .Field(f => f.Location)
                        .ValidationMethod(GeoValidationMethod.Strict)
                        .Points(geoLocations)
                        )
                    )
                );
            return results.Documents;
        }

        private IEnumerable<GeoLocation> ConvertToGeoLocation(IEnumerable<GeoPoint> geoPoints)
        {
            var geoLocations = new List<GeoLocation>();
            foreach (var geoPoint in geoPoints)
            {
                geoLocations.Add(ConvertToGeoLocation(geoPoint));
            }
            return geoLocations;
        }

        private GeoLocation ConvertToGeoLocation(GeoPoint geoPoint)
        {
            return new GeoLocation(geoPoint.Lat, geoPoint.Lon);
        }

        public IEnumerable<Building> GetSortedGeoDistance(GeoCircle circle)
        {
            DistanceUnit distanceUnit = ConvertToDistanceUnit(circle.Distance);
            GeoLocation geoLocation = ConvertToGeoLocation(circle.Center);
            var results = client.Search<Building>(s => s
                .Query(q => q
                    .GeoDistance(g => g
                        .Field(f => f.Location)
                        .DistanceType(GeoDistanceType.Arc)
                        .Location(circle.Center.Lat, circle.Center.Lon)
                        .Distance(circle.Distance.Distance, distanceUnit)
                        .ValidationMethod(GeoValidationMethod.IgnoreMalformed)
                        )
                    )
                .Sort(ss => ss
                    .GeoDistance(g => g
                        .Field(f => f.Location)
                        .DistanceType(GeoDistanceType.Arc)
                        .Order(SortOrder.Ascending)
                        .Unit(distanceUnit)
                        .Mode(SortMode.Min)
                        .Points(geoLocation)
                        )
                    )
                );
            return results.Documents;
        }

        public IEnumerable<Building> GetLineDistance(GeoLine line)
        {
            var queryGenerator = new LineQueryGenerator(line, nameof(Building.Location));
            var query = queryGenerator.GetQuery();

            var results = client.Search<Building>(s => s.Query (q=>query));
            Console.WriteLine(System.Text.Encoding.UTF8.GetString(results.ApiCall.RequestBodyInBytes));
                return results.Documents;
        }

    }
}
