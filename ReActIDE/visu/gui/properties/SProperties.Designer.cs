namespace posh.visu
{
    partial class SProperties
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.retries = new System.Windows.Forms.NumericUpDown();
            this.action = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comparator = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.retries)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comparator);
            this.groupBox1.Controls.Add(this.retries);
            this.groupBox1.Controls.Add(this.action);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 88);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sense";
            // 
            // retries
            // 
            this.retries.Location = new System.Drawing.Point(100, 63);
            this.retries.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.retries.Name = "retries";
            this.retries.Size = new System.Drawing.Size(38, 20);
            this.retries.TabIndex = 5;
            this.retries.ValueChanged += new System.EventHandler(this.retries_ValueChanged);
            // 
            // action
            // 
            this.action.FormattingEnabled = true;
            this.action.Location = new System.Drawing.Point(45, 19);
            this.action.Name = "action";
            this.action.Size = new System.Drawing.Size(93, 21);
            this.action.TabIndex = 3;
            this.action.SelectedIndexChanged += new System.EventHandler(this.action_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "name";
            // 
            // comparator
            // 
            this.comparator.FormattingEnabled = true;
            this.comparator.Items.AddRange(new object[] {
            "==",
            "<=",
            ">=",
            "!=",
            "<",
            ">"});
            this.comparator.Location = new System.Drawing.Point(45, 62);
            this.comparator.Name = "comparator";
            this.comparator.Size = new System.Drawing.Size(40, 21);
            this.comparator.TabIndex = 6;
            this.comparator.SelectedIndexChanged += new System.EventHandler(this.comparator_SelectedIndexChanged);
            // 
            // SProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "SProperties";
            this.Size = new System.Drawing.Size(150, 94);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.retries)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown retries;
        private System.Windows.Forms.ComboBox action;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comparator;
    }
}
