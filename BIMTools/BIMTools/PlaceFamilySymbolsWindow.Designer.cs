namespace BIMTools
{
    partial class PlaceFamilySymbolsWindow
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
            this.FamiliiesBox = new System.Windows.Forms.ComboBox();
            this.RunButton = new System.Windows.Forms.Button();
            this.FamilySymbolsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.selectAllCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // FamiliiesBox
            // 
            this.FamiliiesBox.DropDownWidth = 300;
            this.FamiliiesBox.FormattingEnabled = true;
            this.FamiliiesBox.Location = new System.Drawing.Point(9, 10);
            this.FamiliiesBox.Margin = new System.Windows.Forms.Padding(2);
            this.FamiliiesBox.Name = "FamiliiesBox";
            this.FamiliiesBox.Size = new System.Drawing.Size(406, 21);
            this.FamiliiesBox.Sorted = true;
            this.FamiliiesBox.TabIndex = 0;
            this.FamiliiesBox.SelectionChangeCommitted += new System.EventHandler(this.SelectedFamily);
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(9, 262);
            this.RunButton.Margin = new System.Windows.Forms.Padding(2);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(406, 31);
            this.RunButton.TabIndex = 1;
            this.RunButton.Text = "Запуск расстановки";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.Start);
            // 
            // FamilySymbolsCheckedListBox
            // 
            this.FamilySymbolsCheckedListBox.CheckOnClick = true;
            this.FamilySymbolsCheckedListBox.FormattingEnabled = true;
            this.FamilySymbolsCheckedListBox.Location = new System.Drawing.Point(9, 65);
            this.FamilySymbolsCheckedListBox.Name = "FamilySymbolsCheckedListBox";
            this.FamilySymbolsCheckedListBox.Size = new System.Drawing.Size(406, 184);
            this.FamilySymbolsCheckedListBox.Sorted = true;
            this.FamilySymbolsCheckedListBox.TabIndex = 3;
            // 
            // selectAllCheckBox
            // 
            this.selectAllCheckBox.AutoSize = true;
            this.selectAllCheckBox.Checked = true;
            this.selectAllCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.selectAllCheckBox.Location = new System.Drawing.Point(9, 42);
            this.selectAllCheckBox.Name = "selectAllCheckBox";
            this.selectAllCheckBox.Size = new System.Drawing.Size(163, 17);
            this.selectAllCheckBox.TabIndex = 4;
            this.selectAllCheckBox.Text = "Выбрать все типоразмеры";
            this.selectAllCheckBox.UseVisualStyleBackColor = true;
            this.selectAllCheckBox.CheckedChanged += new System.EventHandler(this.selectAllCheckBox_CheckedChanged);
            // 
            // PlaceFamilySymbolsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 294);
            this.Controls.Add(this.selectAllCheckBox);
            this.Controls.Add(this.FamilySymbolsCheckedListBox);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.FamiliiesBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PlaceFamilySymbolsWindow";
            this.Text = "PlaceFamilySymbolsWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox FamiliiesBox;
        public System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.CheckedListBox FamilySymbolsCheckedListBox;
        private System.Windows.Forms.CheckBox selectAllCheckBox;
    }
}