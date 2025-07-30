using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OMI.Formats.FUI;
using OMI.Formats.FUI.Components;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CalciteEditor.Forms.Editors
{
    public partial class TimelineEventEditor : Form
    {
        public FourjUserInterface _fui;
        public FuiTimelineEvent _event;

        public TimelineEventEditor(FourjUserInterface fui, int index)
        {
            InitializeComponent();
            _fui = fui;
            _event = _fui.TimelineEvents[index];

            foreach (FuiReference _ref in _fui.References)
            {
                comboBox1.Items.Add(_ref.Name);
            }

            numericUpDown1.Value = _event.EventType;
            numericUpDown2.Value = _event.ObjectType;
            if(_event.Index < comboBox1.Items.Count)
                comboBox1.SelectedIndex = _event.Index;
            else
                comboBox1.Enabled = false;

            numericUpDown3.Value = (decimal)_event.Matrix.Scale.Height;
            numericUpDown4.Value = (decimal)_event.Matrix.Scale.Width;

            numericUpDown5.Value = (decimal)_event.Matrix.Translation.X;
            numericUpDown6.Value = (decimal)_event.Matrix.Translation.Y;

            numericUpDown7.Value = (decimal)_event.NameIndex;
            numericUpDown8.Value = (decimal)_event.Index;

            numericUpDown10.Value = (decimal)_event.Matrix.RotateSkew0;
            numericUpDown9.Value = (decimal)_event.Matrix.RotateSkew1;

            checkBox1.Checked = _event.Matrix.Translation.IsEmpty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _event.EventType = (short)numericUpDown1.Value;
            _event.ObjectType = (short)numericUpDown2.Value;
            if(comboBox1.Enabled)
                _event.Index = (short)comboBox1.SelectedIndex;

            _event.Matrix.Scale.Height = (float)numericUpDown3.Value;
            _event.Matrix.Scale.Width = (float)numericUpDown4.Value;

            _event.Matrix.Translation.X = (float)numericUpDown5.Value;
            _event.Matrix.Translation.Y = (float)numericUpDown6.Value;

            _event.Matrix.RotateSkew0 = (float)numericUpDown10.Value;
            _event.Matrix.RotateSkew1 = (float)numericUpDown9.Value;


            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
