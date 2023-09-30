using DataHandler;
using LetsTalk.Models;

namespace LetsTalk.Services
{
    public class ChatService : IChatService
    {
        private readonly IMessageHandler _messageHandler;
        private readonly int pageSize = 50;

        public ChatService(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        public async Task<ConversationViewModel> GetMessages(Guid ChatRoomId, int page)
        {
            if (page <1)
            {
                page = 1;
            }
            if (await _messageHandler.ChatRoomExists(ChatRoomId))
            {
               var msgData =  await  _messageHandler
                    .GetMessages(ChatRoomId,
                    new Range(pageSize * (page - 1), pageSize * page));

                return msgData.ToConversationViewModel();
            }
            return new ConversationViewModel() { Errors = new List<Error> { new Error("Invalid Chatroom", 404) } };
        }

        public async Task<MessageViewModel> SendMessage(MessageModel sentMessage)
        {
            if (await _messageHandler.ChatRoomExists(sentMessage.ChatRoomId))
            {
                try
                {
                    var msgData = sentMessage.ToDbMessageModel();
                    var res = await _messageHandler.SendMessage(msgData);
                    return res.ToMessageViewModel();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return new MessageViewModel()
                    {
                        Errors = new List<Error>() { new Error("Somthing went wrong", 5001) }
                    };
                }

            }
            return new MessageViewModel()
            {
                Errors = new List<Error>() { new Error("ChatRoom Does not exist ", 6001) }
            };
        }
    }
}
