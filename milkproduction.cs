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
    public partial class milkproduction : Form
    {
        public milkproduction()
        {
            InitializeComponent();
            FillCowId();
            populate();
        }

       

        private void label5_Click(object sender, EventArgs e)
        {
            cows ob = new cows();
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

        private void label16_Click(object sender, EventArgs e)
        {
            Finance ob = new Finance();
            ob.Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            Dashboard ob = new Dashboard();
            ob.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CowIdCb.SelectedIndex == -1 || Cownametb.Text == "" || AmTb.Text == "" || PmTb.Text == "" || TotalTb.Text == "" )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into MilkTbl values(" + CowIdCb.SelectedValue.ToString() + ",'" + Cownametb.Text + "','" + AmTb.Text + "','" + PmTb.Text+ "','" + TotalTb.Text + "','"+DateTime.Now.ToString("yyyy-MM-dd")+"')";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Milk Saved Successfully");
                    Con.Close();
                    populate();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\91709\Documents\DairyFarmDp.mdf;Integrated Security=True;Connect Timeout=30");
        private void FillCowId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CowId from CowTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CowId", typeof(int));
            dt.Load(Rdr);
            CowIdCb.ValueMember = "CowId";
            CowIdCb.DataSource = dt;

            Con.Close();
        }
        private void populate()
        {
            Con.Open();
            string query = "select * from MilkTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            MilkDGV.DataSource = ds.Tables[0];

            Con.Close();
        }

        private void milkproduction_Load(object sender, EventArgs e)
        {
           
        }
       
        private void Clear()
        {
            Cownametb.Text = "";
            AmTb.Text = "";
          //  NoonTb.Text = "";
            PmTb.Text = "";
            TotalTb.Text = "";
            key = 0;

        }
        private void GetCowname()
        {
            Con.Open();
            string query="select * from CowTbl where CowId="+CowIdCb.SelectedValue.ToString()+"";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                Cownametb.Text = dr["CowName"].ToString();

            }
            Con.Close();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            Clear();
            MessageBox.Show("clear data");
        }

        private void CowIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCowname();
        }

      

        

        
        int key = 0;
        private void MilkDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MilkDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {

                CowIdCb.SelectedValue = MilkDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
                Cownametb.Text = MilkDGV.Rows[e.RowIndex].Cells[2].Value.ToString();
                AmTb.Text = MilkDGV.Rows[e.RowIndex].Cells[3].Value.ToString();
               // NoonTb.Text = MilkDGV.Rows[e.RowIndex].Cells[4].Value.ToString();
                PmTb.Text = MilkDGV.Rows[e.RowIndex].Cells[4].Value.ToString();
                TotalTb.Text = MilkDGV.Rows[e.RowIndex].Cells[5].Value.ToString();
                Date.Text = MilkDGV.Rows[e.RowIndex].Cells[6].Value.ToString();
                if (Cownametb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(MilkDGV.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
            }
        }

        

      
        
        private void button4_Click(object sender, EventArgs e)
        {

            if (key == 0)
            {
                MessageBox.Show("Selected The Milk Product To Be Deleted");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "delete from MilkTbl where MId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted Successfully");
                    Con.Close();
                    populate();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CowIdCb.SelectedIndex == -1 || Cownametb.Text == "" || AmTb.Text == "" || PmTb.Text == "" || TotalTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "update MilkTbl set CowName='" + Cownametb.Text + "',AmMilk='" + AmTb.Text + "',PmMilk='" + PmTb.Text + "',TotalMilk='" + TotalTb.Text + "',DateProd='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where MId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Updated Successfully");
                    Con.Close();
                    populate();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void TotalTb_Leave(object sender, EventArgs e)
        {
            int total = Convert.ToInt32(AmTb.Text) + Convert.ToInt32(PmTb.Text);
            TotalTb.Text = "" + total;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            this.Close();
            login back = new login();
            back.Show();
        }

        private void Cownametb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
