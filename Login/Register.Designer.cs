namespace Login
{
    partial class Register
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
            label1 = new Label();
            txtUser = new TextBox();
            txtPass1 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            txtPass2 = new TextBox();
            label4 = new Label();
            btnReg = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(70, 9);
            label1.Name = "label1";
            label1.Size = new Size(150, 46);
            label1.TabIndex = 0;
            label1.Text = "Register";
            // 
            // txtUser
            // 
            txtUser.Location = new Point(37, 94);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(100, 23);
            txtUser.TabIndex = 1;
            // 
            // txtPass1
            // 
            txtPass1.Location = new Point(160, 94);
            txtPass1.Name = "txtPass1";
            txtPass1.PasswordChar = '*';
            txtPass1.Size = new Size(100, 23);
            txtPass1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(37, 76);
            label2.Name = "label2";
            label2.Size = new Size(63, 15);
            label2.TabIndex = 2;
            label2.Text = "Username:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(160, 76);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 2;
            label3.Text = "Password:";
            // 
            // txtPass2
            // 
            txtPass2.Location = new Point(160, 138);
            txtPass2.Name = "txtPass2";
            txtPass2.PasswordChar = '*';
            txtPass2.Size = new Size(100, 23);
            txtPass2.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(163, 120);
            label4.Name = "label4";
            label4.Size = new Size(94, 15);
            label4.TabIndex = 2;
            label4.Text = "Password Again:";
            // 
            // btnReg
            // 
            btnReg.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnReg.Location = new Point(37, 138);
            btnReg.Name = "btnReg";
            btnReg.Size = new Size(100, 53);
            btnReg.TabIndex = 4;
            btnReg.Text = "Register as New User";
            btnReg.UseVisualStyleBackColor = true;
            btnReg.Click += btnReg_Click;
            // 
            // Register
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(283, 205);
            Controls.Add(btnReg);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtPass2);
            Controls.Add(txtPass1);
            Controls.Add(txtUser);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Register";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Register";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtUser;
        private TextBox txtPass1;
        private Label label2;
        private Label label3;
        private TextBox txtPass2;
        private Label label4;
        private Button btnReg;
    }
}