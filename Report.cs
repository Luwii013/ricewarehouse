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
using System.Xml;
using System.IO;
using System.Drawing.Printing;

namespace ricewarehouse
{
    public partial class Report : Form
    {
        private string connectionString = "Provider = Microsoft.ACE.OleDb.12.0; Data Source = riceinventory1.accdb";
        private PrintDocument printDocument;
       
        public Report()
        {
            InitializeComponent();

            printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
        }



        private void dgvInventory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 obj = new Form3();
            obj.Show();
            this.Hide();
        }






        private void Report_Load(object sender, EventArgs e)
        {

            {
                LoadReportData();
            }
        }
        private void LoadReportData()
        {
            DataTable reportData = GetReportData();
            dgvInventory.DataSource = reportData;
        }
        private DataTable GetReportData()
        {
            DataTable reportData = new DataTable();
            string query = "SELECT i.Product_ID, i.BRAND, i.[UNIT_PRICE(KG)], i.Quantity, Now() AS TIME_AND_DATE " +
                           "FROM Inventory1 i";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connection);
                    dataAdapter.Fill(reportData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }

            return reportData;
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (printDocument != null)
            {
                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = printDocument;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.Print();
                }

            }
            else
            {
                MessageBox.Show("PrintDocument object is not initialized.");
            }
        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font font = new Font("Arial", 10);
            float x = 50;
            float y = 50;
            int rowHeight = 30;

            
            foreach (DataGridViewColumn column in dgvInventory.Columns)
            {
                g.DrawString(column.HeaderText, font, Brushes.Black, x, y);
                x += column.Width + 15;
            }
            y += rowHeight;
            x = 50;

           
            for (int i = 0; i < dgvInventory.Rows.Count; i++)
            {
                DataGridViewRow row = dgvInventory.Rows[i];
                foreach (DataGridViewCell cell in row.Cells)
                {
                    g.DrawString(cell.Value.ToString(), font, Brushes.Black, x, y);
                    x += cell.Size.Width + 8;
                }
                y += rowHeight;
                x = 50;

                if (y + rowHeight > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }
            e.HasMorePages = false;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            saveFileDialog.Title = "Save Report as CSV";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                    {
                       
                        for (int i = 0; i < dgvInventory.Columns.Count; i++)
                        {
                            sw.Write(dgvInventory.Columns[i].HeaderText);
                            if (i < dgvInventory.Columns.Count - 1)
                                sw.Write(",");
                        }
                        sw.WriteLine();

                       
                        foreach (DataGridViewRow row in dgvInventory.Rows)
                        {
                            for (int i = 0; i < dgvInventory.Columns.Count; i++)
                            {
                               
                                string value = row.Cells[i].Value?.ToString() ?? "";
                                sw.Write(value);
                                if (i < dgvInventory.Columns.Count - 1)
                                    sw.Write(",");
                            }
                            sw.WriteLine();
                        }
                    }

                    MessageBox.Show("Report downloaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error downloading report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
