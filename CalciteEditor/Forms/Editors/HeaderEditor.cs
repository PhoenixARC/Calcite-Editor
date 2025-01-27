using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OMI.Formats.FUI.Components;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CalciteEditor.Forms.Editors
{
    public partial class HeaderEditor : Form
    {
        public FuiHeader _header;
        public HeaderEditor(FuiHeader header)
        {
            InitializeComponent();
            _header = header;
            textBox1.Text = header.SwfFileName;
            MinXSize.Value = (decimal)header.FrameSize.Min.X;
            MinYSize.Value = (decimal)header.FrameSize.Min.Y;
            MaxXSize.Value = (decimal)header.FrameSize.Max.X;
            MaxYSize.Value = (decimal)header.FrameSize.Max.Y;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            _header.SwfFileName = textBox1.Text;
               
            _header.FrameSize.Min.X = (float)MinXSize.Value;
            _header.FrameSize.Min.Y = (float)MinYSize.Value;
            _header.FrameSize.Max.X = (float)MaxXSize.Value;
            _header.FrameSize.Max.Y = (float)MaxYSize.Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
