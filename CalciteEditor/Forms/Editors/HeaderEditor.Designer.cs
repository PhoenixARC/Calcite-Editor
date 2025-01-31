namespace CalciteEditor.Forms.Editors
{
    partial class HeaderEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HeaderEditor));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MinYSize = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.MinXSize = new System.Windows.Forms.NumericUpDown();
            this.MaxXSize = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.MaxYSize = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.MinYSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinXSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxXSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxYSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Frame Size Min Y";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Frame Size Min X";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "FUI Original Name";
            // 
            // MinYSize
            // 
            this.MinYSize.Location = new System.Drawing.Point(12, 102);
            this.MinYSize.Maximum = new decimal(new int[] {
            8064,
            0,
            0,
            0});
            this.MinYSize.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.MinYSize.Name = "MinYSize";
            this.MinYSize.Size = new System.Drawing.Size(120, 20);
            this.MinYSize.TabIndex = 10;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 24);
            this.textBox1.MaxLength = 64;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(281, 20);
            this.textBox1.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(117, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MinXSize
            // 
            this.MinXSize.Location = new System.Drawing.Point(12, 63);
            this.MinXSize.Maximum = new decimal(new int[] {
            8064,
            0,
            0,
            0});
            this.MinXSize.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.MinXSize.Name = "MinXSize";
            this.MinXSize.Size = new System.Drawing.Size(120, 20);
            this.MinXSize.TabIndex = 14;
            // 
            // MaxXSize
            // 
            this.MaxXSize.Location = new System.Drawing.Point(173, 63);
            this.MaxXSize.Maximum = new decimal(new int[] {
            8064,
            0,
            0,
            0});
            this.MaxXSize.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.MaxXSize.Name = "MaxXSize";
            this.MaxXSize.Size = new System.Drawing.Size(120, 20);
            this.MaxXSize.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(173, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Frame Size Max Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(173, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Frame Size Max X";
            // 
            // MaxYSize
            // 
            this.MaxYSize.Location = new System.Drawing.Point(173, 102);
            this.MaxYSize.Maximum = new decimal(new int[] {
            8064,
            0,
            0,
            0});
            this.MaxYSize.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.MaxYSize.Name = "MaxYSize";
            this.MaxYSize.Size = new System.Drawing.Size(120, 20);
            this.MaxYSize.TabIndex = 15;
            // 
            // HeaderEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 156);
            this.Controls.Add(this.MaxXSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.MaxYSize);
            this.Controls.Add(this.MinXSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MinYSize);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HeaderEditor";
            this.Text = "Header Editor";
            ((System.ComponentModel.ISupportInitialize)(this.MinYSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinXSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxXSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxYSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown MinYSize;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown MinXSize;
        private System.Windows.Forms.NumericUpDown MaxXSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown MaxYSize;
    }
}