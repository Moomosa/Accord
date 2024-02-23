using Accord;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class Register : Form
    {
        Dictionary<string, string> Users;

        public Register(Dictionary<string, string> Users)
        {
            InitializeComponent();
            this.Users = Users;
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            if (!Users.ContainsKey(txtUser.Text))
            {
                if (txtUser.Text != "" && txtPass1.Text == txtPass2.Text)
                {
                    string newUser = txtUser.Text;
                    string newPass = txtPass1.Text;
                    Users.Add(newUser, newPass);
                    MessageBox.Show($"You've successfully registered as {newUser}.");
                    Close();
                }
                else MessageBox.Show("Please enter valid credentials.");
            }
            else MessageBox.Show("Another user has that username.");
        }
    }
}
