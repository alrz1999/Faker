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
        }
    }
}
