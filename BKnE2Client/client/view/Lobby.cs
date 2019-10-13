using BKnE2Client.client.controller;
using BKnE2Lib;
using BKnE2Lib.data;
using System;
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
            playButton.Click += PlayButtonPressed;
            UpdatePlayerList();
            SetMessages();
        }

        //NOT CORRECT! ONLY FOR TESTING PURPOSES! *****************************************************************************************************************
        private void PlayButtonPressed(object sender, EventArgs e)
        {
            //GameForm gameForm = new GameForm(controller);
            //gameForm.Show();
            //controller.lobbyForm.Hide();
            //controller.gameForm = gameForm;
            controller.GameRequest(true);
        }

        //Load the older messages in the new Form
        private void SetMessages()
        {
            SharedUIupdate.SetMessages(chatListBox, controller);
        }

        //Send a message to the server when the ENTER is pressed
        private void ChatTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedUIupdate.ChatTextBox_KeyPress(e, chatTextBox, controller);
        }

        //Puts a message in the UI chatbox
        public void AddChat(string msg)
        {
            SharedUIupdate.AddChatMessage(msg, chatListBox, controller);
        }

        //Updates the UI list with players
        public void UpdatePlayerList()
        {
            SharedUIupdate.UpdatePlayerList(playerListBox, controller);
        }

        public void SetServerMessage(string msg)
        {
            SharedUIupdate.SetServerMessage(serverListBox, msg);
        }
    }
}
