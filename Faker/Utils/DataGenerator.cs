using Faker.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Utils
{
    class DataGenerator : IDataGenerator<Building>
    {

        public IEnumerable<Building> GenerateData(int size)
        {
            Building[] buildings = new Building[size];
            for (int i = 0; i < size; i++)
            {
                GeoLocation location = new GeoLocation(RandomGenerator.GetRandomLat(),RandomGenerator.GetRandomLon());
                string usage = RandomGenerator.GetRandomUsage();
                int age = RandomGenerator.GetRandomAge();
                string postalCode = RandomGenerator.GetRandomPostalCode();
                Building building = new Building(location,age,postalCode,usage);
                buildings[i]= building;
            }
            return buildings;
        }

        
    }
}
