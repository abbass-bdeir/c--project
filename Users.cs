using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HotelManagement
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            pop();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            insertusers();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-QKPOMD5\SQLEXPRESS01;Initial Catalog=Myproject;Integrated Security=True");
        private void pop() {
            SqlCommand command = new SqlCommand("Select * from [User]", conn);
            DataTable dataTable = new DataTable();
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dataTable);
            conn.Close();
            dgvusers.DataSource = dataTable;




        }


        private void EditUsers()
        {
            if (unametb.Text == " " || ugendercb.SelectedIndex == -1 || uphonetb.Text == " " || passwordtb.Text == " ")
            {
                MessageBox.Show("Missing input!!");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Update [User] set UName=@UN,UPhone=@UPH,UGender=@UG,UPassword=@UP where YNum=@Ukey ", conn);
                    cmd.Parameters.AddWithValue("@UN", unametb.Text);
                    cmd.Parameters.AddWithValue("@UPH", uphonetb.Text);
                    cmd.Parameters.AddWithValue("@UG", ugendercb.SelectedIndex.ToString());
                    cmd.Parameters.AddWithValue("@UP", passwordtb.Text);
                    cmd.Parameters.AddWithValue("@Ukey", key1);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated!!");

                    conn.Close();
                    pop();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }
        private void insertusers()
        {

            if (unametb.Text == " " || ugendercb.SelectedIndex == -1 || uphonetb.Text == " " || passwordtb.Text == " ")
            {
                MessageBox.Show("Missing input!!");
            }

            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into [User] (UName,UPhone,UGender,UPassword) values (@UN,@UPH,@UG,@UP)", conn);
                    cmd.Parameters.AddWithValue("@UN", unametb.Text);
                    cmd.Parameters.AddWithValue("@UPH", uphonetb.Text);
                    cmd.Parameters.AddWithValue("@UG", ugendercb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@UP", passwordtb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Added!!");

                    conn.Close();
                    pop();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }
        private void DeleteUsers()
        {

            if (dgvusers.SelectedRows.Count == 0)
            {
                MessageBox.Show("please select a User!!");
            }

            else
            {
                


                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Delete from [User] where YNum =@Ukey", conn);
                    cmd.Parameters.AddWithValue("@Ukey", key1);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted!!");

                    conn.Close();
                    pop();              }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }
        int key = 0;
        private void dgvusers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }
        int key1 = 0;
        private void dgvusers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                unametb.Text = dgvusers.Rows[e.RowIndex].Cells[1].Value.ToString();
                uphonetb.Text = dgvusers.Rows[e.RowIndex].Cells[2].Value.ToString();
                ugendercb.Text = dgvusers.Rows[e.RowIndex].Cells[3].Value.ToString();
                passwordtb.Text = dgvusers.Rows[e.RowIndex].Cells[4].Value.ToString();

            }
            if (unametb.Text == "")
            { key1 = 0; }
            else
            {
                key1 = Convert.ToInt32(dgvusers.Rows[e.RowIndex].Cells[0].Value.ToString());
            }

        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            EditUsers();
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            DeleteUsers();
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

        private void label5_Click(object sender, EventArgs e)
        {
            Customer obj = new Customer();
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
