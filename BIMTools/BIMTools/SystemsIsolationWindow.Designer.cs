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
            this.deleteInsulationButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.showSystemButton = new System.Windows.Forms.Button();
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
            this.insulationTypesComboBox.Location = new System.Drawing.Point(382, 12);
            this.insulationTypesComboBox.Name = "insulationTypesComboBox";
            this.insulationTypesComboBox.Size = new System.Drawing.Size(281, 21);
            this.insulationTypesComboBox.TabIndex = 1;
            this.insulationTypesComboBox.Text = "Выберите тип изоляции:";
            // 
            // PlaceInsulationButton
            // 
            this.PlaceInsulationButton.Location = new System.Drawing.Point(382, 107);
            this.PlaceInsulationButton.Name = "PlaceInsulationButton";
            this.PlaceInsulationButton.Size = new System.Drawing.Size(281, 23);
            this.PlaceInsulationButton.TabIndex = 4;
            this.PlaceInsulationButton.Text = "Назначить выбранную изоляцию на трубу";
            this.PlaceInsulationButton.UseVisualStyleBackColor = true;
            this.PlaceInsulationButton.Click += new System.EventHandler(this.PlaceInsulation_Click);
            // 
            // insulationWidthTextBox
            // 
            this.insulationWidthTextBox.Location = new System.Drawing.Point(525, 39);
            this.insulationWidthTextBox.Name = "insulationWidthTextBox";
            this.insulationWidthTextBox.Size = new System.Drawing.Size(138, 20);
            this.insulationWidthTextBox.TabIndex = 5;
            this.insulationWidthTextBox.Text = "25";
            this.insulationWidthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // deleteInsulationButton
            // 
            this.deleteInsulationButton.Location = new System.Drawing.Point(382, 78);
            this.deleteInsulationButton.Name = "deleteInsulationButton";
            this.deleteInsulationButton.Size = new System.Drawing.Size(281, 23);
            this.deleteInsulationButton.TabIndex = 4;
            this.deleteInsulationButton.Text = "Удалить изоляцию с участка";
            this.deleteInsulationButton.UseVisualStyleBackColor = true;
            this.deleteInsulationButton.Click += new System.EventHandler(this.DeleteInsulation_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(412, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Толщина изоляции:";
            // 
            // showSystemButton
            // 
            this.showSystemButton.Location = new System.Drawing.Point(382, 351);
            this.showSystemButton.Name = "showSystemButton";
            this.showSystemButton.Size = new System.Drawing.Size(281, 23);
            this.showSystemButton.TabIndex = 7;
            this.showSystemButton.Text = "Подсветить систему";
            this.showSystemButton.UseVisualStyleBackColor = true;
            this.showSystemButton.Click += new System.EventHandler(this.showSystemButton_Click);
            // 
            // SystemsIsolationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 381);
            this.Controls.Add(this.showSystemButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.insulationWidthTextBox);
            this.Controls.Add(this.deleteInsulationButton);
            this.Controls.Add(this.PlaceInsulationButton);
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
        private System.Windows.Forms.Button deleteInsulationButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button showSystemButton;
    }
}