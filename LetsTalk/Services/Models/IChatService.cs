using LetsTalk.Models;

namespace LetsTalk.Services
{
    public interface IChatService
    {
        public Task<MessageViewModel> SendMessage(MessageModel sentMessage);
        public Task<ConversationViewModel> GetMessages(Guid ChatRoomId, int page);
    }
}
