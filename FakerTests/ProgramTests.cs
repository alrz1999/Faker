using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faker.Classes;
using Faker.Factory;
using Faker.Repository;
using Faker.Models;
using System.Configuration;


namespace Faker.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        private static IDocumentRepository<Building> documentRepository;

        [TestMethod()]
        public void MainTest()
        {
            Uri uri = new Uri("http://localhost:9200");
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
            Assert.Fail();
        }
    }
}