
namespace DataHandler
{
    public class MessageData
    {
        public Guid MessageId {  get; set; }
        public DateTime TimeSent { get; set; }
        public Guid SenderId {  get; set; }
        public string Content {  get; set; }
        public string Type {  get; set; }
        public Guid ChatRoomId {  get; set; }
    }
}
