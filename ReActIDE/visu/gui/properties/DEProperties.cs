using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace posh.visu
{
    public partial class DEProperties : UserControl
    {
        private TextBox name;
        private NumericUpDown frequency;
        private ComboBox time;
        private ComboBox action;
        private Label label3;
        private Label label2;
        private Label label1;
        private GroupBox groupBox1;
    
        public DEProperties()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.name = new System.Windows.Forms.TextBox();
            this.frequency = new System.Windows.Forms.NumericUpDown();
            this.time = new System.Windows.Forms.ComboBox();
            this.action = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frequency)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.name);
            this.groupBox1.Controls.Add(this.frequency);
            this.groupBox1.Controls.Add(this.time);
            this.groupBox1.Controls.Add(this.action);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 144);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Drive Element";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(66, 30);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(72, 20);
            this.name.TabIndex = 6;
            this.name.TextChanged += new System.EventHandler(this.name_TextChanged);
            // 
            // frequency
            // 
            this.frequency.Location = new System.Drawing.Point(66, 109);
            this.frequency.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.frequency.Name = "frequency";
            this.frequency.Size = new System.Drawing.Size(38, 20);
            this.frequency.TabIndex = 5;
            this.frequency.ValueChanged += new System.EventHandler(this.frequency_ValueChanged);
            // 
            // time
            // 
            this.time.FormattingEnabled = true;
            this.time.Items.AddRange(new object[] {
            "none",
            "ms",
            "s",
            "m",
            "h"});
            this.time.Location = new System.Drawing.Point(110, 108);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(28, 21);
            this.time.TabIndex = 4;
            this.time.SelectedIndexChanged += new System.EventHandler(this.time_SelectedIndexChanged);
            // 
            // action
            // 
            this.action.FormattingEnabled = true;
            this.action.Location = new System.Drawing.Point(66, 65);
            this.action.Name = "action";
            this.action.Size = new System.Drawing.Size(72, 21);
            this.action.TabIndex = 3;
            this.action.SelectedIndexChanged += new System.EventHandler(this.action_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "frequency";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "action";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "name";
            // 
            // DEProperties
            // 
            this.Controls.Add(this.groupBox1);
            this.Name = "DEProperties";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frequency)).EndInit();
            this.ResumeLayout(false);

        }

        private void name_TextChanged(object sender, EventArgs e)
        {

        }

        private void action_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void time_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frequency_ValueChanged(object sender, EventArgs e)
        {

        }

    }
}
