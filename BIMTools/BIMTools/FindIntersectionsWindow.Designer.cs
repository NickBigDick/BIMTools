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
            this.selectAllCategoriesButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.startSearchButton = new System.Windows.Forms.Button();
            this.firstDocumentsComboBox = new System.Windows.Forms.ComboBox();
            this.firstCategoriesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.secondCategoriesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.secondDocumentsComboBox = new System.Windows.Forms.ComboBox();
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
            this.splitContainer1.Panel1.Controls.Add(this.selectAllCategoriesButton);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.startSearchButton);
            this.splitContainer1.Panel1.Controls.Add(this.firstDocumentsComboBox);
            this.splitContainer1.Panel1.Controls.Add(this.firstCategoriesCheckedListBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.secondCategoriesCheckedListBox);
            this.splitContainer1.Panel2.Controls.Add(this.secondDocumentsComboBox);
            this.splitContainer1.Size = new System.Drawing.Size(684, 356);
            this.splitContainer1.SplitterDistance = 336;
            this.splitContainer1.TabIndex = 0;
            // 
            // selectAllCategoriesButton
            // 
            this.selectAllCategoriesButton.Location = new System.Drawing.Point(12, 245);
            this.selectAllCategoriesButton.Name = "selectAllCategoriesButton";
            this.selectAllCategoriesButton.Size = new System.Drawing.Size(300, 23);
            this.selectAllCategoriesButton.TabIndex = 4;
            this.selectAllCategoriesButton.Text = "Выбрать все категории";
            this.selectAllCategoriesButton.UseVisualStyleBackColor = true;
            this.selectAllCategoriesButton.Click += new System.EventHandler(this.selectAllCategoriesButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Текущий документ:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 308);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(312, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Если анализируете сампересечения, выбирайте категории \r\nтолько в этой колонке";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // startSearchButton
            // 
            this.startSearchButton.Location = new System.Drawing.Point(12, 282);
            this.startSearchButton.Name = "startSearchButton";
            this.startSearchButton.Size = new System.Drawing.Size(300, 23);
            this.startSearchButton.TabIndex = 1;
            this.startSearchButton.Text = "Начать поиск пересечений";
            this.startSearchButton.UseVisualStyleBackColor = false;
            this.startSearchButton.Click += new System.EventHandler(this.startSearchButton_Click);
            // 
            // firstDocumentsComboBox
            // 
            this.firstDocumentsComboBox.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.firstDocumentsComboBox.FormattingEnabled = true;
            this.firstDocumentsComboBox.Location = new System.Drawing.Point(12, 28);
            this.firstDocumentsComboBox.Name = "firstDocumentsComboBox";
            this.firstDocumentsComboBox.Size = new System.Drawing.Size(300, 21);
            this.firstDocumentsComboBox.TabIndex = 0;
            // 
            // firstCategoriesCheckedListBox
            // 
            this.firstCategoriesCheckedListBox.CheckOnClick = true;
            this.firstCategoriesCheckedListBox.FormattingEnabled = true;
            this.firstCategoriesCheckedListBox.Location = new System.Drawing.Point(12, 55);
            this.firstCategoriesCheckedListBox.Name = "firstCategoriesCheckedListBox";
            this.firstCategoriesCheckedListBox.Size = new System.Drawing.Size(300, 184);
            this.firstCategoriesCheckedListBox.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Экземпляр связанного документа:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 308);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(298, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Выбирайте второй связанный документ и его категории \r\nпри необходимости";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // secondCategoriesCheckedListBox
            // 
            this.secondCategoriesCheckedListBox.CheckOnClick = true;
            this.secondCategoriesCheckedListBox.FormattingEnabled = true;
            this.secondCategoriesCheckedListBox.Location = new System.Drawing.Point(15, 55);
            this.secondCategoriesCheckedListBox.Name = "secondCategoriesCheckedListBox";
            this.secondCategoriesCheckedListBox.Size = new System.Drawing.Size(300, 184);
            this.secondCategoriesCheckedListBox.TabIndex = 1;
            // 
            // secondDocumentsComboBox
            // 
            this.secondDocumentsComboBox.FormattingEnabled = true;
            this.secondDocumentsComboBox.Location = new System.Drawing.Point(15, 28);
            this.secondDocumentsComboBox.Name = "secondDocumentsComboBox";
            this.secondDocumentsComboBox.Size = new System.Drawing.Size(300, 21);
            this.secondDocumentsComboBox.Sorted = true;
            this.secondDocumentsComboBox.TabIndex = 0;
            // 
            // FindIntersectionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 356);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FindIntersectionsWindow";
            this.Text = "FindIntersectionsWindow";
            this.Load += new System.EventHandler(this.FindIntersectionsWindow_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button selectAllCategoriesButton;
    }
}