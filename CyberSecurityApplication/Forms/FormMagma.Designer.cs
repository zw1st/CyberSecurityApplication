namespace CyberSecurityApplication.Forms
{
    partial class FormMagma
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
            label1 = new Label();
            buttonSelectEncryptInput = new Button();
            textBoxEncryptInput = new TextBox();
            buttonSelectDecryptInput = new Button();
            textBoxDecryptInput = new TextBox();
            label3 = new Label();
            label4 = new Label();
            radioButtonPasswordFromKeyboard = new RadioButton();
            radioButtonPasswordFromFile = new RadioButton();
            label5 = new Label();
            textBoxPassword = new TextBox();
            textBoxPasswordFilePath = new TextBox();
            buttonSelectPasswordFile = new Button();
            buttonSelectEncryptOutput = new Button();
            textBoxEncryptOutput = new TextBox();
            label6 = new Label();
            label2 = new Label();
            label7 = new Label();
            buttonSelectDecryptOutput = new Button();
            textBoxDecryptOutput = new TextBox();
            buttonEncrypt = new Button();
            buttonDecrypt = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 196);
            label1.Name = "label1";
            label1.Size = new Size(43, 20);
            label1.TabIndex = 17;
            label1.Text = "Input";
            // 
            // buttonSelectEncryptInput
            // 
            buttonSelectEncryptInput.BackColor = Color.Ivory;
            buttonSelectEncryptInput.Location = new Point(342, 219);
            buttonSelectEncryptInput.Name = "buttonSelectEncryptInput";
            buttonSelectEncryptInput.Size = new Size(38, 27);
            buttonSelectEncryptInput.TabIndex = 16;
            buttonSelectEncryptInput.Text = "...";
            buttonSelectEncryptInput.UseVisualStyleBackColor = false;
            buttonSelectEncryptInput.Click += buttonSelectEncryptInput_Click;
            // 
            // textBoxEncryptInput
            // 
            textBoxEncryptInput.Location = new Point(12, 219);
            textBoxEncryptInput.Name = "textBoxEncryptInput";
            textBoxEncryptInput.ReadOnly = true;
            textBoxEncryptInput.Size = new Size(324, 27);
            textBoxEncryptInput.TabIndex = 14;
            // 
            // buttonSelectDecryptInput
            // 
            buttonSelectDecryptInput.BackColor = Color.Ivory;
            buttonSelectDecryptInput.Location = new Point(342, 374);
            buttonSelectDecryptInput.Name = "buttonSelectDecryptInput";
            buttonSelectDecryptInput.Size = new Size(38, 27);
            buttonSelectDecryptInput.TabIndex = 19;
            buttonSelectDecryptInput.Text = "...";
            buttonSelectDecryptInput.UseVisualStyleBackColor = false;
            buttonSelectDecryptInput.Click += buttonSelectDecryptInput_Click;
            // 
            // textBoxDecryptInput
            // 
            textBoxDecryptInput.Location = new Point(12, 374);
            textBoxDecryptInput.Name = "textBoxDecryptInput";
            textBoxDecryptInput.ReadOnly = true;
            textBoxDecryptInput.Size = new Size(324, 27);
            textBoxDecryptInput.TabIndex = 18;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.Location = new Point(12, 158);
            label3.Name = "label3";
            label3.Size = new Size(109, 38);
            label3.TabIndex = 21;
            label3.Text = "Encrypt";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label4.Location = new Point(12, 313);
            label4.Name = "label4";
            label4.Size = new Size(114, 38);
            label4.TabIndex = 22;
            label4.Text = "Decrypt";
            // 
            // radioButtonPasswordFromKeyboard
            // 
            radioButtonPasswordFromKeyboard.AutoSize = true;
            radioButtonPasswordFromKeyboard.Checked = true;
            radioButtonPasswordFromKeyboard.Location = new Point(26, 80);
            radioButtonPasswordFromKeyboard.Name = "radioButtonPasswordFromKeyboard";
            radioButtonPasswordFromKeyboard.Size = new Size(110, 24);
            radioButtonPasswordFromKeyboard.TabIndex = 23;
            radioButtonPasswordFromKeyboard.TabStop = true;
            radioButtonPasswordFromKeyboard.Text = "Клавиатура";
            radioButtonPasswordFromKeyboard.UseVisualStyleBackColor = true;
            radioButtonPasswordFromKeyboard.CheckedChanged += radioButtonPasswordFromKeyboard_CheckedChanged;
            // 
            // radioButtonPasswordFromFile
            // 
            radioButtonPasswordFromFile.AutoSize = true;
            radioButtonPasswordFromFile.Location = new Point(26, 50);
            radioButtonPasswordFromFile.Name = "radioButtonPasswordFromFile";
            radioButtonPasswordFromFile.Size = new Size(66, 24);
            radioButtonPasswordFromFile.TabIndex = 24;
            radioButtonPasswordFromFile.Text = "Файл";
            radioButtonPasswordFromFile.UseVisualStyleBackColor = true;
            radioButtonPasswordFromFile.CheckedChanged += radioButtonPasswordFromFile_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label5.Location = new Point(12, 9);
            label5.Name = "label5";
            label5.Size = new Size(132, 38);
            label5.TabIndex = 25;
            label5.Text = "Password";
            // 
            // textBoxPassword
            // 
            textBoxPassword.Location = new Point(175, 80);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.PasswordChar = '*';
            textBoxPassword.Size = new Size(276, 27);
            textBoxPassword.TabIndex = 26;
            textBoxPassword.Tag = "";
            // 
            // textBoxPasswordFilePath
            // 
            textBoxPasswordFilePath.Location = new Point(175, 50);
            textBoxPasswordFilePath.Name = "textBoxPasswordFilePath";
            textBoxPasswordFilePath.ReadOnly = true;
            textBoxPasswordFilePath.Size = new Size(276, 27);
            textBoxPasswordFilePath.TabIndex = 27;
            // 
            // buttonSelectPasswordFile
            // 
            buttonSelectPasswordFile.BackColor = Color.Ivory;
            buttonSelectPasswordFile.Location = new Point(457, 50);
            buttonSelectPasswordFile.Name = "buttonSelectPasswordFile";
            buttonSelectPasswordFile.Size = new Size(31, 27);
            buttonSelectPasswordFile.TabIndex = 28;
            buttonSelectPasswordFile.Text = "...";
            buttonSelectPasswordFile.UseVisualStyleBackColor = false;
            buttonSelectPasswordFile.Click += buttonSelectPasswordFile_Click;
            // 
            // buttonSelectEncryptOutput
            // 
            buttonSelectEncryptOutput.BackColor = Color.Ivory;
            buttonSelectEncryptOutput.Location = new Point(342, 283);
            buttonSelectEncryptOutput.Name = "buttonSelectEncryptOutput";
            buttonSelectEncryptOutput.Size = new Size(38, 27);
            buttonSelectEncryptOutput.TabIndex = 30;
            buttonSelectEncryptOutput.Text = "...";
            buttonSelectEncryptOutput.UseVisualStyleBackColor = false;
            buttonSelectEncryptOutput.Click += buttonSelectEncryptOutput_Click;
            // 
            // textBoxEncryptOutput
            // 
            textBoxEncryptOutput.Location = new Point(12, 283);
            textBoxEncryptOutput.Name = "textBoxEncryptOutput";
            textBoxEncryptOutput.ReadOnly = true;
            textBoxEncryptOutput.Size = new Size(324, 27);
            textBoxEncryptOutput.TabIndex = 29;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 260);
            label6.Name = "label6";
            label6.Size = new Size(55, 20);
            label6.TabIndex = 31;
            label6.Text = "Output";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 351);
            label2.Name = "label2";
            label2.Size = new Size(43, 20);
            label2.TabIndex = 32;
            label2.Text = "Input";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 414);
            label7.Name = "label7";
            label7.Size = new Size(55, 20);
            label7.TabIndex = 35;
            label7.Text = "Output";
            // 
            // buttonSelectDecryptOutput
            // 
            buttonSelectDecryptOutput.BackColor = Color.Ivory;
            buttonSelectDecryptOutput.Location = new Point(342, 437);
            buttonSelectDecryptOutput.Name = "buttonSelectDecryptOutput";
            buttonSelectDecryptOutput.Size = new Size(38, 27);
            buttonSelectDecryptOutput.TabIndex = 34;
            buttonSelectDecryptOutput.Text = "...";
            buttonSelectDecryptOutput.UseVisualStyleBackColor = false;
            buttonSelectDecryptOutput.Click += buttonSelectDecryptOutput_Click;
            // 
            // textBoxDecryptOutput
            // 
            textBoxDecryptOutput.Location = new Point(12, 437);
            textBoxDecryptOutput.Name = "textBoxDecryptOutput";
            textBoxDecryptOutput.ReadOnly = true;
            textBoxDecryptOutput.Size = new Size(324, 27);
            textBoxDecryptOutput.TabIndex = 33;
            // 
            // buttonEncrypt
            // 
            buttonEncrypt.Location = new Point(412, 281);
            buttonEncrypt.Name = "buttonEncrypt";
            buttonEncrypt.Size = new Size(94, 29);
            buttonEncrypt.TabIndex = 36;
            buttonEncrypt.Text = "Ok";
            buttonEncrypt.UseVisualStyleBackColor = true;
            buttonEncrypt.Click += buttonEncrypt_Click;
            // 
            // buttonDecrypt
            // 
            buttonDecrypt.Location = new Point(412, 435);
            buttonDecrypt.Name = "buttonDecrypt";
            buttonDecrypt.Size = new Size(94, 29);
            buttonDecrypt.TabIndex = 37;
            buttonDecrypt.Text = "Ok";
            buttonDecrypt.UseVisualStyleBackColor = true;
            buttonDecrypt.Click += buttonDecrypt_Click;
            // 
            // FormMagma
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(809, 507);
            Controls.Add(buttonDecrypt);
            Controls.Add(buttonEncrypt);
            Controls.Add(label7);
            Controls.Add(buttonSelectDecryptOutput);
            Controls.Add(textBoxDecryptOutput);
            Controls.Add(label2);
            Controls.Add(label6);
            Controls.Add(buttonSelectEncryptOutput);
            Controls.Add(textBoxEncryptOutput);
            Controls.Add(buttonSelectPasswordFile);
            Controls.Add(textBoxPasswordFilePath);
            Controls.Add(textBoxPassword);
            Controls.Add(label5);
            Controls.Add(radioButtonPasswordFromFile);
            Controls.Add(radioButtonPasswordFromKeyboard);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(buttonSelectDecryptInput);
            Controls.Add(textBoxDecryptInput);
            Controls.Add(label1);
            Controls.Add(buttonSelectEncryptInput);
            Controls.Add(textBoxEncryptInput);
            Name = "FormMagma";
            Text = "CodeForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button buttonSelectEncryptInput;
        private TextBox textBoxEncryptInput;
        private Button buttonSelectDecryptInput;
        private TextBox textBoxDecryptInput;
        private Label label3;
        private Label label4;
        private RadioButton radioButtonPasswordFromKeyboard;
        private RadioButton radioButtonPasswordFromFile;
        private Label label5;
        private TextBox textBoxPassword;
        private TextBox textBoxPasswordFilePath;
        private Button buttonSelectPasswordFile;
        private Button buttonSelectEncryptOutput;
        private TextBox textBoxEncryptOutput;
        private Label label6;
        private Label label2;
        private Label label7;
        private Button buttonSelectDecryptOutput;
        private TextBox textBoxDecryptOutput;
        private Button buttonEncrypt;
        private Button buttonDecrypt;
    }
}