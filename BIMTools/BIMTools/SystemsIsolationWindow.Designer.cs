namespace BIMTools
{
    partial class SystemsIsolationWindow
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
            this.systemsTreeView = new System.Windows.Forms.TreeView();
            this.insulationTypesComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PlaceInsulation = new System.Windows.Forms.Button();
            this.insulationWidthTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // systemsTreeView
            // 
            this.systemsTreeView.Location = new System.Drawing.Point(12, 12);
            this.systemsTreeView.Name = "systemsTreeView";
            this.systemsTreeView.Size = new System.Drawing.Size(364, 362);
            this.systemsTreeView.TabIndex = 0;
            this.systemsTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.nodeWasSelected);
            // 
            // insulationTypesComboBox
            // 
            this.insulationTypesComboBox.FormattingEnabled = true;
            this.insulationTypesComboBox.Location = new System.Drawing.Point(440, 104);
            this.insulationTypesComboBox.Name = "insulationTypesComboBox";
            this.insulationTypesComboBox.Size = new System.Drawing.Size(281, 21);
            this.insulationTypesComboBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(537, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "diameterLabel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(437, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Размер системы:";
            // 
            // PlaceInsulation
            // 
            this.PlaceInsulation.Location = new System.Drawing.Point(440, 195);
            this.PlaceInsulation.Name = "PlaceInsulation";
            this.PlaceInsulation.Size = new System.Drawing.Size(281, 23);
            this.PlaceInsulation.TabIndex = 4;
            this.PlaceInsulation.Text = "Назначить выбранную изоляцию на трубу";
            this.PlaceInsulation.UseVisualStyleBackColor = true;
            this.PlaceInsulation.Click += new System.EventHandler(this.PlaceInsulation_Click);
            // 
            // insulationWidthTextBox
            // 
            this.insulationWidthTextBox.Location = new System.Drawing.Point(560, 135);
            this.insulationWidthTextBox.Name = "insulationWidthTextBox";
            this.insulationWidthTextBox.Size = new System.Drawing.Size(100, 20);
            this.insulationWidthTextBox.TabIndex = 5;
            this.insulationWidthTextBox.Text = "50";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(437, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Толщина изоляции";
            // 
            // SystemsIsolationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.insulationWidthTextBox);
            this.Controls.Add(this.PlaceInsulation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.insulationTypesComboBox);
            this.Controls.Add(this.systemsTreeView);
            this.Name = "SystemsIsolationWindow";
            this.Text = "SystemsIsolationWindow";
            this.Load += new System.EventHandler(this.SystemsIsolationWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView systemsTreeView;
        private System.Windows.Forms.ComboBox insulationTypesComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button PlaceInsulation;
        private System.Windows.Forms.TextBox insulationWidthTextBox;
        private System.Windows.Forms.Label label3;
    }
}