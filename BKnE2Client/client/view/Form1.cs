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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            loginButton.Click += loginButtonPressed;
            registerButton.Click += registerButtonPressed;
        }

        private void loginButtonPressed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void registerButtonPressed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
