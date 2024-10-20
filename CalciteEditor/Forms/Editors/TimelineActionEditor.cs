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

namespace CalciteEditor.Forms.Editors
{
    public partial class TimelineActionEditor : Form
    {
        public FuiTimelineAction _action;
        public TimelineActionEditor(FuiTimelineAction Action)
        {
            InitializeComponent();
            _action = Action;
            textBox1.Text = _action.StringArg0;
            textBox2.Text = _action.StringArg1;
            numericUpDown1.Value = _action.ActionType;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _action.StringArg0 = textBox1.Text;
            _action.StringArg1 = textBox2.Text;
            _action.ActionType = (byte)numericUpDown1.Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
