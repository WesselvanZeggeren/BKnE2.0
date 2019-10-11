using BKnE2Client.client.controller;
using BKnE2Client.client.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BKnE2Client
{
    public partial class Login : Form
    {
        private Controller controller;

        public Login()
        {
            InitializeComponent();
            loginButton.Click += OnLoginButtonPressed;
            registerButton.Click += OnRegisterButtonPressed;
            this.controller = new Controller();
            this.controller.form = this;
        }

        private void OnLoginButtonPressed(object sender, EventArgs e)
        {
            controller.Login(loginNameTB.Text, passwordTB.Text, false);
        }

        private void OnRegisterButtonPressed(object sender, EventArgs e)
        {
            controller.Login(loginNameTB.Text, passwordTB.Text, true);
        }
    }
}
