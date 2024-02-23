using Accord;
using Client;
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
    public partial class Login : Form
    {
        Dictionary<string, string> Users = new();

        public Login()
        {
            InitializeComponent();
            Users.Add("Super", "Duper");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == "Admin" && txtPass.Text == "Password")
            {
                ServerForm serverForm = new();
                serverForm.FormClosed += (s, args) => Show();
                Hide();
                serverForm.ShowDialog();
            }
            if (Users.ContainsKey(txtUser.Text) && Users[txtUser.Text] == txtPass.Text)
            {
                ClientForm clientForm = new(txtUser.Text);
                clientForm.FormClosed += (s, args) => Show();
                Hide();
                clientForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Login Failed");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register register = new(Users);
            register.FormClosed += (s, args) => Show();
            Hide();
            register.ShowDialog();
        }
    }
}
