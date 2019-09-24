﻿using BKnE2Server.server.model.client;
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

            prepareClientData();

            if (register)
                clients.Add(new ClientData(generateId(), name, password));

            foreach (ClientData client in clients)
                if (client.name == name && client.password == password)
                    return client;

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

        // file io
        public static void save()
        {

            if (clients.Count() != 0)
            {

                List<string> clientJson = new List<string>();

                foreach (ClientData client in clients)
                    clientJson.Add(JsonConvert.SerializeObject(client));

                string json = JsonConvert.SerializeObject(clientJson);

                File.WriteAllText(Config.jsonPath, json);
            }
        }

        private static void prepareClientData()
        {

            Console.Read();

            if (File.Exists(Config.jsonPath) && clients.Count() == 0)
            {

                Console.WriteLine(clients.Count());
                Console.Read();

                StreamReader reader = new StreamReader(Config.jsonPath);

                string text = "";
                string line;

                while ((line = reader.ReadLine()) != null)
                    text += line;

                  
//                List<string> clientJson = JsonConvert.DeserializeObject<List<string>>(text);

  //              foreach (string client in clientJson)
    //                clients.Add(JsonConvert.DeserializeObject<ClientData>(client));
            }
        }
    }
}
