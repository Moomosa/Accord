using Newtonsoft.Json;
using PacketTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Client
    {
        string host;
        int port;
        private TcpClient client;
        public delegate void ReceivePacketMessage(Packet msg);
        public event ReceivePacketMessage? ReceivePacketMessageEvent;
        public delegate void CancelledConnection();
        public event CancelledConnection CancelCon;

        public string UserName { get; set; }

        public Client(string host, int port, string username)
        {
            UserName = username;
            this.host = host;
            this.port = port;
        }

        public void Start()
        {
            try
            {
                //Bind the client to an Endpoint (IPAddress+Port)
                client = new TcpClient(host, port);
                Packet packet = new()
                {
                    ContentType = MessageType.SendUsername,
                    Payload = UserName
                };
                SendMessage(packet);
                //Run an async task
                Task.Run(() => Receive());
            }
            catch
            {
                MessageBox.Show($"The server {host} you're trying to connect to is offline or unavailable at this time.");
                if (CancelCon != null)
                    CancelCon();
            }
        }

        public async Task SendMessage(Packet msg)
        {
            try
            {
                //Grab that stream we're working with
                NetworkStream stream = client.GetStream();
                //Turn our packet into a string
                var tmp = JsonConvert.SerializeObject(msg);
                //Turn our packet into a byte[] for transport
                byte[] buffer = Encoding.UTF8.GetBytes(tmp);
                //Write the bytes to the stream
                await stream.WriteAsync(buffer, 0, buffer.Length);
            }
            catch
            {
                Packet packet = new();
                packet.ContentType = MessageType.BroadCast;
                packet.Payload = "You're not connected to a server";
                ReceivePacketMessageEvent(packet);
            }
        }

        //Receives some data in the form of a packet from the server
        public async Task Receive()
        {
            //Make a connection to the network stream client side
            NetworkStream stream = client.GetStream();
            //Byte array to hold data coming off the stream, set to 4096 to match the server
            byte[] buffer = new byte[4096];
            StringBuilder sb = new();

            //Run a loop forever
            while (true)
            {
                //Read data off that stream, into our buffer
                int bytesread = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesread == 0)
                    break;

                //Accumulate incoming chunks into a StringBuilder until we have a complete JSON string
                sb.Append(Encoding.UTF8.GetString(buffer, 0, bytesread));
                string jsonString = sb.ToString();
                int lastJsonStart = jsonString.LastIndexOf('{');

                while (lastJsonStart >= 0 && lastJsonStart < jsonString.Length - 1)
                {
                    string jsonSubstring = jsonString.Substring(lastJsonStart);
                    try    //Try to deserialize the substring as a packet
                    {                        
                        var packet = JsonConvert.DeserializeObject<Packet>(jsonSubstring);
                        if (ReceivePacketMessageEvent != null && packet != null)                        
                            ReceivePacketMessageEvent(packet);                        
                    }
                    catch (JsonException)  //Incomplete JSON string, wait for more data
                    {                        
                        break;
                    }

                    //Remove the processed JSON substring from the accumulated string builder
                    sb.Remove(0, lastJsonStart + jsonSubstring.Length);
                    jsonString = sb.ToString();
                    lastJsonStart = jsonString.LastIndexOf('{');
                }
            }
        }
    }
}
