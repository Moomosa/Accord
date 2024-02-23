namespace Client
{
    partial class ScreenShot
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
            picScreenShot = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picScreenShot).BeginInit();
            SuspendLayout();
            // 
            // picScreenShot
            // 
            picScreenShot.Location = new Point(0, 0);
            picScreenShot.Name = "picScreenShot";
            picScreenShot.Size = new Size(640, 360);
            picScreenShot.TabIndex = 0;
            picScreenShot.TabStop = false;
            // 
            // Image
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(640, 361);
            Controls.Add(picScreenShot);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Image";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Image";
            ((System.ComponentModel.ISupportInitialize)picScreenShot).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picScreenShot;
    }
}