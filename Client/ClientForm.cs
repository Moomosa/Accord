using PacketTransfer;
using System;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Client
{
    public partial class ClientForm : Form
    {
        Client client = null;
        string Username = "";
        Random rnd = new();
        bool isTyping = false;
        private DateTime lastTypingTime = DateTime.MinValue;
        List<string> typingUsers = new();
        List<byte> imageBytes = new List<byte>();

        public ClientForm(string Username)
        {
            InitializeComponent();
            this.Username = Username;
            lblUsername.Text = Username;
            Text = $"{Username} Client";
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string connectTo = "10.0.0.220";  //This is my IP
            btnConnect.Enabled = false;
            btnDisconnect.Enabled = true;
            btnScreenGrab.Enabled = true;
            btnSendMessage.Enabled = true;

            client = new Client(connectTo, 25565, Username);
            client.ReceivePacketMessageEvent += Client_ReceivePacketMessageEvent;
            client.CancelCon += Client_CancelCon;
            client.Start();
        }

        private void Client_CancelCon()
        {
            Invoke(() => btnDisconnect_Click(null, null));
        }

        private void Client_ReceivePacketMessageEvent(Packet msg)
        {
            if (msg.ContentType == MessageType.BroadCast && msg.Payload != null)
                Invoke(() => lstMessages.Items.Add(msg.Payload));
            else if (msg.ContentType == MessageType.Connected && msg.Payload != null)
                Invoke(() => lstMessages.Items.Add(msg.Payload));
            else if (msg.ContentType == MessageType.Disconnected && msg.Payload != null)
                Invoke(() => lstMessages.Items.Add(msg.Payload));
            else if (msg.ContentType == MessageType.DupeUser)
            {
                Invoke(() => lstMessages.Items.Add("Connection destroyed. Duplicate Username error."));
                Invoke((MethodInvoker)delegate
                {
                    btnDisconnect_Click(null, null);
                });
            }
            else if (msg.ContentType == MessageType.ServerCommand)
            {
                if (msg.Payload == "Kicked")
                {
                    Invoke(() => lstMessages.Items.Add("Connection destroyed. Server terminated connection."));
                    Invoke((MethodInvoker)delegate
                    {
                        btnDisconnect_Click(null, null);
                    });
                }
                if (msg.Payload == "Banned")
                {
                    Invoke(() => lstMessages.Items.Add("Connection permanently destroyed. You've been banned."));
                    Invoke((MethodInvoker)delegate
                    {
                        btnDisconnect_Click(null, null);
                    });
                }
                if (msg.Payload == "Stop")
                {
                    Invoke(() => lstMessages.Items.Add("Connection interrupted. Server has stopped."));
                    Invoke((MethodInvoker)delegate
                    {
                        btnDisconnect_Click(null, null);
                    });
                }
            }
            else if (msg.ContentType == MessageType.Typing)
            {
                string[] tmpTyping = msg.Payload.Split(' ').ToArray();
                //If the list does not contain the user and its a start and it's not this user
                if (!typingUsers.Contains(tmpTyping[0]) && tmpTyping[1] == "start" && tmpTyping[0] != Username)
                {
                    typingUsers.Add(tmpTyping[0]);
                    Invoke((MethodInvoker)delegate
                    {
                        WhoIsTyping();
                    });
                }
                //If the list contains the user and its a stop
                else if (typingUsers.Contains(tmpTyping[0]) && tmpTyping[1] == "stop")
                {
                    typingUsers.Remove(tmpTyping[0]);
                    Invoke((MethodInvoker)delegate
                    {
                        WhoIsTyping();
                    });
                }
            }
            else if (msg.ContentType == MessageType.Chunk)
            {
                try
                {
                    // Convert the chunk from base64 to bytes and add it to the list
                    byte[] chunkBytes = Convert.FromBase64String(msg.Payload.TrimEnd('='));
                    imageBytes.AddRange(chunkBytes);
                }
                catch
                {

                }
                // Check if this is the last chunk
            }
            else if (msg.ContentType == MessageType.IsFinalChunk)
            {
                try
                {
                    byte[] chunkBytes = Convert.FromBase64String(msg.Payload);
                    string chunkString = Encoding.UTF8.GetString(chunkBytes.TakeWhile(b => b != '=').ToArray());
                    imageBytes.AddRange(Encoding.UTF8.GetBytes(chunkString));

                    // Convert the concatenated chunks to an image and display it
                    Image screenie = ByteArrayToImage(imageBytes.ToArray());
                    ScreenShot screenShot = new(screenie);
                    screenShot.ShowDialog();

                    // Clear the list for the next image
                    imageBytes.Clear();
                }
                catch
                {

                }
            }

            Invoke(() => lstMessages.TopIndex = lstMessages.Items.Count - 1);
        }

        public Image ByteArrayToImage(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream(data);
                Image returnImage = Image.FromStream(ms, true);
                return returnImage;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task WhoIsTyping()
        {
            if (typingUsers.Count == 0)
                lblWriting.Text = "";
            else if (typingUsers.Count == 1)
                lblWriting.Text = $"{typingUsers[0]} is typing...";
            else if (typingUsers.Count == 2)
                lblWriting.Text = $"{typingUsers[0]} and {typingUsers[1]} are typing...";
            else
            {
                var users = string.Join(", ", typingUsers.Take(typingUsers.Count - 1));
                var lastUser = typingUsers.Last();
                lblWriting.Text = $"{users}, and {lastUser} are typing...";
            }
            lblWriting.Invalidate();
            await Task.Delay(10);
        }


        private async void btnDisconnect_Click(object sender, EventArgs e)
        {
            Packet p = new();
            p.ContentType = MessageType.Disconnected;
            p.Payload = $"{client.UserName} has disconnected.";
            await client.SendMessage(p);

            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            btnScreenGrab.Enabled = false;
            btnSendMessage.Enabled = false;
        }

        private async void btnSendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                Packet p = new();
                p.ContentType = MessageType.BroadCast;
                if (txtMessage.Text == "")
                    p.Payload = $"{client.UserName} sent nothing.";
                else
                    p.Payload = $"{client.UserName}: {txtMessage.Text}";

                await Task.Delay(50);
                await client.SendMessage(p);
                txtMessage.Clear();
            }
            catch
            {
                MessageBox.Show("You're not connected to a server");
            }
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSendMessage_Click(sender, e);
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnConnect_Click(sender, e);
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private async void txtMessage_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (e is KeyPressEventArgs keyPressEventArgs && keyPressEventArgs.KeyChar == (char)Keys.Enter)
                    return;

                lastTypingTime = DateTime.Now;
                if (!string.IsNullOrEmpty(txtMessage.Text) && !isTyping)
                {
                    Packet typing = new()
                    {
                        ContentType = MessageType.Typing,
                        Payload = $"{Username} start"
                    };
                    await client.SendMessage(typing);
                    isTyping = true;
                }
                if (!string.IsNullOrEmpty(txtMessage.Text))
                    tmrTyping.Stop();
                tmrTyping.Start();
            }
            catch
            {
            }
        }

        private async void tmrTyping_Tick(object sender, EventArgs e)
        {
            if (isTyping && DateTime.Now - lastTypingTime >= TimeSpan.FromSeconds(2))
            {
                Packet typing = new()
                {
                    ContentType = MessageType.Typing,
                    Payload = $"{Username} stop"
                };
                await client.SendMessage(typing);
                isTyping = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private async void btnScreenGrab_Click(object sender, EventArgs e)
        {
            Packet getSS = new()
            {
                ContentType = MessageType.ServerCommand,
                Payload = "ScreenShot"
            };
            await client.SendMessage(getSS);
        }
    }
}