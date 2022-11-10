namespace homeWork04
{
    partial class Form1
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
            this.UpDownProbability = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.UpDownTrials = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.UpDownTrajectories = new System.Windows.Forms.NumericUpDown();
            this.Compute = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownProbability)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownTrials)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownTrajectories)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // UpDownProbability
            // 
            this.UpDownProbability.Location = new System.Drawing.Point(23, 35);
            this.UpDownProbability.Name = "UpDownProbability";
            this.UpDownProbability.Size = new System.Drawing.Size(120, 22);
            this.UpDownProbability.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Success Probability";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Number of Trials";
            // 
            // UpDownTrials
            // 
            this.UpDownTrials.Location = new System.Drawing.Point(206, 35);
            this.UpDownTrials.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.UpDownTrials.Name = "UpDownTrials";
            this.UpDownTrials.Size = new System.Drawing.Size(103, 22);
            this.UpDownTrials.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(355, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Number of Trajectories";
            // 
            // UpDownTrajectories
            // 
            this.UpDownTrajectories.Location = new System.Drawing.Point(358, 35);
            this.UpDownTrajectories.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.UpDownTrajectories.Name = "UpDownTrajectories";
            this.UpDownTrajectories.Size = new System.Drawing.Size(141, 22);
            this.UpDownTrajectories.TabIndex = 12;
            // 
            // Compute
            // 
            this.Compute.Location = new System.Drawing.Point(580, 34);
            this.Compute.Name = "Compute";
            this.Compute.Size = new System.Drawing.Size(135, 23);
            this.Compute.TabIndex = 13;
            this.Compute.Text = "Compute";
            this.Compute.UseVisualStyleBackColor = true;
            this.Compute.Click += new System.EventHandler(this.Compute_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(829, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "Blue: Normalized";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(829, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "Red: Relative";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(829, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "Yellow: Absolute";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(23, 77);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(551, 546);
            this.richTextBox1.TabIndex = 17;
            this.richTextBox1.Text = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(23, 77);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(551, 546);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(580, 77);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(395, 546);
            this.richTextBox2.TabIndex = 19;
            this.richTextBox2.Text = "";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(580, 77);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(397, 546);
            this.pictureBox2.TabIndex = 20;
            this.pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 635);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Compute);
            this.Controls.Add(this.UpDownTrajectories);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.UpDownTrials);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UpDownProbability);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.UpDownProbability)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownTrials)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownTrajectories)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown UpDownProbability;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown UpDownTrials;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown UpDownTrajectories;
        private System.Windows.Forms.Button Compute;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

