using BKnE2Lib;
using BKnE2Server.server.model.client;
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
        public static ClientData login(string name, string password, bool register)
        {

            load();

            foreach (ClientData client in clients)
                if (client.name == name && client.password == password)
                    return client;

            if (register && clients.Count() < 100)
            {

                clients.Add(ClientData.newClient(generateId(), name, password));
                save();

                return login(name, password, false);
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

            if (File.Exists(Config.accountPath) && clients.Count() != 0)
                File.WriteAllText(Config.accountPath, JsonConvert.SerializeObject(clients));
        }

        private static void load()
        {

            if (File.Exists(Config.accountPath) && clients.Count() == 0)
                clients = JsonConvert.DeserializeObject<List<ClientData>>(File.ReadAllText(Config.accountPath));
        }
    }
}
