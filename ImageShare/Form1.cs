using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageShare
{
    public partial class Form1 : Form
    {
        private int number = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            number++;
            Program.GetOneImage(number);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            number--;
            Program.GetOneImage(number);
        }
    }
}
