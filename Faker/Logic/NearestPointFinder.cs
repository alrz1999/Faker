using Faker.Classes;
using Faker.Models;
using Faker.Repository;
using System.Linq;

namespace Faker.Logic
{
    public class NearestPointFinder
    {
        private readonly IDocumentRepository<Building> documentRepository;

        private GeoCircle circle;

        public NearestPointFinder(IDocumentRepository<Building> documentRepository)
        {
            this.documentRepository = documentRepository;
        }

        public GeoPoint GetNearestPoint(GeoCircle circle, int multipleBy)
        {
            this.circle = circle.Clone();

            while (true)
            {
                var result = GetData();

                if (result == null)
                {
                    this.circle.Distance.Distance *= multipleBy;
                    continue;
                }
                
                return new GeoPoint()
                {
                    Lat = result.Location.Latitude,
                    Lon = result.Location.Longitude
                };
            }
        }

        private Building GetData()
        {
            return documentRepository.GetSortedGeoDistance(circle).FirstOrDefault();
        }
    }
}
