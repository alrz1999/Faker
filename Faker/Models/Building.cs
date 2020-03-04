using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Models
{
    public class Building
    {
        

        public Building(GeoLocation location, int age, string postalCode, string usage)
        {
            Location = location;
            Age = age;
            PostalCode = postalCode;
            Usage = usage;
        }

        [GeoPoint(Name = "location")    ]
        public GeoLocation Location { get; set; }

        public int Age { get; set; }

        public string PostalCode { get; set; }

        public string Usage { get; set; }



    }
}
