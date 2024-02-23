using Newtonsoft.Json;
using PacketTransfer;
using System.Net;
using System.Net.Sockets;

namespace Accord
{
    public partial class ServerForm : Form
    {
        Server s;
        bool ssCan = false;

        public ServerForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            s = new Server(25565);
            s.ServerMessageEvent += S_ServerMessageEvent;
            s.Start(ssCan);
            lstMessages.Items.Add($"Server has started at {DateTime.Now}");
            btnMessage.Enabled = true;
            btnKick.Enabled = true;
            btnBan.Enabled = true;
            btnStop.Enabled = true;
            btnUnban.Enabled = true;
            btnStart.Enabled = false;
        }

        private void S_ServerMessageEvent(Packet msg)
        {
            //Add the messages to the listbox
            if (msg.ContentType == MessageType.BroadCast)
            {
                Invoke(() => lstMessages.Items.Add(msg.Payload));
                Invoke((MethodInvoker)delegate
                {
                    UpdateUserList();
                });
            }
            else if (msg.ContentType == MessageType.Connected)
            {
                Invoke(() => lstMessages.Items.Add(msg.Payload));
                Invoke((MethodInvoker)delegate
                {
                    UpdateUserList();
                });

            }
            else if (msg.ContentType == MessageType.Disconnected)
            {
                string[] tmp = msg.Payload.Split(' ').ToArray();
                if (!s.bannedUsers.Contains(tmp[0]))
                    Invoke(() => lstMessages.Items.Add(msg.Payload));

                Invoke((MethodInvoker)delegate
                {
                    UpdateUserList();
                });
            }
            Invoke(() => lstMessages.TopIndex = lstMessages.Items.Count - 1);
        }

        private void UpdateUserList()
        {
            lstUsers.Items.Clear();
            foreach (var x in s.Users)
            {
                lstUsers.Items.Add(x.Key);
                s.Users[x.Key] = x.Value;
            }
            lstBanned.DisplayMember = null;
            lstBanned.Items.Clear();
            foreach (string y in s.bannedUsers)
                lstBanned.Items.Add(y);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                Packet stopped = new()
                {
                    ContentType = MessageType.ServerCommand,
                    Payload = "Stop"
                };
                foreach (var user in s.Users.Values)
                    s.SendToSingleClient(stopped, user);

                s.Users.Clear();
                lstUsers.Items.Clear();

                s.ServerMessageEvent -= S_ServerMessageEvent;
                s.Stop();
                lstMessages.Items.Add($"Server has stopped at {DateTime.Now}");
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnMessage.Enabled = false;
                btnBan.Enabled = false;
                btnUnban.Enabled = false;
                btnKick.Enabled = false;
            }
            catch
            {
            }
        }

        private void btnKick_Click(object sender, EventArgs e)
        {
            string selectedUsername = (string)lstUsers.SelectedItem;
            if (selectedUsername != null)
            {
                TcpClient selectedClient = s.Users[selectedUsername];
                Packet kick = new()
                {
                    ContentType = MessageType.ServerCommand,
                    Payload = "Kicked"
                };
                s.SendToSingleClient(kick, selectedClient);

                //Let's everyone know that client has been kicked
                Packet kickresult = new()
                {
                    ContentType = MessageType.BroadCast,
                    Payload = selectedUsername + " has been kicked from the server."
                };
                s.BroadcastToAllClients(kickresult);
            }
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedUsername = (string)lstUsers.SelectedItem;
            if (selectedUsername != null)
            {
                TcpClient selectedClient = s.Users[selectedUsername];
            }
        }

        private void btnBan_Click(object sender, EventArgs e)
        {
            string selectedUsername = (string)lstUsers.SelectedItem;
            if (selectedUsername != null)
            {
                TcpClient selectedClient = s.Users[selectedUsername];
                s.bannedUsers.Add(selectedUsername);
                Packet ban = new()
                {
                    ContentType = MessageType.ServerCommand,
                    Payload = "Banned"
                };
                s.SendToSingleClient(ban, selectedClient);
                Packet banResult = new()
                {
                    ContentType = MessageType.BroadCast,
                    Payload = selectedUsername + " has been banned from the server."
                };
                s.BroadcastToAllClients(banResult);
                S_ServerMessageEvent(banResult);
            }

        }

        private void btnUnban_Click(object sender, EventArgs e)
        {
            string unBanSelectedUsername = (string)lstBanned.SelectedItem;
            if (unBanSelectedUsername != null)
                s.bannedUsers.Remove(unBanSelectedUsername);

            Invoke((MethodInvoker)delegate
            {
                UpdateUserList();
            });
        }

        private void chbSShare_CheckedChanged(object sender, EventArgs e) => ssCan = !ssCan;

        private void btnMessage_Click(object sender, EventArgs e)
        {
            Packet serverMessage = new();
            serverMessage.ContentType = MessageType.BroadCast;
            if (txtMessage.Text == "")
                serverMessage.Payload = $"Server sent nothing.";
            else
                serverMessage.Payload = $"Server: {txtMessage.Text}";

            s.BroadcastToAllClients(serverMessage);
            S_ServerMessageEvent(serverMessage);
            txtMessage.Clear();
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnMessage_Click(sender, e);
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void ServerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            btnStop_Click(sender, e);
        }
    }
}