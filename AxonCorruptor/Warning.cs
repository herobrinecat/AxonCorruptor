using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxonCorruptor
{
    public partial class Warning : Form
    {
        public Warning()
        {
            InitializeComponent();
        }

        private void Warning_Load(object sender, EventArgs e)
        {

        }

        private void Warning_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Properties.Settings.Default.WarningRead == false) 
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.WarningRead = true;
            Properties.Settings.Default.Save();
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button1.Visible = checkBox1.Checked;
        }
    }
}
