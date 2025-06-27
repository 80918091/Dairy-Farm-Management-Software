using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DairyFarmSystem
{
    public partial class MilkSales : Form
    {
        public MilkSales()
        {
            InitializeComponent();
            FillEmpId();
            populate();
        }

       

        private void label17_Click(object sender, EventArgs e)
        {
            Dashboard ob = new Dashboard();
            ob.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Finance ob = new Finance();
            ob.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Breeding ob = new Breeding();
            ob.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            CowHealth ob = new CowHealth();
            ob.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            milkproduction ob = new milkproduction();
            ob.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            cows ob = new cows();
            ob.Show();
            this.Hide();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\91709\Documents\DairyFarmDp.mdf;Integrated Security=True;Connect Timeout=30");
        private void FillEmpId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select EmpId from EmployeeTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpId", typeof(int));
            dt.Load(Rdr);
            EmpIdCb.ValueMember = "EmpId";
            EmpIdCb.DataSource = dt;

            Con.Close();
        }
        private void MilkSales_Load(object sender, EventArgs e)
        {

        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            int total = Convert.ToInt32(txtPrice.Text) * Convert.ToInt32(txtQuantity.Text);
            txtTotal.Text = "" + total;
        }
        private void populate()
        {
            Con.Open();
            string query = "select * from MilkSalesTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SalesDGV.DataSource = ds.Tables[0];

            Con.Close();
        }

        private void Clear()
        {
            txtPrice.Text = "";
            txtQuantity.Text = "";
            txtClientName.Text = "";
            txtPhoneTb.Text = "";
            txtTotal.Text = "";
            FilterPhone.Text = "";
        }
        private void SaveTransaction()
        {
            {
                try
                {
                    string Sales = "Sales";
                    Con.Open();
                    string Query = "insert into IncomeTbl values('" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + Sales + "','" + txtTotal.Text + "'," + EmpIdCb.SelectedValue.ToString() + ")";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("income save successfully");
                    Con.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (EmpIdCb.SelectedIndex == -1 || txtPrice.Text == "" || txtClientName.Text == "" || txtPhoneTb.Text == "" || txtQuantity.Text == "" || txtTotal.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into MilkSalesTbl values('" + DateTime.Now.ToString("yyyy-MM-dd") + "'," + txtPrice.Text + ",'" + txtClientName.Text + "','" + txtPhoneTb.Text + "'," + EmpIdCb.SelectedValue.ToString() + "," + txtQuantity.Text + "," + txtTotal.Text + ")";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Milk Sold Successfully");
                    Con.Close();
                    populate();
                    SaveTransaction();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clear();
            MessageBox.Show("clear data");
        }

        private void SalesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.Close();
            login back = new login();
            back.Show();
        }
        /* private void Filter()
         {
             Con.Open();
             string query = "select * from MilkSalesTbl where ClientPhone='" +FilterPhone.Text+ "'";
             SqlDataAdapter sda = new SqlDataAdapter(query, Con);
             SqlCommandBuilder builder = new SqlCommandBuilder(sda);
             var ds = new DataSet();
             sda.Fill(ds);
             SalesDGV.DataSource = ds.Tables[0];
             Con.Close();
         }*/


        private void Filter()
        {
            Con.Open();
            string query = "select * from MilkSalesTbl where ClientPhone like '%" + FilterPhone.Text + "%'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SalesDGV.DataSource = ds.Tables[0];

            Con.Close();
        }

        private void FilterPhone_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void ClientAmt_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtClientName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
    
}
