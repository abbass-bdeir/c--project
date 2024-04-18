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
    public partial class Bookings : Form
    {
        public Bookings()
        {
            InitializeComponent();
            populate();
            getRooms();
            getCustomer();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-QKPOMD5\SQLEXPRESS01;Initial Catalog=Myproject;Integrated Security=True");
        private void populate()
        {
            conn.Open();
            string Quary = "select * from Booking ";
            SqlDataAdapter sda = new SqlDataAdapter(Quary, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            bookingdgv.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void book() {

            if (amounttb.Text == " " || roomcb.SelectedIndex == -1 || customercb.SelectedIndex == -1 || durationtb.Text == "")
            {
                MessageBox.Show("Missing input!!");
            }

            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into Booking (Room,Customer,BookDate,Duration,Coast) values (@R,@c,@BD,@D,@Co)", conn);
                    cmd.Parameters.AddWithValue("@R", roomcb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@C", customercb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@BD", dtp.Value.Date);
                    cmd.Parameters.AddWithValue("@D", durationtb.Text);
                    cmd.Parameters.AddWithValue("@Co",amounttb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("room booked!!");

                    conn.Close();
                    populate();
                    setbooked();
                    getRooms();

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }



        }
        private void cancelbooking()
        {

            if (bookingdgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("please select a room!!");
            }

            else
            {
               
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Delete from Booking where BookNum =@Bkey", conn);
                    cmd.Parameters.AddWithValue("@Bkey", key2);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("book canceled!!");

                    conn.Close();
                    populate();
                    setNotbooked();
                    getRooms();

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }
        private void getRooms()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Room where RStatus = 'Available'", conn);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RNum", typeof(int));
            dt.Load(rdr);
            roomcb.ValueMember = "RNum";
            roomcb.DataSource = dt;
            conn.Close();

        }
        private void getCustomer()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Customer ", conn);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustNum", typeof(int));
            dt.Load(rdr);
            customercb.ValueMember = "CustNum";
            customercb.DataSource = dt;
            conn.Close();

        }
        int Price ;
        private void coast()
        {
            conn.Open();
            string query = "select TypeCoast from Room join Type on RType=TypeNum where RNum=" + roomcb.SelectedValue.ToString()+""; 
            SqlCommand cmd = new SqlCommand(query, conn);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows) { 
                Price = Convert.ToInt16(dr["TypeCoast"].ToString());
            }
                conn.Close() ;  


        }
        private void setbooked()
        {conn.Open();
            SqlCommand cmd =new SqlCommand("Update Room Set RStatus=@RS where RNum=@key", conn);
            cmd.Parameters.AddWithValue("@RS","Booked");
            cmd.Parameters.AddWithValue("@key",roomcb.SelectedValue.ToString());
            cmd.ExecuteNonQuery();
            MessageBox.Show("room updated");

            conn.Close();
            populate();
       }

        private void setNotbooked()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Update Room Set RStatus=@RS where RNum=@key", conn);
            cmd.Parameters.AddWithValue("@RS", "Available");
            cmd.Parameters.AddWithValue("@key", roomcb.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("room updated");

            conn.Close();
            populate();
        }







        private void button2_Click(object sender, EventArgs e)
        {
            book();
        }

        private void roomcb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            coast();
        }

        private void durationtb_TextChanged(object sender, EventArgs e)
        { 
            
            
            
            
            
            
            int t = Price * Convert.ToInt32(durationtb.Text);
            amounttb.Text = t.ToString();
        }
        

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            cancelbooking();
            
        }
        int key2 = 0;
        private void bookingdgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                roomcb.Text = bookingdgv.Rows[e.RowIndex].Cells[1].Value.ToString();
                customercb.Text = bookingdgv.Rows[e.RowIndex].Cells[2].Value.ToString();
                dtp.Text = bookingdgv.Rows[e.RowIndex].Cells[3].Value.ToString();
                durationtb.Text = bookingdgv.Rows[e.RowIndex].Cells[4].Value.ToString();
                amounttb.Text = bookingdgv.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
        
        
            if (roomcb.SelectedIndex == -1)
            { key2 = 0; }
            else
            {
                key2 = Convert.ToInt32(bookingdgv.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

        private void roomcb_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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

        private void label5_Click(object sender, EventArgs e)
        {
            Customer obj = new Customer();
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
