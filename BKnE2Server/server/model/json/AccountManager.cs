using BKnE2Server.server.model.client;
using BKnE2Server.server.model.helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Server.server.model.json
{

    static class AccountManager
    {

        // attributes
        private static List<ClientData> clients = new List<ClientData>();

        // login
        public static ClientData login(string name, string password, bool register = false)
        {

            load();

            foreach (ClientData client in clients)
                if (client.name == name && client.password == password)
                    return client;

            if (register)
            {

                clients.Add(ClientData.newClient(generateId(), name, password));
                return login(name, password);
            }

            return null;
        }

        private static int generateId()
        {

            int id = 0;

            foreach (ClientData client in clients)
                if (client.id > id)
                    id = client.id;

            return (id + 1);
        }

        public static void writeClients()
        {

            foreach (ClientData client in clients)
            {

                Console.WriteLine(client);
            }
        }

        // file io
        public static void save()
        {

            if (File.Exists(Config.jsonPath) && clients.Count() != 0)
            {

                File.WriteAllText(Config.jsonPath, JsonConvert.SerializeObject(clients));
            }
        }

        private static void load()
        {

            if (File.Exists(Config.jsonPath) && clients.Count() == 0)
            {

                Console.WriteLine(clients.Count());
                Console.ReadLine();

                clients = JsonConvert.DeserializeObject<List<ClientData>>(File.ReadAllText(Config.jsonPath));

                Console.WriteLine(clients.Count());
                Console.ReadLine();
            }
        }
    }
}
