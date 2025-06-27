﻿using System;
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
    public partial class cows : Form
    {
        public cows()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\91709\Documents\DairyFarmDp.mdf;Integrated Security=True;Connect Timeout=30");
       
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
        private void populate()
        {
            Con.Open();
            string query = "select * from CowTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CowsDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void Clear()
        {
            CowNameTb.Text = "";
            EarTagTb.Text = "";
            ColorTb.Text = "";
            BreedTb.Text = "";
            WeightTb.Text = "";
            AgeTb.Text = "";
            PastureTb.Text = "";
            key = 0;    
        }
        int age = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if(CowNameTb.Text=="" || EarTagTb.Text=="" || ColorTb.Text=="" || BreedTb.Text=="" || AgeTb.Text=="" || WeightTb.Text=="" || PastureTb.Text=="")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query="insert into CowTbl values('"+CowNameTb.Text+"','"+EarTagTb.Text+"','"+ColorTb.Text+"','"+BreedTb.Text+"','"+ age +"','"+ WeightTb.Text +"','"+ PastureTb.Text+"')";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cow Saved Successfully");
                    Con.Close();
                    populate();
                    Clear();
                }catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DOBDate_ValueChanged(object sender, EventArgs e)
        {
            age = Convert.ToInt32((DateTime.Today.Date - DOBDate.Value.Date).Days) / 365;

        }

        private void DOBDate_MouseLeave(object sender, EventArgs e)
        {
            age = Convert.ToInt32((DateTime.Today.Date - DOBDate.Value.Date).Days) / 365;
            AgeTb.Text = "" + age;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clear();
            MessageBox.Show("clear Data");
        }
        int key = 0;
        private void CowsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CowsDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {

                CowNameTb.Text = CowsDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
                EarTagTb.Text = CowsDGV.Rows[e.RowIndex].Cells[2].Value.ToString();
                ColorTb.Text = CowsDGV.Rows[e.RowIndex].Cells[3].Value.ToString();
                BreedTb.Text = CowsDGV.Rows[e.RowIndex].Cells[4].Value.ToString();
                WeightTb.Text = CowsDGV.Rows[e.RowIndex].Cells[6].Value.ToString();
                PastureTb.Text = CowsDGV.Rows[e.RowIndex].Cells[7].Value.ToString();
                if (CowNameTb.Text == "")
                {
                    key = 0;
                    age = 0;
                }
                else
                {
                    key = Convert.ToInt32(CowsDGV.Rows[e.RowIndex].Cells[0].Value.ToString());
                    age = Convert.ToInt32(CowsDGV.Rows[e.RowIndex].Cells[5].Value.ToString());
                }
            }
              /*  if(CowsDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                CowNameTb.Text = CowsDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
                EarTagTb.Text = CowsDGV.Rows[e.RowIndex].Cells[2].Value.ToString();
                ColorTb.Text = CowsDGV.Rows[e.RowIndex].Cells[3].Value.ToString();
                BreedTb.Text = CowsDGV.Rows[e.RowIndex].Cells[4].Value.ToString();
                WeightTb.Text = CowsDGV.Rows[e.RowIndex].Cells[6].Value.ToString();
                PastureTb.Text = CowsDGV.Rows[e.RowIndex].Cells[7].Value.ToString();
            }*/
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Selected The Cow To Be Deleted");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "delete from CowTbl where CowId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cow Deleted Successfully");
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
            if (CowNameTb.Text == "" || EarTagTb.Text == "" || ColorTb.Text == "" || BreedTb.Text == "" || AgeTb.Text == "" || WeightTb.Text == "" || PastureTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "update CowTbl set CowName='" + CowNameTb.Text + "',EarTag='" + EarTagTb.Text + "',Color='" + ColorTb.Text + "',Breed='" + BreedTb.Text + "',Age=" + age + ",WeightAtBirth=" + WeightTb.Text + ",Pasture='" + PastureTb.Text + "' where CowId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cow Updated Successfully");
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

        private void cows_Load(object sender, EventArgs e)
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
    }
    }
    
    
    
    
    

