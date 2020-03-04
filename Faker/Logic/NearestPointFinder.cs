using Faker.Classes;
using Faker.Models;
using Faker.Repository;
using System.Linq;

namespace Faker.Logic
{
    class NearestPointFinder
    {
        private readonly GeoPoint geoPoint;
        private readonly GeoDistance distance;
        private readonly int multipleBy;

        public NearestPointFinder(GeoPoint geoPoint, GeoDistance distance, int multipleBy) 
        {
            this.geoPoint = geoPoint;
            this.distance = distance;
            this.multipleBy = multipleBy;
        }

        public GeoPoint GetNearestPoint(IDocumentRepository<Building> documentRepository)
        {

            while (true)
            {
                var result = documentRepository.GetSortedGeoDistance(geoPoint, distance);

                if (result.Count() == 0)
                {
                    distance.Distance *= multipleBy;
                    continue;
                }
                
                return new GeoPoint()
                {
                    Lat = result.First().Location.Latitude,
                    Lon = result.First().Location.Longitude
                };
            }
        }
    }
}
