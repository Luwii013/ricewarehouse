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
    public partial class Inventory : Form
    {
        OleDbConnection conn;
        OleDbCommand cmd;
        OleDbDataAdapter adapter;
        DataTable dt;
        public Inventory()
        {
            InitializeComponent();
        }
        void GetUsers()
        {
            
            conn = new OleDbConnection("Provider= Microsoft.ACE.OleDb.12.0;Data Source=riceinventory1.accdb");
           
            dt = new DataTable();
           
            adapter = new OleDbDataAdapter("SELECT * FROM Inventory1", conn);
           
            conn.Open();
           
            adapter.Fill(dt);
           
            dgvInventory.DataSource = dt;

            conn.Close();

        }






        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 obj = new Form3();
            obj.Show();
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO Inventory1 (BRAND, [UNIT_PRICE(KG)], Quantity) VALUES" +
                   "(@BRAND, @UNIT_PRICE_KG, @Quantity)";
            cmd = new OleDbCommand(query,conn);
            cmd.Parameters.AddWithValue("@BRAND", tbBrand.Text);
            cmd.Parameters.AddWithValue("@UNIT_PRICE_KG", tbUnitPrice.Text);
            
            cmd.Parameters.AddWithValue("@Quantity", tbQuantity.Text); 
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("New Brand Added!");
            GetUsers();
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            GetUsers();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string query = " UPDATE Inventory1 " +
                 " SET BRAND = @BRAND, [UNIT_PRICE(KG)] = @UNIT_PRICE_KG,Quantity = @Quantity " +
                 " WHERE ID = Product_ID ";
            cmd = new OleDbCommand(query, conn);
            cmd.Parameters.AddWithValue("@BRAND", tbBrand.Text);
            cmd.Parameters.AddWithValue("@UNIT_PRICE_KG", tbUnitPrice.Text);
           
            cmd.Parameters.AddWithValue("@Quantity", tbQuantity.Text);
            cmd.Parameters.AddWithValue("@Product_ID", Convert.ToInt32(tbProID.Text));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("UPDATED!");
            GetUsers();


        }

        private void dgvInventory_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
           tbProID.Text=dgvInventory.CurrentRow.Cells[0].Value.ToString();
            tbBrand.Text = dgvInventory.CurrentRow.Cells[1].Value.ToString();
           tbUnitPrice.Text = dgvInventory.CurrentRow.Cells[2].Value.ToString();
            
           tbQuantity.Text = dgvInventory.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string query = " DELETE FROM Inventory1 WHERE Product_ID = @Product_ID";
            cmd = new OleDbCommand(query, conn);
            cmd.Parameters.AddWithValue("@Product_ID", Convert.ToInt32(tbProID.Text));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("ALREADY DELETED!");
            GetUsers();

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = "SELECT * FROM Inventory1 WHERE Product_ID LIKE @Product_ID";
            adapter = new OleDbDataAdapter(searchQuery, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@Product_ID", txtSearch.Text + "%");

            dt = new DataTable();
            conn.Open();
            adapter.Fill(dt);
            conn.Close();
            dgvInventory.DataSource = dt;

        }

        private void tbSupplier_SelectedIndexChanged(object sender, EventArgs e)
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
    }
}
