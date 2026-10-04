namespace CyberSecurityApplication.Forms
{
    partial class HashFile
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
            textBoxFilePath = new TextBox();
            textBoxHashResult = new TextBox();
            buttonSelectFile = new Button();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // textBoxFilePath
            // 
            textBoxFilePath.Location = new Point(12, 32);
            textBoxFilePath.Name = "textBoxFilePath";
            textBoxFilePath.ReadOnly = true;
            textBoxFilePath.Size = new Size(324, 27);
            textBoxFilePath.TabIndex = 0;
            // 
            // textBoxHashResult
            // 
            textBoxHashResult.Location = new Point(12, 108);
            textBoxHashResult.Name = "textBoxHashResult";
            textBoxHashResult.Size = new Size(324, 27);
            textBoxHashResult.TabIndex = 1;
            // 
            // buttonSelectFile
            // 
            buttonSelectFile.BackColor = Color.Ivory;
            buttonSelectFile.Location = new Point(353, 32);
            buttonSelectFile.Name = "buttonSelectFile";
            buttonSelectFile.Size = new Size(161, 27);
            buttonSelectFile.TabIndex = 11;
            buttonSelectFile.Text = "Select file from PC";
            buttonSelectFile.UseVisualStyleBackColor = false;
            buttonSelectFile.Click += buttonSelectFile_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(80, 20);
            label1.TabIndex = 12;
            label1.Text = "Path to file";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 85);
            label2.Name = "label2";
            label2.Size = new Size(89, 20);
            label2.TabIndex = 13;
            label2.Text = "Output hash";
            // 
            // HashFile
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(606, 190);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(buttonSelectFile);
            Controls.Add(textBoxHashResult);
            Controls.Add(textBoxFilePath);
            Name = "HashFile";
            Text = "HashFile";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxFilePath;
        private TextBox textBoxHashResult;
        private Button buttonSelectFile;
        private Label label1;
        private Label label2;
    }
}