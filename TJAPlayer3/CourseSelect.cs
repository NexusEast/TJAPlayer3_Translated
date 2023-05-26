using DirectShowLib.DMO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static TJAPlayer3.CDTX;

namespace TJAPlayer3
{
    public partial class CourseSelect : Form
    {
        public CourseSelect()
        {
            InitializeComponent();

            button2.Select();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public static int GetSelectedCourse()
        {
            CourseSelect courseSelect = new CourseSelect();
            courseSelect.ShowDialog();
            return -1;
        }
        string fileName;
        private void button2_Click(object sender, EventArgs e)
        {
            fileName = TjaPathBox.Text;
            if (!File.Exists(fileName) || Path.GetExtension(fileName) != @".tja")
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.Filter = "Tja files (*.tja)|*.tja";
                openFileDialog1.FilterIndex = 0;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;
                }
            }
            if (File.Exists(fileName) && Path.GetExtension(fileName) == @".tja")
            {
                TjaPathBox.Text = fileName;
                CDTX dtx = new CDTX(fileName, false, 1, 0, 0, 0, true);
                comboBox1.Items.Clear();
                foreach (int item in dtx.LEVELtaiko)
                {
                    if (item != -1)
                    {
                        comboBox1.Items.Add("Level" + item.ToString());
                    }
                }
                comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            }
        }
        const int MAX_TOTAL_NOTES = 5000;
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct MyDataStruct
        {
            [MarshalAs(UnmanagedType.R4)]
            public float BPM;
            [MarshalAs(UnmanagedType.R4)]
            public float Offset;
            [MarshalAs(UnmanagedType.I4)]
            public int Count;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_TOTAL_NOTES)]
            public int[] NoteChannels;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_TOTAL_NOTES)]
            public float[] NoteTimes;
        }

        private void WriteDRXIntoFile(string FilePath,CDTX dtxInstance)
        {

            MyDataStruct myData = new MyDataStruct();
            myData.Count = dtxInstance.listChip.Count;
            myData.NoteChannels = new int[MAX_TOTAL_NOTES];
            myData.NoteTimes = new float[MAX_TOTAL_NOTES];
            myData.BPM = (float)dtxInstance.BPM;
            myData.Offset = dtxInstance.nOFFSET * 0.001f * (dtxInstance.bHasNegativeOFFSET ? -1.0f : 1.0f);
            for (int i = 0; i < dtxInstance.listChip.Count; i++)
            {
                CChip chip = dtxInstance.listChip[i];
                myData.NoteChannels[i] = chip.nChannelNumber;
                myData.NoteTimes[i] = ((float)chip.nNoiseTimems * 0.001f);
            }

            byte[] bytes = new byte[Marshal.SizeOf(myData)];
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            Marshal.StructureToPtr(myData, handle.AddrOfPinnedObject(), false);
            File.WriteAllBytes(FilePath, bytes);
            handle.Free();
        }
        private void button1_Click(object sender, EventArgs e)
        {


            if (!File.Exists(fileName) || Path.GetExtension(fileName) != @".tja")
                return;
            //fileStream.Write(bytes, 0, bytes.Length);
            
              SaveFileDialog saveFileDialog1 = new SaveFileDialog();
              saveFileDialog1.Filter = "Gumge Format|*.gge";
              saveFileDialog1.Title = "Export gge file";
              saveFileDialog1.ShowDialog();

              // If the file name is not an empty string open it for saving.
              if (saveFileDialog1.FileName != "")
              {
                CDTX dtx = new CDTX(fileName, false, 1, 0, 0, 0, true);
                WriteDRXIntoFile(saveFileDialog1.FileName, dtx);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TJAPlayer3.stage選曲.n確定された曲の難易度 = comboBox1.SelectedIndex;
        }
    }
}
