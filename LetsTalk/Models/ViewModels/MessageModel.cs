
namespace LetsTalk.Models
{
    public class MessageModel 
    {
        public Guid? MessageId {  get; set; }
        public DateTime TimeSent { get; set; }
        public Guid SenderId {  get; set; }
        public string Content {  get; set; }
        public string Type {  get; set; }
        public Guid ChatRoomId {  get; set; }
    }
    public class MessageViewModel : MessageModel, IViewModel
    {
        public List<Error>? Errors { get ; set ; }
    }
    public class ConversationViewModel : IViewModel
    {
        public ConversationViewModel()
        {
            Messages = new();
            Errors = new();
        }

        public List<MessageModel> Messages { get; set; }
        public List<Error> Errors { get; set; }
    }
}
