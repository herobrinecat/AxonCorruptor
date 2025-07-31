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
                    string content = string.Empty;
                    for (int i = 0; i < length; i++)
                    {
                        content = File.ReadAllText(filenames[i]);
                        for (int j = 0; j < intensity; j++)
                        {
                            int randomnumber1 = RandomNumber((int)numericUpDown1.Value, (int)numericUpDown2.Value);
                            int foundindex = content.IndexOf(randomnumber1.ToString(),RandomNumber(0,content.Length - 1));
                            if (foundindex != -1) 
                            {
                                Console.WriteLine("Found the value at index " + foundindex);
                                Debug.WriteLine("Found the value at index " + foundindex);
                                int randomnumber2 = RandomNumber((int)numericUpDown1.Value, (int)numericUpDown2.Value);
                                content = content.Remove(foundindex, randomnumber1.ToString()).Insert(foundindex,randomnumber2.ToString());
                                randomnumber2 = null;
                            }
                            else
                            {
                                Console.WriteLine("No search for the value has been found, skipping...");
                                Debug.WriteLine("No search for the value has been found, skipping...");
                            }
                            randomnumber1 = null;
                            // content = content.Replace(RandomNumber((int)numericUpDown1.Value, (int)numericUpDown2.Value).ToString(), RandomNumber((int)numericUpDown1.Value, (int)numericUpDown2.Value).ToString());
                        }
                        File.Delete(filenames[i]);
                        File.WriteAllText(filenames[i], content);
                    }
                    content = null;
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
            if (Properties.Settings.Default.MultiThread)
            {
                thread.TrySetApartmentState(ApartmentState.MTA);
            }
            else
            {
                thread.TrySetApartmentState(ApartmentState.STA);
            }
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
