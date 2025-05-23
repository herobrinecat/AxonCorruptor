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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(checkBox1, "Disk Caching allows you to save the original file to the disk instead of being stored to RAM.\n\nMost of the time you don't need this, however if the file is large, then this would be recommended.");
            toolTip2.SetToolTip(checkBox2, "Multithreading allows you to use multiple threads for corrupting instead of a single thread\n\nThis could improve performance of corruption, depending on the situation.");
            checkBox1.Checked = Properties.Settings.Default.DiskCache;
            checkBox2.Checked = Properties.Settings.Default.MultiThread;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DiskCache = checkBox1.Checked;
            Properties.Settings.Default.MultiThread = checkBox2.Checked;
            Properties.Settings.Default.Save();
            Close();
        }
    }
}
