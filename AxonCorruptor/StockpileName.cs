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
    public partial class StockpileName : Form
    {
        Form1 frm1;
        public StockpileName(Form1 form1)
        {
            InitializeComponent();
            frm1 = form1;
        }

        private void StockpileName_Load(object sender, EventArgs e)
        {
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm1.stockpilenames.Add(textBox1.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
