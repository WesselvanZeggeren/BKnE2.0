using BKnE2Client.client.controller;
using BKnE2Lib;
using BKnE2Lib.data;
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
    public partial class GameForm : Form
    {
        private Controller controller;
        private List<ButtonStruct> buttons;

        public GameForm(Controller controller)
        {
            InitializeComponent();
            this.controller = controller;
            controller.gameForm = this;
            InitButtons();            
        }

        //Places the buttons in a struct and list
        private void InitButtons()
        {
            this.buttons = new List<ButtonStruct>();

            Panel gamePanel = (Panel)Controls["gamePanel"];
            Panel buttonPanel = (Panel)gamePanel.Controls["buttonPanel"];

            foreach (Button button in buttonPanel.Controls.OfType<Button>())
            {
                if (button.Name[0] == 'x' && button.Name[2] == 'y')
                {
                    int x = int.Parse(button.Name.Substring(1, 1));
                    int y = int.Parse(button.Name.Substring(3, 1));
                    buttons.Add(new ButtonStruct(x, y, button));
                    button.Click += OnButtonPressed;
                }
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
            int x = obj.get("x");
            int y = obj.get("y");
            int red = obj.get("r");
            int green = obj.get("g");
            int blue = obj.get("b");

            Button b = GetButton(x, y);

            if(b != null)
            {
                b.ForeColor = System.Drawing.Color.FromArgb(red, green, blue);
                b.Text = "X";
            }
        }

        //Return a specified button
        private Button GetButton(int x, int y)
        {
            foreach(ButtonStruct b in buttons)
            {
                if(b.x == x && b.y == y)
                {
                    return b.button;
                }
            }
            return null;
        }
    }

    internal struct ButtonStruct
    {
        public int x;
        public int y;
        public Button button;

        public ButtonStruct(int x, int y, Button button)
        {
            this.x = x;
            this.y = y;
            this.button = button;
        }
    }
}
