using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ricewarehouse
{
    public partial class Form1 : Form
    {

        private OleDbConnection conn;
        public Form1()
        {
            InitializeComponent();
            conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=riceinventory1.accdb;");
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            conn.Open();

            
            string query = "SELECT ID FROM UserAcc WHERE Username = @username AND Password = @password";
            OleDbCommand cmd = new OleDbCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", txtUsername.Text);
            cmd.Parameters.AddWithValue("@password", txtPassword.Text);

            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                int userId = (int)result;

                
         

               
                MessageBox.Show("Login Successfully");
                
                Form2 logoutForm = new Form2(userId);
                logoutForm.Show();
                this.Hide();
               
            }
            else
            {
                MessageBox.Show("Invalid Username or Password.");
            }

            conn.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void login_Showpassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = login_Showpassword.Checked ? '\0' : '*';
        }

        private void label5_Click(object sender, EventArgs e)
        {
            UserManagement obj = new UserManagement();
            obj.Show();
            this.Hide();
        }
    }
}
