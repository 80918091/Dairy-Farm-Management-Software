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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            Finance();
            Logistic();
            GetMax();
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

        private void label16_Click(object sender, EventArgs e)
        {
            Finance ob = new Finance();
            ob.Show();
            this.Hide();
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\91709\Documents\DairyFarmDp.mdf;Integrated Security=True;Connect Timeout=30");
        private void Finance()
        {
            Con.Open();

            SqlDataAdapter sda = new SqlDataAdapter("select sum(IncAmt) from IncomeTbl", Con);
            SqlDataAdapter sda1 = new SqlDataAdapter("select sum(ExpAmount) from ExpenditureTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            int inc, exp1;
            double bal;
            inc = Convert.ToInt32(dt.Rows[0][0].ToString());
            IncLbl.Text = "Rs"+dt.Rows[0][0].ToString();
            
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            exp1= Convert.ToInt32(dt1.Rows[0][0].ToString());
            ExpLbl.Text = "Rs"+dt1.Rows[0][0].ToString();

            bal = inc - exp1;
            BalLbl.Text = "Rs" +bal;
            Con.Close();
        }

        private void Logistic()
        {
            Con.Open();

            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from CowTbl", Con);
           SqlDataAdapter sda1 = new SqlDataAdapter("select sum(TotalMilk) from MilkTbl", Con);
            SqlDataAdapter sda2 = new SqlDataAdapter("select count(*) from EmployeeTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            
            CowsNumLbl.Text = dt.Rows[0][0].ToString();

            DataTable dt1 = new DataTable();
           sda1.Fill(dt1);  
            MilkNumLbl.Text = dt1.Rows[0][0].ToString()+"Litters";
            Con.Close();

            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            EmpNumLbl.Text = dt2.Rows[0][0].ToString();
            Con.Close();
        }

        private void GetMax()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select max(IncAmt) from IncomeTbl", Con);
            SqlDataAdapter sda1 = new SqlDataAdapter("select max(ExpAmount) from ExpenditureTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            HighAmtLbl.Text = "Rs" + dt.Rows[0][0].ToString();
            //  HighDateLbl.Text = dt.Rows[0][1].ToString();
            HighExpLbl.Text = "Rs"+ dt1.Rows[0][0].ToString();
           
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void IncLbl_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            this.Close();
            login back = new login();
            back.Show();

        }
    }
}
