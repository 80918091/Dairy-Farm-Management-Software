using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DairyFarmSystem
{
    public partial class CowHealth : Form
    {
        public CowHealth()
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

        private void label6_Click(object sender, EventArgs e)
        {
            milkproduction ob = new milkproduction();
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
            string query = "select * from HealthTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            HealthDGV.DataSource = ds.Tables[0];

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
                COwNameTb.Text = dr["CowName"].ToString();

            }
            Con.Close();

        }
        private void CowHealth_Load(object sender, EventArgs e)
        {

        }

        private void CowIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCowname();
        }
        private void Clear()
        {
            COwNameTb.Text = "";
            EventTb.Text = "";
            CostTb.Text = "";
            DiagnosisTb.Text = "";
            TreatmentTb.Text = "";
            VetNameTb.Text = "";
            key = 0;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (CowIdCb.SelectedIndex == -1 || COwNameTb.Text == "" || EventTb.Text == "" || CostTb.Text == "" || DiagnosisTb.Text == "" || TreatmentTb.Text == ""|| VetNameTb.Text=="")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into HealthTbl values(" + CowIdCb.SelectedValue.ToString() + ",'" + COwNameTb.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + EventTb.Text + "','" + DiagnosisTb.Text + "','" + TreatmentTb.Text + "','" + CostTb.Text + "','"+VetNameTb.Text+"')";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Health Issue Saved");
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

        private void TreatmentTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clear();
            MessageBox.Show("clear data");
        }
        int key = 0;
        private void HealthDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (HealthDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {

                CowIdCb.SelectedValue = HealthDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
                COwNameTb.Text = HealthDGV.Rows[e.RowIndex].Cells[2].Value.ToString();
                Date.Text = HealthDGV.Rows[e.RowIndex].Cells[3].Value.ToString();
                EventTb.Text = HealthDGV.Rows[e.RowIndex].Cells[4].Value.ToString();
                DiagnosisTb.Text = HealthDGV.Rows[e.RowIndex].Cells[5].Value.ToString();
                TreatmentTb.Text = HealthDGV.Rows[e.RowIndex].Cells[6].Value.ToString();
                CostTb.Text= HealthDGV.Rows[e.RowIndex].Cells[7].Value.ToString();
                VetNameTb.Text = HealthDGV.Rows[e.RowIndex].Cells[8].Value.ToString();
                if (COwNameTb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(HealthDGV.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Selected The Health Report To Be Deleted");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "delete from HealthTbl where RepId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Report Deleted Successfully");
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
            if (CowIdCb.SelectedIndex == -1 || COwNameTb.Text == "" || EventTb.Text == "" || CostTb.Text == "" || DiagnosisTb.Text == "" || TreatmentTb.Text == "" || VetNameTb.Text == "" )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "update HealthTbl set CowId=" + CowIdCb.SelectedValue.ToString() + ",CowName= '" + COwNameTb.Text + "',RepDate='" + DateTime.Now.ToString("yyyy-MM-dd")+"',Event= '" +EventTb.Text + "',Diagnosis='" + DiagnosisTb.Text + "',Treatment='" + TreatmentTb.Text + "',Cost='" + CostTb.Text + "',VetName='" + VetNameTb.Text + "' where RepId=" + key + " ; ";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Health report Updated Successfully");
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
