﻿using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Factory
{
    class Client : ElasticClient
    {
        public Client(IConnectionSettingsValues connectionSettings) : base(connectionSettings)
        {
        }
    }
}