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

        public void Corrupt(int intensity, List<string> filenames, Form1 main = null)
        {
            int currentType = 0;
            currentType = comboBox1.SelectedIndex;
            Thread thread = new Thread(() =>
            {
                try
                {
                    Int32 length = filenames.Count();
                    string content = string.Empty;
                    for (int i = 0; i < length; i++)
                    {
                        content = File.ReadAllText(filenames[i]);
                        if (currentType == 0)
                        {
                            for (int j = 0; j < intensity; j++)
                            {
                                int randomnumber1 = (int)RandomNumber((int)numericUpDown1.Value, (int)numericUpDown2.Value);
                                int foundindex = content.IndexOf(randomnumber1.ToString(), (int)RandomNumber((int)Math.Round(numericUpDown3.Value, 0), content.Length - 1), StringComparison.OrdinalIgnoreCase);
                                if (foundindex != -1)
                                {
                                    Console.WriteLine("Found the value at index " + foundindex);
                                    int randomnumber2 = (int)RandomNumber((int)numericUpDown1.Value, (int)numericUpDown2.Value);
                                    content = content.Remove(foundindex, randomnumber1.ToString().Length).Insert(foundindex, randomnumber2.ToString());
                                    randomnumber2 = 0;
                                }
                                else
                                {
                                    Console.WriteLine("No search for the value has been found, skipping...");
                                }
                                randomnumber1 = 0;
                                // content = content.Replace(RandomNumber((int)numericUpDown1.Value, (int)numericUpDown2.Value).ToString(), RandomNumber((int)numericUpDown1.Value, (int)numericUpDown2.Value).ToString());
                            }
                        }
                        else if (currentType == 1)
                        {
                            for (int j = 0; j < intensity; j++)
                            {
                                int randomnumber1 = (int)RandomNumber((int)numericUpDown1.Value, (int)numericUpDown2.Value);
                                int foundindex = content.IndexOf(randomnumber1.ToString(), (int)RandomNumber((int)Math.Round(numericUpDown3.Value, 0), content.Length - 1), StringComparison.OrdinalIgnoreCase);
                                if (foundindex != -1)
                                {
                                    if (RandomNumber(0, 1) == 0)
                                    {
                                        Console.WriteLine("Found the value at index " + foundindex);
                                        int randomnumber2 = (int)RandomNumber((int)numericUpDown1.Value, (int)numericUpDown2.Value);
                                        content = content.Remove(foundindex, randomnumber1.ToString().Length).Insert(foundindex, randomnumber2.ToString());
                                        randomnumber2 = 0;
                                    }
                                    else
                                    {
                                        if (RandomNumber(0,1) == 0)
                                {
                                    content = content.Remove(foundindex, randomnumber1.ToString().Length).Insert(foundindex, (randomnumber1 + 1).ToString());
                                }
                                else
                                {
                                    content = content.Remove(foundindex, randomnumber1.ToString().Length).Insert(foundindex, (randomnumber1 - 1).ToString());
                                }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No search for the value has been found, skipping...");
                                }
                                randomnumber1 = 0;
                                // content = content.Replace(RandomNumber((int)numericUpDown1.Value, (int)numericUpDown2.Value).ToString(), RandomNumber((int)numericUpDown1.Value, (int)numericUpDown2.Value).ToString());
                            }
                        }
                        else if (currentType == 2)
                        {
                            for (int j = 0; j < intensity; j++)
                        {
                            int randomnumber1 = (int)RandomNumber((int)numericUpDown1.Value, (int)numericUpDown2.Value);
                            int foundindex = content.IndexOf(randomnumber1.ToString(),(int)RandomNumber((int)Math.Round(numericUpDown3.Value,0),content.Length - 1), StringComparison.OrdinalIgnoreCase);
                            if (foundindex != -1) 
                            {
                                Console.WriteLine("Found the value at index " + foundindex);
                                if (RandomNumber(0,1) == 0)
                                {
                                    content = content.Remove(foundindex, randomnumber1.ToString().Length).Insert(foundindex, (randomnumber1 + 1).ToString());
                                }
                                else
                                {
                                    content = content.Remove(foundindex, randomnumber1.ToString().Length).Insert(foundindex, (randomnumber1 - 1).ToString());
                                }
                            }
                            else
                            {
                                Console.WriteLine("No search for the value has been found, skipping...");
                            }
                            randomnumber1 = 0;
                            // content = content.Replace(RandomNumber((int)numericUpDown1.Value, (int)numericUpDown2.Value).ToString(), RandomNumber((int)numericUpDown1.Value, (int)numericUpDown2.Value).ToString());
                        }
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
