using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OMI.Formats.FUI;
using OMI.Formats.FUI.Components;
using OMI.Workers.FUI;

namespace CalciteEditor
{
    public partial class Form1 : Form
    {
        public FourjUserInterface _fui;
        public FourjUIReader _fuiReader;
        public FourjUIWriter _fuiWriter;
        public string _fuiFilePath;
        public Form1()
        {
            InitializeComponent();
            _fuiReader = new FourjUIReader();
            _fuiWriter = new FourjUIWriter(_fui);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "FUI Files|*.fui";
            if (ofd.ShowDialog() == DialogResult.OK) 
            {
                _fui = _fuiReader.FromFile(ofd.FileName);
                _fuiFilePath = ofd.FileName;
                ActiveForm.Text = "[BETA]Calcite FUI Editor: " + _fui.Header.SwfFileName;
                treeView1.Nodes.Clear();
                MapImages();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fuiWriter = new FourjUIWriter(_fui);
            if (string.IsNullOrEmpty(_fuiFilePath))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "FUI Files|*.fui";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    _fuiWriter.WriteToFile(sfd.FileName);
                }
                MessageBox.Show("Saved!");
                return;
            }
            _fuiWriter.WriteToFile(_fuiFilePath);
            MessageBox.Show("Saved!");

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "FUI Files|*.fui";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                _fuiWriter = new FourjUIWriter(_fui);
                _fuiWriter.WriteToFile(sfd.FileName);
            }
            MessageBox.Show("Saved!");
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fui = null;
            System.GC.Collect();
            ActiveForm.Text = "[BETA]Calcite FUI Editor";
            treeView1.Nodes.Clear();
        }

        public void MapImages() 
        {
            TreeNode tn = new TreeNode();
            tn.Text = _fui.Header.SwfFileName;

            TreeNode tnImages = new TreeNode();
            tnImages.Text = "images";

            int i = 0;
            foreach (FuiBitmap bitmap in _fui.Bitmaps)
            {
                TreeNode tnBitmap = new TreeNode();
                tnBitmap.Text = "Image["+i+"]";
                tnImages.Nodes.Add(tnBitmap);
                i++;
            }
            tn.Nodes.Add(tnImages);

            treeView1.Nodes.Add(tn);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            richTextBox1.Text = "";
            pictureBoxWithInterpolationMode1.Image = null;

            if (treeView1.SelectedNode.Text.StartsWith("Image[")) 
            {
                int index = int.Parse(treeView1.SelectedNode.Text.Replace("Image[","").Replace("]",""));
                byte[] ImageData = _fui.ImagesData[index];
                FuiBitmap bitmap = _fui.Bitmaps[index];
                using (var ms = new MemoryStream(ImageData))
                {
                    Image img = Image.FromStream(ms);
                    Bitmap bmp = new Bitmap(img);
                    if (bitmap.ImageFormat == FuiBitmap.FuiImageFormat.PNG_WITH_ALPHA_DATA || bitmap.ImageFormat == FuiBitmap.FuiImageFormat.PNG_NO_ALPHA_DATA)
                        SwapColor(bmp);
                    pictureBoxWithInterpolationMode1.Image = bmp;
                }
                richTextBox1.Text += "SymbolIndex: " + bitmap.SymbolIndex + "\n";
                richTextBox1.Text += "FORMAT: ";
                switch (bitmap.ImageFormat)
                {
                    case FuiBitmap.FuiImageFormat.PNG_WITH_ALPHA_DATA:
                        richTextBox1.Text += "PNG_WITH_ALPHA_DATA\n";
                        break;
                    case FuiBitmap.FuiImageFormat.PNG_NO_ALPHA_DATA:
                        richTextBox1.Text += "PNG_NO_ALPHA_DATA\n";
                        break;
                    case FuiBitmap.FuiImageFormat.JPEG_NO_ALPHA_DATA:
                        richTextBox1.Text += "JPEG_NO_ALPHA_DATA\n";
                        break;
                    case FuiBitmap.FuiImageFormat.JPEG_UNKNOWN:
                        richTextBox1.Text += "JPEG_UNKNOWN\n";
                        break;
                    case FuiBitmap.FuiImageFormat.JPEG_WITH_ALPHA_DATA:
                        richTextBox1.Text += "JPEG_WITH_ALPHA_DATA\n";
                        break;
                }
            }
        }

        private static void SwapColor(Bitmap bmp)
        {
            for (int y = 0; y < bmp.Height; y++)
    {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color col = bmp.GetPixel(x, y);
                    bmp.SetPixel(x, y, Color.FromArgb(col.A, col.B, col.G, col.R));
                }
            }
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Text.StartsWith("Image["))
            {
                int index = int.Parse(treeView1.SelectedNode.Text.Replace("Image[", "").Replace("]", ""));

                OpenFileDialog ofd = new OpenFileDialog();
                FuiBitmap bitmap = _fui.Bitmaps[index];
                switch (bitmap.ImageFormat)
                {
                    case FuiBitmap.FuiImageFormat.PNG_WITH_ALPHA_DATA:
                        ofd.Filter = "PNG Images|*.png";
                        break;
                    case FuiBitmap.FuiImageFormat.PNG_NO_ALPHA_DATA:
                        ofd.Filter = "PNG Images|*.png";
                        break;
                    case FuiBitmap.FuiImageFormat.JPEG_NO_ALPHA_DATA:
                        ofd.Filter = "JPEG Images|*.jpg";
                        break;
                    case FuiBitmap.FuiImageFormat.JPEG_UNKNOWN:
                        ofd.Filter = "JPEG Images|*.jpg";
                        break;
                    case FuiBitmap.FuiImageFormat.JPEG_WITH_ALPHA_DATA:
                        ofd.Filter = "JPEG Images|*.jpg";
                        break;
                }
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _fui.ImagesData[index] = File.ReadAllBytes(ofd.FileName);
                }

            }
            MessageBox.Show("Replaced!");
        }

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Text.StartsWith("Image["))
            {
                int index = int.Parse(treeView1.SelectedNode.Text.Replace("Image[", "").Replace("]", ""));

                SaveFileDialog ofd = new SaveFileDialog();
                FuiBitmap bitmap = _fui.Bitmaps[index];
                switch (bitmap.ImageFormat)
                {
                    case FuiBitmap.FuiImageFormat.PNG_WITH_ALPHA_DATA:
                        ofd.Filter = "PNG Images|*.png";
                        break;
                    case FuiBitmap.FuiImageFormat.PNG_NO_ALPHA_DATA:
                        ofd.Filter = "PNG Images|*.png";
                        break;
                    case FuiBitmap.FuiImageFormat.JPEG_NO_ALPHA_DATA:
                        ofd.Filter = "JPEG Images|*.jpg";
                        break;
                    case FuiBitmap.FuiImageFormat.JPEG_UNKNOWN:
                        ofd.Filter = "JPEG Images|*.jpg";
                        break;
                    case FuiBitmap.FuiImageFormat.JPEG_WITH_ALPHA_DATA:
                        ofd.Filter = "JPEG Images|*.jpg";
                        break;
                }
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(ofd.FileName, _fui.ImagesData[index]);
                }

            }
            MessageBox.Show("Extracted!");
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About abtform = new About();
            abtform.ShowDialog();
        }
    }
}
