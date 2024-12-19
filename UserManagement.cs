using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ricewarehouse
{


    public partial class UserManagement : Form
    {
        private string connectionString = "Provider= Microsoft.ACE.OleDb.12.0;Data Source=riceinventory1.accdb";
        public UserManagement()
        {
            InitializeComponent();
        }

        private void UserManagement_Load(object sender, EventArgs e)
        {

        }



        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {



        }

        private void btnResetPassword_Click_1(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string newPassword = txtpassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Please enter both username and new password.");
                return;
            }

            if (ResetPassword(username, newPassword))
            {
                MessageBox.Show("Password has been reset successfully!");
            }
            else
            {
                MessageBox.Show("Username not found.");
            }
        }
              private bool ResetPassword(string username, string newPassword)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE [UserAcc] SET [Password] = ? WHERE [Username] = ?";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?", newPassword);
                        cmd.Parameters.AddWithValue("?", username);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0; 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }

        }

        private void login_Showpassword_CheckedChanged(object sender, EventArgs e)
        {
            txtNewPassword.PasswordChar = login_Showpassword.Checked ? '\0' : '*';
            txtpassword.PasswordChar = login_Showpassword.Checked ? '\0' : '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Cancel?", "Confirmation Message",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 loginform = new Form1();

                loginform.Show();
                this.Hide();
            }


        }
       

    }
}

