using System.ComponentModel;


namespace JettonPass.App.Forms
{
    partial class AppsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppsForm));
            this.appsBox = new System.Windows.Forms.GroupBox();
            this.appsContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.appsBox.SuspendLayout();
            this.SuspendLayout();

            // 
            // appsBox
            // 
            resources.ApplyResources(this.appsBox, "appsBox");
            this.appsBox.Controls.Add(this.appsContainer);
            this.appsBox.ForeColor = System.Drawing.Color.White;
            this.appsBox.Name = "appsBox";
            this.appsBox.TabStop = false;

            // 
            // appsContainer
            // 
            resources.ApplyResources(this.appsContainer, "appsContainer");
            this.appsContainer.Name = "appsContainer";

            // 
            // AppsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.appsBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AppsForm";
            this.ShowIcon = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.appsBox.ResumeLayout(false);
            this.appsBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }


        private System.Windows.Forms.FlowLayoutPanel appsContainer;

        private System.Windows.Forms.GroupBox appsBox;
        #endregion
    }
}