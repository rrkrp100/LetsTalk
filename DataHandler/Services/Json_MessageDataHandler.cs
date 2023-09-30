using DataHandler.Models;
using DataHandler.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler.Services
{
    public class Json_MessageDataHandler : IMessageHandler
    {
        private List<MessageData> _cachedMessages = new();
        private List<ChatRoomData> _chatRooms = new();
        public async Task<bool> ChatRoomExists(Guid ChatRoomId)
        {
            await CheckOrUpdateCache();
            return _chatRooms.Any(x => x.ChatRoomId == ChatRoomId);
        }

        public async Task<List<MessageData>> GetMessages(Guid ChatRoomId, Range indexRange)
        {
            await CheckOrUpdateCache();
            if(_cachedMessages is not null && _cachedMessages.Count > 0)
            {
                var relevantChatRoomChats = _cachedMessages.Where(msg => msg.ChatRoomId == ChatRoomId)?? new List<MessageData>();
                var msgs = relevantChatRoomChats
                            .OrderBy(msg => msg.TimeSent)
                            .Take(indexRange.End.Value)
                            .TakeLast(50);
                return msgs.ToList();
            }
            return new List<MessageData>();
        }

        public async Task<MessageData> SendMessage(MessageData sentMessage)
        {
            await CheckOrUpdateCache();
            sentMessage.MessageId = Guid.NewGuid();
            sentMessage.TimeSent = DateTime.UtcNow;
            try
            {
                _cachedMessages.Add(sentMessage);
                await LocalFileReader.WriteJsonFile<List<MessageData>>(_cachedMessages, "Messages");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to write message data", ex);
            }
            return sentMessage;
        }

        private async Task CheckOrUpdateCache(bool forceUpdate = false)
        {
            if (_cachedMessages != null && _cachedMessages.Count > 0 && !forceUpdate)
                return;
            try
            {
                List<MessageData> messageData = await LocalFileReader.ReadJsonFile<List<MessageData>>("Messages");
                List<ChatRoomData> chatRoomData = await LocalFileReader.ReadJsonFile<List<ChatRoomData>>("ChatRooms");
                if (messageData == null)
                {
                    throw new FileLoadException("No messageData found in the designated storage.");
                }
                if (chatRoomData == null)
                {
                    throw new FileLoadException("No chatRoomData found in the designated storage.");
                }
                _cachedMessages = messageData;
                _chatRooms = chatRoomData;
            }
            catch (Exception ex)
            {
                // Use Logger
                Console.WriteLine("Unable to load message data", ex);
            }
            return;
        }
    }
}
