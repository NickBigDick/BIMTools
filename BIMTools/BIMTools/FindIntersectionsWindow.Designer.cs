namespace BIMTools
{
    partial class FindIntersectionsWindow
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.secondCategoriesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.secondDocumentsComboBox = new System.Windows.Forms.ComboBox();
            this.firstCategoriesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.startSearchButton = new System.Windows.Forms.Button();
            this.firstDocumentsComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.startSearchButton);
            this.splitContainer1.Panel1.Controls.Add(this.firstDocumentsComboBox);
            this.splitContainer1.Panel1.Controls.Add(this.firstCategoriesCheckedListBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.secondCategoriesCheckedListBox);
            this.splitContainer1.Panel2.Controls.Add(this.secondDocumentsComboBox);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 394;
            this.splitContainer1.TabIndex = 0;
            // 
            // secondCategoriesCheckedListBox
            // 
            this.secondCategoriesCheckedListBox.CheckOnClick = true;
            this.secondCategoriesCheckedListBox.FormattingEnabled = true;
            this.secondCategoriesCheckedListBox.Location = new System.Drawing.Point(15, 48);
            this.secondCategoriesCheckedListBox.Name = "secondCategoriesCheckedListBox";
            this.secondCategoriesCheckedListBox.Size = new System.Drawing.Size(375, 274);
            this.secondCategoriesCheckedListBox.TabIndex = 1;
            // 
            // secondDocumentsComboBox
            // 
            this.secondDocumentsComboBox.FormattingEnabled = true;
            this.secondDocumentsComboBox.Location = new System.Drawing.Point(15, 12);
            this.secondDocumentsComboBox.Name = "secondDocumentsComboBox";
            this.secondDocumentsComboBox.Size = new System.Drawing.Size(375, 21);
            this.secondDocumentsComboBox.TabIndex = 0;
            // 
            // firstCategoriesCheckedListBox
            // 
            this.firstCategoriesCheckedListBox.CheckOnClick = true;
            this.firstCategoriesCheckedListBox.FormattingEnabled = true;
            this.firstCategoriesCheckedListBox.Location = new System.Drawing.Point(12, 48);
            this.firstCategoriesCheckedListBox.Name = "firstCategoriesCheckedListBox";
            this.firstCategoriesCheckedListBox.Size = new System.Drawing.Size(370, 274);
            this.firstCategoriesCheckedListBox.TabIndex = 0;
            // 
            // startSearchButton
            // 
            this.startSearchButton.Location = new System.Drawing.Point(99, 328);
            this.startSearchButton.Name = "startSearchButton";
            this.startSearchButton.Size = new System.Drawing.Size(186, 23);
            this.startSearchButton.TabIndex = 1;
            this.startSearchButton.Text = "Начать поиск пересечений";
            this.startSearchButton.UseVisualStyleBackColor = false;
            this.startSearchButton.Click += new System.EventHandler(this.startSearchButton_Click);
            // 
            // firstDocumentsComboBox
            // 
            this.firstDocumentsComboBox.FormattingEnabled = true;
            this.firstDocumentsComboBox.Location = new System.Drawing.Point(7, 12);
            this.firstDocumentsComboBox.Name = "firstDocumentsComboBox";
            this.firstDocumentsComboBox.Size = new System.Drawing.Size(375, 21);
            this.firstDocumentsComboBox.TabIndex = 0;
            // 
            // FindIntersectionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FindIntersectionsWindow";
            this.Text = "FindIntersectionsWindow";
            this.Load += new System.EventHandler(this.FindIntersectionsWindow_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox secondDocumentsComboBox;
        private System.Windows.Forms.CheckedListBox secondCategoriesCheckedListBox;
        private System.Windows.Forms.CheckedListBox firstCategoriesCheckedListBox;
        private System.Windows.Forms.Button startSearchButton;
        private System.Windows.Forms.ComboBox firstDocumentsComboBox;
    }
}