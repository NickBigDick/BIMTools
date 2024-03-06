using System.Drawing;

namespace BIMTools
{
    partial class PlaceFamilyInstancesWindow
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
            this.startButton = new System.Windows.Forms.Button();
            this.dxBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dyBox = new System.Windows.Forms.TextBox();
            this.counterXBox = new System.Windows.Forms.TextBox();
            this.counterYBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.maxYBox = new System.Windows.Forms.TextBox();
            this.maxXBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(12, 116);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(227, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Расставить экземпляры семейств";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // dxBox
            // 
            this.dxBox.Location = new System.Drawing.Point(45, 9);
            this.dxBox.Name = "dxBox";
            this.dxBox.Size = new System.Drawing.Size(62, 20);
            this.dxBox.TabIndex = 3;
            this.dxBox.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Количество по X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "ΔX";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "ΔY";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(36, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Количество по Y";
            // 
            // dyBox
            // 
            this.dyBox.Location = new System.Drawing.Point(45, 35);
            this.dyBox.Name = "dyBox";
            this.dyBox.Size = new System.Drawing.Size(62, 20);
            this.dyBox.TabIndex = 3;
            this.dyBox.Text = "0";
            // 
            // counterXBox
            // 
            this.counterXBox.Location = new System.Drawing.Point(139, 64);
            this.counterXBox.Name = "counterXBox";
            this.counterXBox.Size = new System.Drawing.Size(39, 20);
            this.counterXBox.TabIndex = 3;
            this.counterXBox.Text = "2";
            // 
            // counterYBox
            // 
            this.counterYBox.Location = new System.Drawing.Point(139, 90);
            this.counterYBox.Name = "counterYBox";
            this.counterYBox.Size = new System.Drawing.Size(39, 20);
            this.counterYBox.TabIndex = 3;
            this.counterYBox.Text = "2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Длина по Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Длина по X";
            // 
            // maxYBox
            // 
            this.maxYBox.Location = new System.Drawing.Point(189, 35);
            this.maxYBox.Name = "maxYBox";
            this.maxYBox.Size = new System.Drawing.Size(62, 20);
            this.maxYBox.TabIndex = 8;
            this.maxYBox.Text = "0";
            // 
            // maxXBox
            // 
            this.maxXBox.Location = new System.Drawing.Point(189, 9);
            this.maxXBox.Name = "maxXBox";
            this.maxXBox.Size = new System.Drawing.Size(62, 20);
            this.maxXBox.TabIndex = 9;
            this.maxXBox.Text = "0";
            // 
            // PlaceFamilyInstancesWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 183);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.maxYBox);
            this.Controls.Add(this.maxXBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.counterYBox);
            this.Controls.Add(this.counterXBox);
            this.Controls.Add(this.dyBox);
            this.Controls.Add(this.dxBox);
            this.Controls.Add(this.startButton);
            this.Name = "PlaceFamilyInstancesWindow";
            this.Text = "PlaceFamilyInstancesWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.TextBox dxBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox dyBox;
        private System.Windows.Forms.TextBox counterXBox;
        private System.Windows.Forms.TextBox counterYBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox maxYBox;
        private System.Windows.Forms.TextBox maxXBox;
    }
}