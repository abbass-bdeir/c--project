using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (antb.Text == "" || aptb.Text == "") { MessageBox.Show("missing input"); }
            else {
                if (antb.Text == "Admin" && aptb.Text=="Password") { 
                Users obj = new Users();
                    obj.Show(); 
                    this.Hide();
                
                }
            
            
            }
        }
    }
}
