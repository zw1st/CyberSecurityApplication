namespace CyberSecurityApplication.Forms
{
    partial class FormChangePassword
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
            buttonClear = new System.Windows.Forms.Button();
            buttonOk = new System.Windows.Forms.Button();
            labelNewPassword = new System.Windows.Forms.Label();
            labelOldPassword = new System.Windows.Forms.Label();
            textBoxNewPassword = new System.Windows.Forms.TextBox();
            textBoxOldPassword = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            textBoxNewPasswordConfirmation = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // buttonClear
            // 
            buttonClear.Location = new System.Drawing.Point(103, 276);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new System.Drawing.Size(94, 29);
            buttonClear.TabIndex = 17;
            buttonClear.Text = "Clear forms";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;
            // 
            // buttonOk
            // 
            buttonOk.Location = new System.Drawing.Point(203, 276);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new System.Drawing.Size(94, 29);
            buttonOk.TabIndex = 16;
            buttonOk.Text = "Ok";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // labelNewPassword
            // 
            labelNewPassword.AutoSize = true;
            labelNewPassword.Location = new System.Drawing.Point(12, 134);
            labelNewPassword.Name = "labelNewPassword";
            labelNewPassword.Size = new System.Drawing.Size(106, 20);
            labelNewPassword.TabIndex = 15;
            labelNewPassword.Text = "New password";
            // 
            // labelOldPassword
            // 
            labelOldPassword.AutoSize = true;
            labelOldPassword.Location = new System.Drawing.Point(12, 64);
            labelOldPassword.Name = "labelOldPassword";
            labelOldPassword.Size = new System.Drawing.Size(100, 20);
            labelOldPassword.TabIndex = 14;
            labelOldPassword.Text = "Old password";
            // 
            // textBoxNewPassword
            // 
            textBoxNewPassword.Location = new System.Drawing.Point(12, 157);
            textBoxNewPassword.Name = "textBoxNewPassword";
            textBoxNewPassword.PasswordChar = '*';
            textBoxNewPassword.PlaceholderText = "Enter new password";
            textBoxNewPassword.Size = new System.Drawing.Size(285, 27);
            textBoxNewPassword.TabIndex = 13;
            // 
            // textBoxOldPassword
            // 
            textBoxOldPassword.Location = new System.Drawing.Point(12, 87);
            textBoxOldPassword.Name = "textBoxOldPassword";
            textBoxOldPassword.PlaceholderText = "Enter old password";
            textBoxOldPassword.Size = new System.Drawing.Size(285, 27);
            textBoxOldPassword.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 20F);
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(285, 46);
            label1.TabIndex = 18;
            label1.Text = "Change password";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 198);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(195, 20);
            label2.TabIndex = 20;
            label2.Text = "New password confirmation";
            // 
            // textBoxNewPasswordConfirmation
            // 
            textBoxNewPasswordConfirmation.Location = new System.Drawing.Point(12, 221);
            textBoxNewPasswordConfirmation.Name = "textBoxNewPasswordConfirmation";
            textBoxNewPasswordConfirmation.PasswordChar = '*';
            textBoxNewPasswordConfirmation.PlaceholderText = "Enter new password confirmation";
            textBoxNewPasswordConfirmation.Size = new System.Drawing.Size(285, 27);
            textBoxNewPasswordConfirmation.TabIndex = 19;
            // 
            // FormChangePassword
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(343, 349);
            Controls.Add(label2);
            Controls.Add(textBoxNewPasswordConfirmation);
            Controls.Add(label1);
            Controls.Add(buttonClear);
            Controls.Add(buttonOk);
            Controls.Add(labelNewPassword);
            Controls.Add(labelOldPassword);
            Controls.Add(textBoxNewPassword);
            Controls.Add(textBoxOldPassword);
            Text = "FormChangePassword";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonOk;
        private Label labelNewPassword;
        private Label labelOldPassword;
        private TextBox textBoxNewPassword;
        private TextBox textBoxOldPassword;
        private Label label1;
        private Label label2;
        private TextBox textBoxNewPasswordConfirmation;
    }
}