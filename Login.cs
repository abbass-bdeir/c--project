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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-QKPOMD5\SQLEXPRESS01;Initial Catalog=Myproject;Integrated Security=True");
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (untb.Text == "" || uptb.Text == "")
            {
                MessageBox.Show("missing username or password");

            }
            else { 
            
            try { conn.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("select count(*) from [User] where UName='" + untb.Text + "'and UPassword ='" + uptb.Text + "'", conn);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1") { 
                    Rooms obj = new Rooms();
                        obj.Show();
                        this.Hide();
                        conn.Close();
                        
                    
                    }
                    else
                    {
                        MessageBox.Show("wrong username or password");
                    }

                    conn.Close();


                }
               
                
                
                catch (Exception ex) { MessageBox.Show( ex.Message); }
                        
                        





            
            
            
            
            
            }
            
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Admin obj = new Admin();
            obj.Show();
            this.Hide();

        }
    }
}
