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
            Thread thread = new Thread(() =>
            {
               try
                {
                    Int32 length = filenames.Count;
                    for (int i = 0; i < length; i++)
                    {
                        byte[] buffer;
                        FileStream file = File.Open(filenames[i], FileMode.Open);
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
                        file.Dispose();
                        buffer = null;
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
