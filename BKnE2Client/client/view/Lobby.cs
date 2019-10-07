using BKnE2Client.client.controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BKnE2Client.client.view
{
    public partial class Lobby : Form
    {
        private Controller controller;

        public Lobby(Controller controller)
        {
            InitializeComponent();
            this.controller = controller;
        }
    }
}
