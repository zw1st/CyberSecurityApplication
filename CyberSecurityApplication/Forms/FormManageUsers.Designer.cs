namespace CyberSecurityApplication.Forms
{
    partial class FormManageUsers
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
            dataGridView = new DataGridView();
            buttonAdd = new Button();
            buttonBlock = new Button();
            buttonEdit = new Button();
            buttonDelete = new Button();
            buttonRefresh = new Button();
            ColumnId = new DataGridViewTextBoxColumn();
            ColumnUsername = new DataGridViewTextBoxColumn();
            ColumnIsLocked = new DataGridViewCheckBoxColumn();
            ColumnMinLength = new DataGridViewTextBoxColumn();
            ColumnDuration = new DataGridViewTextBoxColumn();
            ColumnRestrictions = new DataGridViewCheckBoxColumn();
            ColumnLastChange = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { ColumnId, ColumnUsername, ColumnIsLocked, ColumnMinLength, ColumnDuration, ColumnRestrictions, ColumnLastChange });
            dataGridView.Dock = DockStyle.Left;
            dataGridView.Location = new Point(0, 0);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersVisible = false;
            dataGridView.RowHeadersWidth = 51;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(696, 450);
            dataGridView.TabIndex = 0;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(702, 12);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(163, 47);
            buttonAdd.TabIndex = 1;
            buttonAdd.Text = "Add";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonBlock
            // 
            buttonBlock.Location = new Point(702, 65);
            buttonBlock.Name = "buttonBlock";
            buttonBlock.Size = new Size(163, 47);
            buttonBlock.TabIndex = 2;
            buttonBlock.Text = "Block";
            buttonBlock.UseVisualStyleBackColor = true;
            buttonBlock.Click += buttonBlock_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.Location = new Point(702, 118);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(163, 47);
            buttonEdit.TabIndex = 3;
            buttonEdit.Text = "Edit";
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(702, 171);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(163, 47);
            buttonDelete.TabIndex = 4;
            buttonDelete.Text = "Delete";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonRefresh
            // 
            buttonRefresh.Location = new Point(702, 224);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(163, 47);
            buttonRefresh.TabIndex = 5;
            buttonRefresh.Text = "Refresh";
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // ColumnId
            // 
            ColumnId.DataPropertyName = "Id";
            ColumnId.HeaderText = "Id";
            ColumnId.MinimumWidth = 6;
            ColumnId.Name = "ColumnId";
            ColumnId.ReadOnly = true;
            ColumnId.Visible = false;
            // 
            // ColumnUsername
            // 
            ColumnUsername.DataPropertyName = "Username";
            ColumnUsername.FillWeight = 1F;
            ColumnUsername.HeaderText = "Username";
            ColumnUsername.MinimumWidth = 6;
            ColumnUsername.Name = "ColumnUsername";
            ColumnUsername.ReadOnly = true;
            // 
            // ColumnIsLocked
            // 
            ColumnIsLocked.DataPropertyName = "IsLocked";
            ColumnIsLocked.FillWeight = 1F;
            ColumnIsLocked.HeaderText = "IsLocked";
            ColumnIsLocked.MinimumWidth = 6;
            ColumnIsLocked.Name = "ColumnIsLocked";
            ColumnIsLocked.ReadOnly = true;
            // 
            // ColumnMinLength
            // 
            ColumnMinLength.DataPropertyName = "MinPasswordLength";
            ColumnMinLength.FillWeight = 1F;
            ColumnMinLength.HeaderText = "MinLength";
            ColumnMinLength.MinimumWidth = 6;
            ColumnMinLength.Name = "ColumnMinLength";
            ColumnMinLength.ReadOnly = true;
            // 
            // ColumnDuration
            // 
            ColumnDuration.DataPropertyName = "PasswordDurationMonths";
            ColumnDuration.FillWeight = 1F;
            ColumnDuration.HeaderText = "Duration";
            ColumnDuration.MinimumWidth = 6;
            ColumnDuration.Name = "ColumnDuration";
            ColumnDuration.ReadOnly = true;
            // 
            // ColumnRestrictions
            // 
            ColumnRestrictions.DataPropertyName = "PasswordRestrictionsEnabled";
            ColumnRestrictions.FillWeight = 1F;
            ColumnRestrictions.HeaderText = "Restrictions";
            ColumnRestrictions.MinimumWidth = 6;
            ColumnRestrictions.Name = "ColumnRestrictions";
            ColumnRestrictions.ReadOnly = true;
            // 
            // ColumnLastChange
            // 
            ColumnLastChange.DataPropertyName = "LastPasswordChangeDate";
            ColumnLastChange.FillWeight = 1F;
            ColumnLastChange.HeaderText = "LastChange";
            ColumnLastChange.MinimumWidth = 6;
            ColumnLastChange.Name = "ColumnLastChange";
            ColumnLastChange.ReadOnly = true;
            // 
            // FormManageUsers
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(877, 450);
            Controls.Add(buttonRefresh);
            Controls.Add(buttonDelete);
            Controls.Add(buttonEdit);
            Controls.Add(buttonBlock);
            Controls.Add(buttonAdd);
            Controls.Add(dataGridView);
            Name = "FormManageUsers";
            Text = "FormUsersView";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonRefresh;

        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonBlock;

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private DataGridViewTextBoxColumn ColumnId;
        private DataGridViewTextBoxColumn ColumnUsername;
        private DataGridViewCheckBoxColumn ColumnIsLocked;
        private DataGridViewTextBoxColumn ColumnMinLength;
        private DataGridViewTextBoxColumn ColumnDuration;
        private DataGridViewCheckBoxColumn ColumnRestrictions;
        private DataGridViewTextBoxColumn ColumnLastChange;
    }
}