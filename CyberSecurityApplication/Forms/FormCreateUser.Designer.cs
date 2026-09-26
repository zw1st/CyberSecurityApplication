namespace CyberSecurityApplication.Forms
{
    partial class FormCreateUser
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
            textBoxUsername = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBoxPassword = new TextBox();
            checkBoxPasswordRestrictions = new CheckBox();
            label4 = new Label();
            numericUpDownMinLength = new NumericUpDown();
            numericUpDownDuration = new NumericUpDown();
            labelDuration = new Label();
            buttonCreate = new Button();
            buttonClear = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMinLength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDuration).BeginInit();
            SuspendLayout();
            // 
            // textBoxUsername
            // 
            textBoxUsername.Location = new Point(12, 94);
            textBoxUsername.Name = "textBoxUsername";
            textBoxUsername.Size = new Size(218, 27);
            textBoxUsername.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F);
            label1.Location = new Point(131, 9);
            label1.Name = "label1";
            label1.Size = new Size(218, 46);
            label1.TabIndex = 1;
            label1.Text = "User creation";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 71);
            label2.Name = "label2";
            label2.Size = new Size(75, 20);
            label2.TabIndex = 2;
            label2.Text = "Username";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 139);
            label3.Name = "label3";
            label3.Size = new Size(179, 20);
            label3.TabIndex = 4;
            label3.Text = "Password (may be empty)";
            // 
            // textBoxPassword
            // 
            textBoxPassword.Location = new Point(12, 162);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new Size(218, 27);
            textBoxPassword.TabIndex = 3;
            // 
            // checkBoxPasswordRestrictions
            // 
            checkBoxPasswordRestrictions.AutoSize = true;
            checkBoxPasswordRestrictions.Location = new Point(12, 214);
            checkBoxPasswordRestrictions.Name = "checkBoxPasswordRestrictions";
            checkBoxPasswordRestrictions.Size = new Size(168, 24);
            checkBoxPasswordRestrictions.TabIndex = 5;
            checkBoxPasswordRestrictions.Text = "Password restrictions";
            checkBoxPasswordRestrictions.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(283, 71);
            label4.Name = "label4";
            label4.Size = new Size(145, 20);
            label4.TabIndex = 6;
            label4.Text = "Password min length";
            // 
            // numericUpDownMinLength
            // 
            numericUpDownMinLength.Location = new Point(283, 94);
            numericUpDownMinLength.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericUpDownMinLength.Name = "numericUpDownMinLength";
            numericUpDownMinLength.Size = new Size(150, 27);
            numericUpDownMinLength.TabIndex = 7;
            // 
            // numericUpDownDuration
            // 
            numericUpDownDuration.Location = new Point(283, 162);
            numericUpDownDuration.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            numericUpDownDuration.Name = "numericUpDownDuration";
            numericUpDownDuration.Size = new Size(150, 27);
            numericUpDownDuration.TabIndex = 9;
            // 
            // labelDuration
            // 
            labelDuration.AutoSize = true;
            labelDuration.Location = new Point(283, 139);
            labelDuration.Name = "labelDuration";
            labelDuration.Size = new Size(130, 20);
            labelDuration.TabIndex = 8;
            labelDuration.Text = "Password duration";
            // 
            // buttonCreate
            // 
            buttonCreate.BackColor = Color.LimeGreen;
            buttonCreate.Location = new Point(304, 280);
            buttonCreate.Name = "buttonCreate";
            buttonCreate.Size = new Size(148, 29);
            buttonCreate.TabIndex = 10;
            buttonCreate.Text = "Create";
            buttonCreate.UseVisualStyleBackColor = false;
            buttonCreate.Click += buttonOk_Click;
            // 
            // buttonClear
            // 
            buttonClear.BackColor = Color.IndianRed;
            buttonClear.Location = new Point(150, 280);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(148, 29);
            buttonClear.TabIndex = 11;
            buttonClear.Text = "Clear fields";
            buttonClear.UseVisualStyleBackColor = false;
            buttonClear.Click += buttonClear_Click;
            // 
            // FormCreateUser
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(474, 336);
            Controls.Add(buttonClear);
            Controls.Add(buttonCreate);
            Controls.Add(numericUpDownDuration);
            Controls.Add(labelDuration);
            Controls.Add(numericUpDownMinLength);
            Controls.Add(label4);
            Controls.Add(checkBoxPasswordRestrictions);
            Controls.Add(label3);
            Controls.Add(textBoxPassword);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBoxUsername);
            Name = "FormCreateUser";
            Text = "FormCreateUser";
            ((System.ComponentModel.ISupportInitialize)numericUpDownMinLength).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDuration).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxUsername;
        private Label label1;
        private Label label2;
        private Label label3;
        private System.Windows.Forms.TextBox textBoxPassword;
        private CheckBox checkBoxPasswordRestrictions;
        private Label label4;
        private NumericUpDown numericUpDownMinLength;
        private NumericUpDown numericUpDownDuration;
        private Label labelDuration;
        private System.Windows.Forms.Button buttonCreate;
        private Button buttonClear;
    }
}