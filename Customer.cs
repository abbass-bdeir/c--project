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
using System.Xml.Linq;

namespace HotelManagement
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
            populate();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteCustomers();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-QKPOMD5\SQLEXPRESS01;Initial Catalog=Myproject;Integrated Security=True");
        private void populate()
        {
            conn.Open();
            string Quary = "select * from Customer ";
            SqlDataAdapter sda = new SqlDataAdapter(Quary, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            customersdgv.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void insertcustomer()
        {

            if (nametb.Text == " " || gendercb.SelectedIndex == -1 || phonetb.Text=="")
            {
                MessageBox.Show("Missing input!!");
            }

            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into Customer (CustName,CustPhone,CustGender) values (@CN,@CP,@CG)", conn);
                    cmd.Parameters.AddWithValue("@CN", nametb.Text);
                    
                    cmd.Parameters.AddWithValue("@CP", phonetb.Text);
                    cmd.Parameters.AddWithValue("@CG", gendercb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Added!!");

                    conn.Close();
                    populate();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            insertcustomer();
        }
        int key = 0;
        private void customersdgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                nametb.Text = customersdgv.Rows[e.RowIndex].Cells[1].Value.ToString();
                phonetb.Text = customersdgv.Rows[e.RowIndex].Cells[2].Value.ToString();
                gendercb.Text = customersdgv.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
            if (nametb.Text == "")
            { key = 0; }
            else
            {
                key = Convert.ToInt32(customersdgv.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }
        private void EditCustomers()
        {
            if (nametb.Text == " " || gendercb.SelectedIndex == -1 || phonetb.Text=="")
            {
                MessageBox.Show("Missing input!!");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Update  Customer  SET CustName= @CN ,CustPhone=@CP,CustGender= @CG where CustNum= @Ckey ", conn);
                    cmd.Parameters.AddWithValue("@CN", nametb.Text);

                    cmd.Parameters.AddWithValue("@CP", phonetb.Text);
                    cmd.Parameters.AddWithValue("@CG", gendercb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Ckey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated!!");

                    conn.Close();
                    populate();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }


        }

        private void DeleteCustomers()
        {

            if (customersdgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("please select a room!!");
            }

            else
            {
                DataGridViewRow selectedRow = customersdgv.SelectedRows[0];

                string rowId = selectedRow.Cells["CustNum"].Value.ToString();
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Delete from Customer where CustNum =@Ckey", conn);
                    cmd.Parameters.AddWithValue("@Ckey", rowId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer  Deleted!!");

                    conn.Close();
                    populate();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }


        private void editbtn_Click(object sender, EventArgs e)
        {
            EditCustomers();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Rooms obj = new Rooms();
            obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Types obj = new Types();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Users obj = new Users();
            obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Bookings obj = new Bookings();
            obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            splash obj = new splash();
            obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }
    }


}
