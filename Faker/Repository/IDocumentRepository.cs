using Faker.Classes;
using System.Collections.Generic;

namespace Faker.Repository
{
    public interface IDocumentRepository <T>
    {
        void Index(T document);

        void IndexMany(IEnumerable<T> documents);

        void Delete(string id);

        T Get(string id);

        IEnumerable<T> GeoDistance(GeoCircle circle);

        IEnumerable<T> GeoPolygon(GeoPolygon polygon);

        IEnumerable<T> GetSortedGeoDistance(GeoCircle circle);

        IEnumerable<T> GetLineDistance(GeoLine line);

    }
}
