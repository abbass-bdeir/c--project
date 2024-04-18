using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HotelManagement
{
    public partial class Rooms : Form
    {
        public Rooms()
        {
            InitializeComponent();
            populate();
            getcat();
            
            
        }
        
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
        int key = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-QKPOMD5\SQLEXPRESS01;Initial Catalog=Myproject;Integrated Security=True");
        private void populate()
        {
            conn.Open();
            string Quary = "select * from Room ";
            SqlDataAdapter sda = new SqlDataAdapter(Quary, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            roomsdgv.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void Editrooms()
        {
            if (tbname.Text == " " || Rtypecb.SelectedIndex == -1 || Rstatuscb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing input!!");
            }
            else { 
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("Update  Room  SET RName= @RN ,RType=@RT,RStatus= @Rs where RNum= @key ", conn);
                        cmd.Parameters.AddWithValue("@RN", tbname.Text);
                        cmd.Parameters.AddWithValue("@RT", Rtypecb.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@RS", Rstatuscb.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@key", key);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Room Updated!!");

                        conn.Close();
                        populate();
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                }


            }
        
        private void insert() {

            if (tbname.Text == " " || Rtypecb.SelectedIndex == -1 || Rstatuscb.SelectedIndex == -1) {
                MessageBox.Show("Missing input!!");
            }

            else
            {
               
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into Room (RName,RType,RStatus) values (@RN,@RT,@RS)", conn);
                    cmd.Parameters.AddWithValue("@RN", tbname.Text);
                    cmd.Parameters.AddWithValue("@RT", Rtypecb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@RS", Rstatuscb.SelectedItem.ToString());
                   

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Added!!");

                    conn.Close();
                    populate();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }
        private void DeleteRoom()
        {

            if (roomsdgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("please select a room!!");
            }

            else
            {
                DataGridViewRow selectedRow = roomsdgv.SelectedRows[0];

                string rowId = selectedRow.Cells["RNum"].Value.ToString();
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Delete from Room where RNum =@key", conn);
                    cmd.Parameters.AddWithValue("@key", rowId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Deleted!!");

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
            insert();

        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            Editrooms();
        }

        private void roomsdgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {   tbname.Text = roomsdgv.Rows[e.RowIndex].Cells[1].Value.ToString();
                Rtypecb.Text= roomsdgv.Rows[e.RowIndex].Cells[2].Value.ToString();
                Rstatuscb.Text = roomsdgv.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
            if (tbname.Text == "")
            { key = 0; }
            else { key = Convert.ToInt32(roomsdgv.Rows[e.RowIndex].Cells[0].Value.ToString()); 
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            DeleteRoom();
        }
        private void getcat() {
        conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Type",conn);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeNum", typeof(int));
            dt.Load(rdr);
            Rtypecb.ValueMember = "TypeNum";
            Rtypecb.DataSource = dt;
            conn.Close();
        
        }
       
        private void label3_Click(object sender, EventArgs e)
        {Types obj = new Types();
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
        {Bookings obj = new Bookings();
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
        
        
       

        private void Rtypecb_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }
    }
}
