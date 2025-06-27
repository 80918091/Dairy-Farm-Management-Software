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
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
            populate();
        }

        private void Employee_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\91709\Documents\DairyFarmDp.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            Con.Open();
            string query = "select * from EmployeeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeeDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void Clear()
        {
            PhoneTb.Text = "";
            EmployeeNameTb.Text = "";
            AddressTb.Text = "";
            GenderCb.SelectedIndex = -1;
            key = 0;
            EmpPassTb.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (EmployeeNameTb.Text == "" || GenderCb.SelectedIndex == -1 || PhoneTb.Text == "" || AddressTb.Text == "" || EmpPassTb.Text=="")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into EmployeeTbl values('" + EmployeeNameTb.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + GenderCb.SelectedItem.ToString() + "','" + PhoneTb.Text + "','" + AddressTb.Text + "','"+EmpPassTb.Text+"')";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Saved Successfully");
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
        int key = 0;
        private void EmployeeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (EmployeeDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {

                EmployeeNameTb.Text = EmployeeDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
                DOB.Text = EmployeeDGV.Rows[e.RowIndex].Cells[2].Value.ToString();
                GenderCb.SelectedItem = EmployeeDGV.Rows[e.RowIndex].Cells[3].Value.ToString();
                PhoneTb.Text = EmployeeDGV.Rows[e.RowIndex].Cells[4].Value.ToString();
                AddressTb.Text = EmployeeDGV.Rows[e.RowIndex].Cells[5].Value.ToString();
                EmpPassTb.Text = EmployeeDGV.Rows[e.RowIndex].Cells[6].Value.ToString();

                if (EmployeeNameTb.Text == "")
                {
                    key = 0;

                }
                else
                {
                    key = Convert.ToInt32(EmployeeDGV.Rows[e.RowIndex].Cells[0].Value.ToString());

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Selected The Employee To Be Deleted");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "delete from EmployeeTbl where EmpId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Deleted Successfully");
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
            if (EmployeeNameTb.Text == "" || GenderCb.SelectedIndex == -1 || PhoneTb.Text == "" || AddressTb.Text == "")
            { 
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "update EmployeeTbl set EmpName='" + EmployeeNameTb.Text + "',EmpDOB='"+DateTime.Now.ToString("yyyy-MM-dd")+"',Gender='" + GenderCb.SelectedItem.ToString() + "',Phone='" + PhoneTb.Text + "',Address='" + AddressTb.Text + "',EmpPass='"+EmpPassTb.Text+"' where EmpId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Updated Successfully");
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            login back = new login();
            back.Show();
        }
    }
}

