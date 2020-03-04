using Faker.Classes;
using Faker.Models;
using Faker.Repository;
using System.Linq;

namespace Faker.Logic
{
    public class NearestPointFinder
    {
        private readonly IDocumentRepository<Building> documentRepository;

        private GeoPoint center;

        public NearestPointFinder(IDocumentRepository<Building> documentRepository)
        {
            this.documentRepository = documentRepository;
        }

        public GeoPoint GetNearestPoint(GeoPoint geoPoint, GeoDistance distance, int multipleBy)
        {
            center = geoPoint;

            var newDistance = distance.Clone();

            while (true)
            {
                var result = GetData(newDistance);

                if (result == null)
                {
                    newDistance.Distance *= multipleBy;
                    continue;
                }
                
                return new GeoPoint()
                {
                    Lat = result.Location.Latitude,
                    Lon = result.Location.Longitude
                };
            }
        }

        private Building GetData(GeoDistance distance)
        {
            return documentRepository.GetSortedGeoDistance(center, distance).FirstOrDefault();
        }
    }
}
