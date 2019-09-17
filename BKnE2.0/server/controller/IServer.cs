using BKnE2._0.server.model.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2._0.server.controller
{

    interface IObserver
    {

        string host { get; }

        void receiveClient(Client client);
    }
}
