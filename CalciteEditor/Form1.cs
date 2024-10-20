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
        public Form1(string Filepath)
        {
            InitializeComponent();
            _fuiReader = new FourjUIReader();

            _fui = _fuiReader.FromFile(Filepath);
            _fuiFilePath = Filepath;
            //ActiveForm.Text = "[BETA]Calcite FUI Editor: " + _fui.Header.SwfFileName;
            treeView1.Nodes.Clear();
            MapImages();

            saveToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            closeToolStripMenuItem.Enabled = true;
            exportToJSONToolStripMenuItem.Enabled = true;
            treeView1.ExpandAll();
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

                saveToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
                closeToolStripMenuItem.Enabled = true;
                exportToJSONToolStripMenuItem.Enabled = true;
                treeView1.ExpandAll();

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
                MessageBox.Show("Saved!");
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fui = null;
            System.GC.Collect();
            ActiveForm.Text = "[BETA]Calcite FUI Editor";
            treeView1.Nodes.Clear();
            richTextBox1.Text = "";
            pictureBoxWithInterpolationMode1.Image = null;


            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            closeToolStripMenuItem.Enabled = false;
            exportToJSONToolStripMenuItem.Enabled = false;
        }

        public void MapImages() 
        {
            TreeNode tn = new TreeNode();
            tn.Text = _fui.Header.SwfFileName;

            TreeNode tnImages = new TreeNode();
            tnImages.Text = "Images";
            
            TreeNode tnActions = new TreeNode();
            tnActions.Text = "Timeline Actions";
            
            TreeNode tnEvents = new TreeNode();
            tnEvents.Text = "Timeline Events";
            

            int i = 0;
            foreach (FuiBitmap bitmap in _fui.Bitmaps)
            {
                TreeNode tnBitmap = new TreeNode();

                if (bitmap.SymbolIndex == -1)
                    tnBitmap.Text = "Image[" + i + "]";
                else 
                {
                    tnBitmap.Text = _fui.Symbols[bitmap.SymbolIndex].Name;
                }

                tnBitmap.Tag = i;
                tnImages.Nodes.Add(tnBitmap);
                i++;
            }
            i = 0;
            foreach (FuiTimelineAction action in _fui.TimelineActions)
            {
                TreeNode tnBitmap = new TreeNode();
                tnBitmap.Text = action.StringArg0 + ":" + action.StringArg1;
                tnBitmap.Tag = i;
                if (!string.IsNullOrEmpty(action.StringArg0) && !string.IsNullOrEmpty(action.StringArg1))
                    tnActions.Nodes.Add(tnBitmap);
                i++;
            }
            i = 0;
            foreach (FuiTimelineEvent _event in _fui.TimelineEvents)
            {
                TreeNode tnBitmap = new TreeNode();
                if (_fui.References.Count > (int)_event.Index)
                    tnBitmap.Text = _fui.References[(int)_event.Index].Name;
                else
                    tnBitmap.Text = "Event["+i+"]";
                tnBitmap.Tag = i;
                tnEvents.Nodes.Add(tnBitmap);
                i++;
            }

            tn.Nodes.Add(tnActions);
            tn.Nodes.Add(tnEvents);
            tn.Nodes.Add(tnImages);

            treeView1.Nodes.Add(tn);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            richTextBox1.Text = "";
            pictureBoxWithInterpolationMode1.Image = null;
            if (treeView1.SelectedNode.Parent == null)
                return;
            if (treeView1.SelectedNode.Parent.Text.StartsWith("Images")) 
            {
                int index = (int)treeView1.SelectedNode.Tag;
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
            if (treeView1.SelectedNode.Parent == null)
                return;
            if (treeView1.SelectedNode.Parent.Text.StartsWith("Images"))
            {
                int index = (int)treeView1.SelectedNode.Tag;

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

                    if (ofd.Filter.Contains("PNG Images|*.png"))
                    {
                        Bitmap PNGImage = new Bitmap(ofd.FileName);
                        SwapColor(PNGImage);
                        ImageConverter converter = new ImageConverter();
                        byte[] PNGData = (byte[])converter.ConvertTo(PNGImage, typeof(byte[]));
                        _fui.ImagesData[index] = PNGData;
                    }
                    else
                        _fui.ImagesData[index] = File.ReadAllBytes(ofd.FileName);
                    MessageBox.Show("Replaced!");
                }

            }
        }

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Parent == null)
                return;
            if (treeView1.SelectedNode.Parent.Text.StartsWith("Images"))
            {
                int index = (int)treeView1.SelectedNode.Tag;

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
                ofd.FileName = treeView1.SelectedNode.Text;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (ofd.Filter.Contains("PNG Images|*.png"))
                    {
                        byte[] ImageData = _fui.ImagesData[index];
                        using (var ms = new MemoryStream(ImageData))
                        {
                            Image img = Image.FromStream(ms);
                            Bitmap bmp = new Bitmap(img);
                            SwapColor(bmp);
                            bmp.Save(ofd.FileName);
                            img.Dispose();
                            bmp.Dispose();
                            ms.Close();
                            ms.Dispose();
                        }
                    }
                    else
                        File.WriteAllBytes(ofd.FileName, _fui.ImagesData[index]);
                    MessageBox.Show("Extracted!");
                }

            }
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About abtform = new About();
            abtform.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (treeView1.SelectedNode.Parent == null)
            {
                replaceToolStripMenuItem.Visible = false;
                extractToolStripMenuItem.Visible = false;
                copyActionObjectNameToolStripMenuItem.Visible = false;
                return;
            }
            if (treeView1.SelectedNode.Parent.Text.StartsWith("Images"))
            {
                replaceToolStripMenuItem.Visible = true;
                extractToolStripMenuItem.Visible = true;
                copyActionObjectNameToolStripMenuItem.Visible = false;
                return;
            }
            if (treeView1.SelectedNode.Parent.Text.StartsWith("Timeline Actions"))
            {
                replaceToolStripMenuItem.Visible = false;
                extractToolStripMenuItem.Visible = false;
                copyActionObjectNameToolStripMenuItem.Visible = true;
                return;
            }
            replaceToolStripMenuItem.Visible = false;
            extractToolStripMenuItem.Visible = false;
            copyActionObjectNameToolStripMenuItem.Visible = false;
        }

        private void copyActionObjectNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string actionName = treeView1.SelectedNode.Text.Split(':')[0];
            Clipboard.SetText(actionName);
            MessageBox.Show("Copied \"" + actionName + "\" to the clipboard!");

        }

        private void exportToJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JSON File|*.json";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, Newtonsoft.Json.JsonConvert.SerializeObject(_fui, Newtonsoft.Json.Formatting.Indented));
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Parent == null)
            {
                return;
            }
            if (treeView1.SelectedNode.Parent.Text.StartsWith("Timeline Actions"))
            {
                int index = (int)treeView1.SelectedNode.Tag;
                Forms.Editors.TimelineActionEditor ActionEditor = new Forms.Editors.TimelineActionEditor(_fui.TimelineActions[index]);
                if (ActionEditor.ShowDialog() == DialogResult.OK) 
                {
                    _fui.TimelineActions[index] = ActionEditor._action;
                }
                return;
            }
            if (treeView1.SelectedNode.Parent.Text.StartsWith("Timeline Events"))
            {
                int index = (int)treeView1.SelectedNode.Tag;
                Forms.Editors.TimelineEventEditor ActionEditor = new Forms.Editors.TimelineEventEditor(_fui, index);
                if (ActionEditor.ShowDialog() == DialogResult.OK) 
                {
                    _fui.TimelineEvents[index] = ActionEditor._event;
                }
                return;
            }
        }
    }
}
