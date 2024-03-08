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
            this.PlaceInsulationButton = new System.Windows.Forms.Button();
            this.insulationWidthTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.deleteInsulationButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // systemsTreeView
            // 
            this.systemsTreeView.HotTracking = true;
            this.systemsTreeView.Location = new System.Drawing.Point(12, 12);
            this.systemsTreeView.Name = "systemsTreeView";
            this.systemsTreeView.Size = new System.Drawing.Size(364, 362);
            this.systemsTreeView.TabIndex = 0;
            // 
            // insulationTypesComboBox
            // 
            this.insulationTypesComboBox.FormattingEnabled = true;
            this.insulationTypesComboBox.Location = new System.Drawing.Point(382, 37);
            this.insulationTypesComboBox.Name = "insulationTypesComboBox";
            this.insulationTypesComboBox.Size = new System.Drawing.Size(281, 21);
            this.insulationTypesComboBox.TabIndex = 1;
            // 
            // PlaceInsulationButton
            // 
            this.PlaceInsulationButton.Location = new System.Drawing.Point(382, 132);
            this.PlaceInsulationButton.Name = "PlaceInsulationButton";
            this.PlaceInsulationButton.Size = new System.Drawing.Size(281, 23);
            this.PlaceInsulationButton.TabIndex = 4;
            this.PlaceInsulationButton.Text = "Назначить выбранную изоляцию на трубу";
            this.PlaceInsulationButton.UseVisualStyleBackColor = true;
            this.PlaceInsulationButton.Click += new System.EventHandler(this.PlaceInsulation_Click);
            // 
            // insulationWidthTextBox
            // 
            this.insulationWidthTextBox.Location = new System.Drawing.Point(382, 77);
            this.insulationWidthTextBox.Name = "insulationWidthTextBox";
            this.insulationWidthTextBox.Size = new System.Drawing.Size(281, 20);
            this.insulationWidthTextBox.TabIndex = 5;
            this.insulationWidthTextBox.Text = "25";
            this.insulationWidthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(379, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Укажите толщину изоляции:";
            // 
            // deleteInsulationButton
            // 
            this.deleteInsulationButton.Location = new System.Drawing.Point(382, 103);
            this.deleteInsulationButton.Name = "deleteInsulationButton";
            this.deleteInsulationButton.Size = new System.Drawing.Size(281, 23);
            this.deleteInsulationButton.TabIndex = 4;
            this.deleteInsulationButton.Text = "Удалить изоляцию с участка";
            this.deleteInsulationButton.UseVisualStyleBackColor = true;
            this.deleteInsulationButton.Click += new System.EventHandler(this.DeleteInsulation_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(379, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Выберите тип изоляции:";
            // 
            // SystemsIsolationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 433);
            this.Controls.Add(this.insulationWidthTextBox);
            this.Controls.Add(this.deleteInsulationButton);
            this.Controls.Add(this.PlaceInsulationButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
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
        private System.Windows.Forms.Button PlaceInsulationButton;
        private System.Windows.Forms.TextBox insulationWidthTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button deleteInsulationButton;
        private System.Windows.Forms.Label label2;
    }
}