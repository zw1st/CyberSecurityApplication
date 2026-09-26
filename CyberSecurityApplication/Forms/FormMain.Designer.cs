namespace CyberSecurityApplication.Forms
{
    partial class FormMain
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
            menuStrip = new System.Windows.Forms.MenuStrip();
            adminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            changePswdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            cusersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            infoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { adminToolStripMenuItem, userToolStripMenuItem, infoToolStripMenuItem });
            menuStrip.Location = new System.Drawing.Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new System.Drawing.Size(800, 28);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip1";
            // 
            // adminToolStripMenuItem
            // 
            adminToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { changePswdToolStripMenuItem, cusersToolStripMenuItem });
            adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            adminToolStripMenuItem.Size = new System.Drawing.Size(67, 24);
            adminToolStripMenuItem.Text = "Admin";
            // 
            // changePswdToolStripMenuItem
            // 
            changePswdToolStripMenuItem.Name = "changePswdToolStripMenuItem";
            changePswdToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            changePswdToolStripMenuItem.Text = "Change password";
            // 
            // cusersToolStripMenuItem
            // 
            cusersToolStripMenuItem.Name = "cusersToolStripMenuItem";
            cusersToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            cusersToolStripMenuItem.Text = "Manage users";
            // 
            // userToolStripMenuItem
            // 
            userToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { changePasswordToolStripMenuItem });
            userToolStripMenuItem.Name = "userToolStripMenuItem";
            userToolStripMenuItem.Size = new System.Drawing.Size(52, 24);
            userToolStripMenuItem.Text = "User";
            // 
            // changePasswordToolStripMenuItem
            // 
            changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            changePasswordToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            changePasswordToolStripMenuItem.Text = "Change password";
            // 
            // infoToolStripMenuItem
            // 
            infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { infoToolStripMenuItem1, exitToolStripMenuItem });
            infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            infoToolStripMenuItem.Size = new System.Drawing.Size(84, 24);
            infoToolStripMenuItem.Text = "Common";
            // 
            // infoToolStripMenuItem1
            // 
            infoToolStripMenuItem1.Name = "infoToolStripMenuItem1";
            infoToolStripMenuItem1.Size = new System.Drawing.Size(118, 26);
            infoToolStripMenuItem1.Text = "Info";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new System.Drawing.Size(118, 26);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Text = "FormMain";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem adminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePswdToolStripMenuItem;
        private ToolStripMenuItem userToolStripMenuItem;
        private ToolStripMenuItem infoToolStripMenuItem;
        private ToolStripMenuItem cusersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private ToolStripMenuItem infoToolStripMenuItem1;
        private ToolStripMenuItem exitToolStripMenuItem;
    }
}