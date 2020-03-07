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
using CoordinateSharp;

namespace Faker.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        private static IDocumentRepository<Building> documentRepository;

        [TestMethod()]
        public void LineQuerTest()
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
                Distance = 1,
                DistanceUnit = GeoDistanceUnit.Kilometers
            };
            GeoLine geoLine = new GeoLine(distance, routePoints);
            var results = documentRepository.GetLineDistance(geoLine);
            
            foreach (var result in results)
            {
                Coordinate resultCoordinate = new Coordinate(result.Location.Latitude,result.Location.Longitude);
                foreach (var point in routePoints)
                {
                    Coordinate routePointCoordinate = new Coordinate(point.Lat, point.Lon);
                    if(resultCoordinate.Get_Distance_From_Coordinate(routePointCoordinate).Meters <= distance.Distance)
                        break;
                    Assert.Fail($"point with lat = {point.Lat} and lon = {point.Lon} shouldn't be in search result");
                }
            }
        }
    }
}