using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PacketTransfer;
using Newtonsoft.Json;
using System.Drawing.Imaging;

namespace Accord
{
    public class Server
    {
        //A class to listen for connections
        private TcpListener listener;

        //Every incoming connection will get a tcp client
        List<TcpClient> clients = new();
        //A Dictionary of usernames and their client info
        public Dictionary<string, TcpClient> Users = new();
        //List of usernames on the banned list
        public List<string> bannedUsers = new();
        //bool to check if clients can screenshot the host
        public bool canSS = false;

        private bool running;
        private CancellationTokenSource cts = new();

        public delegate void ServerPacketMessage(Packet msg);
        public event ServerPacketMessage ServerMessageEvent;

        public Server(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
        }

        public async Task Start(bool canSS)
        {
            listener.Start();
            running = true;
            TcpClient client;
            this.canSS = canSS;

            while (running)
            {
                //This will fire when a TcpClient connects to our ipaddress/port
                client = await listener.AcceptTcpClientAsync();
                //Track the user
                clients.Add(client);

                //Start the task, will continue to run, while the client connection is alive
                //Checks the cancellation token if it should cancel
                Task.Run(() => HandleClient(client), cts.Token);
                //Delays the task by 100 milliseconds to allow cancellation
                await Task.Delay(100, cts.Token);
            }
        }

        public void Stop()
        {
            //https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/task-cancellation
            if (cts != null && !cts.IsCancellationRequested)
            {
                cts.Cancel();
                listener.Stop();
                running = false;
            }
        }

        public async void BroadcastToAllClients(Packet message)
        {
            //Turn into a json string
            var msg = JsonConvert.SerializeObject(message);

            //Turn the json string into a byte array
            byte[] broadcast = Encoding.UTF8.GetBytes(msg);

            //Send to each client that is known in our clients list
            foreach (TcpClient client in clients)
            {
                try
                {
                    //Get stream
                    NetworkStream stream = client.GetStream();

                    //Write to stream
                    await stream.WriteAsync(broadcast, 0, broadcast.Length);
                }
                catch
                {
                    //Be good to do some exception handling here, maybe clean up broken connections
                }
            }
        }

        public async void SendToSingleClient(Packet message, TcpClient client)
        {
            //Turn into a json string
            var msg = JsonConvert.SerializeObject(message);

            //Turn the json string into a byte array
            byte[] broadcast = Encoding.UTF8.GetBytes(msg);

            //Send to each client that is known in our clients list
            try
            {
                //Get stream
                NetworkStream stream = client.GetStream();

                //Write to stream
                await stream.WriteAsync(broadcast, 0, broadcast.Length);
            }
            catch
            {
                //Be good to do some exception handling here, maybe clean up broken connections
            }
        }

        private byte[] CaptureScreen()
        {
            // Capture the screen
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            using Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
            using Graphics g = Graphics.FromImage(bitmap);
            g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);
            Bitmap bmpResized = new Bitmap(bitmap, new Size(640, 360));

            // Convert the image to bytes
            using (MemoryStream ms = new MemoryStream())
            {
                bmpResized.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        private static IEnumerable<string> ChunkString(string str, int chunkSize)
        {
            //This isn't my code
            //Splits the input into small chunks
            for (int i = 0; i < str.Length; i += chunkSize)
                yield return str.Substring(i, Math.Min(chunkSize, str.Length - i));
        }

        private async void HandleClient(TcpClient client)
        {
            try
            {
                //Make a stream between serve and client
                NetworkStream stream = client.GetStream();

                //Make a buffer to get bytes from the stream
                byte[] buffer = new byte[4096];
                int bytesread = 0;

                //If value greater than 0, there are bytes to read from the network stream
                while ((bytesread = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0 || client.Client != null)
                {
                    //Read everything as a string
                    string message = Encoding.UTF8.GetString(buffer);
                    var packet = JsonConvert.DeserializeObject<Packet>(message);
                    switch (packet.ContentType)
                    {
                        case MessageType.Connected:
                            //Invoke the event -> hook into this later, to display server messages
                            ServerMessageEvent?.Invoke(packet);
                            //Broadcast the connected event to all connected clients
                            BroadcastToAllClients(packet);
                            break;

                        case MessageType.SendUsername:
                            if (bannedUsers.Contains(packet.Payload))
                            {
                                Packet nope = new()
                                {
                                    ContentType = MessageType.BroadCast,
                                    Payload = "You've been banned from this server"
                                };
                                SendToSingleClient(nope, client);
                                //Not done, send disconnect packet
                                Packet denied = new()
                                {
                                    ContentType = MessageType.ServerCommand,
                                    Payload = "Banned"
                                };
                                SendToSingleClient(denied, client);
                            }
                            else if (!Users.ContainsKey(packet.Payload))    //Username is not in Dictionary
                            {
                                Users.Add(packet.Payload, client);
                                Packet newUser = new()
                                {
                                    ContentType = MessageType.Connected,
                                    Payload = $"{packet.Payload} has connected from {client.Client.RemoteEndPoint}"
                                };
                                ServerMessageEvent(newUser);
                                BroadcastToAllClients(newUser);
                            }
                            else                //Username is in use
                            {
                                Packet notAllowed = new()
                                {
                                    ContentType = MessageType.DupeUser,
                                    Payload = $"Username is already in use. Connection destroyed."
                                };
                                //Send disconnect command to client
                                SendToSingleClient(notAllowed, client);
                            }
                            break;

                        case MessageType.BroadCast:
                            ServerMessageEvent?.Invoke(packet);
                            buffer = new byte[4096];
                            BroadcastToAllClients(packet);
                            break;

                        case MessageType.Disconnected:
                            string[] tmp = packet.Payload.Split(' ').ToArray();
                            //Broadcast to all clients
                            if (!bannedUsers.Contains(tmp[0]))
                                BroadcastToAllClients(packet);

                            //try and remove it from our known clients
                            clients.Remove(client);
                            TcpClient clientToRemove = client;
                            var keyToRemove = Users.FirstOrDefault(x => x.Value == clientToRemove).Key;
                            if (keyToRemove != null)
                                Users.Remove(keyToRemove);

                            //Trigger the event
                            ServerMessageEvent?.Invoke(packet);
                            client.Close();

                            break;

                        case MessageType.Typing:
                            BroadcastToAllClients(packet);
                            break;

                        case MessageType.ServerCommand:
                            if (canSS)
                                if (packet.Payload == "ScreenShot")
                                {
                                    byte[] screenshotBytes = CaptureScreen();

                                    string base64String = Convert.ToBase64String(screenshotBytes);
                                    var jsonChunks = ChunkString(base64String, 1024);
                                    int chunkIndex = 0;
                                    int numChunks = jsonChunks.Count();

                                    // Send each chunk separately
                                    foreach (var chunk in jsonChunks)
                                    {
                                        bool isFinalChunk = chunkIndex == numChunks - 1;
                                        Packet chunkPacket;
                                        if (!isFinalChunk)
                                        {
                                            chunkPacket = new()
                                            {
                                                ContentType = MessageType.Chunk,
                                                Payload = chunk
                                            };
                                        }
                                        else
                                        {
                                            int paddingLength = (4 - (chunk.Length % 4)) % 4;
                                            string paddedChunk = chunk.PadRight(chunk.Length + paddingLength, '=');
                                            chunkPacket = new()
                                            {
                                                ContentType = MessageType.IsFinalChunk,
                                                Payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(paddedChunk))
                                            };
                                        }
                                        SendToSingleClient(chunkPacket, client);
                                        await Task.Delay(20); // Delay between sending chunks to avoid overloading the stream
                                        chunkIndex++;
                                    }
                                }

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                //Alert disconnect to all clients

                //Packet p = new();
                //p.ContentType = MessageType.Disconnected;
                //p.Payload = $"Client disconnected from the server :: {ex.Message}";
                //client.Close();
                ////try and remove it from our known clients
                //clients.Remove(client);
                ////Trigger the event
                //ServerMessageEvent?.Invoke(p);
                ////Broadcast to all clients
                //BroadcastToAllClients(p);
            }
        }
    }
}