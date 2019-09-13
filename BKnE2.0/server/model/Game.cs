using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BKnE2._0.server.model.entity
{

    class Game
    {

        private List<Client> clients;
        private Thread thread;

        public Game()
        {


        }

        public void addClient(Client client)
        {

            this.clients.Add(client);
        }

        public bool isRunning()
        {

            return false;
        }
    }
}
