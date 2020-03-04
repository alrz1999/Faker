using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Classes
{
    public abstract class GeoShape
    {
        public GeoSearchType SearchType { get; protected set; }
    }
}
