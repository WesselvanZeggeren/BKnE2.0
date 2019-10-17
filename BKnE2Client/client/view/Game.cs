using BKnE2Client.client.controller;
using BKnE2Lib;
using BKnE2Lib.data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BKnE2Client.client.view
{
    public partial class GameForm : Form
    {
        private Controller controller;
        private List<Pin> buttons;

        public GameForm(Controller controller)
        {

            InitializeComponent();

            this.controller = controller;
            controller.gameForm = this;

            InitButtons();
            UpdatePlayerList();

            chatTextBox.KeyPress += ChatTextBox_KeyPress;

            SetMessages();
        }

        //Places the buttons in a Pin struct and list
        private void InitButtons()
        {

            this.buttons = new List<Pin>();

            Panel gamePanel = (Panel)Controls["gamePanel"];
            Panel buttonPanel = (Panel)gamePanel.Controls["buttonPanel"];

            foreach (Button button in buttonPanel.Controls.OfType<Button>())
                if (button.Name[0] == 'x' && button.Name[2] == 'y')
                {

                    int x = int.Parse(button.Name.Substring(1, 1));
                    int y = int.Parse(button.Name.Substring(3, 1));

                    buttons.Add(new Pin(x, y, button));
                    button.Click += OnButtonPressed;
                }
        }

        //Send the coordinates to the controller
        private void OnButtonPressed(object sender, EventArgs e)
        {

            Button b = sender as Button;
            int x = int.Parse(b.Name.Substring(1, 1));
            int y = int.Parse(b.Name.Substring(3, 1));
            controller.SendPin(x, y);
        }
                
        //Set the specified pin to the specified color
        public void SetPin(Request obj)
        {

            int x = (int) obj.get("x");
            int y = (int) obj.get("y");

            Color color = JsonConvert.DeserializeObject<Color>(obj.get("color"));
            Button b = GetButton(x, y);

            if(b != null)
            {
                b.ForeColor = System.Drawing.Color.FromArgb(color.r, color.b, color.g);
                b.Text = Config.pinCharacter;
            }
        }

        //Return a specified button
        private Button GetButton(int x, int y)
        {
            foreach(Pin b in buttons)
            {
                if(b.x == x && b.y == y)
                {
                    return b.button;
                }
            }
            return null;
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

    internal struct Pin
    {
        public int x;
        public int y;
        public Button button;

        public Pin(int x, int y, Button button)
        {
            this.x = x;
            this.y = y;
            this.button = button;
        }
    }
}
