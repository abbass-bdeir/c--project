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
    public partial class splash : Form
    {
        public splash()
        {
            InitializeComponent();
        }
        private List<string> imagePaths; 
        private int currentIndex = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
           
                progressBar1.Value++; 

                if (progressBar1.Value >= progressBar1.Maximum)
                {
                    timer1.Stop();
                this.Hide();
                Admin loginForm = new Admin();
                loginForm.ShowDialog();
                this.Hide();
            }
            
        }

        private void splash_Load(object sender, EventArgs e)
        {
            

            
            timer1.Start();


        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
    }

