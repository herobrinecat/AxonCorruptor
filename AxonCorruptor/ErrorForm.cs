using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxonCorruptor
{
    public partial class ErrorForm : Form
    {

        Exception err;

        public ErrorForm(Exception e)
        {
            InitializeComponent();
            err = e;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ErrorForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = err.Message + "\n\n" + err.StackTrace;
        }
    }
}
