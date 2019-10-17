using BKnE2Client.client.controller;
using BKnE2Lib.data;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BKnE2Client.client.view
{

    /*  This class was made for the Lobby and Game classes
     *  It isn't possible to derive from more than 1 class in C#
     *  Best solution we had afterward: make a static class
     */

    static class SharedUIupdate
    {

        //Send a message when the enter key is pressed
        public static void ChatTextBox_KeyPress(KeyPressEventArgs e, TextBox chatTextBox, Controller controller)
        {

            if (e.KeyChar == (char)Keys.Return)
                if (!(chatTextBox.Text == ""))
                {
                    controller.SendMessage(controller.loginForm.loginName, chatTextBox.Text);
                    chatTextBox.Text = string.Empty;
                }
        }

        //Add a message to the chat
        public static void AddChatMessage(string msg, ListBox chatListBox, Controller controller)
        {

            int maxCharacters = 25;
            Stack<string> messages = new Stack<string>();

            for (int i = 0; i < msg.Length; i += maxCharacters)
                if (msg.Length - maxCharacters - i >= 0)
                    messages.Push(msg.Substring(i, maxCharacters) + "-");
                else
                    messages.Push(msg.Substring(i, msg.Length - i));

            int msgLength = messages.Count();

            for (int i = 0; i < msgLength; i++)
                if (!messages.Peek().Trim().Equals("-"))
                    chatListBox.Items.Insert(0, messages.Pop());
                else
                    messages.Pop();

            controller.messages = chatListBox.Items;
        }

        //Add all the gained players to the playerListBox
        public static void UpdatePlayerList(ListBox playerListBox, Controller controller)
        {

            playerListBox.Items.Clear();

            foreach (Player player in controller.players)
                playerListBox.Items.Insert(0, player.name);
        }

        //Set the messages back after reloading form
        public static void SetMessages(ListBox chatListBox, Controller controller)
        {

            if (controller.messages != null)
                foreach (string msg in controller.messages)
                    AddChatMessage(msg, chatListBox, controller);
        }

        public static void SetServerMessage(ListBox serverListbox, string msg)
        {

            serverListbox.Items.Insert(0, msg);
        }
    }
}
