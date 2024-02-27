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
            this.SuspendLayout();
            // 
            // FamiliiesBox
            // 
            this.FamiliiesBox.DropDownWidth = 300;
            this.FamiliiesBox.FormattingEnabled = true;
            this.FamiliiesBox.Location = new System.Drawing.Point(9, 10);
            this.FamiliiesBox.Margin = new System.Windows.Forms.Padding(2);
            this.FamiliiesBox.Name = "FamiliiesBox";
            this.FamiliiesBox.Size = new System.Drawing.Size(234, 21);
            this.FamiliiesBox.Sorted = true;
            this.FamiliiesBox.TabIndex = 0;
            this.FamiliiesBox.SelectionChangeCommitted += new System.EventHandler(this.SelectedFamily);
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(9, 161);
            this.RunButton.Margin = new System.Windows.Forms.Padding(2);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(234, 19);
            this.RunButton.TabIndex = 1;
            this.RunButton.Text = "Запуск расстановки";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.Start);
            // 
            // FamilySymbolsCheckedListBox
            // 
            this.FamilySymbolsCheckedListBox.FormattingEnabled = true;
            this.FamilySymbolsCheckedListBox.Location = new System.Drawing.Point(9, 50);
            this.FamilySymbolsCheckedListBox.Name = "FamilySymbolsCheckedListBox";
            this.FamilySymbolsCheckedListBox.Size = new System.Drawing.Size(234, 94);
            this.FamilySymbolsCheckedListBox.Sorted = true;
            this.FamilySymbolsCheckedListBox.TabIndex = 3;
            // 
            // PlaceFamilySymbolsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 294);
            this.Controls.Add(this.FamilySymbolsCheckedListBox);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.FamiliiesBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PlaceFamilySymbolsWindow";
            this.Text = "PlaceFamilySymbolsWindow";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox FamiliiesBox;
        public System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.CheckedListBox FamilySymbolsCheckedListBox;
    }
}