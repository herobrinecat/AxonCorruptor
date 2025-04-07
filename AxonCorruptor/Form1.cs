using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace AxonCorruptor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<byte[]> data = new List<byte[]>();
        List<string> filenames = new List<string>();
        bool corrupted = false;
        private static readonly object syncLock = new object();
        Random random = new Random();
        public bool error = false;
        string stockpileloaded = "0";
        int filepile = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            stockpileloaded = RandomNumber(0, 2147483646).ToString();
            saveFileDialog1.FileName = stockpileloaded.ToString();
            listBox1.AllowDrop = true;
            if (Properties.Settings.Default.WarningRead == false)
            {
                new Warning().ShowDialog();
            }
            NightmareEngine nightmareEngine = new NightmareEngine();
            nightmareEngine.TopLevel = false;
            EngineForm.Controls.Add(nightmareEngine);

            nightmareEngine.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value <= 100000) 
            {
                trackBar1.Value = (int)numericUpDown1.Value;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Value <= 100000)
            {
                numericUpDown1.Value = (int)trackBar1.Value;
            }
        }
        private int RandomNumber(int min, int max)
        {
            lock (syncLock)
            {
                return random.Next(min, max);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
      if (filenames.Count > 0)
            {
                try
                {
                    if (corrupted == true)
                    {
                        if (EngineForm.Controls[0] is NightmareEngine)
                        {
                            List<string> selecteddata = new List<string>();
                            for (int i = 0; i < listBox1.Items.Count; i++)
                            {
                                if (listBox1.GetSelected(i) == true)
                                {
                                    selecteddata.Add(filenames[i]);
                                }
                            }
                            button1.Visible = false;
                            for (int i = 0; i < filenames.Count; i++)
                            {
                                using (FileStream file = File.Open(filenames[i], FileMode.Open))
                                {
                                    file.Write(data[i], 0, data[i].Length);
                                    file.Flush();
                                    file.Close();
                                }
                            }
                        (EngineForm.Controls[0] as NightmareEngine).Corrupt((int)numericUpDown1.Value, selecteddata);
                            button1.Visible = true;
                            button6.Visible = true;
                            button5.Visible = true;
                            corrupted = true;
                            selecteddata.Clear();
                        }
                        else if (EngineForm.Controls[0] is NumberEngine)
                        {
                            List<string> selecteddata = new List<string>();
                            for (int i = 0; i < listBox1.Items.Count; i++)
                            {
                                if (listBox1.GetSelected(i) == true)
                                {
                                    selecteddata.Add(filenames[i]);
                                }
                            }
                            button1.Visible = false;
                            for (int i = 0; i < filenames.Count; i++)
                            {
                                using (FileStream file = File.Open(filenames[i], FileMode.Open))
                                {
                                    file.Write(data[i], 0, data[i].Length);
                                    file.Flush();
                                    file.Close();
                                }
                            }
                            (EngineForm.Controls[0] as NumberEngine).Corrupt((int)numericUpDown1.Value, selecteddata);
                            button1.Visible = true;
                            button6.Visible = true;
                            button5.Visible = true;
                            corrupted = true;
                            selecteddata.Clear();
                        }
                        else if (EngineForm.Controls[0] is ChunkEngine)
                        {
                            List<string> selecteddata = new List<string>();
                            for (int i = 0; i < listBox1.Items.Count; i++)
                            {
                                if (listBox1.GetSelected(i) == true)
                                {
                                    selecteddata.Add(filenames[i]);
                                }
                            }
                            button1.Visible = false;
                            for (int i = 0; i < filenames.Count; i++)
                            {
                                using (FileStream file = File.Open(filenames[i], FileMode.Open))
                                {
                                    file.Write(data[i], 0, data[i].Length);
                                    file.Flush();
                                    file.Close();
                                }
                            }
                        (EngineForm.Controls[0] as ChunkEngine).Corrupt((int)numericUpDown1.Value, selecteddata);
                            button1.Visible = true;
                            button6.Visible = true;
                            button5.Visible = true;
                            corrupted = true;
                            selecteddata.Clear();
                        }
                    }
                    else
                    {
                        if (EngineForm.Controls[0] is NightmareEngine)
                        {
                            List<string> selecteddata = new List<string>();
                            for (int i = 0; i < listBox1.Items.Count; i++)
                            {
                                if (listBox1.GetSelected(i) == true)
                                {
                                    selecteddata.Add(filenames[i]);
                                }
                            }
                            button1.Visible = false;
                            (EngineForm.Controls[0] as NightmareEngine).Corrupt((int)numericUpDown1.Value, selecteddata, this);
                            button1.Visible = true;
                            if (error == false)
                            {
                                button6.Visible = true;
                                button5.Visible = true;
                                corrupted = true;
                            }
                            error = false;
                            selecteddata.Clear();
                        }
                        else if (EngineForm.Controls[0] is NumberEngine)
                        {
                            List<string> selecteddata = new List<string>();
                            for (int i = 0; i < listBox1.Items.Count; i++)
                            {
                                if (listBox1.GetSelected(i) == true)
                                {
                                    selecteddata.Add(filenames[i]);
                                }
                            }
                            button1.Visible = false;
                            (EngineForm.Controls[0] as NumberEngine).Corrupt((int)numericUpDown1.Value, selecteddata, this);
                            button1.Visible = true;
                            if (error == false)
                            {
                                button6.Visible = true;
                                button5.Visible = true;
                                corrupted = true;
                            }
                            error = false;
                            selecteddata.Clear();
                        }
                        else if (EngineForm.Controls[0] is ChunkEngine)
                        {
                            List<string> selecteddata = new List<string>();
                            for (int i = 0; i < listBox1.Items.Count; i++)
                            {
                                if (listBox1.GetSelected(i) == true)
                                {
                                    selecteddata.Add(filenames[i]);
                                }
                            }
                            button1.Visible = false;
                            (EngineForm.Controls[0] as ChunkEngine).Corrupt((int)numericUpDown1.Value, selecteddata, this);
                            button1.Visible = true;
                            if (error == false)
                            {
                                button6.Visible = true;
                                button5.Visible = true;
                                corrupted = true;
                            }
                            error = false;
                            selecteddata.Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    button1.Visible = true;
                    new ErrorForm(ex).ShowDialog();
                }
            }
      else
            {
                MessageBox.Show("There's no files in the domain!", "Axon Corruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) 
            {
           if (!(EngineForm.Controls[0] is NightmareEngine))
                {
                    EngineForm.Controls[0].Dispose();
                    EngineForm.Controls.Clear();
                    NightmareEngine nightmare = new NightmareEngine();
                    nightmare.TopLevel = false;
                    EngineForm.Controls.Add(nightmare);
                    nightmare.Show();
                }
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                if (!(EngineForm.Controls[0] is NumberEngine))
                {
                    EngineForm.Controls[0].Dispose();
                    EngineForm.Controls.Clear();
                    NumberEngine number = new NumberEngine();
                    number.TopLevel = false;
                    EngineForm.Controls.Add(number);
                    number.Show();
                }
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                if (!(EngineForm.Controls[0] is ChunkEngine))
                {
                    EngineForm.Controls[0].Dispose();
                    EngineForm.Controls.Clear();
                    ChunkEngine chunk = new ChunkEngine();
                    chunk.TopLevel = false;
                    EngineForm.Controls.Add(chunk);
                    chunk.Show();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < filenames.Count; i++)
                {
                    using (FileStream file = File.Open(filenames[i], FileMode.Open))
                    {
                        file.Write(data[i], 0, data[i].Length);
                        file.Flush();
                        file.Close();
                    }
                }
                button6.Visible = false;
                button5.Visible = false;
                corrupted = false;
            }
            catch (Exception ex) 
            {
                new ErrorForm(ex).Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) 
            {
                int listboxcount = listBox1.Items.Count;
                for (int i = 0; i < openFileDialog1.FileNames.Count(); i++) 
                {
                    button7.Visible = true;
                    listBox1.Items.Add(openFileDialog1.FileNames[i]);
                    data.Add(File.ReadAllBytes(openFileDialog1.FileNames[i]));
                    filenames.Add(openFileDialog1.FileNames[i]);
                    listBox1.SetSelected(i + listboxcount, true);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           for (int i = 0; i < listBox1.Items.Count; i++)
            {
                listBox1.SetSelected(i, true);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                listBox1.SetSelected(i, false);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (corrupted == true)
            {
                if (MessageBox.Show("Looks like you still have corrupted files! Wanna restore it?", "Wait!", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        for (int i = 0; i < filenames.Count; i++)
                        {
                            using (FileStream file = File.Open(filenames[i], FileMode.Open))
                            {
                                file.Write(data[i], 0, data[i].Length);
                                file.Flush();
                                file.Close();
                            }
                        }
                        button6.Visible = false;
                        button5.Visible = false;
                        corrupted = false;
                        filenames.Clear();
                        listBox1.Items.Clear();
                        data.Clear();
                        button7.Visible = false;
                    }
                    catch (Exception ex) 
                    {
                        new ErrorForm(ex).ShowDialog();
                    }
                }
                else
                {
                    button6.Visible = false;
                    button5.Visible = false;
                    corrupted = false;
                    filenames.Clear();
                    listBox1.Items.Clear();
                    data.Clear();
                    button7.Visible = false;
                }
            }
            else
            {
                button6.Visible = false;
                button5.Visible = false;
                corrupted = false;
                filenames.Clear();
                listBox1.Items.Clear();
                data.Clear();
                button7.Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < filenames.Count(); i++)
            {
                data[i] = File.ReadAllBytes(openFileDialog1.FileNames[i]);
                button6.Visible = false;
                button5.Visible = false;
                corrupted = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (corrupted == true)
            {
                if (MessageBox.Show("Looks like you still have corrupted files! Wanna restore it?", "Wait!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        for (int i = 0; i < filenames.Count; i++)
                        {
                            using (FileStream file = File.Open(filenames[i], FileMode.Open))
                            {
                                file.Write(data[i], 0, data[i].Length);
                                file.Flush();
                                file.Close();
                            }
                        }
                        button6.Visible = false;
                        button5.Visible = false;
                        corrupted = false;
                        filenames.Clear();
                        listBox1.Items.Clear();
                        data.Clear();
                        button7.Visible = false;
                    }
                    catch (Exception ex) 
                    {
                        new ErrorForm(ex).ShowDialog();
                        e.Cancel = true;
                    }
                }
                else
                {
                    button6.Visible = false;
                    button5.Visible = false;
                    corrupted = false;
                    filenames.Clear();
                    listBox1.Items.Clear();
                    data.Clear();
                    button7.Visible = false;
                }
            }
            else
            {
                button6.Visible = false;
                button5.Visible = false;
                corrupted = false;
                filenames.Clear();
                listBox1.Items.Clear();
                data.Clear();
                button7.Visible = false;
            }
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            int listboxcount = listBox1.Items.Count;
            for (int i = 0; i < files.Length; i++)
            {
                
                button7.Visible = true;
                listBox1.Items.Add(files[i]);
                data.Add(File.ReadAllBytes(files[i]));
                filenames.Add(files[i]);
                listBox1.SetSelected(i + listboxcount, true);
            }

        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int selectedindex = listBox2.SelectedIndex;
                if (selectedindex != -1)
                {
                    button1.Visible = false;
                    string[] files = Directory.GetFiles(Path.GetTempPath() + @"AxonTemp\" + stockpileloaded + @"\" + listBox2.Items[selectedindex]);
                    for (int i = 0; i < files.Length; i++)
                    {
                        foreach (string file in filenames)
                        {
                            if (Path.GetFileName(files[i]) == Path.GetFileName(file))
                            {
                                File.Copy(files[i], file, true);
                            }
                        }

                    }
                    button6.Visible = true;
                    button5.Visible = true;
                    corrupted = true;
                    button1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                new ErrorForm(ex).Show();
                button1.Visible = true;
            }
        }
        

        private void button9_Click(object sender, EventArgs e)
        {
           try
            {
                if (openFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    if (Directory.Exists(Path.GetTempPath() + "AxonTemp"))
                    {
                        if (Directory.GetFiles(Path.GetTempPath() + "AxonTemp").Length > 0)
                        {
                            string[] filestest = Directory.GetFiles(Path.GetTempPath() + "AxonTemp");
                            foreach (string filename in filestest)
                            {
                                if (filename.EndsWith(".zip"))
                                {
                                    Directory.Delete(Path.GetTempPath() + @"AxonTemp\" + Path.GetFileName(filename).Replace(".zip", ""), true);
                                    File.Delete(filename);
                                    listBox2.Items.Clear();
                                    File.Copy(openFileDialog2.FileName, Path.GetTempPath() + @"AxonTemp\" + Path.GetFileName(openFileDialog2.FileName));
                                    File.Move(Path.GetTempPath() + @"AxonTemp\" + Path.GetFileNameWithoutExtension(openFileDialog2.FileName) + ".asp", Path.GetTempPath() + @"AxonTemp\" + Path.GetFileNameWithoutExtension(openFileDialog2.FileName) + ".zip");
                                    Directory.CreateDirectory(Path.GetTempPath() + @"AxonTemp\" + Path.GetFileNameWithoutExtension(openFileDialog2.FileName));
                                    ZipFile.ExtractToDirectory(Path.GetTempPath() + @"AxonTemp\" + Path.GetFileNameWithoutExtension(openFileDialog2.FileName) + ".zip", Path.GetTempPath() + @"AxonTemp\" + Path.GetFileNameWithoutExtension(openFileDialog2.FileName));
                                    stockpileloaded = Path.GetFileNameWithoutExtension(openFileDialog2.FileName);
                                    foreach (string corfilename in Directory.GetDirectories(Path.GetTempPath() + @"AxonTemp\" + Path.GetFileNameWithoutExtension(openFileDialog2.FileName)))
                                    {
                                        listBox2.Items.Add(Path.GetFileNameWithoutExtension(corfilename));
                                    }
                                }
                            }

                        }
                        else
                        {
                            string[] directories = Directory.GetDirectories(Path.GetTempPath() + @"AxonTemp");
                            foreach (string directory in directories)
                            {
                                Directory.Delete(directory, true);
                            }
                            listBox2.Items.Clear();
                            File.Copy(openFileDialog2.FileName, Path.GetTempPath() + @"AxonTemp\" + Path.GetFileName(openFileDialog2.FileName));
                            File.Move(Path.GetTempPath() + @"AxonTemp\" + Path.GetFileNameWithoutExtension(openFileDialog2.FileName) + ".asp", Path.GetTempPath() + @"AxonTemp\" + Path.GetFileNameWithoutExtension(openFileDialog2.FileName) + ".zip");
                            Directory.CreateDirectory(Path.GetTempPath() + @"AxonTemp\" + Path.GetFileNameWithoutExtension(openFileDialog2.FileName));
                            ZipFile.ExtractToDirectory(Path.GetTempPath() + @"AxonTemp\" + Path.GetFileNameWithoutExtension(openFileDialog2.FileName) + ".zip", Path.GetTempPath() + @"AxonTemp\" + Path.GetFileNameWithoutExtension(openFileDialog2.FileName));
                            stockpileloaded = Path.GetFileNameWithoutExtension(openFileDialog2.FileName);
                            foreach (string corfilename in Directory.GetDirectories(Path.GetTempPath() + @"AxonTemp\" + Path.GetFileNameWithoutExtension(openFileDialog2.FileName)))
                            {
                                listBox2.Items.Add(Path.GetFileNameWithoutExtension(corfilename));
                            }
                        }


                    }
                    else
                    {
                        Directory.CreateDirectory(Path.GetTempPath() + "AxonTemp");
                        File.Copy(openFileDialog2.FileName, Path.GetTempPath() + @"AxonTemp\" + Path.GetFileName(openFileDialog2.FileName));
                        File.Move(Path.GetTempPath() + @"AxonTemp\" + Path.GetFileNameWithoutExtension(openFileDialog2.FileName) + ".asp", Path.GetTempPath() + @"AxonTemp\" + Path.GetFileNameWithoutExtension(openFileDialog2.FileName) + ".zip");
                        ZipFile.ExtractToDirectory(Path.GetTempPath() + @"AxonTemp\" + Path.GetFileNameWithoutExtension(openFileDialog2.FileName) + ".zip", Path.GetTempPath() + @"AxonTemp\" + Path.GetFileNameWithoutExtension(openFileDialog2.FileName));
                        stockpileloaded = Path.GetFileNameWithoutExtension(openFileDialog2.FileName);
                        foreach (string corfilename in Directory.GetDirectories(Path.GetTempPath() + @"AxonTemp\" + Path.GetFileNameWithoutExtension(openFileDialog2.FileName)))
                        {
                            listBox2.Items.Add(Path.GetFileNameWithoutExtension(corfilename));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorForm(ex).Show();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
          try
            {
                if (corrupted == true)
                {

                    filepile = RandomNumber(0, 2147483646);
                    Directory.CreateDirectory(Path.GetTempPath() + @"AxonTemp\" + stockpileloaded + @"\" + filepile);
                    for (int i = 0; i < filenames.Count; i++)
                    {
                        File.Copy(filenames[i], Path.GetTempPath() + @"AxonTemp\" + stockpileloaded + @"\" + filepile + @"\" + Path.GetFileName(filenames[i]));
                    }
                    listBox2.Items.Add(filepile);
                }
                else
                {
                    MessageBox.Show("You haven't corrupted anything yet!", "Axon Corruptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                new ErrorForm(ex).Show();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (Directory.Exists(Path.GetTempPath() + "AxonTemp"))
                {
                    Directory.Delete(Path.GetTempPath() + "AxonTemp", true);
                }
            }
            catch (Exception ex)
            {
                new ErrorForm(ex).Show();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
           try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ZipFile.CreateFromDirectory(Path.GetTempPath() + @"AxonTemp\" + stockpileloaded, Path.GetTempPath() + @"AxonTemp\" + stockpileloaded + ".zip");
                    File.Copy(Path.GetTempPath() + @"AxonTemp\" + stockpileloaded + ".zip", saveFileDialog1.FileName, true);
                }
            }
            catch (Exception ex)
            {
                new ErrorForm(ex).Show();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedindex = listBox2.SelectedIndex;
                if (selectedindex != -1)
                {
                    button1.Visible = false;
                    string[] files = Directory.GetFiles(Path.GetTempPath() + @"AxonTemp\" + stockpileloaded + @"\" + listBox2.Items[selectedindex]);
                    for (int i = 0; i < files.Length; i++)
                    {
                        foreach (string file in filenames)
                        {
                            if (Path.GetFileName(files[i]) == Path.GetFileName(file))
                            {
                                File.Copy(files[i], file, true);
                            }
                        }

                    }
                    button6.Visible = true;
                    button5.Visible = true;
                    corrupted = true;
                    button1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                new ErrorForm(ex).Show();
                button1.Visible = true;
            }
        }
    }
}
