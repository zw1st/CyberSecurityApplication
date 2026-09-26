namespace CyberSecurityApplication
{
    partial class FormTest
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
            textBoxPassword = new TextBox();
            textBoxSalt = new TextBox();
            textBoxText = new TextBox();
            label1 = new Label();
            labelText = new Label();
            label3 = new Label();
            label4 = new Label();
            textBoxHash = new TextBox();
            buttonHash = new Button();
            buttonMakeSalt = new Button();
            label5 = new Label();
            textBoxMakeSalt = new TextBox();
            labelPhrase = new Label();
            textBoxPhrase = new TextBox();
            buttonEncrypt = new Button();
            label2 = new Label();
            textBoxRes = new TextBox();
            label6 = new Label();
            textBoxKey = new TextBox();
            buttonDecrypt = new Button();
            label7 = new Label();
            textBoxDecrypted = new TextBox();
            SuspendLayout();
            // 
            // textBoxPassword
            // 
            textBoxPassword.Location = new Point(12, 36);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new Size(200, 27);
            textBoxPassword.TabIndex = 0;
            // 
            // textBoxSalt
            // 
            textBoxSalt.Location = new Point(262, 36);
            textBoxSalt.Name = "textBoxSalt";
            textBoxSalt.Size = new Size(200, 27);
            textBoxSalt.TabIndex = 1;
            // 
            // textBoxText
            // 
            textBoxText.Location = new Point(12, 246);
            textBoxText.Name = "textBoxText";
            textBoxText.Size = new Size(200, 27);
            textBoxText.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 13);
            label1.Name = "label1";
            label1.Size = new Size(62, 20);
            label1.TabIndex = 3;
            label1.Text = "Пароль";
            // 
            // labelText
            // 
            labelText.AutoSize = true;
            labelText.Location = new Point(12, 223);
            labelText.Name = "labelText";
            labelText.Size = new Size(45, 20);
            labelText.TabIndex = 4;
            labelText.Text = "Текст";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(262, 13);
            label3.Name = "label3";
            label3.Size = new Size(43, 20);
            label3.TabIndex = 5;
            label3.Text = "Соль";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(506, 13);
            label4.Name = "label4";
            label4.Size = new Size(92, 20);
            label4.TabIndex = 7;
            label4.Text = "Хэш пароля";
            // 
            // textBoxHash
            // 
            textBoxHash.Location = new Point(506, 36);
            textBoxHash.Name = "textBoxHash";
            textBoxHash.Size = new Size(200, 27);
            textBoxHash.TabIndex = 6;
            // 
            // buttonHash
            // 
            buttonHash.Location = new Point(506, 69);
            buttonHash.Name = "buttonHash";
            buttonHash.Size = new Size(94, 29);
            buttonHash.TabIndex = 8;
            buttonHash.Text = "Make Hash";
            buttonHash.UseVisualStyleBackColor = true;
            buttonHash.Click += buttonHash_Click;
            // 
            // buttonMakeSalt
            // 
            buttonMakeSalt.Location = new Point(506, 214);
            buttonMakeSalt.Name = "buttonMakeSalt";
            buttonMakeSalt.Size = new Size(94, 29);
            buttonMakeSalt.TabIndex = 11;
            buttonMakeSalt.Text = "Make Salt";
            buttonMakeSalt.UseVisualStyleBackColor = true;
            buttonMakeSalt.Click += buttonMakeSalt_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(506, 158);
            label5.Name = "label5";
            label5.Size = new Size(111, 20);
            label5.TabIndex = 10;
            label5.Text = "Получить соль";
            // 
            // textBoxMakeSalt
            // 
            textBoxMakeSalt.Location = new Point(506, 181);
            textBoxMakeSalt.Name = "textBoxMakeSalt";
            textBoxMakeSalt.Size = new Size(200, 27);
            textBoxMakeSalt.TabIndex = 9;
            // 
            // labelPhrase
            // 
            labelPhrase.AutoSize = true;
            labelPhrase.Location = new Point(12, 290);
            labelPhrase.Name = "labelPhrase";
            labelPhrase.Size = new Size(52, 20);
            labelPhrase.TabIndex = 13;
            labelPhrase.Text = "Фраза";
            // 
            // textBoxPhrase
            // 
            textBoxPhrase.Location = new Point(12, 313);
            textBoxPhrase.Name = "textBoxPhrase";
            textBoxPhrase.Size = new Size(200, 27);
            textBoxPhrase.TabIndex = 12;
            // 
            // buttonEncrypt
            // 
            buttonEncrypt.Location = new Point(12, 361);
            buttonEncrypt.Name = "buttonEncrypt";
            buttonEncrypt.Size = new Size(178, 29);
            buttonEncrypt.TabIndex = 14;
            buttonEncrypt.Text = "Зашифровать";
            buttonEncrypt.UseVisualStyleBackColor = true;
            buttonEncrypt.Click += buttonEncrypt_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(260, 290);
            label2.Name = "label2";
            label2.Size = new Size(75, 20);
            label2.TabIndex = 16;
            label2.Text = "Результат";
            // 
            // textBoxRes
            // 
            textBoxRes.Location = new Point(260, 313);
            textBoxRes.Name = "textBoxRes";
            textBoxRes.Size = new Size(200, 27);
            textBoxRes.TabIndex = 15;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(260, 223);
            label6.Name = "label6";
            label6.Size = new Size(46, 20);
            label6.TabIndex = 18;
            label6.Text = "Ключ";
            // 
            // textBoxKey
            // 
            textBoxKey.Location = new Point(260, 246);
            textBoxKey.Name = "textBoxKey";
            textBoxKey.Size = new Size(200, 27);
            textBoxKey.TabIndex = 17;
            // 
            // buttonDecrypt
            // 
            buttonDecrypt.Location = new Point(260, 361);
            buttonDecrypt.Name = "buttonDecrypt";
            buttonDecrypt.Size = new Size(178, 29);
            buttonDecrypt.TabIndex = 19;
            buttonDecrypt.Text = "Расшифровать";
            buttonDecrypt.UseVisualStyleBackColor = true;
            buttonDecrypt.Click += buttonDecrypt_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(506, 290);
            label7.Name = "label7";
            label7.Size = new Size(104, 20);
            label7.TabIndex = 21;
            label7.Text = "Расшифровка";
            // 
            // textBoxDecrypted
            // 
            textBoxDecrypted.Location = new Point(506, 313);
            textBoxDecrypted.Name = "textBoxDecrypted";
            textBoxDecrypted.Size = new Size(200, 27);
            textBoxDecrypted.TabIndex = 20;
            // 
            // FormTest
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label7);
            Controls.Add(textBoxDecrypted);
            Controls.Add(buttonDecrypt);
            Controls.Add(label6);
            Controls.Add(textBoxKey);
            Controls.Add(label2);
            Controls.Add(textBoxRes);
            Controls.Add(buttonEncrypt);
            Controls.Add(labelPhrase);
            Controls.Add(textBoxPhrase);
            Controls.Add(buttonMakeSalt);
            Controls.Add(label5);
            Controls.Add(textBoxMakeSalt);
            Controls.Add(buttonHash);
            Controls.Add(label4);
            Controls.Add(textBoxHash);
            Controls.Add(label3);
            Controls.Add(labelText);
            Controls.Add(label1);
            Controls.Add(textBoxText);
            Controls.Add(textBoxSalt);
            Controls.Add(textBoxPassword);
            Name = "FormTest";
            Text = "FormTest";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxPassword;
        private TextBox textBoxSalt;
        private TextBox textBoxText;
        private Label label1;
        private Label labelText;
        private Label label3;
        private Label label4;
        private TextBox textBoxHash;
        private Button buttonHash;
        private Button buttonMakeSalt;
        private Label label5;
        private TextBox textBoxMakeSalt;
        private Label labelPhrase;
        private TextBox textBoxPhrase;
        private Button buttonEncrypt;
        private Label label2;
        private TextBox textBoxRes;
        private Label label6;
        private TextBox textBoxKey;
        private Button buttonDecrypt;
        private Label label7;
        private TextBox textBoxDecrypted;
    }
}