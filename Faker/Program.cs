using Faker.Classes;
using Faker.Factory;
using Faker.Logic;
using Faker.Models;
using Faker.Repository;
using Nest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

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

            var routePoints = new List<GeoPoint>(){
                new GeoPoint(){ Lat = 20, Lon = 20},
                new GeoPoint(){ Lat = 20.1, Lon = 20.2},
                new GeoPoint(){ Lat = 20.3, Lon = 20.3},
                new GeoPoint(){ Lat = 20.3, Lon = 20.4},
                new GeoPoint(){ Lat = 20.5, Lon = 20.5},
                new GeoPoint(){ Lat = 20.6, Lon = 20.5},
                new GeoPoint(){ Lat = 20.9, Lon = 20.7},
                new GeoPoint(){ Lat = 21, Lon = 21},
            };
            var distance = new GeoDistance()
            {
                Distance = 100,
                DistanceUnit = GeoDistanceUnit.Kilometers
            };
            GeoLine geoLine = new GeoLine(distance, routePoints);


            var results = documentRepository.GetLineDistance(geoLine);
            foreach (var result in results)
            {
                Console.WriteLine(result.Location);
            }




            //Console.WriteLine("Enter Latitude :");
            //var lat = Convert.ToDouble(Console.ReadLine());
            //Console.WriteLine("Enter Longitude :");
            //var lon = Convert.ToDouble(Console.ReadLine());

            //var center = new GeoPoint()
            //{
            //    Lat = lat,
            //    Lon = lon
            //};
            //var distance = new GeoDistance()
            //{
            //    Distance = 100,
            //    DistanceUnit = GeoDistanceUnit.Meters
            //};

            //var circle = new GeoCircle(center, distance);
            //var multipleBy = 10;

            //NearestPointFinder nearestPointFinder = new NearestPointFinder(documentRepository);

            //Console.WriteLine(nearestPointFinder.GetNearestPoint(circle, multipleBy));
        }
    }
}
