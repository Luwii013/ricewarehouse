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
using System.Net.NetworkInformation;

namespace ricewarehouse
{
    public partial class SupplierManagement : Form
    {
        OleDbConnection conn;
        OleDbCommand cmd;
        OleDbDataAdapter adapter;
        DataTable dt;
        public SupplierManagement()
        {
            InitializeComponent();
        }
        void GetUsers()
        {
            
            conn = new OleDbConnection("Provider= Microsoft.ACE.OleDb.12.0;Data Source=riceinventory1.accdb");
            
            dt = new DataTable();
           
            adapter = new OleDbDataAdapter("SELECT * FROM Supplier_management", conn);
            
            conn.Open();
           
            adapter.Fill(dt);
           
            dgvInventory.DataSource = dt;

            conn.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 obj = new Form4();
            obj.Show();
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO Supplier_Management ([Supplier_Name],[Type_of_Rice], [Quantity], [Time_and_Date]) VALUES" +
                   "(@Supplier_Name, @Type_of_Rice, @Quantity, @Time_and_Date)";
            cmd = new OleDbCommand(query, conn);
            cmd.Parameters.AddWithValue("@Supplier_Name", tbSupName.Text);
            cmd.Parameters.AddWithValue("@Type_of_Rice", tbRice.Text);
            cmd.Parameters.AddWithValue("@Quantity", tbQuant.Text);
            cmd.Parameters.AddWithValue("@Time_and_Date",dtpsup.Value);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("New Supplier Added!");
            GetUsers();
        }

        private void SupplierManagement_Load(object sender, EventArgs e)
        {
            GetUsers();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Supplier_Management " +
        "SET Supplier_Name = @Supplier_Name, Type_of_Rice = @Type_of_Rice, Quantity = @Quantity, Time_and_Date = @Time_and_Date " +
        "WHERE ID = Supplier_ID";
            cmd = new OleDbCommand(query, conn);
            cmd.Parameters.AddWithValue("@Supplier_Name", tbSupName.Text);
            cmd.Parameters.AddWithValue("@Type_of_Rice", tbRice.Text);
            cmd.Parameters.AddWithValue("@Quantity", tbQuant.Text);
            cmd.Parameters.AddWithValue("@Time_and_Date", dtpsup.Value);
            cmd.Parameters.AddWithValue("@Supplier_ID", Convert.ToInt32(tbSupId.Text));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("UPDATED!");
            GetUsers();
        }

        private void dgvInventory_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            tbSupId.Text = dgvInventory.CurrentRow.Cells[0].Value.ToString();
            tbSupName.Text = dgvInventory.CurrentRow.Cells[1].Value.ToString();
           tbRice.Text = dgvInventory.CurrentRow.Cells[2].Value.ToString();
            tbQuant.Text = dgvInventory.CurrentRow.Cells[3].Value.ToString();
            dtpsup.Text = dgvInventory.CurrentRow.Cells[4].Value.ToString();




        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string query = " DELETE FROM Supplier_Management WHERE Supplier_ID = @supplier_ID";
            cmd = new OleDbCommand(query, conn);
            cmd.Parameters.AddWithValue("@Supplier_ID", Convert.ToInt32(tbSupId.Text));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("ALREADY DELETED!");
            GetUsers();
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = "SELECT * FROM Supplier_Management WHERE Supplier_ID LIKE @Supplier_ID";
            adapter = new OleDbDataAdapter(searchQuery, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@i", txtSearch.Text + "%");

            dt = new DataTable();
            conn.Open();
            adapter.Fill(dt);
            conn.Close();
            dgvInventory.DataSource = dt;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
