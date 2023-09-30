using LetsTalk.Models;
using DataHandler;
using System.Net;

namespace LetsTalk
{
    public static class DataTranslatorService{
        public static UserData? ToDbUser(this UserBase userBase)
        {
            UserData dbUser = null;

            if (userBase != null)
            {
                dbUser = new UserData()
                {
                    UserId = userBase.UserId,
                    UserName = userBase.UserName,
                    Name = userBase.GivenName,
                    Email = userBase.Email,
                    Role = userBase.Role,
                    Password = userBase.Password,
                    Age = userBase.Age,
                    ChatRooms = userBase.ChatRooms,
                };
            }
            
            return dbUser;

        }

        public static UserBase? ToUserBase(this UserData dbUser)
        {
            UserBase baseUser = null;

            if (dbUser != null)
            {
                baseUser = new UserBase()
                {
                    UserId = dbUser.UserId,
                    UserName = dbUser.UserName,
                    GivenName = dbUser.Name,
                    Email = dbUser.Email,
                    Role = dbUser.Role,
                    Password = dbUser.Password,
                    Age = dbUser.Age,
                    ChatRooms = dbUser.ChatRooms,
                };
            }
            return baseUser;

        }

        public static ConversationViewModel ToConversationViewModel(this List<MessageData> dbMessageList)
        {
            ConversationViewModel conversationViewModel = new();
            if (dbMessageList is null || dbMessageList.Count is 0)
            {
                conversationViewModel.Errors = new List<Error> { new Error("No Messages", 201) };
            }
            else
            {
                foreach (var msg in dbMessageList)
                {
                    conversationViewModel.Messages.Add(
                        new MessageModel()
                        {
                            MessageId = msg.MessageId,
                            TimeSent = msg.TimeSent,
                            SenderId = msg.SenderId,
                            Content = msg.Content,
                            Type = msg.Type,
                            ChatRoomId = msg.ChatRoomId,
                        }
                    );

                }
            }
            
            return conversationViewModel;
        }

        public static MessageData ToDbMessageModel(this MessageModel msgModel)
        {
            if (msgModel is null)
                return null;

            MessageData messageData = new MessageData()
            {
                MessageId = Guid.NewGuid(),
                TimeSent = DateTime.UtcNow,
                SenderId= msgModel.SenderId,
                Content = msgModel.Content,
                Type = msgModel.Type,
                ChatRoomId= msgModel.ChatRoomId,

            };
            return messageData;
        }
        public static MessageViewModel ToMessageViewModel(this MessageData msgModel)
        {
            if (msgModel is null)
                return null;

            MessageViewModel messageData = new MessageViewModel()
            {
                MessageId = msgModel.MessageId,
                TimeSent = msgModel.TimeSent,
                SenderId = msgModel.SenderId,
                Content = msgModel.Content,
                Type = msgModel.Type,
                ChatRoomId = msgModel.ChatRoomId,

            };
            return messageData;
        }
    }
}