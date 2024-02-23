namespace Client
{
    partial class ClientForm
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
            components = new System.ComponentModel.Container();
            lstMessages = new ListBox();
            txtMessage = new TextBox();
            btnSendMessage = new Button();
            btnConnect = new Button();
            btnDisconnect = new Button();
            lstUsers = new ListBox();
            lblUsername = new Label();
            lblWriting = new Label();
            tmrTyping = new System.Windows.Forms.Timer(components);
            btnScreenGrab = new Button();
            btnClose = new Button();
            SuspendLayout();
            // 
            // lstMessages
            // 
            lstMessages.FormattingEnabled = true;
            lstMessages.ItemHeight = 15;
            lstMessages.Location = new Point(12, 64);
            lstMessages.Name = "lstMessages";
            lstMessages.Size = new Size(552, 289);
            lstMessages.TabIndex = 0;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(12, 359);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(245, 23);
            txtMessage.TabIndex = 1;
            txtMessage.TextChanged += txtMessage_TextChanged;
            txtMessage.KeyDown += txtMessage_KeyDown;
            // 
            // btnSendMessage
            // 
            btnSendMessage.Enabled = false;
            btnSendMessage.Location = new Point(12, 388);
            btnSendMessage.Name = "btnSendMessage";
            btnSendMessage.Size = new Size(128, 23);
            btnSendMessage.TabIndex = 2;
            btnSendMessage.Text = "Send Message";
            btnSendMessage.UseVisualStyleBackColor = true;
            btnSendMessage.Click += btnSendMessage_Click;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(570, 64);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(75, 23);
            btnConnect.TabIndex = 3;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Enabled = false;
            btnDisconnect.Location = new Point(570, 93);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(75, 23);
            btnDisconnect.TabIndex = 3;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // lstUsers
            // 
            lstUsers.FormattingEnabled = true;
            lstUsers.ItemHeight = 15;
            lstUsers.Location = new Point(570, 184);
            lstUsers.Name = "lstUsers";
            lstUsers.Size = new Size(120, 169);
            lstUsers.TabIndex = 4;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            lblUsername.Location = new Point(12, 9);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(99, 28);
            lblUsername.TabIndex = 5;
            lblUsername.Text = "Username";
            // 
            // lblWriting
            // 
            lblWriting.AutoSize = true;
            lblWriting.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblWriting.Location = new Point(263, 362);
            lblWriting.Name = "lblWriting";
            lblWriting.Size = new Size(0, 19);
            lblWriting.TabIndex = 6;
            // 
            // tmrTyping
            // 
            tmrTyping.Interval = 2000;
            tmrTyping.Tick += tmrTyping_Tick;
            // 
            // btnScreenGrab
            // 
            btnScreenGrab.Enabled = false;
            btnScreenGrab.Location = new Point(570, 122);
            btnScreenGrab.Name = "btnScreenGrab";
            btnScreenGrab.Size = new Size(75, 56);
            btnScreenGrab.TabIndex = 7;
            btnScreenGrab.Text = "See Host Screen";
            btnScreenGrab.UseVisualStyleBackColor = true;
            btnScreenGrab.Click += btnScreenGrab_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(489, 35);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 3;
            btnClose.Text = "Log Out";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // ClientForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(700, 421);
            Controls.Add(btnScreenGrab);
            Controls.Add(lblWriting);
            Controls.Add(lblUsername);
            Controls.Add(lstUsers);
            Controls.Add(btnDisconnect);
            Controls.Add(btnClose);
            Controls.Add(btnConnect);
            Controls.Add(btnSendMessage);
            Controls.Add(txtMessage);
            Controls.Add(lstMessages);
            Name = "ClientForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Client";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstMessages;
        private TextBox txtMessage;
        private Button btnSendMessage;
        private Button btnConnect;
        private Button btnDisconnect;
        private ListBox lstUsers;
        private Label lblUsername;
        private Label lblWriting;
        private System.Windows.Forms.Timer tmrTyping;
        private Button btnScreenGrab;
        private Button btnClose;
    }
}