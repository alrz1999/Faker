using Faker.Classes;
using Faker.Factory;
using Faker.Logic;
using Faker.Models;
using Faker.Repository;
using System;
using System.Configuration;

namespace Faker
{
    class Program
    {
        private static IDocumentRepository<Building> documentRepository;
        static void Main(string[] args)
        {
            Uri uri = new Uri(ConfigurationManager.AppSettings["Search-Uri"]);
            ClientFactory clientFactory = new ClientFactory(uri, "building");
            documentRepository = new DocumentRepository(clientFactory.GetClient());

            //var routePoints = new List<GeoPoint>();
            //var distance = new GeoDistance() {
            //    Distance = 1000,
            //    DistanceUnit = GeoDistanceUnit.Meters
            //};
            //var foundPoints = new HashSet<Building>();

            //foreach (var point in routePoints)
            //{
            //    foundPoints.UnionWith(documentRepository.GeoDistance(point,distance));
            //}
            //for (int i = 0; i < routePoints.Count - 1; i++)
            //{
            //    var startPoint = routePoints[i];
            //    var endPoint = routePoints[i + 1];
            //    var boundingBoxFinder = new BoundingBoxFinder(distance, startPoint,endPoint);
            //    var results = documentRepository.GeoPolygon(boundingBoxFinder.GetBoundingBox());
            //    foundPoints.UnionWith(results);
            //}

            //foreach (var point in foundPoints)
            //{
            //    Console.WriteLine(point.Location);
            //}
            Console.WriteLine("Enter Latitude :");
            var lat = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter Longitude :");
            var lon = Convert.ToDouble(Console.ReadLine());

            var coordinate = new GeoPoint()
            {
                Lat = lat,
                Lon = lon
            };
            var radious = new GeoDistance()
            {
                Distance = 100,
                DistanceUnit = GeoDistanceUnit.Meters
            };
            var multipleBy = 10;
            NearestPointFinder nearestPointFinder = new NearestPointFinder(documentRepository);

            Console.WriteLine(nearestPointFinder.GetNearestPoint(coordinate,radious,multipleBy));
        }
    }
}
