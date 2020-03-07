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
    public class Program
    {
        private static IDocumentRepository<Building> documentRepository;
        public static void Main(string[] args)
        {
            Uri uri = new Uri(ConfigurationManager.AppSettings["Search-Uri"]);
            ClientFactory clientFactory = new ClientFactory(uri, "building");
            documentRepository = new DocumentRepository(clientFactory.GetClient());

            




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
