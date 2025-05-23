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
    public partial class ChunkEngine : Form
    {
        public ChunkEngine()
        {
            InitializeComponent();
        }
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        private void NightmareEngine_Load(object sender, EventArgs e)
        {

        }
        public void Corrupt(int intensity, List<string> filenames, Form1 main = null)
        {
            int currentType = 0;
            currentType = comboBox1.SelectedIndex;
            Thread thread = new Thread(() =>
            {
               try
                {
                    if (currentType == 0) 
                    {
                        Int32 length = filenames.Count;
                        for (int i = 0; i < length; i++)
                        {
                            byte[] buffer;
                            using (FileStream file = File.Open(filenames[i], FileMode.Open))
                            {
                                for (int j = 0; j < intensity; j++)
                                {
                                    buffer = new byte[(long)numericUpDown1.Value];
                                    file.Position = (int)RandomNumber(0 + (long)numericUpDown3.Value, file.Length - (long)numericUpDown1.Value);
                                    file.Read(buffer, 0, (int)numericUpDown1.Value);
                                    file.Position = (int)RandomNumber(0 + (long)numericUpDown3.Value, file.Length - (long)numericUpDown1.Value);
                                    OverwriteBytesinFile(buffer, file);
                                }
                                file.Flush();
                                file.Close();
                                buffer = null;
                            }
                        }
                    }
                    else if (currentType == 1)
                    {
                        Int32 length = filenames.Count;
                        for (int i = 0; i < length; i++)
                        {
                            byte[] buffer;
                            byte[] buffer1;
                            byte[] buffercombined;
                            using (FileStream file = File.Open(filenames[i], FileMode.Open))
                            {
                                for (int j = 0; j < intensity; j++)
                                {
                                    buffer = new byte[(long)numericUpDown1.Value];
                                    buffer1 = new byte[(long)numericUpDown1.Value];
                                    buffercombined = new byte[(long)numericUpDown1.Value + (long)numericUpDown1.Value];
                                    int oldvalue = (int)RandomNumber(0 + (long)numericUpDown3.Value, file.Length - ((long)numericUpDown1.Value + (long)numericUpDown1.Value));
                                    file.Position = oldvalue;
                                    file.Read(buffer, 0, (int)numericUpDown1.Value);
                                    file.Read(buffer1, 0, (int)numericUpDown1.Value);
                                    file.Position = oldvalue;
                                    Buffer.BlockCopy(buffer1, 0, buffercombined, 0, buffer1.Length);
                                    Buffer.BlockCopy(buffer, 0, buffercombined, buffer1.Length, buffer.Length);
                                    OverwriteBytesinFile(buffercombined, file);
                                }
                                file.Flush();
                                file.Close();
                                buffer = null;
                                buffer1 = null;
                                buffercombined = null;
                            }
                           
                        }
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
        public void OverwriteBytesinFile(byte[] buffer, FileStream stream)
        {
            for (int i = 0; i < buffer.Length; i++) 
            {
                stream.WriteByte(buffer[i]);
            }
        }
    }
}
