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
    public partial class Types : Form
    {
        public Types()
        {
            InitializeComponent();
            populate();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
         

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-QKPOMD5\SQLEXPRESS01;Initial Catalog=Myproject;Integrated Security=True");
        
        private void populate()
        {
            conn.Open();
            string Quary = "select * from Type ";
            SqlDataAdapter sda =  new SqlDataAdapter(Quary,conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            typesdgv.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void insertcategories()
        {

            if (typenametb.Text == " " || coasttb.Text == " ")
            {
                MessageBox.Show("Missing input!!");
            }

            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into Type (TypeName,TypeCoast) values (@TN,@TC)", conn);
                    cmd.Parameters.AddWithValue("@TN", typenametb.Text);
                    cmd.Parameters.AddWithValue("@TC", coasttb.Text);
                    MessageBox.Show("Category Inserted!!!");
                    cmd.ExecuteNonQuery();
                   

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
            insertcategories();
        }
        private void Editcategory()
        {
            if (typenametb.Text == " " || coasttb.Text == " "  )
            {
                MessageBox.Show("Missing input!!");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Update  Type SET TypeName= @TN ,TypeCoast=@TC where TypeNum= @Rkey ", conn);
                    cmd.Parameters.AddWithValue("@TN", typenametb.Text);
                    cmd.Parameters.AddWithValue("@TC", coasttb.Text);
                    
                    cmd.Parameters.AddWithValue("@Rkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Updated!!");

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
            Editcategory();
        }
        int key = 0;
        private void typesdgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                typenametb.Text = typesdgv.Rows[e.RowIndex].Cells[1].Value.ToString();
                coasttb.Text = typesdgv.Rows[e.RowIndex].Cells[2].Value.ToString();
                
            }
            if (typenametb.Text == "" )
            { key = 0; }
            else
            {
                key = Convert.ToInt32(typesdgv.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }
        private void Deletecategory()
        {

            if (typesdgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("please select a Category!!");
            }

            else
            {
                
                try
                {
                    DataGridViewRow selectedRow = typesdgv.SelectedRows[0];

                    string rowId = selectedRow.Cells["TypeNum"].Value.ToString();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Delete from Type where TypeNum =@Tkey", conn);
                    cmd.Parameters.AddWithValue("@Tkey", rowId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Deleted!!");

                    conn.Close();
                    populate();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            Deletecategory();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Rooms obj = new Rooms();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Users obj = new Users();
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
