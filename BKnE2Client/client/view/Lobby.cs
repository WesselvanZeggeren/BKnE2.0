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
    public partial class LobbyForm : Form
    {
        private Controller controller;

        public LobbyForm(Controller controller)
        {
            InitializeComponent();
            this.controller = controller;
            this.controller.lobbyForm = this;
            chatTextBox.KeyPress += ChatTextBox_KeyPress;
            UpdatePlayerList();
        }

        //Send a message to the server when the ENTER is pressed
        private void ChatTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Return)
            {
                if(!(chatTextBox.Text == ""))
                {
                    controller.SendMessage(controller.loginForm.loginName + ": " + chatTextBox.Text);
                    chatTextBox.Text = string.Empty;
                }
            }
        }

        //Puts a message in the UI chatbox
        public void AddChat(string msg)
        {
            int maxCharacters = 25;
            Stack<string> messages = new Stack<string>();

            for (int i = 0; i < msg.Length; i += maxCharacters)
            {
                if (msg.Length - maxCharacters - i >= 0)
                {
                    messages.Push(msg.Substring(i, maxCharacters) + "-");
                }
                else
                {
                    messages.Push(msg.Substring(i, msg.Length - i));
                }
            }

            int msgLength = messages.Count();
            for(int i = 0; i < msgLength; i++)
            {
                if (!messages.Peek().Trim().Equals("-"))
                {
                    chatListBox.Items.Insert(0, messages.Pop());
                } else
                {
                    messages.Pop();
                }
            }
        }

        //Updates the UI list with players
        public void UpdatePlayerList()
        {
            playerListBox.Items.Clear();

            foreach(string name in controller.players)
            {
                playerListBox.Items.Insert(0, name);
            }
        }
    }
}
