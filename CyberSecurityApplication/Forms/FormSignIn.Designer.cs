namespace CyberSecurityApplication.Forms
{
    partial class FormSignIn
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            buttonClear = new System.Windows.Forms.Button();
            buttonEnter = new System.Windows.Forms.Button();
            labelPassword = new System.Windows.Forms.Label();
            labelLogin = new System.Windows.Forms.Label();
            textBoxPassword = new System.Windows.Forms.TextBox();
            textBoxLogin = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            textBoxPhrase = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // buttonClear
            // 
            buttonClear.BackColor = System.Drawing.Color.IndianRed;
            buttonClear.Location = new System.Drawing.Point(12, 245);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new System.Drawing.Size(94, 29);
            buttonClear.TabIndex = 11;
            buttonClear.Text = "Clear fields";
            buttonClear.UseVisualStyleBackColor = false;
            buttonClear.Click += buttonClear_Click;
            // 
            // buttonEnter
            // 
            buttonEnter.BackColor = System.Drawing.Color.LimeGreen;
            buttonEnter.Location = new System.Drawing.Point(130, 245);
            buttonEnter.Name = "buttonEnter";
            buttonEnter.Size = new System.Drawing.Size(94, 29);
            buttonEnter.TabIndex = 10;
            buttonEnter.Text = "OK";
            buttonEnter.UseVisualStyleBackColor = false;
            buttonEnter.Click += buttonEnter_Click;
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new System.Drawing.Point(12, 169);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new System.Drawing.Size(70, 20);
            labelPassword.TabIndex = 9;
            labelPassword.Text = "Password";
            // 
            // labelLogin
            // 
            labelLogin.AutoSize = true;
            labelLogin.Location = new System.Drawing.Point(12, 99);
            labelLogin.Name = "labelLogin";
            labelLogin.Size = new System.Drawing.Size(46, 20);
            labelLogin.TabIndex = 8;
            labelLogin.Text = "Login";
            // 
            // textBoxPassword
            // 
            textBoxPassword.Location = new System.Drawing.Point(12, 192);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.PasswordChar = '*';
            textBoxPassword.PlaceholderText = "Enter password";
            textBoxPassword.Size = new System.Drawing.Size(212, 27);
            textBoxPassword.TabIndex = 7;
            // 
            // textBoxLogin
            // 
            textBoxLogin.Location = new System.Drawing.Point(12, 122);
            textBoxLogin.Name = "textBoxLogin";
            textBoxLogin.PlaceholderText = "Enter login";
            textBoxLogin.Size = new System.Drawing.Size(212, 27);
            textBoxLogin.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 35);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(52, 20);
            label1.TabIndex = 13;
            label1.Text = "Phrase";
            // 
            // textBoxPhrase
            // 
            textBoxPhrase.Location = new System.Drawing.Point(12, 58);
            textBoxPhrase.Name = "textBoxPhrase";
            textBoxPhrase.PasswordChar = '*';
            textBoxPhrase.PlaceholderText = "Enter phrase";
            textBoxPhrase.Size = new System.Drawing.Size(212, 27);
            textBoxPhrase.TabIndex = 12;
            // 
            // FormSignIn
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(255, 335);
            Controls.Add(label1);
            Controls.Add(textBoxPhrase);
            Controls.Add(buttonClear);
            Controls.Add(buttonEnter);
            Controls.Add(labelPassword);
            Controls.Add(labelLogin);
            Controls.Add(textBoxPassword);
            Controls.Add(textBoxLogin);
            Text = "FormPassword";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPhrase;

        #endregion

        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxLogin;
    }
}
