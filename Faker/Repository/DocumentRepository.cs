using Faker.Classes;
using Faker.Factory;
using Faker.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IEnumerable<Building> GeoDistance(GeoPoint geoPoint, GeoDistance geoDistance)
        {
            DistanceUnit distanceUnit = ConvertToDistanceUnit(geoDistance);
            var results = client.Search<Building>(s => s
                .Query(q => q
                    .GeoDistance(g => g
                        .Field(f => f.Location)
                        .DistanceType(GeoDistanceType.Arc)
                        .Location(geoPoint.Lat, geoPoint.Lon)
                        .Distance(geoDistance.Distance, distanceUnit)
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

        public IEnumerable<Building> GeoPolygon(IEnumerable<GeoPoint> geoPoints)
        {
            var geoLocations = ConvertToGeoLocation(geoPoints);
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
            return new GeoLocation(geoPoint.Lat,geoPoint.Lon);
        }

        public IEnumerable<Building> GetSortedGeoDistance(GeoPoint geoPoint, GeoDistance radious)
        {
            DistanceUnit distanceUnit = ConvertToDistanceUnit(radious);
            GeoLocation geoLocation = ConvertToGeoLocation(geoPoint);
            var results = client.Search<Building>(s => s
                .Query(q => q
                    .GeoDistance(g => g
                        .Field(f => f.Location)
                        .DistanceType(GeoDistanceType.Arc)
                        .Location(geoPoint.Lat, geoPoint.Lon)
                        .Distance(radious.Distance, distanceUnit)
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
    }
}
