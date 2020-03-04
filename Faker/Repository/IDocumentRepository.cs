using Faker.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Repository
{
    interface IDocumentRepository <T>
    {
        void Index(T document);

        void IndexMany(IEnumerable<T> documents);

        void Delete(string id);

        T Get(string id);

        IEnumerable<T> GeoDistance(GeoPoint geoPoint, GeoDistance geoDistance);

        IEnumerable<T> GeoPolygon(IEnumerable<GeoPoint> geoPoints);

        IEnumerable<T> GetSortedGeoDistance(GeoPoint geoPoint, GeoDistance radious);

    }
}
