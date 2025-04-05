using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxonCorruptor
{
    public partial class NumberEngine : Form
    {
        public NumberEngine()
        {
            InitializeComponent();
        }
        private static readonly object syncLock = new object();
        private static readonly Random random = new Random();
        private void NumberEngine_Load(object sender, EventArgs e)
        {

        }
        public void Corrupt(int intensity, List<string> filenames, Form1 main = null)
        {
            Thread thread = new Thread(() =>
            {
                try
                {
                    Int32 length = filenames.Count();
                    for (int i = 0; i < length; i++)
                    {
                        string content = File.ReadAllText(filenames[i]);
                        for (int j = 0; j < intensity; j++)
                        {
                            content = content.Replace(RandomNumber((int)numericUpDown1.Value, (int)numericUpDown2.Value).ToString(), RandomNumber((int)numericUpDown1.Value, (int)numericUpDown2.Value).ToString());
                        }
                        File.WriteAllText(filenames[i], content);
                    }
                }
                catch (Exception ex) 
                {
                    new ErrorForm(ex).ShowDialog();
                    if (main != null) 
                    {
                        main.error = true;
                    }
                }
            });
            thread.TrySetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        public static long RandomNumber(int min, int max)
        {
            lock (syncLock)
            {
                return random.Next(min, max);
            }
        }
    }
}
