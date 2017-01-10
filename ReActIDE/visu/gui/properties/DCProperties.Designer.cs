namespace posh.visu
{
    partial class DCProperties
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
            this.strict = new System.Windows.Forms.CheckBox();
            this.realtime = new System.Windows.Forms.CheckBox();
            this.name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.strict);
            this.groupBox1.Controls.Add(this.realtime);
            this.groupBox1.Controls.Add(this.name);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 144);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Drive Collection";
            // 
            // strict
            // 
            this.strict.AutoSize = true;
            this.strict.Location = new System.Drawing.Point(20, 105);
            this.strict.Name = "strict";
            this.strict.Size = new System.Drawing.Size(86, 17);
            this.strict.TabIndex = 8;
            this.strict.Text = "Strict Mode?";
            this.strict.UseVisualStyleBackColor = true;
            this.strict.CheckedChanged += new System.EventHandler(this.strict_CheckedChanged);
            // 
            // realtime
            // 
            this.realtime.AutoSize = true;
            this.realtime.Location = new System.Drawing.Point(20, 71);
            this.realtime.Name = "realtime";
            this.realtime.Size = new System.Drawing.Size(76, 17);
            this.realtime.TabIndex = 7;
            this.realtime.Text = "Real-time?";
            this.realtime.UseVisualStyleBackColor = true;
            this.realtime.CheckedChanged += new System.EventHandler(this.realtime_CheckedChanged);
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(66, 30);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(72, 20);
            this.name.TabIndex = 6;
            this.name.TextChanged += new System.EventHandler(this.name_TextChanged);
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
            // DCProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "DCProperties";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox realtime;
        private System.Windows.Forms.CheckBox strict;
    }
}
