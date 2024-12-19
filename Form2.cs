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
using System.Net;

namespace ricewarehouse
{
    public partial class Form2 : Form
    {
        private OleDbConnection conn;
        private int userId;
       


        public Form2(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=rice.accdb;");

        }


        public Form2()
        {
        }


        private void btnLogout_Click(object sender, EventArgs e)
        {
            


            if (MessageBox.Show("Are you sure you want to Log out?", "Confirmation Message",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 loginform = new Form1();
                
                loginform.Show();
                this.Hide();
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close?", "Confirmation Message",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Report obj = new Report();
            obj.Show();
            this.Hide();

        }


        private void button6_Click(object sender, EventArgs e)
        {


          
             

           






        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form3 obj = new Form3();
            obj.Show();
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form4 obj = new Form4();
            obj.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 obj = new Form5();
            obj.Show();
            this.Hide();
        }
    }
}

