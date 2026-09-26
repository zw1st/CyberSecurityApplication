using System.ComponentModel;

namespace CyberSecurityApplication.Forms;

partial class FormEditUser
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        buttonUpdate = new Button();
        numericUpDownDuration = new NumericUpDown();
        labelDuration = new Label();
        numericUpDownMinLength = new NumericUpDown();
        label4 = new Label();
        checkBoxRestrictions = new CheckBox();
        label2 = new Label();
        label1 = new Label();
        textBoxUsername = new TextBox();
        ((ISupportInitialize)numericUpDownDuration).BeginInit();
        ((ISupportInitialize)numericUpDownMinLength).BeginInit();
        SuspendLayout();
        // 
        // buttonUpdate
        // 
        buttonUpdate.BackColor = Color.LimeGreen;
        buttonUpdate.Location = new Point(305, 280);
        buttonUpdate.Name = "buttonUpdate";
        buttonUpdate.Size = new Size(148, 29);
        buttonUpdate.TabIndex = 22;
        buttonUpdate.Text = "Update";
        buttonUpdate.UseVisualStyleBackColor = false;
        buttonUpdate.Click += buttonOk_Click;
        // 
        // numericUpDownDuration
        // 
        numericUpDownDuration.Location = new Point(284, 162);
        numericUpDownDuration.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
        numericUpDownDuration.Name = "numericUpDownDuration";
        numericUpDownDuration.Size = new Size(150, 27);
        numericUpDownDuration.TabIndex = 21;
        // 
        // labelDuration
        // 
        labelDuration.AutoSize = true;
        labelDuration.Location = new Point(284, 139);
        labelDuration.Name = "labelDuration";
        labelDuration.Size = new Size(130, 20);
        labelDuration.TabIndex = 20;
        labelDuration.Text = "Password duration";
        // 
        // numericUpDownMinLength
        // 
        numericUpDownMinLength.Location = new Point(284, 94);
        numericUpDownMinLength.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
        numericUpDownMinLength.Name = "numericUpDownMinLength";
        numericUpDownMinLength.Size = new Size(150, 27);
        numericUpDownMinLength.TabIndex = 19;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(284, 71);
        label4.Name = "label4";
        label4.Size = new Size(145, 20);
        label4.TabIndex = 18;
        label4.Text = "Password min length";
        // 
        // checkBoxRestrictions
        // 
        checkBoxRestrictions.AutoSize = true;
        checkBoxRestrictions.Location = new Point(13, 163);
        checkBoxRestrictions.Name = "checkBoxRestrictions";
        checkBoxRestrictions.Size = new Size(168, 24);
        checkBoxRestrictions.TabIndex = 17;
        checkBoxRestrictions.Text = "Password restrictions";
        checkBoxRestrictions.UseVisualStyleBackColor = true;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(13, 71);
        label2.Name = "label2";
        label2.Size = new Size(75, 20);
        label2.TabIndex = 14;
        label2.Text = "Username";
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font("Segoe UI", 20F);
        label1.Location = new Point(132, 9);
        label1.Name = "label1";
        label1.Size = new Size(202, 46);
        label1.TabIndex = 13;
        label1.Text = "User update";
        // 
        // textBoxUsername
        // 
        textBoxUsername.Location = new Point(13, 94);
        textBoxUsername.Name = "textBoxUsername";
        textBoxUsername.Size = new Size(218, 27);
        textBoxUsername.TabIndex = 12;
        // 
        // FormEditUser
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(489, 333);
        Controls.Add(buttonUpdate);
        Controls.Add(numericUpDownDuration);
        Controls.Add(labelDuration);
        Controls.Add(numericUpDownMinLength);
        Controls.Add(label4);
        Controls.Add(checkBoxRestrictions);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(textBoxUsername);
        Name = "FormEditUser";
        Text = "FormEditUser";
        ((ISupportInitialize)numericUpDownDuration).EndInit();
        ((ISupportInitialize)numericUpDownMinLength).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button buttonUpdate;
    private NumericUpDown numericUpDownDuration;
    private Label labelDuration;
    private NumericUpDown numericUpDownMinLength;
    private Label label4;
    private CheckBox checkBoxRestrictions;
    private Label label2;
    private Label label1;
    private TextBox textBoxUsername;
}