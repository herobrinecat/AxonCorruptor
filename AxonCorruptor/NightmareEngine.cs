using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxonCorruptor
{
    public partial class NightmareEngine : Form
    {
        public NightmareEngine()
        {
            InitializeComponent();
        }
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        private void NightmareEngine_Load(object sender, EventArgs e)
        {

        }
        public void Corrupt(int intensity, List<string> filenames)
        {
            int currentType = 0;
            currentType = comboBox1.SelectedIndex;
            Thread thread = new Thread(() =>
            {
                Int32 length = filenames.Count;
                for (int i = 0; i < length; i++)
                {
                    FileStream file = File.Open(filenames[i], FileMode.Open);
                    if (currentType == 0)
                    {
                        for (int j = 0; j < intensity; j++)
                        {
                            file.Position = RandomNumber(0 + (int)numericUpDown3.Value, file.Length);
                            file.WriteByte(Convert.ToByte(RandomNumber((long)numericUpDown1.Value, (long)numericUpDown2.Value)));
                        }
                       
                    }
                    else if (currentType == 1)
                    {
                        for (int j = 0; j < intensity; j++)
                        {
                            if (RandomNumber(0, 1) == 0)
                            {
                                file.Position = RandomNumber(0 + (int)numericUpDown3.Value, file.Length);
                                file.WriteByte(Convert.ToByte(RandomNumber((long)numericUpDown1.Value, (long)numericUpDown2.Value)));
                            }
                            else
                            {
                                byte[] buffer = new byte[1];
                                file.Position = RandomNumber(0 + (int)numericUpDown3.Value, file.Length);
                                file.Read(buffer, 0, 1);
                                if (RandomNumber(0,1) == 0)
                                {
                                    buffer[0] = (byte)(buffer[0] + 0x01);
                                }
                                else
                                {
                                    buffer[0] = (byte)(buffer[0] - 0x01);
                                }
                                    file.Position = file.Position - 1;
                                file.WriteByte(buffer[0]);
                                buffer = null;
                            }
                        }
                      
                    }
                    else if (currentType == 2)
                    {
                        for (int j = 0; j < intensity; j++)
                        {
                            byte[] buffer = new byte[1];
                            file.Position = RandomNumber(0 + (int)numericUpDown3.Value, file.Length);
                            file.Read(buffer, 0, 1);
                            if (RandomNumber(0, 1) == 0)
                            {
                                buffer[0] = (byte)(buffer[0] + 0x01);
                            }
                            else
                            {
                                buffer[0] = (byte)(buffer[0] - 0x01);
                            }
                            file.Position = file.Position - 1;
                            file.WriteByte(buffer[0]);
                            buffer = null;
                        }
                        
                    }

                    file.Flush();
                    file.Close();
                    file.Dispose();
                }
            });
            thread.TrySetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        public static long RandomNumber(long min, long max)
        {
            lock (syncLock)
            {
                long result = random.Next((Int32)(min >> 32), (Int32)(max >> 32));
                result = (result << 32);
                result = result | (long)random.Next((Int32)min, (Int32)max);
                return result;
            }
        }
      
    }
}
