using Microsoft.Win32.SafeHandles;
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

namespace HotelManagement
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            CountRooms();
            CountCustomers();
            Sumamount();
            getCustomer();

        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-QKPOMD5\SQLEXPRESS01;Initial Catalog=Myproject;Integrated Security=True");

        private void getCustomer()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Customer ", conn);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustNum", typeof(int));
            dt.Load(rdr);
            cuscb.ValueMember = "CustNum";
            cuscb.DataSource = dt;
            conn.Close();

        }
        private void CountRooms()
        {
            conn.Open();
            string Query = "select Count(*) from Room ";
            SqlDataAdapter sda = new SqlDataAdapter(Query,conn);
            DataTable dt = new DataTable();
            var ds = new DataSet();
            sda.Fill(dt);
            roomlbl.Text = dt.Rows[0][0].ToString();
            conn.Close();
        }
        private void CountUsers()
        {
            conn.Open();
            string Query = "select Count(*) from User ";
            SqlDataAdapter sda = new SqlDataAdapter(Query, conn);
            DataTable dt = new DataTable();
            var ds = new DataSet();
            sda.Fill(dt);
            userlbl.Text = dt.Rows[0][0].ToString();
            conn.Close();
        }
        private void CountCustomers()
        {
            conn.Open();
            string Query = "select Count(*) from Customer ";
            SqlDataAdapter sda = new SqlDataAdapter(Query, conn);
            DataTable dt = new DataTable();
            var ds = new DataSet();
            sda.Fill(dt);
            cuslbl.Text = dt.Rows[0][0].ToString();
            conn.Close();
        }
        private void Sumamount()
        {
            conn.Open();
            string Query = "select Sum(Coast) from Booking ";
            SqlDataAdapter sda = new SqlDataAdapter(Query, conn);
            DataTable dt = new DataTable();
            var ds = new DataSet();
            sda.Fill(dt);
            booklbl.Text =" $ " + dt.Rows[0][0].ToString();
            conn.Close();
        }
        private void SumaDaily()
        {
            conn.Open();
            string Query = "select Sum(Coast) from Booking where BookDate='"+dtpp.Value.Date+"' ";
            SqlDataAdapter sda = new SqlDataAdapter(Query, conn);
            DataTable dt = new DataTable();
            var ds = new DataSet();
            sda.Fill(dt);
            dailylbl.Text = " $ " + dt.Rows[0][0].ToString();
            conn.Close();
        }
        private void SumByCustomer()
        {
            try
            {
                conn.Open();
                string Query = "select Sum(Coast) from Booking where Customer='" + cuscb.SelectedValue + "' ";
                SqlDataAdapter sda = new SqlDataAdapter(Query, conn);
                DataTable dt = new DataTable();
                var ds = new DataSet();
                sda.Fill(dt);
                incomecuslbl.Text = " $ " + dt.Rows[0][0].ToString();

                conn.Close();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            conn.Close();
            }
            
            
            }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtpp_Validating(object sender, CancelEventArgs e)
        {

        }

        private void dtpp_ValueChanged(object sender, EventArgs e)
        {
            SumaDaily();
        }

        private void cuscb_SelectedIndexChanged(object sender, EventArgs e)
        {
            SumByCustomer();
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
            Customer obj =  new Customer();
            obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Bookings obj = new Bookings();
            obj.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            splash obj = new splash();
            obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Login obj = new Login(); 
            obj.Show();
            this.Hide();
        }
    }
}
