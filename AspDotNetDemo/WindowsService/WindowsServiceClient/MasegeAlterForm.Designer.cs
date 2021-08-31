
namespace WindowsServiceClient
{
    partial class MasegeAlterForm
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
            this.MassageText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MassageText
            // 
            this.MassageText.AutoSize = true;
            this.MassageText.Location = new System.Drawing.Point(94, 60);
            this.MassageText.Name = "MassageText";
            this.MassageText.Size = new System.Drawing.Size(71, 12);
            this.MassageText.TabIndex = 0;
            this.MassageText.Text = "MassageText";
            this.MassageText.Click += new System.EventHandler(this.MassageText_Click);
            // 
            // MasegeAlterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(284, 179);
            this.ControlBox = false;
            this.Controls.Add(this.MassageText);
            this.MaximizeBox = false;
            this.Name = "MasegeAlterForm";
            this.Text = "MasegeAlterForm";
            this.Load += new System.EventHandler(this.MasegeAlterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MassageText;
    }
}