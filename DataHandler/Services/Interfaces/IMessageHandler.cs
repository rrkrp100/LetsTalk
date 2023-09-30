using DataHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    public interface IMessageHandler
    {
        public Task<bool> ChatRoomExists(Guid ChatRoomId);
        public Task<MessageData> SendMessage(MessageData sentMessage);
        public Task<List<MessageData>> GetMessages(Guid ChatRoomId, Range indexRange);

    }
}
