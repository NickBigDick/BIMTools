namespace BIMTools
{
    partial class FamilyManagerParameterViewerWindow
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.sharedParametersCheckBox = new System.Windows.Forms.CheckBox();
            this.AcceptButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 38);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(887, 301);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.CellIsChanged);
            // 
            // sharedParametersCheckBox
            // 
            this.sharedParametersCheckBox.AutoSize = true;
            this.sharedParametersCheckBox.Checked = true;
            this.sharedParametersCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sharedParametersCheckBox.Location = new System.Drawing.Point(12, 12);
            this.sharedParametersCheckBox.Name = "sharedParametersCheckBox";
            this.sharedParametersCheckBox.Size = new System.Drawing.Size(121, 17);
            this.sharedParametersCheckBox.TabIndex = 1;
            this.sharedParametersCheckBox.Text = "Общие параметры";
            this.sharedParametersCheckBox.UseVisualStyleBackColor = true;
            this.sharedParametersCheckBox.CheckedChanged += new System.EventHandler(this.sharedParametersCheckBox_CheckedChanged);
            // 
            // AcceptButton
            // 
            this.AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AcceptButton.Location = new System.Drawing.Point(12, 355);
            this.AcceptButton.Name = "AcceptButton";
            this.AcceptButton.Size = new System.Drawing.Size(174, 23);
            this.AcceptButton.TabIndex = 2;
            this.AcceptButton.Text = "Сохранить изменения";
            this.AcceptButton.UseVisualStyleBackColor = true;
            this.AcceptButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // FamilyManagerParameterViewerWindow
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(911, 390);
            this.Controls.Add(this.AcceptButton);
            this.Controls.Add(this.sharedParametersCheckBox);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FamilyManagerParameterViewerWindow";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox sharedParametersCheckBox;
        private System.Windows.Forms.Button AcceptButton;
    }
}