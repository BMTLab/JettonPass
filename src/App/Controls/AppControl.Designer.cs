using System.ComponentModel;


namespace JettonPass.App.Controls
{
    partial class AppControl
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


        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.appIcon = new System.Windows.Forms.Panel();
            this.appName = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 
            // appIcon
            // 
            this.appIcon.BackColor = System.Drawing.Color.Transparent;
            this.appIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.appIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.appIcon.ForeColor = System.Drawing.Color.Transparent;
            this.appIcon.Location = new System.Drawing.Point(15, 19);
            this.appIcon.Name = "appIcon";
            this.appIcon.Size = new System.Drawing.Size(128, 128);
            this.appIcon.TabIndex = 0;
            this.appIcon.Click += new System.EventHandler(this.OnClick);

            // 
            // appName
            // 
            this.appName.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.appName.AutoSize = true;
            this.appName.BackColor = System.Drawing.Color.Transparent;
            this.appName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.appName.ForeColor = System.Drawing.Color.White;
            this.appName.Location = new System.Drawing.Point(15, 160);
            this.appName.Name = "appName";
            this.appName.Size = new System.Drawing.Size(80, 20);
            this.appName.TabIndex = 1;
            this.appName.Text = "AppName";
            this.appName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // AppControl
            // 
            this.Controls.Add(this.appName);
            this.Controls.Add(this.appIcon);
            this.Name = "AppControl";
            this.Size = new System.Drawing.Size(157, 211);
            this.ResumeLayout(false);
            this.PerformLayout();
        }


        private System.Windows.Forms.Label appName;
        private System.Windows.Forms.Panel appIcon;
        #endregion
    }
}