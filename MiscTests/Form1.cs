using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTO
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            //string str1 = "It will change depending on the resistor's temperature coefficient";

            //Console.WriteLine(str1);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            stopButton.Enabled = true;
            // this.Refresh();
            HTOAuto.StartAutomation();
            stopButton.Enabled = false;
            startButton.Enabled = true;
            //this.Refresh();
        }

        private void userID_TextChanged(object sender, EventArgs e)
        {
           // do nothing
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            stopButton.Enabled = false;
            startButton.Enabled = true;
           // this.Refresh();
        }
    }
}
