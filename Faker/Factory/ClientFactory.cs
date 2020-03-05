using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Factory
{
    class ClientFactory
    {
        private IConnectionSettingsValues connectionSetting;

        public ClientFactory(Uri uri, string defaultIndex)
        {
            this.connectionSetting = new ConnectionSettings(uri).DefaultIndex(defaultIndex).EnableDebugMode().DisableDirectStreaming().PrettyJson();
        }



        public Client GetClient()
        {
            return new Client(this.connectionSetting);
        }
    }
}
