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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            UserNameTb.Text = "";
            PasswordTb.Text = "";

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\91709\Documents\DairyFarmDp.mdf;Integrated Security=True;Connect Timeout=30");
        private void button1_Click(object sender, EventArgs e)
        {   
            if(RolesCb.SelectedIndex==-1)
            {
                MessageBox.Show("Select Role");
            }else
            if (UserNameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Enter The UserName And Password");
            }

            else
            {
                if (RolesCb.SelectedIndex > -1)
                {
                    if (RolesCb.SelectedItem.ToString() == "Admin")
                    {
                        if (UserNameTb.Text == "Admin" && PasswordTb.Text == "Admin")
                        {
                            Employee emp = new Employee();
                            emp.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Wrong Username And Password");
                        }
                    }
                }
                if(RolesCb.SelectedItem.ToString()=="Employee")
                { 
                    Con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("select count(*) from EmployeeTbl where EmpName='" + UserNameTb.Text + "' and EmpPass='" + PasswordTb.Text + "'", Con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        cows Cow = new cows();
                        Cow.Show();
                        this.Hide();
                        Con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Wrong UserName or Password");
                    }
            
                    Con.Close();

            





                }
            }

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 back = new Form1();
            back.Show();
        }
    }
}
