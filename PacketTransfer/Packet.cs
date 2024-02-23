namespace PacketTransfer
{
    public enum MessageType
    {
        BroadCast,
        Connected,
        Disconnected,
        ServerOnlyMessage,
        SendUsername,
        DupeUser,
        ServerCommand,
        Typing,
        Image,
        Chunk,
        IsFinalChunk
    }
    public class Packet
    {
        public MessageType ContentType { get; set; }
        public string Payload { get; set; }
    }
}