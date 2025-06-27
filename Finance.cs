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
    public partial class Finance : Form
    {
        public Finance()
        {
            InitializeComponent();
            populateExp();
            populateInc();
        }

       

        private void label17_Click(object sender, EventArgs e)
        {
            Dashboard ob = new Dashboard();
            ob.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            milkproduction ob = new milkproduction();
            ob.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            CowHealth ob = new CowHealth();
            ob.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Breeding ob = new Breeding();
            ob.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            MilkSales ob = new MilkSales();
            ob.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            cows ob = new cows();
            ob.Show();
            this.Hide();

        }

        private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\91709\Documents\DairyFarmDp.mdf;Integrated Security=True;Connect Timeout=30");
        
        private void populateExp()
        {
            Con.Open();
            string query = "select * from IncomeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            IncDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void populateInc()
        {
            Con.Open();
            string query = "select * from ExpenditureTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ExpDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void FilterIncome()
        {
            Con.Open();
            string query = "select * from IncomeTbl where IncDate='"+Incomedatefilter.Value.Date+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ExpDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void FilterExp()
        {
            Con.Open();
            string query = "select * from ExpenditureTbl where ExpDate='"+ExpDateFilter.Value.Date+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ExpDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void ClearExp()
        {
            AmountTb.Text = "";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (PurposeCb.SelectedIndex == -1 || AmountTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into ExpenditureTbl values('" +DateTime.Now.ToString("yyyy-MM-dd") + "','" + PurposeCb.SelectedItem.ToString() + "'," + AmountTb.Text + "," + EmpIdLbl.Text + ")";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Expenditure Saved Successfully");
                    Con.Close();
                    populateExp();
                    ClearExp();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void CowsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void ClearInc()
        {
            IncCb.SelectedIndex = -1;
            IncAmount.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (IncCb.SelectedIndex == -1 || IncAmount.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into IncomeTbl values('" + DateTime.Now.ToString("yyyy-MM-dd")+ "','" + IncCb.SelectedItem.ToString() + "','" + IncAmount.Text + "'," + EmpIdLbl.Text + ")";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Income Saved Successfully");
                    Con.Close();
                    populateInc();
                    ClearInc();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void ExpDateFilter_ValueChanged(object sender, EventArgs e)
        {
            FilterExp();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void IncomeDate_ValueChanged(object sender, EventArgs e)
        {
          
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            FilterIncome();
        }

        private void Finance_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
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
    }
}
