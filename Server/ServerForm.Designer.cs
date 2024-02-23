namespace Accord
{
    partial class ServerForm
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lstMessages = new ListBox();
            lstUsers = new ListBox();
            btnKick = new Button();
            btnStart = new Button();
            btnStop = new Button();
            txtMessage = new TextBox();
            btnMessage = new Button();
            btnBan = new Button();
            lstBanned = new ListBox();
            btnUnban = new Button();
            label1 = new Label();
            label2 = new Label();
            chbSShare = new CheckBox();
            SuspendLayout();
            // 
            // lstMessages
            // 
            lstMessages.FormattingEnabled = true;
            lstMessages.HorizontalScrollbar = true;
            lstMessages.ItemHeight = 15;
            lstMessages.Location = new Point(12, 12);
            lstMessages.Name = "lstMessages";
            lstMessages.Size = new Size(489, 364);
            lstMessages.TabIndex = 0;
            // 
            // lstUsers
            // 
            lstUsers.FormattingEnabled = true;
            lstUsers.ItemHeight = 15;
            lstUsers.Location = new Point(588, 45);
            lstUsers.Name = "lstUsers";
            lstUsers.Size = new Size(147, 154);
            lstUsers.TabIndex = 0;
            lstUsers.SelectedIndexChanged += lstUsers_SelectedIndexChanged;
            // 
            // btnKick
            // 
            btnKick.BackColor = Color.Red;
            btnKick.Enabled = false;
            btnKick.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnKick.Location = new Point(741, 45);
            btnKick.Name = "btnKick";
            btnKick.Size = new Size(75, 75);
            btnKick.TabIndex = 1;
            btnKick.Text = "Kick User";
            btnKick.UseVisualStyleBackColor = false;
            btnKick.Click += btnKick_Click;
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.Lime;
            btnStart.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnStart.Location = new Point(507, 43);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 75);
            btnStart.TabIndex = 1;
            btnStart.Text = "Start Server";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.BackColor = Color.FromArgb(255, 128, 0);
            btnStop.Enabled = false;
            btnStop.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnStop.Location = new Point(507, 124);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(75, 75);
            btnStop.TabIndex = 1;
            btnStop.Text = "Stop Server";
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Click += btnStop_Click;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(12, 382);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(300, 23);
            txtMessage.TabIndex = 2;
            txtMessage.KeyDown += txtMessage_KeyDown;
            // 
            // btnMessage
            // 
            btnMessage.Enabled = false;
            btnMessage.Location = new Point(12, 411);
            btnMessage.Name = "btnMessage";
            btnMessage.Size = new Size(97, 23);
            btnMessage.TabIndex = 3;
            btnMessage.Text = "Send Message";
            btnMessage.UseVisualStyleBackColor = true;
            btnMessage.Click += btnMessage_Click;
            // 
            // btnBan
            // 
            btnBan.BackColor = Color.Black;
            btnBan.Enabled = false;
            btnBan.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnBan.ForeColor = Color.FromArgb(255, 128, 128);
            btnBan.Location = new Point(741, 126);
            btnBan.Name = "btnBan";
            btnBan.Size = new Size(75, 75);
            btnBan.TabIndex = 1;
            btnBan.Text = "Ban User";
            btnBan.UseVisualStyleBackColor = false;
            btnBan.Click += btnBan_Click;
            // 
            // lstBanned
            // 
            lstBanned.FormattingEnabled = true;
            lstBanned.ItemHeight = 15;
            lstBanned.Location = new Point(507, 282);
            lstBanned.Name = "lstBanned";
            lstBanned.Size = new Size(228, 94);
            lstBanned.TabIndex = 4;
            // 
            // btnUnban
            // 
            btnUnban.BackColor = Color.FromArgb(128, 255, 255);
            btnUnban.Enabled = false;
            btnUnban.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnUnban.ForeColor = Color.Black;
            btnUnban.Location = new Point(741, 301);
            btnUnban.Name = "btnUnban";
            btnUnban.Size = new Size(75, 75);
            btnUnban.TabIndex = 1;
            btnUnban.Text = "Unban User";
            btnUnban.UseVisualStyleBackColor = false;
            btnUnban.Click += btnUnban_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(588, 27);
            label1.Name = "label1";
            label1.Size = new Size(99, 15);
            label1.TabIndex = 5;
            label1.Text = "Connected Users:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(507, 264);
            label2.Name = "label2";
            label2.Size = new Size(81, 15);
            label2.TabIndex = 5;
            label2.Text = "Banned Users:";
            // 
            // chbSShare
            // 
            chbSShare.AutoSize = true;
            chbSShare.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            chbSShare.Location = new Point(588, 205);
            chbSShare.Name = "chbSShare";
            chbSShare.Size = new Size(139, 23);
            chbSShare.TabIndex = 6;
            chbSShare.Text = "Allow ScreenShare";
            chbSShare.UseVisualStyleBackColor = true;
            chbSShare.CheckedChanged += chbSShare_CheckedChanged;
            // 
            // ServerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(828, 450);
            Controls.Add(chbSShare);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lstBanned);
            Controls.Add(btnMessage);
            Controls.Add(txtMessage);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(btnUnban);
            Controls.Add(btnBan);
            Controls.Add(btnKick);
            Controls.Add(lstUsers);
            Controls.Add(lstMessages);
            Name = "ServerForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Server";
            FormClosed += ServerForm_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstMessages;
        private ListBox lstUsers;
        private Button btnKick;
        private Button btnStart;
        private Button btnStop;
        private TextBox txtMessage;
        private Button btnMessage;
        private Button btnBan;
        private ListBox lstBanned;
        private Button btnUnban;
        private Label label1;
        private Label label2;
        private CheckBox chbSShare;
    }
}