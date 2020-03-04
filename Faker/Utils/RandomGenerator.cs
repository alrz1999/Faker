using Faker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Utils
{
    class RandomGenerator
    {
        private const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const int POSTAL_CODE_LENGTH = 10;
        private static readonly Random random = new Random();


        public static int GetRandomAge()
        {
            return random.Next(1, 50);
        }

        public static double GetRandomLat()
        {
            return (random.NextDouble() - (0.5)) * 180;
        }

        public static double GetRandomLon()
        {
            return (random.NextDouble() - (0.5)) * 360;
        }

        public static string GetRandomPostalCode()
        {
            return new string(Enumerable.Repeat(CHARS, POSTAL_CODE_LENGTH)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetRandomUsage()
        {
            return Enum.GetNames(typeof(Usage))[random.Next(0,3)];
        }
    }
}
