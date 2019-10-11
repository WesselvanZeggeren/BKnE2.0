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
            chatTextBox.KeyPress += ChatTextBox_KeyPress;
        }

        //Send a message to the server when the ENTER is pressed
        private void ChatTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Return)
            {
                controller.SendMessage(chatTextBox.Text);
                chatTextBox.Text = string.Empty;
            }
        }
    }
}
