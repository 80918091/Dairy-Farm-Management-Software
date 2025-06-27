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
    public partial class Breeding : Form
    {
        public Breeding()
        {
            InitializeComponent();
            populate();
            FillCowId();
        }

       

       

        
       

        private void label15_Click(object sender, EventArgs e)
        {
            MilkSales ob = new MilkSales();
            ob.Show();
            this.Hide();
        }

       

        private void label17_Click(object sender, EventArgs e)
        {
            Dashboard ob = new Dashboard();
            ob.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            cows ob = new cows();
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

        private void label16_Click(object sender, EventArgs e)
        {
            Finance ob = new Finance();
            ob.Show();
            this.Hide();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\91709\Documents\DairyFarmDp.mdf;Integrated Security=True;Connect Timeout=30");
        private void FillCowId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CowId from CowTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CoeId", typeof(int));
            dt.Load(Rdr);
            CowIdCb.ValueMember = "CowId";
            CowIdCb.DataSource = dt;

            Con.Close();
        }
        private void populate()
        {
            Con.Open();
            string query = "select * from BreedTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BreedDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        
        private void GetCowname()
        {
            Con.Open();
            string query = "select * from CowTbl where CowId=" + CowIdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CowNameTb.Text = dr["CowName"].ToString();
                CowAgeTb.Text = dr["Age"].ToString();

            }
            Con.Close();

        }

        private void CowIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCowname();
        }
        private void Clear()
        {
            CowNameTb.Text = "";
            RemarksTb.Text = "";
            CowAgeTb.Text = "";
            key = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (CowIdCb.SelectedIndex == -1 || CowNameTb.Text == "" || RemarksTb.Text == "" || CowAgeTb.Text=="")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into BreedTbl values('"+ DateTime.Now.ToString("yyyy-MM-dd") +"','"+ DateTime.Now.ToString("yyyy-MM-dd") +"'," + CowIdCb.SelectedValue.ToString() + ",'" + CowNameTb.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" +DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("yyyyy-MM-dd") + "','" + CowAgeTb.Text + "','" + RemarksTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Breeding Report Saved");
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

        private void button3_Click(object sender, EventArgs e)
        {
            Clear();
            MessageBox.Show("clear data");
        }
        int key = 0;
        private void BreedDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (BreedDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {

                HeatDate.Text = BreedDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
                BreedDate.Text = BreedDGV.Rows[e.RowIndex].Cells[2].Value.ToString();
                CowIdCb.SelectedValue = BreedDGV.Rows[e.RowIndex].Cells[3].Value.ToString();
                CowNameTb.Text = BreedDGV.Rows[e.RowIndex].Cells[4].Value.ToString();
                PregDate.Text = BreedDGV.Rows[e.RowIndex].Cells[5].Value.ToString();
                ExpDate.Text = BreedDGV.Rows[e.RowIndex].Cells[6].Value.ToString();
                DateCalved.Text = BreedDGV.Rows[e.RowIndex].Cells[7].Value.ToString();
                CowAgeTb.Text = BreedDGV.Rows[e.RowIndex].Cells[8].Value.ToString();
                RemarksTb.Text = BreedDGV.Rows[e.RowIndex].Cells[9].Value.ToString();
                if (CowNameTb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(BreedDGV.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Selected The Breed Report To Be Deleted");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "delete from BreedTbl where BrId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Breeding Deleted Successfully");
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
            if (CowIdCb.SelectedIndex == -1 || CowNameTb.Text == "" || RemarksTb.Text == "" || CowAgeTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "update BreedTbl set HeatDate=" + DateTime.Now.ToString("yyyy-MM-dd") + ",BreedDate= '" + DateTime.Now.ToString("yyyy-MM-dd") + "',CowId='" + CowIdCb.SelectedValue.ToString() + "',CowName='" + CowNameTb.Text + "',PregDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "',ExpDateCalve='" + DateTime.Now.ToString("yyyy-MM-dd") + "',DateCalved='" + DateTime.Now.ToString("yyyy-MM-dd") + "',CowAge='" + CowAgeTb.Text + "',Remarks='"+RemarksTb.Text+"' where BrId=" + key + " ; ";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Breeding Updated Successfully");
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

        private void Breeding_Load(object sender, EventArgs e)
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
